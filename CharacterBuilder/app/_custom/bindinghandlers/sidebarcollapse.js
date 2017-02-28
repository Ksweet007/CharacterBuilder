define(function (require) {
    var _i = {
        $: require('jquery'),
        ko: require('knockout')
    };

    _i.ko.bindingHandlers.sidebarcollapse = {
        init: function (element, valueAccessor) {
            var value = valueAccessor();
            var $element = _i.$(element);
            var overlay = _i.$('.pmd-sidebar-overlay');
            var sidebar = _i.$('.pmd-sidebar');
            var lsidebar = _i.$('.pmd-sidebar-left');
            var rsidebar = _i.$('.pmd-sidebar-right-fixed');
            var pmdnavbarsidebar = _i.$('.pmd-navbar-sidebar');
            var sidebar = _i.$('.pmd-sidebar');
            var sidebarHeader = _i.$('#sidebar .sidebar-header');
            var sidebarImg = sidebarHeader.css('background-image');
            var toggleButtons = _i.$('.pmd-sidebar-toggle');
            var pmdtopbartoggle = _i.$('.topbar-fixed');

            _i.$('.pmd-sidebar-toggle').on('click', function (e) {
                lsidebar.toggleClass('pmd-sidebar-open');
                if ((lsidebar.hasClass('pmd-sidebar-left-fixed') || lsidebar.hasClass('pmd-sidebar-right-fixed')) && lsidebar.hasClass('pmd-sidebar-open')) {
                    overlay.addClass('pmd-sidebar-overlay-active');
                    _i.$('body').addClass("pmd-body-open");
                } else {
                    overlay.removeClass('pmd-sidebar-overlay-active');
                    _i.$('body').removeClass("pmd-body-open");
                }
            });

            _i.$(".pmd-sidebar .dropdown-menu, .pmd-navbar-sidebar .dropdown-menu").click(function (event) {
                event.stopPropagation();
            });

            // Right Sidebar
            _i.$('.pmd-sidebar-toggle-right').on('click', function (e) {
                rsidebar.toggleClass('pmd-sidebar-open');
                if ((rsidebar.hasClass('pmd-sidebar-right')) && rsidebar.hasClass('pmd-sidebar-open')) {
                    overlay.addClass('pmd-sidebar-overlay-active');
                    _i.$('body').addClass("pmd-body-open");
                } else {
                    overlay.removeClass('pmd-sidebar-overlay-active');
                    _i.$('body').removeClass("pmd-body-open");
                }
            });

            // Right Sidebar
            _i.$('.pmd-topbar-toggle').on('click', function (e) {
                pmdtopbartoggle.toggleClass('pmd-sidebar-open');
            });

            _i.$('.topbar-close').on('click', function () {
                pmdtopbartoggle.removeClass('pmd-sidebar-open');
            });

            // Nave bar in Sidebar
            _i.$('.pmd-navbar-toggle').on('click', function (e) {
                pmdnavbarsidebar.toggleClass('pmd-sidebar-open');
                if ((pmdnavbarsidebar.hasClass('pmd-navbar-sidebar')) && pmdnavbarsidebar.hasClass('pmd-sidebar-open')) {
                    overlay.addClass('pmd-sidebar-overlay-active');
                    _i.$('body').addClass("pmd-body-open");
                } else {
                    overlay.removeClass('pmd-sidebar-overlay-active');
                    _i.$('body').removeClass("pmd-body-open");
                }
            });

            // Overlay
            overlay.on('click', function (e) {
                _i.$(this).removeClass('pmd-sidebar-overlay-active');
                _i.$('.pmd-sidebar').removeClass('pmd-sidebar-open');
                _i.$('.pmd-navbar-sidebar').removeClass('pmd-sidebar-open');
                _i.$('body').removeClass("pmd-body-open");
                event.stopPropagation();
            });

            // Window load browser resize position
            if (_i.$(window).width() < 1200) {
                sidebar.removeClass('pmd-sidebar-open pmd-sidebar-slide-push');
                lsidebar.addClass('pmd-sidebar-left-fixed');
                rsidebar.addClass('pmd-sidebar-right');
                toggleButtons.css('display', 'inherit');
                _i.$('body').removeClass("pmd-body-open");
            }
            
            // window resize position
            _i.$(window).resize(function () {
                if (_i.$(window).width() < 1200) {
                    sidebar.removeClass('pmd-sidebar-open pmd-sidebar-slide-push');
                    lsidebar.addClass('pmd-sidebar-left-fixed');
                    rsidebar.addClass('pmd-sidebar-right');
                    toggleButtons.css('display', 'inherit');
                    overlay.removeClass('pmd-sidebar-overlay-active');
                    _i.$('body').removeClass("pmd-body-open");
                } else {
                    lsidebar.removeClass('pmd-sidebar-left-fixed').addClass('pmd-sidebar-open pmd-sidebar-slide-push');
                    rsidebar.removeClass('pmd-sidebar-right');
                    overlay.removeClass('pmd-sidebar-overlay-active');
                    _i.$('body').removeClass("pmd-body-open");
                }
            });

            (function (removeClass) {
                _i.$.fn.removeClass = function (value) {
                    if (value && typeof value.test === "function") {
                        for (var i = 0, l = this.length; i < l; i++) {
                            var elem = this[i];
                            if (elem.nodeType === 1 && elem.className) {
                                var classNames = elem.className.split(/\s+/);

                                for (var n = classNames.length; n--;) {
                                    if (value.test(classNames[n])) {
                                        classNames.splice(n, 1);
                                    }
                                }
                                elem.className = _i.$.trim(classNames.join(" "));
                            }
                        }
                    } else {
                        removeClass.call(this, value);
                    }
                    return this;
                }
            })(_i.$.fn.removeClass);




        } //End Init



    };
});
