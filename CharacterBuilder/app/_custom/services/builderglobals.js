define(function (require) {

    function BuilderGlobalsCls() { }

    BuilderGlobalsCls.prototype.getSheetId = function () {
        return window.builder.global_sheetid;
    };

    BuilderGlobalsCls.prototype.getUserName = function () {
        return window.builder.global_userName;
    };

    BuilderGlobalsCls.prototype.setSheetToEdit = function (sheetId) {
        return window.builder.global_sheetid = sheetId;
    };

    BuilderGlobalsCls.prototype.hasSelectedClass = function () {
        return window.builder.global_hasSelectedClass;
    };

    BuilderGlobalsCls.prototype.selectClass = function () {
        window.builder.global_hasSelectedClass = true;
    };

    BuilderGlobalsCls.prototype.hasSelectedBackground = function () {
        return window.builder.global_hasSelectedBackground;
    };

    BuilderGlobalsCls.prototype.selectBackground = function () {
        window.builder.global_hasSelectedBackground = true;
    };

    BuilderGlobalsCls.prototype.hasSelectedRace = function () {
        return window.builder.global_hasSelectedRace;
    };

    BuilderGlobalsCls.prototype.selectRace = function () {
        window.builder.global_hasSelectedRace = true;
    };

    return new BuilderGlobalsCls();
});