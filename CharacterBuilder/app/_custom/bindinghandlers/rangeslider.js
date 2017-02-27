define(function(require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    }

    _i.ko.bindingHandlers.rangeslider = {
        init: function(element, valueAccessor,allBindings, viewModel, bindingContext) {
            var settings = _i.$.extend({
                valueInput  : _i.ko.observable(''),
                type: 'input'
            }, _i.ko.unwrap(valueAccessor()))
            var sliderElement = _i.$(element)[0];

            noUiSlider.create(sliderElement,
                {
                    start: settings.valueInput(),
                    connect: 'lower',
                    tooltips: [wNumb({ decimals: 0 }) ],
                    format: wNumb({ decimals: 0}),
                    range: {'min': 1, 'max': 20 },
                    step: 1,
                    pips: {mode: 'steps', density: 1}
                }
            );

            // When the slider value changes, update the input and span
        	sliderElement.noUiSlider.on('update', function( values, handle ) {
                var newInput = values[handle];
                settings.valueInput(newInput * 1);
        	});

        }//END INIT
    };

});
