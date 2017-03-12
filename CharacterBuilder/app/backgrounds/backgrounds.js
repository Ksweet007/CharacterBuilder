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
        self.hasSelectedBackground = _i.globals.hasSelectedBackground;

        /*==================== BASE DATA ====================*/
        self.backgrounds = _i.ko.observableArray([]);

        /*==================== PAGE STATE/FILTERED ITEMS ====================*/
        self.selectedBackground = _i.ko.observable();
        
        self.viewingDetails = _i.ko.observable(false);
        self.backgroundListToShow = _i.ko.computed(function () {
            var returnList = self.backgrounds();
            return _i.list.sortAlphabeticallyObservables(returnList);
        });

        self.skillsList = function () {            
            var skills = [];
            self.selectedBackground().Skills().forEach(function(item) {
                skills.push(item.Name());
            });

            return skills.join(', ');
        };

        self.languageVerbiage = function() {
            var languages = [];
            self.selectedBackground().Languages().forEach(function (item) {
                languages.push(item.Name());
            });
            var formattedArray = languages.join(', ');
            var countVerbiage = 'Choose ' + self.selectedBackground().LanguageCount() + ' from:';

            return countVerbiage + " " + formattedArray;
        };

        self.activate = function () {
            return self.getPageData().done(function () {

            });
        };

        self.getPageData = function () {
            var deferred = _i.deferred.create();
            var promise = _i.deferred.waitForAll(self.getBackgroundList());

            promise.done(function () {
                deferred.resolve();
            });

            return deferred;
        };

        self.getBackgroundList = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/background/GetAllBackgrounds', '').done(function (response) {
                //Handle Characteristics
                response.forEach(function (bg) {
                    bg.BackgroundCharacteristic.forEach(function(char) {
                        for (var i = 0; i < char.BackgroundOptions.length; i++) {                            
                            char.BackgroundOptions[i].rollValue = i + 1;
                        }
                    });
                });



                var mapped = _i.ko.mapping.fromJS(response);
                
                self.backgrounds(mapped());
                deferred.resolve();
            });
            return deferred;
        };

        self.viewMoreDetails = function (bgSelected) {
            self.selectedBackground(bgSelected);
            self.viewingDetails(true);
        };

        self.closeMoreDetails = function () {
            self.viewingDetails(false);
        };

        self.selectBackground = function () {
            self.save();
        };

        self.save = function () {
            return _i.charajax.put('api/background/SaveBackgroundSelection/' + self.selectedBackground().Id() + '/' + self.sheetId()).done(function () {
                _i.alert.showAlert({ type: "success", message: "Background Selected" });
                _i.globals.selectBackground();
            });
        };


    }
});
