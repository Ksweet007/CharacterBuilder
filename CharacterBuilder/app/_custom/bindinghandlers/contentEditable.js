define(function(require){
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    }

    _i.ko.bindingHandlers.contentEditable = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var value = _i.ko.unwrap(valueAccessor()),
            htmlEditable = allBindingsAccessor().htmlEditable;

            element.innerHTML = htmlEditable();
            _i.$(element).on("input", function () {
                if (this.isContentEditable && _i.ko.isWriteableObservable(htmlEditable)) {
                    htmlEditable(this.innerHTML);
                }
            });
        },
        update: function (element, valueAccessor) {
            var value = _i.ko.unwrap(valueAccessor());

            element.contentEditable = value;

            if (element.isContentEditable) {
                _i.$(element).trigger("input");
            }
        }
    };


});
