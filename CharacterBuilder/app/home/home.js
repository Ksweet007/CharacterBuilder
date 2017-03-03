define(function (require) {
    var _i = {
        ko: require('knockout'),
        $: require('jquery'),
        charajax: require('_custom/services/WebAPI'),
        list: require('_custom/services/listmanager'),
        deferred: require('_custom/deferred'),
        globals: require('_custom/services/builderglobals')
    };

    return function () {
        var self = this;
        self.userName = _i.globals.getUserName;

        /*==================== BASE DATA ====================*/
        self.characterSheets = _i.ko.observableArray([]);

        /*==================== PAGE STATE/FILTERED ITEMS ====================*/
        self.selectedSheet = _i.ko.observable();

        self.viewingDetails = _i.ko.observable(false);
        self.characterSheetsToShow = _i.ko.computed(function () {
            var returnList = self.characterSheets();
            return _i.list.sortAlphabeticallyObservables(returnList);
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
            _i.charajax.get('api/charactersheet/GetUserSheets','' ).done(function (response) {              
                var mapped = _i.ko.mapping.fromJS(response);

                self.characterSheets(mapped());
                deferred.resolve();
            });
            return deferred;
        };

        self.viewMoreDetails = function (bgSelected) {
            self.selectedSheet(bgSelected);
            self.viewingDetails(true);
        };

        self.closeMoreDetails = function () {
            self.viewingDetails(false);
        };


    }
});
