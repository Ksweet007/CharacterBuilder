define(function (require) {
    var _i = {
        ko: require('knockout'),
        $: require('jquery'),
        event: require('_custom/services/event')
    };

    function dialogHandler(event) {
        //find all dialogs
        var $dialogs = _i.$('.modalHost');
        //find highest z-index dialog
        var dialog, maxz;
        $dialogs.each(function () {
            var z = parseInt(_i.$(this).css('z-index'), 10);
            if (!dialog || maxz < z) {
                dialog = this;
                maxz = z;
            }
        });

        if (dialog) {
            //get ko obj for child element
            var dialogObj = _i.ko.dataFor(_i.$(dialog).find('[data-view]').get(0));
            //call dialog method
            switch (event.keyCode) {
                case 27:
                    dialogObj.dialogEscKey(event);
                    break;
                case 13:
                    //skip Enter in TextArea elements
                    if (event.target.tagName.toLowerCase() === 'textarea') {
                        break;
                    }
                    dialogObj.dialogEnterKey(event);
                    break;
            }
        }
    }
    _i.event.subscribe('sheet:SpecialKeyPress', function (event) {
        if (event.keyCode === 27 || event.keyCode === 13) {
            dialogHandler(event);
        }
    });
});
