define(function (require) {
    var _i = {
        ko: require('knockout'),
        $: require('jquery'),
        charajax: require('_custom/services/WebAPI'),
        list: require('_custom/services/listmanager'),
        deferred: require('_custom/deferred')
    };

    return function () {
        var self = this;

        /*==================== BASE DATA ====================*/
        self.classes = _i.ko.observableArray([]);
        
        /*==================== PAGE STATE/FILTERED ITEMS ====================*/
        self.selectedClass = _i.ko.observable();
        self.viewingDetails = _i.ko.observable(false);
        self.classListToShow = _i.ko.computed(function () {
            var returnList = self.classes();
            return _i.list.sortAlphabeticallyObservables(returnList);
        });
        
        self.activate = function() {
            return self.getPageData().done(function() {

            });
        };

        self.getPageData = function() {
            var deferred = _i.deferred.create();
            var promise = _i.deferred.waitForAll(self.getClassList());

            promise.done(function() {
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


    }
});
