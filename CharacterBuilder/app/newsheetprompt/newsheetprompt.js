define(function (require) {
    var _i = {
        ko: require('knockout'),
        dialog: require('plugins/dialog')
    };
    var NewSheetPrompt = function () { };

    NewSheetPrompt.show = function () {
        return _i.dialog.show(new NewSheetPrompt());
    };

    NewSheetPrompt.prototype.dialogButtonClick = function (btnText, accepted) {
        /// <summary>Called when Ok and Cancel are clicked</summary>
        var self = this;
        var closeResult = { btnText: btnText, accepted: accepted, canceled: !accepted };

        _i.dialog.close(self, closeResult);

    };
    NewSheetPrompt.prototype.dialogEscKey = function (eventObj) {
        var self = this;
        self.dialogButtonClick('cancel', false, false);
    };
    NewSheetPrompt.prototype.dialogEnterKey = function (eventObj) {
        var self = this;
        self.dialogButtonClick('save', true, true);
    };

    return NewSheetPrompt;
});
