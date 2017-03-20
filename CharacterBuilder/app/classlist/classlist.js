define(function (require) {
    var _i = {
        ko: require('knockout'),
        $: require('jquery'),
        charajax: require('_custom/services/WebAPI'),
        list: require('_custom/services/listmanager'),
        deferred: require('_custom/deferred'),
        globals: require('_custom/services/builderglobals'),
        alert: require('_custom/services/alert'),
        newsheetprompt: require('newsheetprompt/newsheetprompt')
    };

    return function () {
        var self = this;
        /*==================== GLOBAL SHEET STATUS ====================*/
        self.sheetId = _i.globals.getSheetId;
        self.hasPickedClass = _i.globals.hasSelectedClass;
        self.isWorkingSheet = _i.ko.observable(false);

        /*==================== BASE DATA ====================*/
        self.classes = _i.ko.observableArray([]);

        /*==================== PAGE STATE/FILTERED ITEMS ====================*/
        self.selectedClass = _i.ko.observable();
        self.viewingDetails = _i.ko.observable(false);
        self.classListToShow = _i.ko.computed(function () {
            var returnList = self.classes();
            return _i.list.sortAlphabeticallyObservables(returnList);
        });

        self.activate = function () {
            return self.getPageData().done(function () {

                self.isWorkingSheet(_i.globals.checkCookieExists("SheetBeingWorked"));
                if (self.isWorkingSheet() === true) {
                    _i.alert.showAlert({ type: "success", message: "Currently working sheet" });
                } else {
                    _i.alert.showAlert({ type: "error", message: "Not working Sheet" });
                }

            });
        };

        self.getPageData = function () {
            var deferred = _i.deferred.create();
            var promise = _i.deferred.waitForAll(self.getClassList());

            promise.done(function () {
                deferred.resolve();
            });

            return deferred;
        };

        self.getClassList = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/class/GetAllClasses', '').done(function (response) {
                var mapped = _i.ko.mapping.fromJS(response);

                self.classes(mapped());
                deferred.resolve();
            });
            return deferred;
        };

        self.viewMoreDetails = function (classSelected) {
            self.selectedClass(classSelected);
            self.viewingDetails(true);
        };

        self.closeMoreDetails = function () {
            self.viewingDetails(false);
        };

        self.selectClass = function () {
            if (self.isWorkingSheet() === true) {
                self.save();
            } else {
                _i.newsheetprompt.show().then(function (response) {
                    if (response.accepted) {
                        _i.charajax.post('api/charactersheet/CreateNewSheetWithClass/'+ self.selectedClass().Id()).done(function () {
                            _i.alert.showAlert({ type: "success", message: "Character Sheet Created" });
                            _i.alert.showAlert({ type: "success", message: "Class Selected" });
                            _i.globals.selectClass();
                        });
                    }
                });
            }
        };

        self.save = function () {
            return _i.charajax.put('api/class/SaveClassSelection/' + self.sheetId() + '/' + self.selectedClass().Id()).done(function () {
                _i.alert.showAlert({ type: "success", message: "Class Selected" });
                _i.globals.selectClass();
            });
        };

    }
});
