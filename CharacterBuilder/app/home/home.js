define(function (require) {
    var _i = {
        ko: require('knockout'),
        $: require('jquery'),
        moment: require('moment'),
        charajax: require('_custom/services/WebAPI'),
        list: require('_custom/services/listmanager'),
        deferred: require('_custom/deferred'),
        alert: require('_custom/services/alert'),
        globals: require('_custom/services/builderglobals'),
        confirmdelete: require('confirmdelete/confirmdelete')
    };

    return function () {
        var self = this;
        
        /*==================== BASE DATA ====================*/
        self.characterSheets = _i.ko.observableArray([]);

        /*==================== PAGE STATE/FILTERED ITEMS ====================*/
        self.selectedSheet = _i.ko.observable();
        
        self.viewingDetails = _i.ko.observable(false);
        self.showAll = _i.ko.observable(true);
        
        self.characterSheetsToShow = _i.ko.computed(function () {
            var returnList = self.characterSheets();
            return returnList;
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
            _i.charajax.get('api/charactersheet/GetUserSheets').done(function (response) {
                response.forEach(function (sheet) {
                    sheet.createdDateFormatted = moment(sheet.CreatedDate).format('LLL');
                    self.addAbilityScoreIncreasesToScores(sheet);
                    self.calculateAbilityModifiers(sheet);
                });
                var mapped = _i.ko.mapping.fromJS(response);
                mapped().forEach(function (sheet) {                                        
                    sheet.HpMax(sheet.ConstitutionMod());
                });

                self.characterSheets(mapped());
                deferred.resolve();
            });
            return deferred;
        };

        self.addAbilityScoreIncreasesToScores = function (sheet) {
            sheet.StrengthBonus = 0;
            sheet.DexterityBonus = 0;
            sheet.ConstitutionBonus = 0;
            sheet.IntelligenceBonus = 0;
            sheet.WisdomBonus = 0;
            sheet.CharismaBonus = 0;
            sheet.AbilityScoreIncreases.forEach(function (increase) {
                sheet.AbilityScores[increase.Name] += increase.IncreaseAmount;
            });
        };

        self.calculateAbilityModifiers = function(sheet) {
            sheet.StrengthMod = Math.floor((sheet.AbilityScores.Strength - 10) / 2);
            sheet.DexterityMod = Math.floor((sheet.AbilityScores.Dexterity - 10) / 2);
            sheet.ConstitutionMod = Math.floor((sheet.AbilityScores.Constitution - 10) / 2);
            sheet.IntelligenceMod = Math.floor((sheet.AbilityScores.Intelligence - 10) / 2);
            sheet.WisdomMod = Math.floor((sheet.AbilityScores.Wisdom - 10) / 2);
            sheet.CharismaMod = Math.floor((sheet.AbilityScores.Charisma - 10) / 2);
        };

        self.calculateSingleModifier = function(abil) {
            return Math.floor((abil - 10) / 2);
        };

        self.setScoreOnRoll = function(abil,bonus,mod) {
            var rolledValue = self.rollAbilityScore();
            var newScore = rolledValue + bonus;
            var newMod = Math.floor((newScore - 10) / 2);

            abil(newScore);
            mod(newMod);
        };

        self.rollStr = function () {
            self.setScoreOnRoll(self.selectedSheet().AbilityScores.Strength, self.selectedSheet().StrengthBonus(), self.selectedSheet().StrengthMod);
            self.saveSheet(self.selectedSheet());            
        };

        self.rollDex = function () {
            self.setScoreOnRoll(self.selectedSheet().AbilityScores.Dexterity, self.selectedSheet().DexterityBonus(), self.selectedSheet().DexterityMod);
            self.saveSheet(self.selectedSheet());
        };

        self.rollCon = function () {
            self.setScoreOnRoll(self.selectedSheet().AbilityScores.Constitution, self.selectedSheet().ConstitutionBonus(), self.selectedSheet().ConstitutionMod);
            self.saveSheet(self.selectedSheet());
        };

        self.rollInt = function () {
            self.setScoreOnRoll(self.selectedSheet().AbilityScores.Intelligence, self.selectedSheet().IntelligenceBonus(), self.selectedSheet().IntelligenceMod);
            self.saveSheet(self.selectedSheet());
        };

        self.rollWis = function () {
            self.setScoreOnRoll(self.selectedSheet().AbilityScores.Wisdom, self.selectedSheet().WisdomBonus(), self.selectedSheet().WisdomMod);
            self.saveSheet(self.selectedSheet());
        };

        self.rollCha = function () {
            self.setScoreOnRoll(self.selectedSheet().AbilityScores.Charisma, self.selectedSheet().CharismaBonus(), self.selectedSheet().CharismaMod);
            self.saveSheet(self.selectedSheet());
        };

        self.rollAbilityScore = function () {
            var rolls = [];

            for (var i = 0; i < 4; i++) {
                var d6 = 1 + Math.floor(Math.random() * 6);
                rolls.push(d6);
            }

            rolls.sort();
            _i.alert.showAlert({
                type: "success",
                message: "Rolls: " + rolls.join(', ')
            });

            rolls.shift();

            var scoreTotal = 0;
            for (var s in rolls) {
                scoreTotal += rolls[s];
            }
            return scoreTotal;
        };

        self.viewMoreDetails = function (bgSelected) {
            self.selectedSheet(bgSelected);
            self.viewingDetails(true);
        };

        self.closeMoreDetails = function () {
            self.viewingDetails(false);
        };

        self.deleteSheet = function(obj) {
            _i.confirmdelete.show().then(function (response) {
                if (response.accepted) {
                    _i.charajax.delete('api/charactersheet/DeleteSheet/' + obj.Id(), '').done(function (response) {
                        var alertMsg = "Character Sheet for " + obj.CharacterName() + " Deleted";                        
                        document.cookie = "SheetBeingWorked=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
                        if (obj.Id() === _i.globals.getSheetId()) {
                            _i.globals.clearToDoList();
                        }
                        self.characterSheets.remove(obj);                        
                        _i.alert.showAlert({ type: "error", message: alertMsg });
                    });
                }
            });
        };
        
        self.selectSheetToEdit = function (sheetToEdit) {
            self.selectedSheet(sheetToEdit);

            _i.globals.setSheetToEdit(self.selectedSheet().Id());
            _i.globals.createCookie("SheetBeingWorked",self.selectedSheet().Id());
            
            var editMessage = "Currently Editing";
            if (sheetToEdit.CharacterName() != null) {
                editMessage += " " + sheetToEdit.CharacterName();
            }
            
            _i.alert.showAlert({ type: "success", message: editMessage });
        };

        self.showAlertAndOpenEditor = function () {
            var alertConfig = { type: "success", message: "New Character Added!" };
            _i.alert.showAlert(alertConfig);
            self.viewingDetails(true);
        };

        self.saveSheet = function (sheetToSave) {
            var dataToSave = {
                Id: sheetToSave.Id(),
                Level: sheetToSave.Level(),
                CharacterName: sheetToSave.CharacterName(),
                PlayerName: sheetToSave.PlayerName(),
                Alignment: sheetToSave.Alignment(),
                HpMax: sheetToSave.HpMax(),
                AbilityScores: _i.ko.toJS(sheetToSave.AbilityScores)
            };

            return _i.charajax.put('api/charactersheet/EditSheet', dataToSave).done(function(response) {
                _i.alert.showAlert({ type: "success", message: "Sheet Saved!" });
            });
        };

        self.addNew = function() {
            _i.charajax.post('api/charactersheet/CreateNewSheet', '').done(function(response) {
                window.builder.global_sheetid = response.Id;
                response.createdDateFormatted = moment(response.CreatedDate).format('LLL');
                self.addAbilityScoreIncreasesToScores(response);
                self.calculateAbilityModifiers(response);

                var mapped = _i.ko.mapping.fromJS(response);

                self.characterSheets.push(mapped);
                self.selectedSheet(mapped);
                self.showAlertAndOpenEditor();
                
            });
        };
    }
});
