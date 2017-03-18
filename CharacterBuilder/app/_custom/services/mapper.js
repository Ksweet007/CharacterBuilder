define(function (require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    };


    function MapperCls() { }

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
        //Need to add write values for when this updates via database saves
        checkList.HpTaskComplete = _i.ko.computed(function () {
            if (data.Level() === 1) {
                return data.ToDo().FirstLevelTasks.HasIncreasedHp();
            }
            return checkList.HasIncreasedHp();
        });
        checkList.AbilityScoreTaskComplete = _i.ko.computed(function () {
            if (data.Level() === 1) {
                return data.ToDo().HasCompletedAbilityScores();
            }
            return !checkList.HasAbilityScoreIncreases() || data.SelectedAbilityScoreIncreases() === 2;
        });
        checkList.SkillTaskComplete = _i.ko.computed(function () {
            return data.ToDo().HasSelectedSkills();
        });
        checkList.HasAbilityScoreIncreases = _i.ko.computed(function () {
            if (data.Class) {
                var match = _i.ko.utils.arrayFilter(data.Class.AbilityScoreIncreaseses(), function (item) {
                    return item.LevelObtained === data.Level();
                });
            }
        });

        return checkList;
    };

    return new MapperCls();
});