define(function (require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    }

    _i.ko.bindingHandlers.rippleeffect = {
        init: function (element, valueAccessor) {

            _i.$(".pmd-ripple-effect").on('mousedown touchstart', function (e) {
                var rippler = $(this);
                _i.$('.ink').remove();
                // create .ink element if it doesn't exist
                if (rippler.find(".ink").length == 0) {
                    rippler.append("<span class='ink'></span>");
                }
                var ink = rippler.find(".ink");
                // prevent quick double clicks
                ink.removeClass("animate");
                // set .ink diametr
                if (!ink.height() && !ink.width()) {
                    var d = Math.max(rippler.outerWidth(), rippler.outerHeight());
                    ink.css({ height: d, width: d });
                }
                // get click coordinates
                var x = e.pageX - rippler.offset().left - ink.width() / 2;
                var y = e.pageY - rippler.offset().top - ink.height() / 2;
                // set .ink position and add class .animate
                ink.css({
                    top: y + 'px',
                    left: x + 'px'
                }).addClass("animate");

                setTimeout(function () {
                    ink.remove();
                }, 1500);
            });

            _i.$('.pmd-checkbox input').after('<span class="pmd-checkbox-label">&nbsp;</span>');
            _i.$(".pmd-checkbox-ripple-effect").on('mousedown touchstart', function (e) {
                var rippler = $(this);
                _i.$('.ink').remove();
                // create .ink element if it doesn't exist
                if (rippler.find(".ink").length == 0) {
                    rippler.append("<span class='ink'></span>");
                }
                var ink = rippler.find(".ink");
                // prevent quick double clicks
                ink.removeClass("animate");
                // set .ink diametr
                if (!ink.height() && !ink.width()) {
                    var d = Math.max(rippler.outerWidth(), rippler.outerHeight());
                    ink.css({ height: d, width: d });
                }
                // get click coordinates
                var x = e.pageX - rippler.offset().left - ink.width() / 2;
                var y = e.pageY - rippler.offset().top - ink.height() / 2;
                // set .ink position and add class .animate
                ink.css({
                    top: y + 'px',
                    left: x + 'px'
                }).addClass("animate");

                setTimeout(function () {
                    ink.remove();
                }, 1500);
            });

            _i.$('.pmd-radio input').after('<span class="pmd-radio-label">&nbsp;</span>');
            _i.$(".pmd-radio-ripple-effect").on('mousedown', function(e) {
                var rippler = $(this);
                _i.$('.ink').remove();
                // create .ink element if it doesn't exist
                if (rippler.find(".ink").length == 0) {
                    rippler.append('<span class="ink"></span>');
                }
                var ink = rippler.find(".ink");
                // prevent quick double clicks
                ink.removeClass("animate");
                // set .ink diametr
                if (!ink.height() && !ink.width()) {
                    var d = Math.max(rippler.outerWidth(), rippler.outerHeight());
                    ink.css({ height: d, width: d });
                }
                // get click coordinates
                var x = e.pageX - rippler.offset().left - ink.width() / 2;
                var y = e.pageY - rippler.offset().top - ink.height() / 2;
                // set .ink position and add class .animate
                ink.css({
                    top: y + 'px',
                    left: x + 'px'
                }).addClass("animate");
                setTimeout(function() {
                    ink.remove();
                }, 1500);
            });


        }
    };

});
