define(function (require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    };

    function AbilityScoreCls(data) {
        var self = this;
        self.data = data;
    }

    AbilityScoreCls.prototype.Strength = function (data) {
        var self = this;
        var foo = "dfdf";
        return {
            Name: "Strength",
            ShortName: "STR",
            Score: _i.ko.computed(function () {
                return self.data.AbilityScores().Strength() + self.data.ScoreIncreases().Strength;
            }),
            Mod: _i.ko.computed(function () {
                return Math.floor((self.data.AbilityScores().Strength() - 10) / 2);
            }),
            CanRoll: _i.ko.computed(function () {
                return !self.data.ToDo().FirstLevelTasks.HasRolledStrength();
            })
        }
    };

    AbilityScoreCls.prototype.Dexterity = function (data) {
        var self = this;
        return {
            Name: "Dexterity",
            ShortName: "DEX",
            Score: _i.ko.computed(function () {
                return self.data.AbilityScores().Dexterity() + self.data.ScoreIncreases().Dexterity;
            }),
            Mod: _i.ko.computed(function () {
                return Math.floor((self.data.AbilityScores().Dexterity() - 10) / 2);
            }),
            CanRoll: _i.ko.computed(function () {
                return !self.data.ToDo().FirstLevelTasks.HasRolledDexterity();
            })
        }
    };

    AbilityScoreCls.prototype.Constitution = function (data) {
        var self = this;
        return {
            Name: "Constitution",
            ShortName: "CON",
            Score: _i.ko.computed(function () {
                return self.data.AbilityScores().Constitution() + self.data.ScoreIncreases().Constitution;
            }),
            Mod: _i.ko.computed(function () {
                return Math.floor((self.data.AbilityScores().Constitution() - 10) / 2);
            }),
            CanRoll: _i.ko.computed(function () {
                return !self.data.ToDo().FirstLevelTasks.HasRolledConstitution();
            })
        }
    };

    AbilityScoreCls.prototype.Intelligence = function (data) {
        var self = this;
        return {
            Name: "Intelligence",
            ShortName: "INT",
            Score: _i.ko.computed(function () {
                return self.data.AbilityScores().Intelligence() + self.data.ScoreIncreases().Intelligence;
            }),
            Mod: _i.ko.computed(function () {
                return Math.floor((self.data.AbilityScores().Intelligence() - 10) / 2);
            }),
            CanRoll: _i.ko.computed(function () {
                return !self.data.ToDo().FirstLevelTasks.HasRolledIntelligence();
            })
        }
    };

    AbilityScoreCls.prototype.Wisdom = function (data) {
        var self = this;
        return {
            Name: "Wisdom",
            ShortName: "WIS",
            Score: _i.ko.computed(function () {
                return self.data.AbilityScores().Wisdom() + self.data.ScoreIncreases().Wisdom;
            }),
            Mod: _i.ko.computed(function () {
                return Math.floor((self.data.AbilityScores().Wisdom() - 10) / 2);
            }),
            CanRoll: _i.ko.computed(function () {
                return !self.data.ToDo().FirstLevelTasks.HasRolledWisdom();
            })
        }
    };

    AbilityScoreCls.prototype.Charisma = function (data) {
        var self = this;
        return {
            Name: "Charisma",
            ShortName: "CHA",
            Score: _i.ko.computed(function () {
                return self.data.AbilityScores().Charisma() + self.data.ScoreIncreases().Charisma;
            }),
            Mod: _i.ko.computed(function () {
                return Math.floor((self.data.AbilityScores().Charisma() - 10) / 2);
            }),
            CanRoll: _i.ko.computed(function () {
                return !self.data.ToDo().FirstLevelTasks.HasRolledCharisma();
            })
        }
    };


    return new AbilityScoreCls();
});