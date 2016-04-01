angular.module("timeControl").factory("activityAPI", function ($http, config, $q, localStorageService, $location) {
	
    var _authentication = {
        isAuth: false,
        userName: "",
        useRefreshTokens: false
    };
    
    var _getActivity = function () {
		return $http.get(config.baseUrl + "/Activity");
	};
    
    var _updateActivity = function (activity) {
        // console.log(activity);
		return $http.put(config.baseUrl + "/Activity", activity);
	};
    
	var _saveActivity = function (activity) {
		return $http.post(config.baseUrl + "/Activity", activity).success(function (response) {
                localStorageService.set('idActivityData', { idActivity: activity.activityId});
        }).error(function (err, status) {
           
        });
	};
    
    var _deleteActivity = function(activity){
        return $http.delete(config.baseUrl + "/Activity/" + activity);
    }
    
    var _singUp = function(user){
        return $http.post("http://localhost:5000/Account/Register", user);
    }
    
    var _login = function(loginData) {

            return $http.post("http://localhost:5000/Account/Login", loginData).success(function (response) {
               
                localStorageService.set('authorizationData', { userName: loginData.Email});
            
                _authentication.isAuth = true;
                _authentication.userName = loginData.Email;
                _authentication.useRefreshTokens = false;
                
        }).error(function (err, status) {
            _logOut();
        });

    };
    
    var _salvarId = function(resul){
        // console.log(resul.activityId);
        localStorageService.set('idActivityData', { idActivity: resul.activityId});
    }
    
    var _logOut = function () {

        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";
        _authentication.useRefreshTokens = false;
        $location.path('/login');

    };
    
    var _fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
            _authentication.useRefreshTokens = authData.useRefreshTokens;
        }

    };
    
    var _verificar = function () {
        if(!_authentication.isAuth)
            $location.path('/login');
    }
    
    var _recuperarIdActivity = function() {
        
        var idData = localStorageService.get('idActivityData');
        // console.log(idData.idActivity);
        return idData.idActivity;
    }
    
    var _continuarActivity = function(activity)
    {
       localStorageService.set('continueActivity', { Link: activity.Link, Observation: activity.Observation});
    }
    
	return {
		getActivity: _getActivity,
		saveActivity: _saveActivity,
        deleteActivity: _deleteActivity,
        signUp: _singUp,
        login: _login,
        authentication: _authentication,
        logOut: _logOut,
        fillAuthData: _fillAuthData,
        verificar: _verificar,
        updateActivity: _updateActivity,
        recuperarIdActivity: _recuperarIdActivity,
        continuarActivity: _continuarActivity,
        salvarId: _salvarId
	};
});