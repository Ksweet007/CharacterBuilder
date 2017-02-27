define(function (require) {
    var _i = {
        app: require('durandal/app'),
        deferred: require('_custom/deferred'),
        $: require('jquery')
    };

    function ShowCls() { }
    ShowCls.prototype.showDialog = function (dialogModule, dialogData, returnFullPromise, context) {
        context = context || 'default';
        var promise = _i.app.showDialog(dialogModule, dialogData, context);
        if (returnFullPromise === true) {
            return promise;
        }
        var dfd = _i.deferred.create();
        promise.then(function (data) {
            if (data && data.accepted) {
                dfd.resolve(data);
                return;
            }
            if (!data || data.canceled) {
                dfd.reject({ failureType: 'canceled', data: data });
            }
        }, function (data) {
            dfd.reject({ failureType: 'error', data: data });
        });
        return dfd.promise();
    };

    return new ShowCls();
});
