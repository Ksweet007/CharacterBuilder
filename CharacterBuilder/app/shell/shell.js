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
        //Race
        //Class
        //Background
        //Skills
            //Class Skills
            //Background Skills
            //Proficiencies
            //Racial Proficiencies
            //Class Proficiencies
            //Background Proficiencies
        //Equipment
            //Armor
            //Weapons
            //Tools
            //Items
            //Other
        //Spells
            //Class Spells
            //Race Spells
            //Bonus Spells
        //Character Overview
            //Features
            //Skills
            //Proficiencies
            //Equipment
            //Items
            //Spells
        self.router = _i.router;

        self.activate = function (foo) {
            return self.getNavData().done(function (result) {
                var routesToMap = [
					{ route: '', title: 'Home', moduleId: 'home/home', nav: false, linktype: 'user' },
					{ route: 'home', title: 'Home', moduleId: 'home/home', nav: true, hash: "#home", linktype: 'user' },
					{ route: 'racelist', title: 'Racees', moduleId: 'racelist/racelist', nav: true, hash:"#racelist", linktype: 'single' },
					{ route: 'classlist', title: 'Classes', moduleId: 'classlist/classlist', nav: true, hash: "#classlist", linktype: 'single' },
					{ route: 'backgrounds', title: 'Backgrounds', moduleId: 'backgrounds/backgrounds', nav: true, hash: "#backgrounds", linktype: 'single' },

					{ route: 'armor', title: 'Armor', moduleId: 'armor/armor', nav: true, hash: '#armor', linktype: 'equipment' },
					{ route: 'weapons', title: 'Weapons', moduleId: 'weapons/weapons', nav: true, hash: '#weapons', linktype: 'equipment' },
                    { route: 'tools', title: 'Tools', moduleId: 'tools/tools', nav: true, hash: '#tools', linktype: 'equipment' },
                    { route: 'items', title: 'Items', moduleId: 'items/items', nav: true, hash: '#items', linktype: 'equipment' },
                    { route: 'other', title: 'Other', moduleId: 'other/other', nav: true, hash: '#other', linktype: 'equipment' }
                    

					// { route: 'tools', title: 'Tools', moduleId: 'tools/tools', nav: true, hash: '#tools', linktype: 'equipment' },
					// { route: 'weapon', other: 'Other', moduleId: 'other/other', nav: true, hash: '#other', linktype: 'equipment' },
					//{ route: 'skills', title: 'Skill List', moduleId: 'skills/skills', nav: true, linktype: 'general' },
					//{ route: 'classdetails/:id', title: 'Class Details', moduleId: 'classdetails/classdetails', nav: false, hash: '#classdetails', linktype: '' },
					//{ route: 'features', title: 'Features', moduleId: 'features/features', nav: true, hash: "#features", linktype: 'admin' },
				    //{ route: 'spells', title: 'Spells', moduleId: 'spells/spells', nav: true, hash: "#spells", linktype: 'classrace' }
                ];

                // if (self.isAdmin()) {
                // 	routesToMap.push({ route: 'features', title: 'Features', moduleId: 'features/features', nav: true, hash: "#features", linktype: 'admin' });
                // }

                self.router.map(routesToMap).buildNavigationModel();
                return self.router.activate();
            });
        };

    };
});
