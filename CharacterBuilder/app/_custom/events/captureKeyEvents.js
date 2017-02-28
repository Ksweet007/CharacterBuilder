define(function (require) {
    var _i = {
        $: require('jquery'),
        event: require('_custom/services/event')
    };

    _i.$(document).on('keydown.CaptureKeyEvents', function (event) {
        if (event.keyCode === 27 || event.keyCode === 13) {
            _i.event.publish('sheet:SpecialKeyPress', event);
        }
    });
});