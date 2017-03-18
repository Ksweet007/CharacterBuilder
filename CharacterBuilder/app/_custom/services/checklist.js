define(function (require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    };


    function ChecklistCls() { }

    ChecklistCls.prototype.CurrentLevelIsComplete = function (levelChecklist, toDo) {
        if (levelChecklist.Level() === 0 ) {
            return toDo.HasCompletedAbilityScores();
        }

        if (levelChecklist.HasAbilityScoreIncrease()) {            
            return self.SelectedAbilityScoreIncreases() === 2;
        }

        return true;
    };

    ChecklistCls.prototype.MarkTaskComplete = function (sheet, taskName) {
        sheet.LevelChecklist[taskName](true);
    };

    ChecklistCls.prototype.IsHpTaskComplete = function (sheet) {
        if (sheet.Level() === 1) {
            return sheet.ToDo.FirstLevelTasks.HasIncreasedHp();
        }
        return sheet.LevelChecklist.HasIncreasedHp();
    };

    ChecklistCls.prototype.IsAbilityScoreTaskComplete = function (sheet) {
        if (sheet.Level() === 1) {
            return sheet.ToDo.HasCompletedAbilityScores();
        }

        if (sheet.LevelChecklist.HasAbilityScoreIncrease()) {
            return sheet.SelectedAbilityScoreIncreases() === 2;
        }

        return true;
    };

    ChecklistCls.prototype.LevelOneComplete = function (toDo) {
        return toDo.HasCompletedAbilityScores() && toDo.HasSelectedClass()
            && toDo.HasSelectedRace() && toDo.HasSelectedSubRace() && toDo.HasSelectedSkills()
            && toDo.HasSelectedBackground();
    };



    return new ChecklistCls();
});