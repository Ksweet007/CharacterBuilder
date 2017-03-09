define(function (require) {
    var _i = {
        ko: require('knockout'),
        $: require('jquery'),
        charajax: require('_custom/services/WebAPI'),
        list: require('_custom/services/listmanager'),
        deferred: require('_custom/deferred'),
        globals: require('_custom/services/builderglobals'),
        alert: require('_custom/services/alert')
    };

    return function () {
        var self = this;
        self.sheetId = _i.globals.getSheetId;
        self.hasSelectedRace = _i.globals.hasSelectedRace; //To Set State of page to edit, or Select
        
        /*==================== BASE DATA ====================*/
        self.races = _i.ko.observableArray([]);
        self.subRaces = _i.ko.observableArray([]);

        /*==================== PAGE STATE/FILTERED ITEMS ====================*/
        self.selectedRace = _i.ko.observable();
        self.selectedSubRace = _i.ko.observable();

        self.viewingDetails = _i.ko.observable(false);
        self.raceListToShow = _i.ko.computed(function () {
            var returnList = self.races();
            return _i.list.sortAlphabeticallyObservables(returnList);
        });

        self.activate = function () {
            return self.getPageData().done(function () {

            });
        };

        self.getPageData = function () {
            var deferred = _i.deferred.create();
            var promise = _i.deferred.waitForAll(self.getRaceList());

            promise.done(function () {
                deferred.resolve();
            });

            return deferred;
        };

        self.getRaceList = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/race/GetAllRaces', '').done(function (response) {
                var mapped = _i.ko.mapping.fromJS(response);               
                self.races(mapped());

                deferred.resolve();
            });
            return deferred;
        };

        self.viewMoreDetails = function (raceSelected) {
            self.selectedRace(raceSelected);
            self.viewingDetails(true);
        };

        self.closeMoreDetails = function () {
            self.viewingDetails(false);
        };

        self.selectRace = function () {
            self.save();
        };

        self.save = function () {
            return _i.charajax.put('api/race/SaveRaceSelection/' + self.sheetId() + '/' + self.selectedRace().Id()).done(function () {
                _i.alert.showAlert({ type: "success", message: "Race Selected" });
                _i.globals.selectRace();
                self.viewingDetails(false);
            });
        };


    }
});
