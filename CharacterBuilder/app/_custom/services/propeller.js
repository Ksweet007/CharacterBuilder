define(function (require) {
    var _i = {
        $: require('jquery')
    }

    function PropCls() {
            // ------- Propeller Textfield component js function ------- //
    // paper input
    _i.$(".pmd-textfield-focused").remove();
    _i.$(".pmd-textfield .form-control").after('<span class="pmd-textfield-focused"></span>');
    // floating label
    _i.$('.pmd-textfield input.form-control').each(function () {
        if (_i.$(this).val() != "") {
            _i.$(this).closest('.pmd-textfield').addClass("pmd-textfield-floating-label-completed");
        }
    });
    // floating change label
    _i.$(".pmd-textfield input.form-control").on('change', function (e) {
        if (_i.$(this).val() != "") {
            _i.$(this).closest('.pmd-textfield').addClass("pmd-textfield-floating-label-completed");
        }
    });
    // floating label animation
    _i.$("body").on("focus", ".pmd-textfield .form-control", function () {
        _i.$(this).closest('.pmd-textfield').addClass("pmd-textfield-floating-label-active pmd-textfield-floating-label-completed");
    });
    // remove floating label animation
    _i.$("body").on("focusout", ".pmd-textfield .form-control", function () {
        if (_i.$(this).val() === "") {
            _i.$(this).closest('.pmd-textfield').removeClass("pmd-textfield-floating-label-completed");
        }
        _i.$(this).closest('.pmd-textfield').removeClass("pmd-textfield-floating-label-active");
    });

    // ------- Propeller Checkbox component js function ------- //
    _i.$('.pmd-checkbox input').after('<span class="pmd-checkbox-label">&nbsp;</span>');
    // Ripple Effect //
    _i.$(".pmd-checkbox-ripple-effect").on('mousedown', function (e) {
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
            ink.css({ height: 20, width: 20 });
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
    })

    // ------- Propeller Radio component js function ------- //
    $('.pmd-radio input').after('<span class="pmd-radio-label">&nbsp;</span>');
    //-- Radio Ripple Effect --//
    $(".pmd-radio-ripple-effect").on('mousedown', function (e) {
        var rippler = $(this);
        $('.ink').remove();
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
            ink.css({ height: 15, width: 15 });
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
    })

    // ------- Propeller Dropdown component js function ------- //
    $('.pmd-dropdown .dropdown-menu').wrap("<div class='pmd-dropdown-menu-container'></div>");
    $('.pmd-dropdown .dropdown-menu').before('<div class="pmd-dropdown-menu-bg"></div>');
    var dropdown = $('.pmd-dropdown');
    var pmdsidebardropdown = function () {
        if ($(window).width() < 767) {
            // Add slidedown animation to dropdown
            dropdown.off('show.bs.dropdown');
            dropdown.on('show.bs.dropdown', function (e) {
                var that = $(this).find('.dropdown-menu');
                var w = that.outerWidth();
                var h = that.outerHeight();
                var dcdmc = that.closest('.pmd-dropdown-menu-container');
                var dcdmbg = dcdmc.find('.pmd-dropdown-menu-bg');
                $dataSidebar = $(this).find('.dropdown-toggle').attr("data-sidebar");
                var dropdowncenter = that.hasClass('pmd-dropdown-menu-center');
                if ($dataSidebar == 'true') {
                    that.first().stop(true, true).slideDown(300);
                    $(this).addClass('pmd-sidebar-dropdown');
                } else if (dropdowncenter) {
                    $('.dropdown-menu').removeAttr('style');
                    that.first().stop(true, true).slideDown(300);
                } else {
                    // that.first().stop(true, true).show();
                    dcdmc.css({ 'width': w + 'px', 'height': h + 'px' }, 0);
                    dcdmc.find('.pmd-dropdown-menu-bg').css({ 'width': w + 'px', 'height': h + 'px' });
                    that.css("clip", "rect(0 " + w + "px " + h + "px 0)");
                    if (that.hasClass('dropdown-menu-right')) {
                        dcdmbg.addClass('pmd-dropdown-menu-bg-right');
                        dcdmc.css({ "right": "0", "left": "auto" })
                    } else if (that.hasClass('pmd-dropdown-menu-top-left')) {
                        dcdmbg.addClass('pmd-dropdown-menu-bg-bottom-left');
                    } else if (that.hasClass('pmd-dropdown-menu-top-right')) {
                        dcdmbg.addClass('pmd-dropdown-menu-bg-bottom-right');
                        dcdmc.css({ "right": "0", "left": "auto" })
                    }
                }
            });
            // Add slideup animation to dropdown
            dropdown.off('hide.bs.dropdown');
            dropdown.on('hide.bs.dropdown', function (e) {
                $dataSidebar = $(this).find('.dropdown-toggle').attr("data-sidebar");
                var dropdowncenter = $(this).find('.dropdown-menu').hasClass('pmd-dropdown-menu-center');
                var that = $(this).find('.dropdown-menu');
                var w = that.outerWidth();
                var h = that.outerHeight();
                var dcdmc = that.closest('.pmd-dropdown-menu-container');
                var dcdmbg = dcdmc.find('.pmd-dropdown-menu-bg');
                if ($dataSidebar == 'true') {
                    that.first().stop(true, true).slideUp(300);
                } else if (dropdowncenter) {
                    $('.dropdown-menu').removeAttr('style');
                    that.first().stop(true, true).slideUp(300);
                } else {
                    that.css("clip", "rect(0 0 0 0)");
                    dcdmc.removeAttr('style');
                    dcdmbg.removeAttr('style');
                    if (that.hasClass('dropdown-menu-right')) {
                        that.css("clip", "rect(0 " + w + "px 0 " + w + "px)");
                    } else if (that.hasClass('pmd-dropdown-menu-top-right')) {
                        that.css("clip", "rect(0 " + w + "px 0 " + w + "px)");
                    }
                }
            });
        } else {
            // Add slidedown animation to dropdown
            $('.dropdown-menu').removeAttr('style');
            dropdown.off('show.bs.dropdown');
            dropdown.on('show.bs.dropdown', function (e) {
                //	$('.dropdown-menu').css({'clip':'rect(0 0 0 0)'});
                $dataSidebar = $(this).find('.dropdown-toggle').attr("data-sidebar");
                var hassidebar = $(this).closest('.pmd-sidebar').hasClass('pmd-sidebar');
                var dropdowncenter = $(this).find('.dropdown-menu').hasClass('pmd-dropdown-menu-center');
                var that = $(this).find('.dropdown-menu');
                var w = that.outerWidth();
                var h = that.outerHeight();
                var dcdmc = that.closest('.pmd-dropdown-menu-container');
                var dcdmbg = dcdmc.find('.pmd-dropdown-menu-bg');

                if (hassidebar) {
                    that.first().stop(true, true).slideDown();
                } else if (dropdowncenter) {
                    if (!$(this).parents().hasClass("pmd-sidebar")) {
                        $('.dropdown-menu').removeAttr('style');
                    }
                    that.first().stop(true, true).slideDown();
                } else {
                    // that.show();
                    if (!$(this).parents().hasClass("pmd-sidebar")) {
                        $('.dropdown-menu').removeAttr('style');
                        $('.pmd-sidebar .open .dropdown-menu').css('display', 'block');
                    }
                    dcdmc.css({ 'width': w + 'px', 'height': h + 'px' }, 200);
                    dcdmbg.css({ 'width': w + 'px', 'height': h + 'px' });
                    if (that.hasClass('dropdown-menu-right')) {
                        that.css("clip", "rect(0 " + w + "px " + h + "px 0)");
                        dcdmbg.addClass('pmd-dropdown-menu-bg-right');
                        dcdmc.css({ "right": "0", "left": "auto" })
                    } else if (that.hasClass('pmd-dropdown-menu-top-left')) {
                        that.css("clip", "rect(0 " + w + "px " + h + "px 0)");
                        dcdmbg.addClass('pmd-dropdown-menu-bg-bottom-left');
                    } else if (that.hasClass('pmd-dropdown-menu-top-right')) {
                        that.css("clip", "rect(0 " + w + "px " + h + "px 0)");
                        dcdmbg.addClass('pmd-dropdown-menu-bg-bottom-right');
                        dcdmc.css({ "right": "0", "left": "auto" })
                    } else {
                        that.css("clip", "rect(0 " + w + "px " + h + "px 0)");
                    }
                }
                this.closable = false;
            });
            dropdown.on('click', function (e) {
                // $('.dropdown-menu').removeAttr('style');
                var hassidebar = $(this).closest('.pmd-sidebar').hasClass('pmd-sidebar');
                var that = $(this).find('.dropdown-menu');
                if (hassidebar && !$(this).hasClass("open")) {
                    dropdown.removeClass("open");
                    $('.dropdown-menu').slideUp(300);
                    //	 $(this).addClass("open");
                } else if ($(this).parents("aside").hasClass("pmd-sidebar")) {
                    $('.dropdown-menu').slideUp(300);
                }

                this.closable = true;
            });
            // Add slideup animation to dropdown
            dropdown.off('hide.bs.dropdown');
            dropdown.on('hide.bs.dropdown', function (e) {
                if ($(this).parents("aside").hasClass("pmd-sidebar")) {
                    return this.closable;
                }
                else {
                    $dataSidebar = $(this).find('.dropdown-toggle').attr("data-sidebar");
                    var hassidebar = $(this).closest('.pmd-sidebar').hasClass('pmd-sidebar');
                    var dropdowncenter = $(this).find('.dropdown-menu').hasClass('pmd-dropdown-menu-center');
                    var that = $(this).find('.dropdown-menu');
                    var w = that.outerWidth();
                    var h = that.outerHeight();
                    var dcdmc = that.closest('.pmd-dropdown-menu-container');
                    var dcdmbg = dcdmc.find('.pmd-dropdown-menu-bg');
                    if (hassidebar) {
                        that.first().stop(true, true).slideUp(300);
                    }
                    else if (dropdowncenter) {
                        if (!$(this).parents().hasClass("pmd-sidebar")) {
                            $('.dropdown-menu').removeAttr('style');
                        }
                        that.first().stop(true, true).slideUp(300);
                    } else {
                        if (!$(this).parents().hasClass("pmd-sidebar")) {
                            $('.dropdown-menu').removeAttr('style');
                            $('.pmd-sidebar .open .dropdown-menu').css('display', 'block');
                        }
                        that.css("clip", "rect(0 0 0 0)");
                        dcdmc.removeAttr('style');
                        dcdmbg.removeAttr('style');
                        if (that.hasClass('dropdown-menu-right')) {
                            that.css("clip", "rect(0 " + w + "px 0 " + w + "px)");
                        } else if (that.hasClass('pmd-dropdown-menu-top-right')) {
                            that.css("clip", "rect(0 " + w + "px 0 " + w + "px)");
                        }
                    }
                }
            });
        }
    }
    pmdsidebardropdown();
    $(window).resize(function () {
        pmdsidebardropdown();
    });

    // ------- Propeller Ripple Effect component js function ------- //
    $(".pmd-ripple-effect").on('mousedown touchstart', function (e) {
        var rippler = $(this);
        $('.ink').remove();
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
    })

    // ------- Propeller Modal component js function ------- //
    function reposition() {
        var modal = $(this),
			dialog = modal.find('.modal-dialog');
        modal.css('display', 'block');
        dialog.css("margin-top", Math.max(0, ($(window).height() - dialog.height()) / 2));
        $(".modal .actions").css("margin-top", Math.max(0, ($(window).height() - dialog.height()) / 2));
    }
    $('.modal').on('show.bs.modal', reposition);
    $(window).on('resize', function () {
        $('.modal:visible').each(reposition);
    });

    // ------- Propeller Accordion component js function ------- //
    //custom function to add and remove active class
    $(function () {
        $(".collapse.in").parents(".panel").addClass("active");

        $('a[data-toggle="collapse"]').on('click', function () {

            var objectID = $(this).attr('href');
            var expandale = $(this).attr('data-expandable');
            if (expandale == 'true') {
                if ($(objectID).hasClass('in')) {
                    $(objectID).collapse('hide');
                }
                else {
                    $(objectID).collapse('show');
                }
            }
            $accoID = $(this).parents(".panel-group").attr("id");
            $availiblity = $(this).parents(".panel").children(".in").length;
            $expandable = $(this).attr("data-expandable");
            $expanded = $(this).attr("aria-expanded");
            $current = $(this).parent().parent().parent().parent().attr("id");
            if ($expandable == "false") {
                if ($expanded == "true") {
                    //alert("not exp closed")
                    $("#" + $current + " .active").removeClass("active");
                }
                else {
                    //alert("not exp open")
                    $("#" + $current + " .active").removeClass("active");
                    $(this).parents('.panel').addClass("active");
                }
            }
            if ($expandable == "true") {
                if ($expanded == "true") {
                    $(this).parents('.panel').addClass("active");
                }
                else {
                    $(this).parents('.panel').removeClass("active");
                }
            }
        });

        //  custom function for expand all and collapse all button
        $('#expandAll').on('click', function () {
            var GetID = $(this).attr("data-target");
            $('#' + GetID + ' ' + 'a[data-toggle="collapse"]').each(function () {
                var objectID = $(this).attr('href');
                if ($(objectID).hasClass('in') === false) {
                    $(objectID).collapse('show');
                    $(objectID).parent().addClass("active");
                }
            });
        });
        //
        $('#collapseAll').on('click', function () {
            var GetID = $(this).attr("data-target");
            $('#' + GetID + ' ' + 'a[data-toggle="collapse"]').each(function () {
                var objectID = $(this).attr('href');
                $(objectID).collapse('hide');
                $(objectID).parent().removeClass("active");
            });
        });

    });


    // ------- Propeller Alert component js function ------- //
    $(".pmd-alert-toggle").click(function () {
        $positionX = $(this).attr("data-positionX");
        $positionY = $(this).attr("data-positionY");
        $dataEffect = $(this).attr("data-effect");
        $dataRevert = $(this).attr("data-revert");
        $dataMessage = $(this).attr("data-message");
        $dataType = $(this).attr("data-type");
        $actionText = $(this).attr("data-action-text");
        $action = $(this).attr("data-action");

        if (!$(".pmd-alert-container." + $positionX + "." + $positionY).length) {
            $('body').append("<div class='pmd-alert-container " + $positionX + " " + $positionY + "'></div>");
        }

        $currentPath = $(".pmd-alert-container." + $positionX + "." + $positionY);

        $notification = notificationValue();

        function notificationValue() {
            if ($action == "true") {
                if ($actionText == null) {
                    $notification = "<div class='pmd-alert' data-action='true'>" + $dataMessage + "<a href='javascript:void(0)' class='pmd-alert-close'>×</a></div>";
                } else {
                    $notification = "<div class='pmd-alert' data-action='true'>" + $dataMessage + "<a href='javascript:void(0)' class='pmd-alert-close'>" + $actionText + "</a></div>";
                }
                return $notification;
            } else {
                if ($actionText == null) {
                    $notification = "<div class='pmd-alert' data-action='false'>" + $dataMessage + "</div>";
                } else {
                    $notification = "<div class='pmd-alert' data-action='false'>" + $dataMessage + "<a href='javascript:void(0)' class='pmd-alert-close'>" + $actionText + "</a></div>";
                }
                return $notification;
            }
        }

        var boxLength = $(".pmd-alert-container." + $positionX + "." + $positionY + " .pmd-alert").length;

        if (boxLength > 0) {
            if ($positionY == 'top') {
                $currentPath.append($notification);
            }
            else {
                $currentPath.prepend($notification);
            }
            $currentPath.width($(".pmd-alert").outerWidth());
            if ($action == "true") {
                $currentPath.children("[data-action='true']").addClass("visible" + " " + $dataEffect);
            } else {
                $currentPath.children("[data-action='false']").addClass("visible" + " " + $dataEffect).delay(3000).slideUp(
					function () {
					    $(this).removeClass("visible" + " " + $dataEffect).remove();
					});
            }
            $currentPath.children(".pmd-alert").eq(boxLength).addClass($dataType);
        } else {
            $currentPath.append($notification);
            $currentPath.width($(".pmd-alert").outerWidth());
            if ($action == "true") {
                $currentPath.children("[data-action='true']").addClass("visible" + " " + $dataEffect);
            } else {
                $currentPath.children("[data-action='false']").addClass("visible" + " " + $dataEffect).delay(3000).slideUp(
					function () {
					    $(this).removeClass("visible" + " " + $dataEffect).remove();
					});
            }
            $currentPath.children(".pmd-alert").eq(boxLength).addClass($dataType);
        }
        $middle = $(".pmd-alert").outerWidth() / 2;
        $(".pmd-alert-container.center").css("marginLeft", "-" + $middle + "px");
    });

    $(document).on("click", ".pmd-alert-close", function () {
        $(this).parents(".pmd-alert").slideUp(function () { $(this).removeClass("visible" + " " + $dataEffect).remove(); });
    });

    // ------- Propeller Popover component js function ------- //
    $('.popover-html[data-toggle="popover"]').popover({
        html: true,
        content: function () {
            var currentID = $(this).attr("data-id");
            var currentHTML = $(currentID).html();
            return currentHTML;
        },
        placement: function (pop, dom_el) {
            var range = 200;
            var curPlacement = $(dom_el).attr("data-placement");
            var scrolled = $(window).scrollTop();
            var winWidth = $(window).width();
            var winHeight = $(window).height();
            var elWidth = $(dom_el).outerWidth();
            var elHeight = $(dom_el).outerHeight();
            var elTop = $(dom_el).offset().top;
            var elLeft = $(dom_el).offset().left;
            var curPosTop = elTop - scrolled;
            var curPosLeft = elLeft;
            var curPosRight = winWidth - curPosLeft - elWidth;
            var curPosBottom = winHeight - curPosTop - elHeight;
            if (curPlacement == "left" && curPosLeft <= range) {
                return 'right';
            }
            else if (curPlacement == "right" && curPosRight <= range) {
                return 'left';
            }
            else if (curPlacement == "top" && curPosTop <= range) {
                return 'bottom';
            }
            if (curPlacement == "bottom" && curPosBottom <= range) {
                return 'top';
            } else {
                return curPlacement;
            }
        }
    });

    var options = {
        placement: function (pop, dom_el) {
            var range = 200;
            var curPlacement = $(dom_el).attr("data-placement");
            var scrolled = $(window).scrollTop();
            var winWidth = $(window).width();
            var winHeight = $(window).height();
            var elWidth = $(dom_el).outerWidth();
            var elHeight = $(dom_el).outerHeight();
            var elTop = $(dom_el).offset().top;
            var elLeft = $(dom_el).offset().left;
            var curPosTop = elTop - scrolled;
            var curPosLeft = elLeft;
            var curPosRight = winWidth - curPosLeft - elWidth;
            var curPosBottom = winHeight - curPosTop - elHeight;
            if (curPlacement == "left" && curPosLeft <= range) {
                return 'right';
            }
            else if (curPlacement == "right" && curPosRight <= range) {
                return 'left';
            }
            else if (curPlacement == "top" && curPosTop <= range) {
                return 'bottom';
            }
            if (curPlacement == "bottom" && curPosBottom <= range) {
                return 'top';
            } else {
                return curPlacement;
            }
        }
    };
    $('[data-toggle="popover"]').popover(options);

    $('[data-toggle="popover"]').on('shown.bs.popover', function () {
        var colorClass = $(this).attr("data-color");
        $(".popover").addClass(colorClass);
    }).on('hidden.bs.popover', function () {
        var colorClass = $(this).attr("data-color");
        $(".popover").removeClass(colorClass);

    });

    var overlay = $('.pmd-sidebar-overlay');
    var sidebar = $('.pmd-sidebar');
    var lsidebar = $('.pmd-sidebar-left');
    var rsidebar = $('.pmd-sidebar-right-fixed');
    var pmdnavbarsidebar = $('.pmd-navbar-sidebar');
    var sidebar = $('.pmd-sidebar');
    var sidebarHeader = $('#sidebar .sidebar-header');
    var sidebarImg = sidebarHeader.css('background-image');
    var toggleButtons = $('.pmd-sidebar-toggle');
    var pmdtopbartoggle = $('.topbar-fixed');

    // toggleButtons.css('display', 'none');

    // Left Sidebar
    $('.pmd-sidebar-toggle').on('click', function (e) {
        lsidebar.toggleClass('pmd-sidebar-open');
        if ((lsidebar.hasClass('pmd-sidebar-left-fixed') || lsidebar.hasClass('pmd-sidebar-right-fixed')) && lsidebar.hasClass('pmd-sidebar-open')) {
            overlay.addClass('pmd-sidebar-overlay-active');
            $('body').addClass("pmd-body-open")
        } else {
            overlay.removeClass('pmd-sidebar-overlay-active');
            $('body').removeClass("pmd-body-open")
        }
    });

    $(".pmd-sidebar .dropdown-menu, .pmd-navbar-sidebar .dropdown-menu").click(function (event) {
        event.stopPropagation();
    });

    // Right Sidebar
    $('.pmd-sidebar-toggle-right').on('click', function (e) {
        rsidebar.toggleClass('pmd-sidebar-open');
        if ((rsidebar.hasClass('pmd-sidebar-right')) && rsidebar.hasClass('pmd-sidebar-open')) {
            overlay.addClass('pmd-sidebar-overlay-active');
            $('body').addClass("pmd-body-open")
        } else {
            overlay.removeClass('pmd-sidebar-overlay-active');
            $('body').removeClass("pmd-body-open")
        }
    });

    // Right Sidebar
    $('.pmd-topbar-toggle').on('click', function (e) {
        pmdtopbartoggle.toggleClass('pmd-sidebar-open');
    });

    $('.topbar-close').on('click', function () {
        pmdtopbartoggle.removeClass('pmd-sidebar-open');
    });

    // Nave bar in Sidebar
    $('.pmd-navbar-toggle').on('click', function (e) {
        pmdnavbarsidebar.toggleClass('pmd-sidebar-open');
        if ((pmdnavbarsidebar.hasClass('pmd-navbar-sidebar')) && pmdnavbarsidebar.hasClass('pmd-sidebar-open')) {
            overlay.addClass('pmd-sidebar-overlay-active');
            $('body').addClass("pmd-body-open")
        } else {
            overlay.removeClass('pmd-sidebar-overlay-active');
            $('body').removeClass("pmd-body-open")
        }
    });

    // Overlay
    overlay.on('click', function (e) {
        $(this).removeClass('pmd-sidebar-overlay-active');
        $('.pmd-sidebar').removeClass('pmd-sidebar-open');
        $('.pmd-navbar-sidebar').removeClass('pmd-sidebar-open');
        $('body').removeClass("pmd-body-open")
        event.stopPropagation();
    });

    // Window load browser resize position
    if ($(window).width() < 1200) {
        sidebar.removeClass('pmd-sidebar-open pmd-sidebar-slide-push');
        lsidebar.addClass('pmd-sidebar-left-fixed');
        rsidebar.addClass('pmd-sidebar-right');
        toggleButtons.css('display', 'inherit');
        $('body').removeClass("pmd-body-open")
    }


    }



    return new PropCls();
});
