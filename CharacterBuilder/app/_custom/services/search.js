define(function(require) {
	var _i = {
		ko: require('knockout'),
		$: require('jquery')
	};

	function SearchCls() {}

	SearchCls.prototype.searchClassname = function(listToSearch, searchTerm) {
		var self = this;

		var options = {
		  shouldSort: true,
		  tokenize: true,
		  matchAllTokens: true,
		  threshold: 0.2,
		  location: 0,
		  distance: 10,
		  maxPatternLength: 32,
		  minMatchCharLength: 1,
		  keys: [
		    "name",
			"powersource"
		]
		};

		var fuse = new Fuse(listToSearch, options);
		var result = fuse.search(searchTerm);

		return result;
	};

	return new SearchCls();
});
