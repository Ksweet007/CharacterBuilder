define(function(require){
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    }

    _i.ko.bindingHandlers.htmlEditable = {
        update: function (element, valueAccessor) {
            var value = _i.ko.unwrap(valueAccessor());

            if (!element.isContentEditable) {
                element.innerHTML = value;
            }
        }
    };

});
