define(function (require) {
    var _i = {
        ko: require('knockout'),
        htmlString: require('text!./navheader.html'),
        router: require('plugins/router'),
        deferred: require('_custom/deferred'),
        events: require('_custom/services/event'),
        showDlg: require('_custom/dialog/show'),
        alert: require('_custom/services/alert')
    };

    function ctor(params) {
        var self = this;
        self.saveMethod = params.save || function () { };
        self.isSaving = _i.ko.observable(false);
        self.sessionHasSaved = _i.ko.observable(false);
        self.saveSuccess = _i.ko.observable(false);
        self.isDirty = params.isDirty || _i.ko.observable(false);
        self.inFlightSave = null; //will be the deferred
        self.inFlightSavePromise = null; //will be the inflightSave promise
        self.lastSavedPartialMessage = _i.ko.observable('');
        self.lastSavedDate = _i.ko.observable();

        self.showLoader = _i.ko.computed(function() {
            if (self.isSaving() || _i.router.isNavigating()) {
                return true;
            }
            return false;
        });

        self.canSave = _i.ko.pureComputed(function () {
            return self.isDirty() && !self.isSaving();
        });

        self.changeCount = 0;
        self.alertConfig = { positionX: "right", positionY: "top", effect: "fadeInUp", message: "", type: "success" }

        function isSave(config) {
            if (config.method && config.method.toLowerCase() !== 'get') {
                return true;
            }

            return false;
        }

        self.subscriptionSave = _i.events.subscribe('sheet:AjaxStart').then(function (config) {
            if (isSave(config)) {
                self.changeCount++;
                if (self.changeCount === 1) {
                    //start save
                    self.save();
                }
            }
        });

        self.subscriptionSaveComplete = _i.events.subscribe('sheet:AjaxComplete').then(function (config) {
            if (isSave(config)) {
                self.changeCount--;
                if (self.changeCount === 0 && self.inFlightSave) {
                    //save complete
                    self.inFlightSave.resolve();
                }
            }
        });

        self.subscriptionSaveError = _i.events.subscribe('sheet:AjaxError').then(function (config) {
            if (isSave(config)) {
                self.changeCount--;
                if (self.changeCount === 0 && self.inFlightSave) {
                    self.inFlightSave.reject();
                    //save complete
                }
            }
        });

        _i.router.guardRoute = function (model, route) {
            if (!self.isDirty()) {
                return true;
            }

            var guardPromise = _i.deferred.create();
            var navAway = _i.showDlg.showDialog('navaway/navaway');

            navAway.done(function (data) {
                //save & continue or save & skip
                if (data && data.btnText === 'skipSave') {
                    guardPromise.resolve(true);
                } else {
                    self.save(self).done(function () {
                        guardPromise.resolve(true);
                    }).fail(function () {
                        guardPromise.resolve(false);
                        self.isSaving(false);
                    });
                }
            });
            navAway.fail(function (error) {
                if (error && error.failureType === 'canceled') {
                    guardPromise.resolve(false);
                }
            });

            return guardPromise;
        };

        self.dispose = function () {
            window.clearTimeout(self.lastSavedInterval);
            self.subscriptionSave.off();
            self.subscriptionSaveComplete.off();
            self.subscriptionSaveError.off();
        };

        self.save = function (context) {
            function formatTime(dateObj) {
                var dateFormat = '';
                var hour = dateObj.getHours() % 12 || 12;
                dateFormat = dateFormat + hour;
                var minute = dateObj.getMinutes();
                dateFormat = dateFormat + ':';
                if (minute < 10) {
                    dateFormat = dateFormat + '0';
                }
                dateFormat = dateFormat + minute;
                if (dateObj.getHours() > 11) {
                    dateFormat = dateFormat + ' PM';
                } else {
                    dateFormat = dateFormat + ' AM';
                }
                return dateFormat;
            }

            if (self.inFlightSave !== null) {
                return self.inFlightSave.promise();
            }

            self.isSaving(true);
            self.inFlightSave = _i.deferred.create();
            self.inFlightSavePromise = self.inFlightSave.promise();

            if (context) {
                var result = self.saveMethod.call(context);

                if (result === false) {
                    self.isSaving(false);
                    self.inFlightSave.resolve(true);
                    self.inFlightSave = null;
                    return _i.deferred.createResolved();
                }
                else {
                    self.inFlightSavePromise = result;
                }
            }

            self.inFlightSavePromise.done(function (data) {
                if (data && data.suppressSaveNotification === true) {
                    self.saveSuccess(false);
                    self.isSaving(false);
                    self.inFlightSave.resolve(true);
                }
                else {
                    self.sessionHasSaved(true);
                    self.saveSuccess(true);
                    self.lastSavedDate(new Date());
                    self.inFlightSave.resolve(true);
                    self.lastSavedPartialMessage('Just Saved');
                    self.lastSavedInterval = setTimeout(function () {
                        self.lastSavedPartialMessage('Saved A Few Seconds Ago');
                        self.lastSavedInterval = setTimeout(function () {
                            self.lastSavedPartialMessage('Saved A Minute Ago');
                            self.lastSavedInterval = setTimeout(function () {
                                self.lastSavedPartialMessage('Last Saved At ' + formatTime(self.lastSavedDate()));
                            }, 60 * 1000);
                        }, 55 * 1000);
                    }, 5 * 1000);
                }
            });

            self.inFlightSavePromise.fail(function (error) {
                self.saveSuccess(false);
                self.inFlightSave.resolve(false);
            });

            self.inFlightSavePromise.always(function () {
                self.isSaving(false);
                self.inFlightSave = null;
            });

            return self.inFlightSavePromise;
        };
    }

    // Return component definition
    return { viewModel: ctor, template: _i.htmlString };
});
