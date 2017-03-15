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
        self.sheetId = _i.ko.observable(0);

        /*==================== PAGE STATE/FILTERED ITEMS ====================*/
        self.rollHpText = function () {
            if (self.characterSheet.Class.Hitdie === undefined) {
                return "";
            }
            return "Roll HP (1d" + self.characterSheet.Class.Hitdie() + ")";
        };

        self.takeDefaultValue = function () {
            if (self.characterSheet.Class.Hitdie === undefined) {
                return "";
            }
            var defaultHp = self.characterSheet.Class.Hitdie() - (self.characterSheet.Class.Hitdie() * .5) + 1;
            return "Default HP " + "(" + defaultHp + ")";
        };

        /*==================== LEVEL CHECKLIST ====================*/

        self.levelIsComplete = function () {
            return _i.checklist.IsLevelComplete(self.characterSheet);
        };

        self.hasRolledHp = function () {
            return _i.checklist.IsHpTaskComplete(self.characterSheet);
        };

        self.hasFinishedAbilityScores = function () {
            return _i.checklist.IsAbilityScoreTaskComplete(self.characterSheet);
        };

        self.activate = function (sheetId) {
            self.sheetId(sheetId);
            return self.getPageData().done(function () {

                self.abilScore = _i.ko.computed(function () {
                    var result = [];

                    for (var propName in self.characterSheet.AbilityScores) {
                        if (self.characterSheet.AbilityScores.hasOwnProperty(propName)) {
                            var abilityScoreObj = _i.abilityscore.CombineScoresWithIncrease(self.characterSheet, propName);
                            result.push({ propName: abilityScoreObj.Name, shortName: abilityScoreObj.ShortName, abilScore: abilityScoreObj.ScoreTotal, abilMod: abilityScoreObj.Modifier, templateName: "scalar_templ" });
                        }
                    }

                    return result;
                });
            });
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

        /*==================== ROLLS ====================*/
        self.rollScore = function (score) {
            _i.roller.RollAbilityScore(self.characterSheet, score);

            self.updateTodoAndTask("HasRolled" + score.propName);
            self.saveSheet(self.characterSheet);
        };

        self.rollHitPoints = function (sheet) {
            var totalHpAfterRollNoMod = _i.roller.RollHitPoints(sheet.Class.Hitdie(), sheet.HpNoMod());
            var rolledVal = totalHpAfterRollNoMod - sheet.HpNoMod();

            self.characterSheet.HpNoMod(totalHpAfterRollNoMod);
            self.updateTodoAndTask("HasIncreasedHp");
            self.saveSheet(self.characterSheet).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Rolled: " + rolledVal });
            });
        };

        self.defaultHitPoints = function (sheet) {
            var defaultIncrease = (sheet.Class.Hitdie() * .5) + 1;
            var totalHpAfterDefaultNoMod = _i.roller.DefaultHitPoints(defaultIncrease, sheet.HpMax());

            self.characterSheet.HpMax(totalHpAfterDefaultNoMod);

            self.saveSheet(self.characterSheet).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Hit Points Increased by Default: " + defaultIncrease });
            });
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

        self.levelUp = function () {
            if (!_i.checklist.IsLevelComplete(self.characterSheet)) {
                return _i.deferred.createResolved();
            }

            var currentLevel = self.characterSheet.Level();
            self.characterSheet.Level(currentLevel + 1);

            return _i.charajax.post('api/charactersheet/AddLevelChecklist/' + self.characterSheet.Id()).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Leveled-up to level " + self.characterSheet.Level() });
                if (self.characterSheet.LevelCheckList === undefined) {
                    self.characterSheet.LevelCheckList = response;
                } else {
                    self.characterSheet.LevelCheckList(response);
                }

            });
        };

        self.updateTodoAndTask = function (taskName) {
            var sheet = self.characterSheet;
            if (sheet.Level() === 1) {
                self.updateFirstLevelTask(taskName);
            } else {
                self.updateLevelChecklist(taskName);
            }
        };

        self.updateFirstLevelTask = function (taskName) {
            self.characterSheet.ToDo.FirstLevelTasks[taskName](true);
        };

        self.updateLevelChecklist = function (taskName) {
            self.characterSheet.LevelChecklist[taskName](true);
            if (!self.characterSheet.LevelChecklist.HasAbilityScoreIncrease()) {
                self.characterSheet.LevelChecklist.HasIncreasedAbilityScores(true);
            }
        };

        self.getPageData = function () {
            var deferred = _i.deferred.create();
            var promise = _i.deferred.waitForAll(self.getCharacterSheets());

            promise.done(function () {
                deferred.resolve();
            });

            return deferred;
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
                self.characterSheet.ToDo.HasSelectedSkills(true);
            }

            var dataToSave = {
                Id: sheetToSave.Id(),
                Level: sheetToSave.Level(),
                CharacterName: sheetToSave.CharacterName(),
                PlayerName: sheetToSave.PlayerName(),
                Alignment: sheetToSave.Alignment(),
                HpMax: sheetToSave.HpNoMod(),
                AbilityScores: _i.ko.toJS(sheetToSave.AbilityScores),
                ToDo: _i.ko.toJS(sheetToSave.ToDo),
                Skills: sheetToSave.Skills()
            };

            return _i.charajax.put('api/charactersheet/EditSheet', dataToSave).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Sheet Saved!" });
            });
        };

        self.viewMoreDetails = function (bgSelected) {
            self.characterSheet(bgSelected);
            self.viewingDetails(true);
        };

        self.closeMoreDetails = function () {
            self.viewingDetails(false);
        };

        self.addNew = function () {
            _i.charajax.post('api/charactersheet/CreateNewSheet', '').done(function (response) {
                window.builder.global_sheetid = response.Id;
                response.createdDateFormatted = moment(response.CreatedDate).format('LLL');

                var mapped = _i.ko.mapping.fromJS(response);

                self.characterSheets.push(mapped);
                self.characterSheet(mapped);
                _i.alert.showAlert({ type: "success", message: "New Character Added!" });
                self.viewingDetails(true);

            });
        };

        self.selectSheetToEdit = function (sheetToEdit) {
            self.characterSheet(sheetToEdit);

            _i.globals.setSheetToEdit(self.characterSheet.Id());
            _i.globals.createCookie("SheetBeingWorked", self.characterSheet.Id());

            var editMessage = "Currently Editing";
            if (sheetToEdit.CharacterName() != null) {
                editMessage += " " + sheetToEdit.CharacterName();
            }

            _i.alert.showAlert({
                type: "success", message: editMessage
            });
        };

        self.getCharacterSheets = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/charactersheet/GetSheetById/' + self.sheetId()).done(function (response) {
                self.characterSheet = response;

                self.characterSheet.createdDateFormatted = moment(self.characterSheet.CreatedDate).format('LLL');
                self.characterSheet.HpNoMod = self.characterSheet.HpMax;
                self.characterSheet.HpMax = self.characterSheet.HpNoMod + Math.floor(((self.characterSheet.AbilityScores["Constitution"] - 10) / 2));

                self.markSkillAsProficiencyChoice(self.characterSheet);
                self.characterSheet.SkillPickCount = 0;

                if (self.characterSheet.Class != null) {
                    self.characterSheet.SkillPickCount += self.characterSheet.Class.SkillPickCount;

                    if (self.characterSheet.LevelChecklist.HasAbilityScoreIncrease && !self.characterSheet.LevelChecklist.HasIncreasedAbilityScores) {
                        self.characterSheet.SelectedAbilityScoreIncreases = 0;
                    }
                }

                if (self.characterSheet.Background != null) {
                    self.characterSheet.SkillPickCount += self.characterSheet.Background.Skills.length;
                }

                self.characterSheet =  _i.ko.mapping.fromJS(self.characterSheet);

                deferred.resolve();
            });
            return deferred;
        };
    }
});
