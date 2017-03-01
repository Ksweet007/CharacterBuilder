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

        /*==================== PAGE-STATE OBSERVABLES ====================*/
        self.isAddingNew = _i.ko.observable(false);
        self.isEditing = _i.ko.observable(false);
        self.showAll = _i.ko.observable(true);

        /*==================== WEAPON OBSERVABLES ====================*/
        self.weapons = _i.ko.observableArray([]);
        self.newWeapon = _i.ko.observable();
        self.selectedWeapon = _i.ko.observable();
        self.weaponProperties = _i.ko.observableArray([]);
        self.weaponProficiencies = _i.ko.observableArray([]);
        
        self.newSelectedWeaponCategories = _i.ko.observableArray([]);
        self.selectedProperties = _i.ko.observableArray([]);
        self.weaponCategories = _i.ko.observableArray([]);
        self.selectedWeaponCategory = _i.ko.observableArray([]);

        self.weaponsToShow = _i.ko.computed(function () {
            var returnList = self.weapons().filter(function (weap) {
                return self.selectedWeaponCategory().includes(weap.ProficiencyName());
            });
            returnList.forEach(function (weap) {
                weap.Damage = weap.DamageDieCount() + 'd' + weap.DamageDie();
                weap.Properties = weap.WeaponProperties().join(',');
            });
            return returnList.sort(function (left, right) { return left.ProficiencyName() == right.ProficiencyName() ? 0 : (left.Name() < right.Name() ? -1 : 1) });
        });

        /*====================CHANGE TRACKER SETUP====================*/
        self.dirtyItems = _i.ko.computed(function () {
            return _i.ko.utils.arrayFilter(self.weapons(), function (item) {
                return item.dirtyFlag.isDirty();
            });
        });
        self.isDirty = _i.ko.computed(function () {
            if (self.dirtyItems().length > 0 || self.isAddingNew()) {
                return true;
            }
            return false;
        });

        /*==================== DATA SETUP ====================*/
        self.activate = function () {
            return self.getPageData().done(function () {
                var foo = 'bar';
            });
        };

        self.getPageData = function () {
            var deferred = _i.deferred.create();
            var promise = _i.deferred.waitForAll(self.getAllWeaponProperties(), self.getWeaponProficiencies(), self.getWeaponCategories());

            promise.done(function () {
                self.getWeapons().done(function () {
                    deferred.resolve();
                });
            });

            return deferred;
        };

        self.getAllWeaponProperties = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/weapon/getAllWeaponProperties', '').done(function (response) {
                var mapped = _i.ko.mapping.fromJS(response);

                response.forEach(function (property) {
                    self.selectedProperties().push(property.Name);
                });

                self.weaponProperties(response);
                deferred.resolve();
            });
            return deferred;
        };

        self.getWeaponProficiencies = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/weapon/GetWeaponProficiencyTypes', '').done(function (response) {
                self.weaponProficiencies(response);
                deferred.resolve();
            });
            return deferred;
        };

        self.getWeaponCategories = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/weapon/GetAllWeaponCategories', '').done(function (response) {
                response.forEach(function (item) {
                    self.selectedWeaponCategory().push(item.Name);
                });

                self.weaponCategories(response);

                deferred.resolve();
            });
            return deferred;
        };

        self.getWeapons = function () {
            var promise = _i.deferred.create();
            _i.charajax.get('api/weapon/GetAllWeapons', '').done(function (response) {
                self.data = _i.ko.mapping.fromJS(response);

                self.data().forEach(function (weap) {                    
                    weap.dirtyFlag = new _i.ko.dirtyFlag(weap);
                });

                self.weapons(self.data());
                _i.list.sortAlphabeticallyObservables(self.weapons());

                promise.resolve();
            });

            return promise;
        };

        /*==================== ALERT THEN CHANGE PAGE STATE ====================*/
        self.resetToBaseList = function (alertType, textToShow, pageState) {
            var alertConfig = { type: alertType, message: textToShow };
            _i.alert.showAlert(alertConfig);
            self.isAddingNew(false);
            self.isEditing(false);
            self.showAll(true);
        };

        /*==================== PAGE FUNCTIONS ====================*/
        self.selectWeaponToEdit = function (obj) {
            self.selectedWeapon(obj);

            self.isAddingNew(false);
            self.isEditing(true);
            self.showAll(false);
        };

        self.openCreateForm = function (obj) {
            self.isAddingNew(true);
            self.isEditing(false);
            self.showAll(false);

            self.newWeapon({
                Id: _i.ko.observable(0),
                Name: _i.ko.observable(''),
                Cost: _i.ko.observable(''),
                DamageDie: _i.ko.observable(),
                DamageDieCount: _i.ko.observable(),
                Weight: _i.ko.observable(''),
                WeaponCategory: _i.ko.observable(),
                WeaponProperties: _i.ko.observableArray([])
            });

        };

        self.cancelEditor = function () {
            self.isAddingNew(false);
            self.isEditing(false);
            self.showAll(true);
        };

        /*==================== SAVE/EDIT/DELETE ====================*/
        self.deleteWeapon = function (obj) {
            _i.confirmdelete.show().then(function (response) {
                if (response.accepted) {
                    _i.charajax.delete('api/weapon/DeleteWeapon/' + obj.Id(), '').done(function (response) {
                        self.weapons.remove(obj);
                        _i.alert.showAlert({ type: "error", message: "Armor Deleted" });
                    });
                }
            });
        };

        self.buildNewWeaponToSave = function (weaponToSave) {
            var proficiencySelected = self.weaponProficiencies().filter(function(prof) {
                return prof.Id === weaponToSave.WeaponCategory().ProficiencyId;
            });
            var categorySelected = self.weaponCategories().filter(function (cat) {
                return cat.Id === weaponToSave.WeaponCategory().Id;
            });

            return  {
                Name: weaponToSave.Name(),
                Id: weaponToSave.Id(),
                Cost: weaponToSave.Cost(),
                DamageDie: weaponToSave.DamageDie(),
                DamageDieCount: weaponToSave.DamageDieCount(),
                Proficiency: proficiencySelected,
                Weight: weaponToSave.Weight(),
                WeaponCategory: categorySelected(),
                WeaponProperties: weaponToSave.WeaponProperties()
            };
        };

        self.buildweaponToEdit = function (weaponToSave) {
            var proficiencySelected = self.weaponProficiencies().filter(function (prof) {
                return prof.Id === weaponToSave.WeaponCategory().ProficiencyId;
            });
            var categorySelected = self.weaponCategories().filter(function (cat) {
                return cat.Id === weaponToSave.WeaponCategory().Id;
            });

            return {
                Name: weaponToSave.Name(),
                Id: weaponToSave.Id(),
                Cost: weaponToSave.Cost(),
                DamageDie: weaponToSave.DamageDie(),
                DamageDieCount: weaponToSave.DamageDieCount(),
                Proficiency: proficiencySelected,
                Weight: weaponToSave.Weight(),
                WeaponCategory: categorySelected(),
                WeaponProperties: weaponToSave.WeaponProperties()
            };
        };

        self.save = function (weaponToSave) {
            var isEditState = self.isDirty() && self.isEditing();
            var isNewState = self.isAddingNew();
            
            if (isNewState) {
                var dataToSave = self.buildNewWeaponToSave(weaponToSave);
                return _i.charajax.post('api/weapon/AddWeapon/', dataToSave).done(function (response) {
                    var mapped = _i.ko.mapping.fromJS(response);
                    mapped.dirtyFlag = new _i.ko.dirtyFlag(mapped);

                    self.weapons.push(mapped);
                    self.resetToBaseList("success", "New Weapon Added");
                });
            }

            if (isEditState) {
                var dataToEdit = self.buildNewWeaponToEdit(weaponToSave);
                return _i.charajax.put('api/weapon/EditWeapon/', dataToEdit).done(function (response) {
                    self.selectedWeapon().dirtyFlag.reset();
                    self.resetToBaseList("success", "Weapon Edit Saved");
                });
            }

            return _i.deferred.createResolved();

        };

    }
});
