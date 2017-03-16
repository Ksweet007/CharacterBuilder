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
        self.AbilityScoreIncreases = _i.ko.observableArray([]);
        self.SelectedAbilityScoreIncreases = _i.ko.observable(0);

        self.AllSkills = _i.ko.observableArray([]);
        self.SkillPickCount = _i.ko.observable(0);
        self.SkillProficiencies = _i.ko.observableArray([]);
        self.Skills = _i.ko.observableArray([]);

        self.ProficiencyBonuses = _i.ko.observableArray([]);
        
        self.ToDo = _i.ko.observable();
        self.LevelChecklist = _i.ko.observable();

        self.activate = function (sheetId) {
            self.sheetId(sheetId);
            return self.getPageData().done(function () {

                self.scoreIncreaseByName = function (scoreName) {
                    var returnVal = 0;
                    self.AbilityScoreIncreases().forEach(function (increase) {
                        if (increase.Name === scoreName) {
                            returnVal += increase.IncreaseAmount;
                        }
                    });

                    return returnVal;
                };

                self.ScoreIncreases = function () {
                    return {
                        Strength: self.scoreIncreaseByName("Strength"),
                        Dexterity: self.scoreIncreaseByName("Dexterity"),
                        Constitution: self.scoreIncreaseByName("Constitution"),
                        Intelligence: self.scoreIncreaseByName("Intelligence"),
                        Wisdom: self.scoreIncreaseByName("Wisdom"),
                        Charisma: self.scoreIncreaseByName("Charisma")
                    }
                };

                self.Strength = {
                    Name: "Strength",
                    ShortName: "STR",
                    Score: _i.ko.computed(function () {
                        return self.AbilityScores().Strength() + self.ScoreIncreases().Strength;
                    }),
                    Mod: _i.ko.computed(function () {
                        return Math.floor((self.AbilityScores().Strength() - 10) / 2);
                    }),
                    CanRoll: _i.ko.computed(function () {
                        return !self.ToDo().FirstLevelTasks.HasRolledStrength();
                    })
                };

                self.Dexterity = {
                    Name: "Dexterity",
                    ShortName: "DEX",
                    Score: _i.ko.computed(function () {
                        return self.AbilityScores().Dexterity() + self.ScoreIncreases().Dexterity;
                    }),
                    Mod: _i.ko.computed(function () {
                        return Math.floor((self.AbilityScores().Dexterity() - 10) / 2);
                    }),
                    CanRoll: _i.ko.computed(function () {
                        return !self.ToDo().FirstLevelTasks.HasRolledDexterity();
                    })
                };

                self.Constitution = {
                    Name: "Constitution",
                    ShortName: "CON",
                    Score: _i.ko.computed(function () {
                        return self.AbilityScores().Constitution() + self.ScoreIncreases().Constitution;
                    }),
                    Mod: _i.ko.computed(function () {
                        return Math.floor((self.AbilityScores().Constitution() - 10) / 2);
                    }),
                    CanRoll: _i.ko.computed(function () {
                        return !self.ToDo().FirstLevelTasks.HasRolledConstitution();
                    })
                };

                self.Intelligence = {
                    Name: "Intelligence",
                    ShortName: "INT",
                    Score: _i.ko.computed(function () {
                        return self.AbilityScores().Intelligence() + self.ScoreIncreases().Intelligence;
                    }),
                    Mod: _i.ko.computed(function () {
                        return Math.floor((self.AbilityScores().Intelligence() - 10) / 2);
                    }),
                    CanRoll: _i.ko.computed(function () {
                        return !self.ToDo().FirstLevelTasks.HasRolledIntelligence();
                    })
                };

                self.Wisdom = {
                    Name: "Wisdom",
                    ShortName: "WIS",
                    Score: _i.ko.computed(function () {
                        return self.AbilityScores().Wisdom() + self.ScoreIncreases().Wisdom;
                    }),
                    Mod: _i.ko.computed(function () {
                        return Math.floor((self.AbilityScores().Wisdom() - 10) / 2);
                    }),
                    CanRoll: _i.ko.computed(function () {
                        return !self.ToDo().FirstLevelTasks.HasRolledWisdom();
                    })
                };

                self.Charisma = {
                    Name: "Charisma",
                    ShortName: "CHA",
                    Score: _i.ko.computed(function () {
                        return self.AbilityScores().Charisma() + self.ScoreIncreases().Charisma;
                    }),
                    Mod: _i.ko.computed(function () {
                        return Math.floor((self.AbilityScores().Charisma() - 10) / 2);
                    }),
                    CanRoll: _i.ko.computed(function () {
                        return !self.ToDo().FirstLevelTasks.HasRolledCharisma();
                    })
                };

                self.AbilityScoreListing = _i.ko.observableArray([self.Strength, self.Dexterity, self.Constitution, self.Intelligence, self.Wisdom, self.Charisma]);

                self.maxHp = _i.ko.pureComputed(function () {
                    var hpIncrease = self.Constitution.Mod() * self.Level();
                    return self.HitPoints() + hpIncrease; //Add Con Mod per Level to our base HP (All Rolled values)
                });

                self.defaultHp = _i.ko.pureComputed(function () {
                    return self.HitDie() - (self.HitDie() * .5) + 1;
                });

                self.ProficiencyBonus = _i.ko.computed(function() {
                    return self.ProficiencyBonuses().filter(function(bonus) {
                        if (bonus.Level === self.Level()) return bonus;
                    });
                });
                
                self.hasFinishedAbilityScores = _i.ko.computed(function () {
                    if (self.Level() === 1) {
                        return self.ToDo().HasCompletedAbilityScores();
                    }

                    if (self.LevelChecklist().HasAbilityScoreIncrease()) {
                        return self.SelectedAbilityScoreIncreases() === 2;
                    }

                    return true;

                });

                self.levelIsComplete = _i.ko.computed(function () {
                    if (self.Level() === 1) {
                        return _i.checklist.LevelOneComplete(self.ToDo());
                    }

                    if (self.LevelChecklist().HasAbilityScoreIncrease) {
                        return self.LevelChecklist().HasIncreasedAbilityScores() && self.LevelChecklist().HasIncreasedHp();
                    }

                    return self.LevelChecklist().HasIncreasedHp();
                });

                self.CanRollHp = _i.ko.computed(function () {
                    if (self.Class.Id === 0) return false;

                    if (self.Level() === 1) {
                        return !self.ToDo().FirstLevelTasks.HasIncreasedHp();
                    }

                    return !self.LevelChecklist().HasIncreasedHp();
                });



            });
        };

        /*==================== ROLLS ====================*/
        self.rollScore = function (score) {
            _i.roller.RollAbilityScore(self.AbilityScores(), score);

            self.updateTodoAndTask("HasRolled" + score.Name);
            self.saveSheet(self.characterSheet);
        };

        self.rollHitPoints = function () {
            var totalHpAfterRollNoMod = _i.roller.RollHitPoints(self.Class.Hitdie, self.HitPoints());
            var rolledVal = totalHpAfterRollNoMod - self.HitPoints();

            self.HitPoints(totalHpAfterRollNoMod);

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
            self.Level(self.Level() + 1);

            return _i.charajax.post('api/charactersheet/AddLevelChecklist/' + self.sheetId()).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Leveled-up to level " + self.Level() });
                self.LevelCheckList(response);
            });
        };

        self.updateTodoAndTask = function (taskName) {
            if (self.Level() === 1) {
                self.updateFirstLevelTask(taskName);
            } else {
                self.updateLevelChecklist(taskName);
            }
        };

        self.updateFirstLevelTask = function (taskName) {
            self.ToDo().FirstLevelTasks[taskName](true);
        };

        self.updateLevelChecklist = function (taskName) {
            self.LevelChecklist[taskName](true);
            if (!self.LevelChecklist().HasAbilityScoreIncrease()) {
                self.LevelChecklist().HasIncreasedAbilityScores(true);
            }
        };

        self.markSkillAsProficiencyChoice = function () {
            self.AllSkills().forEach(function (baseSkill) {
                baseSkill.canPick = false;
                self.SkillProficiencies().forEach(function (skillProficiency) {
                    if (baseSkill.Id === skillProficiency.Id) {
                        baseSkill.canPick = true;
                    }
                });
            });

        };

        self.saveSheet = function () {
            var skillsToSave = [];

            self.Skills().forEach(function (skl) {
                self.AllSkills().forEach(function (baseskill) {
                    if (baseskill.Id === skl) {
                        skillsToSave.push(baseskill);
                    }
                });
            });

            if (self.Skills().length === self.SkillPickCount()) {
                self.ToDo().HasSelectedSkills(true);
            }

            var dataToSave = {
                Id: self.sheetId(),
                Level: self.Level(),
                CharacterName: self.CharacterName(),
                PlayerName: self.PlayerName(),
                Alignment: self.Alignment(),
                HpMax: self.HitPoints(),
                AbilityScores: _i.ko.toJS(self.AbilityScores),
                ToDo: _i.ko.toJS(self.ToDo),
                Skills: self.Skills()
            };

            return _i.charajax.put('api/charactersheet/EditSheet', dataToSave).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Sheet Saved!" });
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

                self.CharacterName(self.characterSheet.CharacterName);
                self.PlayerName(self.characterSheet.PlayerName);
                self.Alignment(self.characterSheet.Alignment);

                self.Level(self.characterSheet.Level);

                self.Class = self.characterSheet.Class;
                self.Background = self.characterSheet.Background;
                self.Race = self.characterSheet.Race;

                self.AbilityScores(_i.ko.mapping.fromJS(self.characterSheet.AbilityScores));
                self.AbilityScoreIncreases(self.characterSheet.AbilityScoreIncreases);

                self.HitDie(self.characterSheet.Class.Hitdie);
                self.HitPoints(self.characterSheet.HpMax);

                self.ProficiencyBonuses(self.characterSheet.ProficiencyBonuses);

                self.ToDo(_i.ko.mapping.fromJS(self.characterSheet.ToDo));
                self.LevelChecklist(_i.ko.mapping.fromJS(self.characterSheet.LevelChecklist));

                deferred.resolve();
            });
            return deferred;
        };
    }
});
