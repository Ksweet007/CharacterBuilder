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
        confirmdelete: require('confirmdelete/confirmdelete'),
        roller: require('_custom/services/roll')
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

        self.rollHp = function () {
            if (self.selectedSheet().Class.Hitdie === undefined ) {
                return "";
            }
            return "Roll HP (1d" + self.selectedSheet().Class.Hitdie() + ")";
        };

        self.takeDefaultValue = function () {
            if (self.selectedSheet().Class.Hitdie === undefined) {                
                return "";
            }
            var defaultHp = self.selectedSheet().Class.Hitdie() - (self.selectedSheet().Class.Hitdie() * .5) + 1;
            return "Default HP " + "(" + defaultHp + ")";
        };

        self.activate = function () {
            return self.getPageData().done(function () {

                self.abilScore = _i.ko.computed(function () {
                    var result = [];
                    if (self.selectedSheet()) {
                        for (var propName in self.selectedSheet().AbilityScores) {
                            var shortName = propName.substr(0, 3);
                            var abilMod = self.selectedSheet()[propName + "Mod"]();
                            var modBonus = self.selectedSheet()[propName + "Bonus"]();
                            var abilScore = self.selectedSheet().AbilityScores[propName];
                            var abilityScoreTotal = abilScore() + modBonus;

                            if (self.selectedSheet().AbilityScores.hasOwnProperty(propName) && propName !== 'propList') {
                                result.push({ propName: propName, shortName: shortName, abilScore: abilityScoreTotal, abilMod: abilMod, templateName: "scalar_templ" });
                            }
                        }
                    }

                    return result;
                });

                self.totalHitPoints = function() {
                    return self.selectedSheet().HpMax() + (self.selectedSheet().ConstitutionMod() * self.selectedSheet().Level());
                };

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
                    self.setAbilityScoreBonusesInitialValue(sheet);
                    self.addAbilityScoreIncreasesToScores(sheet);
                    self.calculateAbilityModifiers(sheet);
                    self.markSkillAsProficiencyChoice(sheet);                    
                });

                var mapped = _i.ko.mapping.fromJS(response);
                self.characterSheets(mapped());

                deferred.resolve();
            });
            return deferred;
        };

        self.hasCompletedCurrentLevel = function() {
            var todoList = self.selectedSheet().ToDo;
            for (var key in todoList) {
                var obj = todoList[key];
                if (obj() === false) {
                    console.log("LEVEL NOT COMPLETED LEVEL NOT COMPLETED");
                    return false;
                }
            }
            console.log("LEVEL COMPLETED LEVEL COMPLETED");
            return true;
        };

        self.markSkillAsProficiencyChoice = function(sheet) {
            for (var key in sheet.SkillProficiencies) {
                var obj = sheet.SkillProficiencies[key];
                for (var skl in sheet.AllSkills) {
                    if (sheet.AllSkills[skl].Name === obj.Name) {
                        sheet.AllSkills[skl].canPick = true;
                    }
                }
            }            
        };

        self.setAbilityScoreBonusesInitialValue = function (sheet) {
            for (var propName in sheet.AbilityScores) {
                sheet[propName + "Bonus"] = 0;
            }
        };

        self.addAbilityScoreIncreasesToScores = function (sheet) {
            sheet.AbilityScoreIncreases.forEach(function (increase) {
                sheet[increase.Name + "Bonus"] += increase.IncreaseAmount;
            });
        };

        self.calculateAbilityModifiers = function (sheet) {
            for (var propName in sheet.AbilityScores) {
                sheet[propName + "Mod"] = Math.floor((sheet.AbilityScores[propName] - 10) / 2);
            }
        };

        self.setScoreOnRoll = function (abil, bonus, mod) {
            var rolledValue = self.rollAbilityScore();
            var newScore = rolledValue + bonus;
            var newMod = Math.floor((newScore - 10) / 2);

            abil(rolledValue);
            mod(newMod);
        };

        self.rollScore = function (score) {
            var scoreToRoll = self.selectedSheet().AbilityScores[score.propName];
            var scoreBonusName = score.propName + "Bonus";
            var scoreBonus = self.selectedSheet()[scoreBonusName]();
            var scoreModName = score.propName + "Mod";
            var scoreMod = self.selectedSheet()[scoreModName];
            self.setScoreOnRoll(scoreToRoll, scoreBonus, scoreMod);
            self.saveSheet(self.selectedSheet());
        };

        self.rollAbilityScore = function () {
            var rolls = [];

            for (var i = 0; i < 4; i++) {
                var d6 = 1 + Math.floor(Math.random() * 6);                
                rolls.push(d6);
            }

            rolls.sort();
            _i.alert.showAlert({type: "success",message: "Rolls: " + rolls.join(', ')});
            rolls.shift();

            var scoreTotal = 0;
            for (var s in rolls) {
                scoreTotal += rolls[s];
            }
            return scoreTotal < 8 ? 8 : scoreTotal;
        };

        self.rollHitPoints = function (sheet) {
            var totalHpAfterRollNoMod = _i.roller.RollHitPoints(sheet.Class.Hitdie(), sheet.HpMax());
            var rolledVal = totalHpAfterRollNoMod - sheet.HpMax();            

            self.selectedSheet().HpMax(totalHpAfterRollNoMod);

            self.saveSheet(self.selectedSheet()).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Rolled: " + rolledVal });
            });
        };

        self.defaultHitPoints = function (sheet) {
            var defaultHp = (sheet.Class.Hitdie() * .5) + 1;

            var currentHp = sheet.HpMax();
            var conMod = sheet.ConstitutionMod();
            var currentLevel = sheet.Level();
            var conModFactor = conMod * currentLevel;

            var newMaxHp = currentHp + defaultHp + conModFactor;
            sheet.HpMax(newMaxHp);

            self.saveSheet(sheet).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Hit Points Increased"});
            });
        };

        self.viewMoreDetails = function (bgSelected) {
            self.selectedSheet(bgSelected);
            self.viewingDetails(true);
        };

        self.closeMoreDetails = function () {
            self.viewingDetails(false);
        };

        self.deleteSheet = function (obj) {
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
            _i.globals.createCookie("SheetBeingWorked", self.selectedSheet().Id());

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

            return _i.charajax.put('api/charactersheet/EditSheet', dataToSave).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Sheet Saved!" });
            });
        };

        self.addNew = function () {
            _i.charajax.post('api/charactersheet/CreateNewSheet', '').done(function (response) {
                window.builder.global_sheetid = response.Id;
                response.createdDateFormatted = moment(response.CreatedDate).format('LLL');
                self.setAbilityScoreBonusesInitialValue(response);
                self.addAbilityScoreIncreasesToScores(response);
                self.calculateAbilityModifiers(response);
                self.markSkillAsProficiencyChoice(response);
                
                var mapped = _i.ko.mapping.fromJS(response);

                self.characterSheets.push(mapped);
                self.selectedSheet(mapped);
                self.showAlertAndOpenEditor();

            });
        };
    }
});
