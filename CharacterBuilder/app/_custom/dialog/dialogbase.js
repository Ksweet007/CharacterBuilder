define(function (require) {
    var _i = {
        ko: require('knockout'),
        dialogPlugin: require('plugins/dialog')
    };

    var DialogBase = function () {
        var self = this;

        self.dlgData = _i.ko.observable();
    };
    DialogBase.prototype.activate = function (activationData) {
        var self = this;

        if (activationData.autoCloseDeferred) {
            activationData.autoCloseDeferred.done(function () {
                _i.dialogPlugin.close(self, { btnText: 'autoClose', accepted: true, canceled: false, autoClose: true, data: self.dlgData() });
            }).fail(function () {
                _i.dialogPlugin.close(self, { btnText: 'autoClose', accepted: false, canceled: true, autoClose: true, data: self.dlgData() });
            });
        }
    };
    DialogBase.prototype.dialogButtonClick = function (btnText, accepted) {
        /// <summary>Called when Ok and Cancel are clicked</summary>
        var self = this;
        var closeResult = { btnText: btnText, accepted: accepted, canceled: !accepted, data: self.dlgData() };
        
        _i.dialogPlugin.close(self, closeResult);
        
    };
    DialogBase.prototype.dialogEscKey = function (eventObj) {
        var self = this;
        self.dialogButtonClick(self.cancelKey(), false, false);
    };
    DialogBase.prototype.dialogEnterKey = function (eventObj) {
        var self = this;
        self.dialogButtonClick(self.okKey(), true, true);
    };
    return DialogBase;
});