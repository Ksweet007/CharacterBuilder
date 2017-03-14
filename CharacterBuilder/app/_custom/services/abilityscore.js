define(function (require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    };


    function AbilityScoreCls() {}

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

        return { Name: scoreName, ShortName: shortName, ScoreTotal: abilityScoreTotal, Modifier: abilityScoreModifier };
    };

    AbilityScoreCls.prototype.CalculateAbilityScoreModifier = function (sheet) {
        for (var propName in sheet.AbilityScores) {
            sheet[propName + "Mod"] = Math.floor((sheet.AbilityScores[propName] - 10) / 2);
        }
    };

    AbilityScoreCls.prototype.GetScore = function (sheet, scoreName) {
        return sheet.AbilityScores[scoreName]();
    };

    AbilityScoreCls.prototype.GetIncreasesForScore = function (sheet, scoreName) {
        var increaseTotal = 0;

        sheet.AbilityScoreIncreases().forEach(function (increase) {
            if (increase.Name() === scoreName) {
                increaseTotal += increase.IncreaseAmount();
            }
        });

        return increaseTotal;
    };

    AbilityScoreCls.prototype.GetScoreModifier = function (sheet, scoreName) {
        var baseScore = this.GetScore;
        var scoreIncreases = this.GetIncreasesForScore(sheet, scoreName);
        var scoreWithIncrease = baseScore + scoreIncreases;
        
        return Math.floor((scoreWithIncrease - 10) / 2);
    };



    return new AbilityScoreCls();
});