define(function (require) {
    var _i = {
        $: require('jquery')
    };

    function DeferredWrapper() { }
    DeferredWrapper.prototype.create = function () {
        /// <summary>
        /// Wrapper for $.Deferred(). No args can be passed to this function.
        /// Provided to permit dependencies on JQuery Deferred without adding a dependency on all of JQuery.
        /// </summary>
        return _i.$.Deferred();
    };
    DeferredWrapper.prototype.createResolved = function (resolveValue) {
        /// <summary>
        /// Wrapper for $.Deferred(). No args can be passed to this function.
        /// Provided to permit dependencies on JQuery Deferred without adding a dependency on all of JQuery.
        /// </summary>
        var dfd = _i.$.Deferred();
        dfd.resolve(resolveValue);
        return dfd;
    };
    DeferredWrapper.prototype.createRejected = function (rejectedValue) {
        /// <summary>
        /// Wrapper for $.Deferred(). No args can be passed to this function.
        /// Provided to permit dependencies on JQuery Deferred without adding a dependency on all of JQuery.
        /// </summary>
        var dfd = _i.$.Deferred();
        dfd.reject(rejectedValue);
        return dfd;
    };
    DeferredWrapper.prototype.when = function () {
        /// <summary>
        /// Wrapper for $.when. Takes same arguments as $.when
        /// Provided to permit dependencies on JQuery Deferred without adding a dependency on all of JQuery.
        /// </summary>
        return _i.$.when.apply(_i.$, arguments);
    };
    DeferredWrapper.prototype.waitForAll = function () {
        var listOfDeferreds = Array.prototype.slice.call(arguments, 0);
        var dfd = _i.$.Deferred();
        if (listOfDeferreds && listOfDeferreds.length === 0) {
            dfd.resolve({});
        }
        var totalDfds = listOfDeferreds.length;
        var resolvedDfds = 0;
        var rejectedDfds = 0;
        var resolveOrRejectIfDone = function () {
            if ((resolvedDfds + rejectedDfds) >= totalDfds) {
                if (rejectedDfds > 0) {
                    dfd.reject({});
                } else {
                    dfd.resolve({});
                }
            }
        };
        listOfDeferreds.forEach(function (itemDfd) {
            if (!itemDfd || !itemDfd.done || !itemDfd.fail) {
                resolvedDfds++;
                resolveOrRejectIfDone();
                return;
            }
            itemDfd.done(function () {
                resolvedDfds++;
                resolveOrRejectIfDone();
            });
            itemDfd.fail(function () {
                rejectedDfds++;
                resolveOrRejectIfDone();
            });
        });
        return dfd.promise();
    };
    return new DeferredWrapper();
});
