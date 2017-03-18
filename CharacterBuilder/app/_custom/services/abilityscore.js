define(function (require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    };

    function AbilityScoreCls(data) {
        var self = this;
        self.AbilityScores = data.AbilityScores;
        self.AbilityScoreIncreases = data.ScoreIncreases;
        self.ToDo = data.ToDo;

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
    }

    AbilityScoreCls.prototype.Strength = function (data) {
        var self = this;
        return {
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
        }
    };

    AbilityScoreCls.prototype.Dexterity = function (data) {
        var self = this;
        return {
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
        }
    };

    AbilityScoreCls.prototype.Constitution = function (data) {
        var self = this;
        return {
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
        }
    };

    AbilityScoreCls.prototype.Intelligence = function (data) {
        var self = this;
        return {
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
        }
    };

    AbilityScoreCls.prototype.Wisdom = function (data) {
        var self = this;
        return {
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
        }
    };

    AbilityScoreCls.prototype.Charisma = function (data) {
        var self = this;
        return {
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
        }
    };

    return AbilityScoreCls;
});