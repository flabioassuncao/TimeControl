angular.module("timeControl").factory("activityAPI", function ($http, config, $q, localStorageService, $location) {
	
    var _authentication = {
        isAuth: false,
        userName: "",
        useRefreshTokens: false
    };
    
    var _getActivity = function () {
		return $http.get(config.baseUrl + "/Activity");
	};

	var _saveActivity = function (activity) {
		return $http.post(config.baseUrl + "/Activity", activity);
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

	return {
		getActivity: _getActivity,
		saveActivity: _saveActivity,
        deleteActivity: _deleteActivity,
        signUp: _singUp,
        login: _login,
        authentication: _authentication,
        logOut: _logOut,
        fillAuthData: _fillAuthData,
        verificar: _verificar
	};
});