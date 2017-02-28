define(function (require) {
    var _i = {
        dialog: require('plugins/dialog'),
        ko: require('knockout'),
        $: require('jquery'),
        localizer: require('_custom/services/localizer')
    };

    function BlockUiCls() {
        var self = this;
        self.$blockout = null;
        self.$blockoutDlg = null;
    }
    BlockUiCls.prototype.start = function (promise, contentKey, showImmediately) {
        var self = this;
        var showDelay = 750;
        var $body = _i.$('body');
        self.$blockout = _i.$('<div class="uilock pagesaving text-center"><h1>Loading...</h1><span class="btn-loader loader"></span></div>')
            .css({ 'z-index': _i.dialog.getNextZIndex() });
        self.$blockout.on('click', function () {
            self.$blockout.addClass('modal-backdrop');
        });
        function showBlockout() {
            self.$blockout.addClass('active');
        }
        if (showImmediately) {
            showBlockout();
        }
        var visibleTimeout = setTimeout(showBlockout, showDelay);
        promise.always(function () {
            clearTimeout(visibleTimeout);
            _i.ko.removeNode(self.$blockout.get(0));
        });
        self.$blockout.prependTo($body);
    };
    return BlockUiCls;
});