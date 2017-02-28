define(function (require) {
    var _i = {
        app: require('durandal/app')
    };

    function EventCls() { }
    EventCls.prototype.subscribe = function (eventName, callback) {
        return _i.app.on.apply(_i.app, arguments);
    };
    EventCls.prototype.unsubscribe = function (eventName, callback) {
        return _i.app.off.apply(_i.app, arguments);
    };
    EventCls.prototype.publish = function (eventName) {
        return _i.app.trigger.apply(_i.app, arguments);
    };
    return new EventCls();
});