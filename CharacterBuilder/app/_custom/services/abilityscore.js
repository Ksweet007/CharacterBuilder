define(function(require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    };


    function AbilityScoreCls() { }

    AbilityScoreCls.prototype.CombineScoresWithIncrease = function (sheet, scoreName) {
        /*========== Setup Initial Score value and Short Name ==========*/
        var shortName = scoreName.substr(0, 3);
        var abilityScoreTotal = sheet.AbilityScores[scoreName];

        /*========== Find increases that match current Ability Score and apply them ==========*/
        sheet.AbilityScoreIncreases().forEach(function (increase) {
            if (increase.Name() === scoreName) {
                var scoreWithIncrease = abilityScoreTotal() + increase.IncreaseAmount();
                abilityScoreTotal(scoreWithIncrease);
            }
        });

        /*========== Calculate Modifier ==========*/
        var abilityScoreModifier = Math.floor((abilityScoreTotal() - 10) / 2);


        return {Name: scoreName, ShortName: shortName, ScoreTotal: abilityScoreTotal, Modifier: abilityScoreModifier};
    };
    
    return new AbilityScoreCls();
});