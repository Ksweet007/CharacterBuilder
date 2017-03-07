define(function (require) {
    var _i = {
        ko: require('knockout'),
        $: require('jquery'),
        moment: require('moment'),
        charajax: require('_custom/services/WebAPI'),
        list: require('_custom/services/listmanager'),
        deferred: require('_custom/deferred'),
        alert: require('_custom/services/alert'),
        globals: require('_custom/services/builderglobals'),
        confirmdelete: require('confirmdelete/confirmdelete')
    };

    return function () {
        var self = this;
        
        /*==================== BASE DATA ====================*/
        self.characterSheets = _i.ko.observableArray([]);

        /*==================== PAGE STATE/FILTERED ITEMS ====================*/
        self.selectedSheet = _i.ko.observable();

        self.viewingDetails = _i.ko.observable(false);
        self.showAll = _i.ko.observable(true);
        
        self.characterSheetsToShow = _i.ko.computed(function () {
            var returnList = self.characterSheets();
            return returnList;
            //return _i.list.sortAlphabeticallyObservables(returnList);
        });

        self.activate = function () {
            return self.getPageData().done(function () {

            });
        };

        self.getPageData = function () {
            var deferred = _i.deferred.create();
            var promise = _i.deferred.waitForAll(self.getCharacterSheets());

            promise.done(function () {
                deferred.resolve();
            });

            return deferred;
        };

        self.getCharacterSheets = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/charactersheet/GetUserSheets').done(function (response) {
                response.forEach(function(sheet) {
                    sheet.createdDateFormatted = moment(sheet.CreatedDate).format('LLL');
                });
                var mapped = _i.ko.mapping.fromJS(response);

                self.characterSheets(mapped());
                deferred.resolve();
            });
            return deferred;
        };

        self.rollAbilityScore = function () {
            var rolls = [];

            for (var i = 0; i < 4; i++) {
                var d6 = 1 + Math.floor(Math.random() * 6);
                rolls.push(d6);
            }

            rolls.sort();        
            rolls.shift();

            var scoreTotal = 0;
            for (var s in rolls) {
                scoreTotal += rolls[s];
            }

        };

        self.viewMoreDetails = function (bgSelected) {
            self.selectedSheet(bgSelected);
            self.viewingDetails(true);
        };

        self.closeMoreDetails = function () {
            self.viewingDetails(false);
        };

        self.deleteSheet = function(obj) {
            _i.confirmdelete.show().then(function (response) {
                if (response.accepted) {
                    _i.charajax.delete('api/charactersheet/DeleteSheet/' + obj.Id(), '').done(function (response) {
                        var alertMsg = "Character Sheet for " + obj.CharacterName() + " Deleted";
                        self.characterSheets.remove(obj);
                        document.cookie = "SheetBeingWorked=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
                        _i.alert.showAlert({ type: "error", message: alertMsg });
                    });
                }
            });
        };

        self.selectClassToEdit = function (sheetToEdit) {
            self.selectedSheet(sheetToEdit);

            _i.globals.setSheetToEdit(self.selectedSheet().Id());

            var editMessage = "Currently Editing";
            if (sheetToEdit.CharacterName() != null) {
                editMessage += " " + sheetToEdit.CharacterName();
            }
            
            _i.alert.showAlert({ type: "success", message: editMessage });
        };

        self.showAlertAndOpenEditor = function () {
            var alertConfig = { type: "success", message: "New Character Added!" };
            _i.alert.showAlert(alertConfig);
            self.viewingDetails(true);
        };

        self.addNew = function() {
            _i.charajax.post('api/charactersheet/CreateNewSheet', '').done(function(response) {
                window.builder.global_sheetid = response.Id;
                response.createdDateFormatted = moment(response.CreatedDate).format('LLL');
                
                var mapped = _i.ko.mapping.fromJS(response);

                self.characterSheets.push(mapped);
                self.selectedSheet(mapped);
                self.showAlertAndOpenEditor();
                
            });
        };

    }
});
