define(function (require) {

    function BuilderGlobalsCls() { }

    BuilderGlobalsCls.prototype.getSheetId = function () {
        return window.builder.global_sheetid;
    };

    BuilderGlobalsCls.prototype.getUserName = function () {
        return window.builder.global_userName;
    };

    BuilderGlobalsCls.prototype.hasPickedClass = function () {
        return window.builder.global_userName;
    };

    BuilderGlobalsCls.prototype.selectClass = function () {
        window.builder.global_hasSelectedClass = true;
    };

    return new BuilderGlobalsCls();
});