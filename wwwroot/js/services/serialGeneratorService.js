angular.module("timeControl").provider("serialGenerator", function (config) {
	
	var _length = 10;
	
	this.getLength = function () {
		return _length
	};

	this.setLength = function (length) {
		_length = length;
	};

	this.$get = function () {
		return {
			generate: function () {
				function s4() {
			    return Math.floor((1 + Math.random()) * 0x10000)
			      .toString(16)
			      .substring(1);
			 	}
			  return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
			    s4() + '-' + s4() + s4() + s4();
			}

		};
	};
});