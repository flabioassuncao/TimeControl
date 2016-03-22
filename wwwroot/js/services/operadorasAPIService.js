angular.module("timeControl").service("operadorasAPI", function ($http, config) {
	this.getOperadoras = function () {
		return $http.get(config.baseUrl + "/Responsible");
	};
    
    this.postExecutor = function (exe) {
		return $http.post(config.baseUrl + "/Responsible", exe);
	};
    
});