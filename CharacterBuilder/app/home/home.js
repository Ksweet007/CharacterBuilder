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
        confirmdelete: require('confirmdelete/confirmdelete'),
        roller: require('_custom/services/roll'),
        abilityscore: require('_custom/services/abilityscore'),
        checklist: require('_custom/services/checklist')
    };

    return function () {
        var self = this;
        
        self.characterSheets = _i.ko.observableArray([]);    
        self.characterSheetsToShow = _i.ko.computed(function () {
            var returnList = self.characterSheets();
            return returnList;
        });

        self.activate = function () {
            return self.getPageData().done(function () {

            });
        };

        self.deleteSheet = function (obj) {
            _i.confirmdelete.show().then(function (response) {
                if (response.accepted) {
                    _i.charajax.delete('api/charactersheet/DeleteSheet/' + obj.Id(), '').done(function (response) {
                        var alertMsg = "Character Sheet # " + obj.Id() + " deleted.";
                        document.cookie = "SheetBeingWorked=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
                        if (obj.Id() === _i.globals.getSheetId()) {
                            _i.globals.clearToDoList();
                        }
                        self.characterSheets.remove(obj);
                        _i.alert.showAlert({ type: "error", message: alertMsg });
                    });
                }
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

        self.editSheet = function (bgSelected) {
            _i.globals.setSheetToEdit(bgSelected.Id());
            _i.globals.createCookie("SheetBeingWorked", bgSelected.Id());
            
            _i.alert.showAlert({
                type: "success",
                message: "Editing Sheet # " + bgSelected.Id()
            });

            window.location.href = '#charactersheet/' + bgSelected.Id();
        };

        self.addNew = function () {
            _i.charajax.post('api/charactersheet/CreateNewSheet', '').done(function (response) {
                window.builder.global_sheetid = response.Id;

                _i.alert.showAlert({ type: "success", message: "New Character Added!" });

                window.location.href = '#charactersheet/' + response.Id;
            });
        };

        self.getCharacterSheets = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/charactersheet/GetUserSheets').done(function (response) {

                response.forEach(function (sheet) {
                    sheet.createdDateFormatted = moment(sheet.CreatedDate).format('LLL');
                });

                var mapped = _i.ko.mapping.fromJS(response);

                self.characterSheets(mapped());

                deferred.resolve();
            });
            return deferred;
        };
    }
});
