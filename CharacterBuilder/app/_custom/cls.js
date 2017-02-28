define(function (require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    };

    function ClsUtilities() { }
    ClsUtilities.prototype.extend = function (cls, basecls) {
        for (var p in basecls) {
            if (basecls.hasOwnProperty(p)) {
                cls[p] = basecls[p];
            }
        }
        function TempCls() {
            this.constructor = cls;
        }
        TempCls.prototype = basecls.prototype;
        cls.prototype = new TempCls();
    };
    ClsUtilities.prototype.fixThisValueForCls = function (clsInst) {
        var constructor = clsInst.constructor;
        if (constructor && constructor.prototype) {
            Object.keys(constructor.prototype).forEach(function (prop) {
                if (prop === 'constructor') {
                    return;
                }
                var clsProp = clsInst[prop];
                if (_i.ko.isObservable(clsProp)) {
                    return;
                }
                if (_i.$.isFunction(clsProp) && clsProp._alreadyBound !== true) {
                    clsInst[prop] = clsProp.bind(clsInst);
                    clsInst[prop]._alreadyBound = true;
                }
            });
        }
    };
    return new ClsUtilities();
});
