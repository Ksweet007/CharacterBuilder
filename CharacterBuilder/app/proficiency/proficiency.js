define(function (require) {
    var _i = {
        ko: require('knockout'),
        $: require('jquery'),
        charajax: require('_custom/services/WebAPI'),
        list: require('_custom/services/listmanager'),
        deferred: require('_custom/deferred'),
        alert: require('_custom/services/alert'),
        confirmdelete: require('confirmdelete/confirmdelete')
    };

    return function () {
        var self = this;

        /*==================== BASE DATA ====================*/
        self.proficiencies = _i.ko.observableArray([]);
        self.proficiencyTypes = _i.ko.observableArray([]);

        /*==================== SETUP FILTER FOR DISPLAYED ITEMS ====================*/
        self.selectedProficiencyTypes = _i.ko.observableArray([]);
        self.proficienciesToShow = _i.ko.computed(function() {
            var returnList = self.proficiencies().filter(function(prof) {
                return self.selectedProficiencyTypes().includes(prof.ProficiencyType.Name());
            });
            return _i.list.sortAlphabeticallyObservables(returnList);            
        });


        self.activate = function() {
            return self.getPageData().done(function (response) {
                
            });
        };

        self.getPageData = function () {
            var deferred = _i.deferred.create();
            var promise = _i.deferred.waitForAll(self.getProficiencyTypes());
            
            promise.done(function () {
                self.getProficiencies().done(function () {
                    deferred.resolve();
                });
            });

            return deferred;
        };

        self.getProficiencies = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/proficiency/GetAllProficiencies', '').done(function (response) {
                var mapped = _i.ko.mapping.fromJS(response);

                self.proficiencies(mapped());
                deferred.resolve();
            });
            return deferred;
        };

        self.getProficiencyTypes = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/proficiency/GetAllProficiencyTypes', '').done(function (response) {
                response.forEach(function (item) {                    
                    self.selectedProficiencyTypes().push(item.Name);
                    self.proficiencyTypes().push(item);
                });

                deferred.resolve();
            });
            return deferred;
        };


        self.save = function () {
        };

    }
});
