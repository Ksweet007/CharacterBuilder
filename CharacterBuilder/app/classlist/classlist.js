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
        /*==================== GLOBAL SHEET STATUS ====================*/
        self.sheetId = _i.globals.getSheetId;
        self.hasPickedClass = _i.globals.hasSelectedClass;

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

        self.selectClass = function () {
            self.save();
        };

        self.save = function() {
            return _i.charajax.put('api/charactersheet/SaveClassSelection/' + self.sheetId() + '/' + self.selectedClass().Id()).done(function () {
                _i.alert.showAlert({ type: "success", message: "Class Selected" });
                _i.globals.selectClass();
            });
        };

    }
});
