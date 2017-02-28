define(function(require){
    var _i = {
		$: require('jquery')
	};

    function UtilsCls() {};

    UtilsCls.prototype.escapeJSON = function(str){
      return str.replace(/\n/g, "\\\\n").replace(/\r/g, "\\\\r").replace(/\t/g, "\\\\t");
    };

    return new UtilsCls();

});
