define(function (require) {

    function BuilderGlobalsCls() { }

    BuilderGlobalsCls.prototype.getSheetId = function () {
        return window.builder.global_sheetid;
    };

    BuilderGlobalsCls.prototype.getUserName = function () {
        return window.builder.global_userName;
    };

    return new BuilderGlobalsCls();
});