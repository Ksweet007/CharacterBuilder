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
        self.CharacterName = _i.ko.observable('');
        self.PlayerName = _i.ko.observable('');
        self.Alignment = _i.ko.observable('');
        self.Level = _i.ko.observable(0);
        self.HitPoints = _i.ko.observable(0);
        self.HitDie = _i.ko.observable(0);

        self.AbilityScores = _i.ko.observable();
        self.SelectedAbilityScoreIncreases = _i.ko.observable(0);

        self.AllSkills = _i.ko.observableArray([]);
        self.SkillPickCount = _i.ko.observable(0);
        self.SkillProficiencies = _i.ko.observableArray([]);
        self.Skills = _i.ko.observableArray([]);

        self.ToDo = _i.ko.observable();
        self.LevelCheckList = _i.ko.observable();

        /*==================== ACTIVATE DEPENDENCIES ====================*/

        self.activate = function (sheetId) {
            self.sheetId(sheetId);
            return self.getPageData().done(function () {

                self.maxHp = _i.ko.pureComputed(function () {
                    var conMod = self.AbilityScores().Constitution;
                    return self.HitPoints() + conMod;
                });

                self.defaultHp = _i.ko.pureComputed(function () {
                    return self.HitDie() - (self.HitDie() * .5) + 1;
                });

                self.hasFinishedAbilityScores = _i.ko.computed(function() {
                    if (self.Level() === 1) {
                        return self.ToDo().HasCompletedAbilityScores();
                    }

                    if (self.LevelCheckList().HasAbilityScoreIncrease) {
                        return self.SelectedAbilityScoreIncreases() === 2;
                    }

                    return true;

                });

                self.levelIsComplete = _i.ko.computed(function() {
                    if (self.Level() === 1) {
                        return _i.checklist.LevelOneComplete(self.ToDo());
                    }

                    if (self.LevelChecklist().HasAbilityScoreIncrease) {
                        return self.LevelChecklist().HasIncreasedAbilityScores && self.LevelChecklist().HasIncreasedHp;
                    }

                    return self.LevelChecklist().HasIncreasedHp;
                });

                self.hasRolledHp = _i.ko.computed(function() {
                    if (self.Level() === 1) {
                        return self.ToDo().FirstLevelTasks.HasIncreasedHp();
                    }

                    return self.LevelCheckList.HasIncreasedHp;
                });

                self.abilityScores = _i.ko.computed(function () {
                    var scores = [];
                    for (var property in self.AbilityScores()) {
                        if (self.AbilityScores().hasOwnProperty(property)) {
                            var score = {};
                            score.shortName = property.substr(0, 3);
                            score.Value = self.AbilityScores()[property];
                            score.Modifier = Math.floor((self.AbilityScores()[property] - 10) / 2);
                            scores.push(score);
                        }
                    }

                    return scores;
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
            if (self.Level() === 1) {
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

        self.markSkillAsProficiencyChoice = function () {            
            self.AllSkills().forEach(function (baseSkill) {
                baseSkill.canPick = false;
                self.SkillProficiencies().forEach(function(skillProficiency) {
                    if (baseSkill.Id === skillProficiency.Id) {
                        baseSkill.canPick = true;
                    }
                });
            });

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

        self.getPageData = function () {
            var deferred = _i.deferred.create();
            var promise = _i.deferred.waitForAll(self.getCharacterSheet(), self.getSheetSkills());

            promise.done(function () {
                deferred.resolve();
            });

            return deferred;
        };

        self.getSheetSkills = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/charactersheet/GetSkillsBySheetId/' + self.sheetId()).done(function (response) {
                self.skillData = response;

                self.AllSkills(self.skillData.AllSkills);
                self.SkillPickCount(self.skillData.SkillPickCount);
                self.SkillProficiencies(self.skillData.SkillProficiencies);
                self.Skills(self.skillData.Skills);

                self.markSkillAsProficiencyChoice();

                deferred.resolve();
            });
            return deferred;
        };

        self.getCharacterSheet = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/charactersheet/GetSheetById/' + self.sheetId()).done(function (response) {
                self.characterSheet = response;
                
                self.AbilityScores(self.characterSheet.AbilityScores);
                self.HitDie(self.characterSheet.Class.Hitdie);
                self.CharacterName(self.characterSheet.CharacterName);
                self.PlayerName(self.characterSheet.PlayerName);
                self.Class = self.characterSheet.Class;
                self.Background = self.characterSheet.Background;
                self.Race = self.characterSheet.Race;

                self.Level(self.characterSheet.Level);

                self.ToDo(_i.ko.mapping.fromJS(self.characterSheet.ToDo));
                self.LevelCheckList(self.characterSheet.LevelChecklist);


                

                deferred.resolve();
            });
            return deferred;
        };
    }
});
