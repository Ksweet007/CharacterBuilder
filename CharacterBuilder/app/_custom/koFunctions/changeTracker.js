define(function(require) {
	var _i = {
		ko: require('knockout'),
		$: require('jquery')
	};

	_i.ko.dirtyFlag = function(root, isInitiallyDirty) {
    var result = function() {},
		_initialState = _i.ko.observable(_i.ko.toJSON(root)),
		_isInitiallyDirty = _i.ko.observable(isInitiallyDirty);

		result.isDirty = _i.ko.computed(function() {
			return _isInitiallyDirty() || _initialState() !== _i.ko.toJSON(root);
		});

		result.reset = function() {
			_initialState(_i.ko.toJSON(root));
			_isInitiallyDirty(false);
		};

		return result;
	};


});
