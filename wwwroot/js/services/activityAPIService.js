angular.module("timeControl").factory("activityAPI", function ($http, config) {
	var _getActivity = function () {
		return $http.get(config.baseUrl + "/Activity");
	};

	var _saveActivity = function (activity) {
		return $http.post(config.baseUrl + "/Activity", activity);
	};
    
    var _deleteActivity = function(activity){
        return $http.delete(config.baseUrl + "/Activity/" + activity);
    }

	return {
		getActivity: _getActivity,
		saveActivity: _saveActivity,
        deleteActivity: _deleteActivity
	};
});