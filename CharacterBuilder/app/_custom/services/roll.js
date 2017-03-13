define(function(require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    };


    function RollCls() { }

    RollCls.prototype.RollAbilityScore = function() {

    };

    RollCls.prototype.RollHitPoints = function (hitDie, currentMaxHpNoModBonus) {
        var rolled = 1 + Math.floor(Math.random() * hitDie);

        return currentMaxHpNoModBonus + rolled;        
    };

    RollCls.prototype.DefaultHitPoints = function (defaultValue, currentMaxHpNoMod) {
        return currentMaxHpNoMod + defaultValue;
    };

    return new RollCls();
});