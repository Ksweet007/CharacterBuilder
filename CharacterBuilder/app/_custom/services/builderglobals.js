define("services/builderglobals", function (require) {

    function BuilderGlobalsCls() { }

    BuilderGlobalsCls.prototype.getSheetId = function () {
        return window.builder.global_sheetid;
    };

    return new BuilderGlobalsCls();
});