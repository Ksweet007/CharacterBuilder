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

        /*==================== ARMOR OBSERVABLES ====================*/
        self.armors = _i.ko.observableArray([]);
        self.newArmor = _i.ko.observable();
        self.selectedArmor = _i.ko.observable();

        self.selectedArmorType = _i.ko.observableArray([]);
        self.armorTypes = _i.ko.observableArray([]);

        self.armorsToShow = _i.ko.computed(function () {
            var returnList = self.armors().filter(function (armor) {
                return self.selectedArmorType().includes(armor.ProficiencyName());
            });
            return returnList.sort(function (left, right) { return left.ProficiencyName() == right.ProficiencyName() ? 0 : (left.ArmorClass() < right.ArmorClass() ? -1 : 1) });
        });

        /*====================CHANGE TRACKER SETUP====================*/
        self.dirtyItems = _i.ko.computed(function () {
            return _i.ko.utils.arrayFilter(self.armors(), function (item) {
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
            });
        };

        self.getPageData = function () {
            var deferred = _i.deferred.create();
            var promise = _i.deferred.waitForAll(self.getArmorProficiencies());

            promise.done(function () {
                self.getArmor().done(function () {
                    deferred.resolve();
                });
            });

            return deferred;
        };

        self.getArmorProficiencies = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/armor/GetArmorProficiencyTypes', '').done(function (response) {
                response.forEach(function (item) {
                    self.selectedArmorType().push(item.Name);
                });

                self.armorTypes(response);
                deferred.resolve();
            });
            return deferred;
        };

        self.getArmor = function () {
            var promise = _i.deferred.create();
            _i.charajax.get('api/armor/GetAllArmor', '').done(function (response) {
                var mapped = _i.ko.mapping.fromJS(response);
                mapped().forEach(function (item) {
                    item.dirtyFlag = new _i.ko.dirtyFlag(item);
                });
                self.armors(mapped());

                _i.list.sortAlphabeticallyObservables(self.armors());

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
        self.selectArmorToEdit = function (obj) {
            self.selectedArmor(obj);

            self.isAddingNew(false);
            self.isEditing(true);
            self.showAll(false);
        };

        self.openCreateForm = function (obj) {
            self.isAddingNew(true);
            self.isEditing(false);
            self.showAll(false);

            self.newArmor({
                Id: _i.ko.observable(0),
                ProficiencyName: _i.ko.observable(''),	//Tied To ArmorProficiencyId as it's descriptor
                ArmorClass: _i.ko.observable(''),
                Cost: _i.ko.observable(''),
                Name: _i.ko.observable(''),
                ProficiencyId: _i.ko.observable(0), //What is the parent Proficiency. For this page it's Armor so ID 1
                Stealth: _i.ko.observable(false),
                Strength: _i.ko.observable(''),
                Weight: _i.ko.observable('')
            });

        };

        /* Close the editor (new or edit) and return to the base list */
        self.cancelEditor = function () {
            self.isAddingNew(false);
            self.isEditing(false);
            self.showAll(true);
        };

        /*==================== SAVE/EDIT/DELETE ====================*/

        self.deleteArmor = function (obj) {
            _i.confirmdelete.show().then(function (response) {
                if (response.accepted) {
                    _i.charajax.delete('api/armor/DeleteArmor/' + obj.Id(), '').done(function (response) {
                        self.armors.remove(obj);
                        _i.alert.showAlert({ type: "error", message: "Armor Deleted" });
                    });
                }
            });
        };

        self.save = function (armorToSave) {
            var isEditState = self.isDirty() && self.isEditing();
            var isNewState = self.isAddingNew();

            var dataToSave = {
                Id: armorToSave.Id(),
                ProficiencyId: armorToSave.ProficiencyId(),
                ArmorClass: armorToSave.ArmorClass(),
                Cost: armorToSave.Cost(),
                Name: armorToSave.Name(),
                ProficiencyName: armorToSave.ProficiencyName(),
                Stealth: armorToSave.Stealth(),
                Strength: armorToSave.Strength(),
                Weight: armorToSave.Weight()
            };

            if (isNewState) {
                return _i.charajax.post('api/armor/AddArmor/', dataToSave).done(function (response) {
                    var mapped = _i.ko.mapping.fromJS(response);
                    mapped.dirtyFlag = new _i.ko.dirtyFlag(mapped);

                    self.armors.push(mapped);
                    self.resetToBaseList("success", "New Armor Added");
                });
            }

            if (isEditState) {
                return _i.charajax.put('api/armor/EditArmor/', dataToSave).done(function (response) {
                    self.selectedArmor().dirtyFlag.reset();
                    self.resetToBaseList("success", "Armor Edit Saved");
                });
            }

            return _i.deferred.createResolved();
        };

    }
});
