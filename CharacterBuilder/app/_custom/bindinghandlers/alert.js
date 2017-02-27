define(function(require) {
	var _i = {
		$: require('jquery'),
		ko: require('knockout')
	}

	_i.ko.bindingHandlers.alert = {
		init: function(element, valueAccessor, allBindingsAccessor) {
			var $element = _i.$(element);
			$element.click(function() {
			    var $positionX = _i.$(this).attr("data-positionX");
			    var $positionY = _i.$(this).attr("data-positionY");
			    var $dataEffect = _i.$(this).attr("data-effect");
			    var $dataRevert = _i.$(this).attr("data-revert");
			    var $dataMessage = _i.$(this).attr("data-message");
			    var $dataType = _i.$(this).attr("data-type");
			    var $actionText = _i.$(this).attr("data-action-text");
			    var $action = _i.$(this).attr("data-action");

				if (!_i.$(".pmd-alert-container." + $positionX + "." + $positionY).length) {
					_i.$('body').append("<div class='pmd-alert-container " + $positionX + " " + $positionY + "'></div>");
				}

				var $currentPath = _i.$(".pmd-alert-container." + $positionX + "." + $positionY);

				var $notification = notificationValue();

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

				var boxLength = _i.$(".pmd-alert-container." + $positionX + "." + $positionY + " .pmd-alert").length;

				if (boxLength > 0) {
					if ($positionY == 'top') {
						$currentPath.append($notification);
					} else {
						$currentPath.prepend($notification);
					}
					$currentPath.width(_i.$(".pmd-alert").outerWidth());
					if ($action == "true") {
						_i.$currentPath.children("[data-action='true']").addClass("visible" +
							" " + $dataEffect);
					} else {
						$currentPath.children("[data-action='false']").addClass("visible" +
							" " + $dataEffect).delay(3000).slideUp(function() {
							_i.$(this).removeClass("visible" +
								" " + $dataEffect).remove();
						});
					}
					$currentPath.children(".pmd-alert").eq(boxLength).addClass($dataType);
				} else {
					$currentPath.append($notification);
					$currentPath.width(_i.$(".pmd-alert").outerWidth());
					if ($action == "true") {
						$currentPath.children("[data-action='true']").addClass("visible" +
							" " + $dataEffect);
					} else {
						$currentPath.children("[data-action='false']").addClass("visible" +
							" " + $dataEffect).delay(3000).slideUp(function() {
							_i.$(this).removeClass("visible" +
								" " + $dataEffect).remove();
						});
					}
					$currentPath.children(".pmd-alert").eq(boxLength).addClass($dataType);
				}
				$middle = _i.$(".pmd-alert").outerWidth() / 2;
				_i.$(".pmd-alert-container.center").css("marginLeft", "-" + $middle + "px");
			});
		}
	};

});
