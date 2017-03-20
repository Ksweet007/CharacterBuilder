define(function (require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    };


    function MapperCls() { }

    MapperCls.prototype.MapPlayerCard = function (card, data) {
        card.PrevCharacterName = _i.ko.observable(card.CharacterName());
     
        card.IsEditing = _i.ko.observable(false);

        card.IsEditing.subscribe(function (val) {
            if (val === false) {
                card.CharacterName(card.PrevCharacterName());
            }            
        });


        return card;
    };

    MapperCls.prototype.MapSkill = function (skill, data) {
        skill.IsLocked = _i.ko.computed(function () {
            return skill.IsLockedChoice() || data.ToDo().HasSelectedSkills();
        }),
        skill.Value = _i.ko.computed(function () {
            if (skill.IsSelected()) {
                return data[skill.AbilityScoreName()].Mod() + data.ProficiencyBonus().BonusValue;
            }
            return data[skill.AbilityScoreName()].Mod();
        });
        return skill;
    };

    MapperCls.prototype.MapLevelChecklist = function (checkList, data) {
        checkList.HpTaskComplete = _i.ko.pureComputed({
            read: function () {
                if (data.Level() === 1) {
                    return data.ToDo().FirstLevelTasks.HasIncreasedHp();
                }
                return checkList.HasIncreasedHp();
            },
            write: function (value) {

                checkList.HasIncreasedHp(value);
            }
        });
        checkList.AbilityScoreTaskComplete = _i.ko.computed(function () {
            if (data.Level() === 1) {
                return data.ToDo().HasCompletedAbilityScores();
            }
            return !checkList.HasAbilityScoreIncrease() || data.SelectedAbilityScoreIncreases() === 2;
        });
        checkList.SkillTaskComplete = _i.ko.computed(function () {
            return data.ToDo().HasSelectedSkills();
        });
        checkList.HasAbilityScoreIncreases = _i.ko.computed(function () {
            if (data.Class && data.Class.AbilityScoreIncreases) {
                var match = _i.ko.utils.arrayFilter(data.Class.AbilityScoreIncreaseses(), function (item) {
                    return item.LevelObtained === data.Level();
                });
            }
        });

        return checkList;
    };

    MapperCls.prototype.MapToDo = function (toDo, data) {
        toDo.HasCompletedAbilityScores = _i.ko.computed(function () {
            if (data.Level() === 1) {
                return toDo.FirstLevelTasks.HasRolledStrength && toDo.FirstLevelTasks.HasRolledDexterity &&
                                        toDo.FirstLevelTasks.HasRolledConstitution && toDo.FirstLevelTasks.HasRolledIntelligence
                                        && toDo.FirstLevelTasks.HasRolledWisdom && toDo.FirstLevelTasks.HasRolledCharisma;
            }
            return true;
        });

        return toDo;
    };

    return new MapperCls();
});