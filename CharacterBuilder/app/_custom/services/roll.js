define(function (require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout'),
        abilityscore: require('_custom/services/abilityscore'),
        alert: require('_custom/services/alert')
    };


    function RollCls() { }

    RollCls.prototype.RollAbilityScore = function (abilityScores, score) {
        var abilityScoreObj = abilityScores[score.Name];
        
        var rolledValue = this.GetRolledValue();
        abilityScoreObj = rolledValue;
    };

    RollCls.prototype.GetRolledValue = function () {
        var rolls = [];

        for (var i = 0; i < 4; i++) {
            var d6 = 1 + Math.floor(Math.random() * 6);
            rolls.push(d6);
        }

        rolls.sort();
        _i.alert.showAlert({ type: "success", message: "Rolls: " + rolls.join(', ') });
        rolls.shift();

        var scoreTotal = 0;
        for (var s in rolls) {
            scoreTotal += rolls[s];
        }

        return scoreTotal < 8 ? 8 : scoreTotal;
    };

    RollCls.prototype.DefaultHitPoints = function (defaultValue, currentMaxHpNoMod) {
        return currentMaxHpNoMod + defaultValue;
    };

    RollCls.prototype.RollHitPoints = function (hitDie, currentMaxHpNoModBonus) {
        var rolled = 1 + Math.floor(Math.random() * hitDie);

        return currentMaxHpNoModBonus + rolled;
    };

    return new RollCls();
});