define(function (require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    };


    function ChecklistCls() { }

    ChecklistCls.prototype.MarkTaskComplete = function (sheet, taskName) {
        sheet.LevelChecklist[taskName](true);
    };
    
    ChecklistCls.prototype.IsHpTaskComplete = function (sheet) {
        if (sheet.Level() === 1) {
            return sheet.ToDo.FirstLevelTasks.HasIncreasedHp();
        }
        return sheet.LevelChecklist.HasIncreasedHp();
    };

    ChecklistCls.prototype.IsAbilityScoreTaskComplete = function (sheet, abilityScoreName) {        
        if (sheet.Level() === 1) {
            return sheet.ToDo.HasCompletedAbilityScores();
        }

        if (sheet.LevelChecklist.HasAbilityScoreIncrease()) {
            return sheet.SelectedAbilityScoreIncreases() === 2;
        }

        return true;
    };

    ChecklistCls.prototype.IsLevelComplete = function (sheet) {
        if (sheet.Level() === 1) {
            return sheet.ToDo.HasCompletedAbilityScores() && sheet.ToDo.HasSelectedClass()
                && sheet.ToDo.HasSelectedRace() && sheet.ToDo.HasSelectedSubRace() && sheet.ToDo.HasSelectedSkills()
                && sheet.ToDo.HasSelectedBackground();
        }

        if (sheet.LevelChecklist.HasAbilityScoreIncrease()) {
            return sheet.LevelChecklist.HasIncreasedAbilityScores() && sheet.LevelChecklist.HasIncreasedHp();
        }

        return sheet.LevelChecklist.HasIncreasedHp();
    };

    return new ChecklistCls();
});