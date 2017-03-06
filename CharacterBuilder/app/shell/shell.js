define(function (require) {
    var _i = {
        ko: require('knockout'),
        $: require('jquery'),
        router: require('plugins/router'),
        charajax: require('_custom/services/WebAPI'),
        app: require('durandal/app'),
        deferred: require('_custom/deferred'),
        moment: require('moment')
    };

    return function () {
        var self = this;
        self.isAdmin = ko.observable(false);

        self.getAdmin = function () {
            return _i.charajax.universal('api/IsAdmin', '', 'GET').done(function (response) {
                if (response) {
                    self.isAdmin(true);
                }
            });
        };

        self.getNavData = function () {
            var promise = _i.deferred.waitForAll(self.getAdmin());
            var deferred = _i.deferred.create();

            promise.done(function () {
                deferred.resolve();
            });

            return deferred;
        };

        self.router = _i.router;

        self.activate = function (foo) {
            return self.getNavData().done(function (result) {
                var routesToMap = [
					{ route: '', title: 'Home', moduleId: 'home/home', nav: false, linktype: 'user' },
					{ route: 'home', title: 'Home', moduleId: 'home/home', nav: true, hash: "#home", linktype: 'user' },
                    { route: 'racelist', title: 'Races', moduleId: 'racelist/racelist', nav: true, hash: "#racelist", linktype: 'single' },
                    { route: 'classlist', title: 'Classes', moduleId: 'classlist/classlist', nav: true, hash: "#classlist", linktype: 'single' },
					{ route: 'backgrounds', title: 'Backgrounds', moduleId: 'backgrounds/backgrounds', nav: true, hash: "#backgrounds", linktype: 'single' },                    
                    { route: 'proficiency', title: 'Proficiencies', moduleId: 'proficiency/proficiency', nav: true, hash: "#proficiency", linktype: 'single' },
                    { route: 'spells', title: 'Spells', moduleId: 'spells/spells', nav: true, hash: "#spells", linktype: 'single' },
                    { route: 'feature', title: 'Features', moduleId: 'features/features', nav: true, hash: "#features", linktype: 'single' },

					{ route: 'armor', title: 'Armor', moduleId: 'armor/armor', nav: true, hash: '#armor', linktype: 'equipment' },
					{ route: 'weapons', title: 'Weapons', moduleId: 'weapons/weapons', nav: true, hash: '#weapons', linktype: 'equipment' },
                    { route: 'tools', title: 'Tools', moduleId: 'tools/tools', nav: true, hash: '#tools', linktype: 'equipment' },
                    { route: 'items', title: 'Items', moduleId: 'items/items', nav: true, hash: '#items', linktype: 'equipment' },
                    { route: 'other', title: 'Other', moduleId: 'other/other', nav: true, hash: '#other', linktype: 'equipment' }
				    
                ];

                self.router.map(routesToMap).buildNavigationModel();
                return self.router.activate();
            });
        };

    };
});
