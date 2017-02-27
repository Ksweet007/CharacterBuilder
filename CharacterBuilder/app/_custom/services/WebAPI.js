define(function(require) {
	var _i = {
		$: require('jquery'),
		deferred: require('_custom/deferred'),
        events: require('_custom/services/event')
	};

	function ApiCls() {}
	ApiCls.prototype.ajax = function (config) {
	    config = _i.$.extend({
	        contentType: 'application/json',
	        dataType: 'json'
	    }, config);

	    _i.events.publish('sheet:AjaxStart', config);

		var dfd = _i.$.Deferred();
		var promise = _i.$.ajax(config);
		promise.done(function() {
		    dfd.resolve.apply(this, arguments);
		    _i.events.publish('sheet:AjaxComplete', config);
		}).fail(function (jqXHR, textStatus, errorThrown) {
		    try {
		        var failData = JSON.parse(jqXHR.responseText);
		        if (failData.errorCode) {
		            debug.log(failData.errorCode);
		        }
		    } catch (e) { }

		    _i.events.publish('sheet:AjaxError',config);
			dfd.reject.apply(this, arguments);
		});

		return dfd.promise();
	};

	ApiCls.prototype.get = function(url, data) {
		return this.ajax({
			type: 'GET',
			url: url,
			dataType: 'json',
			data: data
		});
	};

	ApiCls.prototype.getJSON = function(url, data) {
		return this.ajax({
			url: url,
			data: JSON.stringify(data),
			dataType: 'json'
		});
	};

	ApiCls.prototype.getlocal = function(url, data) {
		return _i.$.getJSON(url, data);
	};

	ApiCls.prototype.put = function(url, data) {
		return this.ajax({
            method: 'PUT',
            contentType: "application/json; charset=utf-8",
			url: url,
			data: JSON.stringify(data)
		});
	};

	ApiCls.prototype.post = function(url, data) {
		return this.ajax({
			url: url,
			dataType: 'application/json',
			data: JSON.stringify(data),
			method: 'POST',
			dataType: 'json'
		});
	};

	ApiCls.prototype.delete = function(url, data) {
		return this.ajax({
			url: url,
			data: JSON.stringify(data),
			method: 'DELETE',
			dataType: 'json'
		});
	};

	ApiCls.prototype.universal = function(url, data, method, async) {
		return this.ajax({
			url: url,
			data: JSON.stringify(data),
			method: method.toUpperCase(),
			async: async !== false
		});
	};

	return new ApiCls();
});
