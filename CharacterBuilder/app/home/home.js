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
        roller: require('_custom/services/roll'),
        abilityscore: require('_custom/services/abilityscore'),
        checklist: require('_custom/services/checklist')
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

        self.rollHpText = function () {
            if (self.selectedSheet().Class.Hitdie === undefined) {
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

        /*==================== PROGRESS TOWARDS COMPLETING CURRENT LEVEL ====================*/

        self.levelIsComplete = function () {
            return _i.checklist.IsLevelComplete(self.selectedSheet());
        };
        
        self.hasRolledHp = function () {
            return _i.checklist.IsHpTaskComplete(self.selectedSheet());           
        };

        self.hasFinishedAbilityScores = function () {
            return _i.checklist.IsAbilityScoreTaskComplete(self.selectedSheet());
        };

        self.activate = function () {
            return self.getPageData().done(function () {

                self.abilScore = _i.ko.computed(function () {
                    var result = [];
                    if (!self.selectedSheet()) return result;

                    for (var propName in self.selectedSheet().AbilityScores) {
                        if (self.selectedSheet().AbilityScores.hasOwnProperty(propName)) {
                            var abilityScoreObj = _i.abilityscore.CombineScoresWithIncrease(self.selectedSheet(), propName);
                            result.push({ propName: abilityScoreObj.Name, shortName: abilityScoreObj.ShortName, abilScore: abilityScoreObj.ScoreTotal(), abilMod: abilityScoreObj.Modifier, templateName: "scalar_templ" });
                        }
                    }

                    return result;
                });

                self.totalHitPoints = function () {
                    return self.selectedSheet().HpMax() + (self.selectedSheet().AbilityScores["Constitution"]());
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

        self.markSkillAsProficiencyChoice = function (sheet) {

            sheet.SkillProficiencies.forEach(function (skl) {
                sheet.AllSkills.forEach(function (skill) {
                    if (skill.Name === skl.Name) {
                        skill.canPick = true;
                    }
                });
            });

        };

        self.rollScore = function (score) {
            _i.roller.RollAbilityScore(self.selectedSheet(), score);

            self.updateTodoAndTask("HasRolled" + score.propName);
            self.saveSheet(self.selectedSheet());
        };

        self.rollHitPoints = function (sheet) {
            var totalHpAfterRollNoMod = _i.roller.RollHitPoints(sheet.Class.Hitdie(), sheet.HpMax());
            var rolledVal = totalHpAfterRollNoMod - sheet.HpMax();

            self.selectedSheet().HpMax(totalHpAfterRollNoMod);
            self.updateTodoAndTask("HasIncreasedHp");
            self.saveSheet(self.selectedSheet()).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Rolled: " + rolledVal });
            });
        };

        self.defaultHitPoints = function (sheet) {
            var defaultIncrease = (sheet.Class.Hitdie() * .5) + 1;
            var totalHpAfterDefaultNoMod = _i.roller.DefaultHitPoints(defaultIncrease, sheet.HpMax());

            self.selectedSheet().HpMax(totalHpAfterDefaultNoMod);

            self.saveSheet(self.selectedSheet()).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Hit Points Increased by Default: " + defaultIncrease });
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

        self.levelUp = function () {
            if (!_i.checklist.IsLevelComplete(self.selectedSheet())) {
                return _i.deferred.createResolved();
            }
            
            var currentLevel = self.selectedSheet().Level();
            self.selectedSheet().Level(currentLevel + 1);

            return _i.charajax.post('api/charactersheet/AddLevelChecklist/' + self.selectedSheet().Id()).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Leveled-up to level " + self.selectedSheet().Level() });
                if (self.selectedSheet().LevelCheckList === undefined) {
                    self.selectedSheet().LevelCheckList = response;
                } else {
                    self.selectedSheet().LevelCheckList(response);
                }
                
            });
        };

        self.updateTodoAndTask = function (taskName) {
            var sheet = self.selectedSheet();
            if (sheet.Level() === 1) {
                self.updateFirstLevelTask(taskName);
            } else {
                self.updateLevelChecklist(taskName);
            }

        };

        self.updateFirstLevelTask = function (taskName) {
            self.selectedSheet().ToDo.FirstLevelTasks[taskName](true);
        };

        self.updateLevelChecklist = function (taskName) {
            self.selectedSheet().LevelChecklist[taskName](true);
            if (!self.selectedSheet().LevelChecklist.HasAbilityScoreIncrease()) {
                self.selectedSheet().LevelChecklist.HasIncreasedAbilityScores(true);
            }
        };

        self.saveSheet = function (sheetToSave) {

            var skillsToSave = [];

            sheetToSave.Skills().forEach(function (skl) {
                sheetToSave.AllSkills().forEach(function (baseskill) {
                    if (baseskill.Id() === skl) {
                        skillsToSave.push(baseskill);
                    }
                });
            });

            if (sheetToSave.Skills().length === sheetToSave.SkillPickCount()) {
                self.selectedSheet().ToDo.HasSelectedSkills(true);
            }

            var dataToSave = {
                Id: sheetToSave.Id(),
                Level: sheetToSave.Level(),
                CharacterName: sheetToSave.CharacterName(),
                PlayerName: sheetToSave.PlayerName(),
                Alignment: sheetToSave.Alignment(),
                HpMax: sheetToSave.HpMax(),
                AbilityScores: _i.ko.toJS(sheetToSave.AbilityScores),
                ToDo: _i.ko.toJS(sheetToSave.ToDo),
                Skills: sheetToSave.Skills()
            };

            return _i.charajax.put('api/charactersheet/EditSheet', dataToSave).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Sheet Saved!" });
            });
        };

        self.addNew = function () {
            _i.charajax.post('api/charactersheet/CreateNewSheet', '').done(function (response) {
                window.builder.global_sheetid = response.Id;
                response.createdDateFormatted = moment(response.CreatedDate).format('LLL');

                var mapped = _i.ko.mapping.fromJS(response);

                self.characterSheets.push(mapped);
                self.selectedSheet(mapped);
                self.showAlertAndOpenEditor();

            });
        };

        self.getCharacterSheets = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/charactersheet/GetUserSheets').done(function (response) {

                response.forEach(function (sheet) {
                    sheet.createdDateFormatted = moment(sheet.CreatedDate).format('LLL');
                    
                    self.markSkillAsProficiencyChoice(sheet);
                    sheet.SkillPickCount = 0;

                    if (sheet.Class != null) {
                        sheet.SkillPickCount += sheet.Class.SkillPickCount;
                        
                        if (sheet.LevelChecklist.HasAbilityScoreIncrease && !sheet.LevelChecklist.HasIncreasedAbilityScores) {
                            sheet.SelectedAbilityScoreIncreases = 0;
                        }                        
                    }
                    
                    if (sheet.Background != null) {
                        sheet.SkillPickCount += sheet.Background.Skills.length;
                    }
                    
                });

                var mapped = _i.ko.mapping.fromJS(response);

                self.characterSheets(mapped());

                deferred.resolve();
            });
            return deferred;
        };
    }
});
