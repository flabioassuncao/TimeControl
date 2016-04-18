angular.module("timeControl").factory("activityAPI", function ($http, config, $q, localStorageService, $location) {
	
    var _authentication = {
        isAuth: false,
        userName: "",
        useRefreshTokens: false
    };
    
    var _getActivity = function () {
		return $http.get(config.baseUrl + "/Activity");
	};
    
    var _getActivityUser = function (User) {
		return $http.get(config.baseUrl + "/Activity/GetAllUser/" + User);
	};
    
    var _getActivityId = function (Id) {
		return $http.get(config.baseUrl + "/Activity/" + Id);
	};
    
    var _updateActivity = function (Activity) {
		return $http.put(config.baseUrl + "/Activity", Activity);
	};
    
    var _updateTime = function(Time) {
        return $http.put(config.baseUrl + "/Activity/UpdateTime", Time);
    }
    
	var _saveActivity = function (Activity) {
		return $http.post(config.baseUrl + "/Activity", Activity).success(function (response) {
            localStorageService.set('idActivityData', { idActivity: response.ActivityId});
        }).error(function (err, status) {
           
        });
	};
    
    var _saveTime = function (Time) {
        return $http.post(config.baseUrl + "/Activity/SaveTime", Time).success(function (response) {
            localStorageService.set('idTimeData', { idTime: response.TimeId});
        }).error(function (err, status) {
           
        });
    }
    
    var _deleteActivity = function(Activity){
        return $http.delete(config.baseUrl + "/Activity/" + Activity);
    }
    
    var _deleteTime = function(TimeId){
        return $http.delete(config.baseUrl + "/Activity/DeleteTime/" + TimeId);
    }
    
    var _singUp = function(User){
        return $http.post("http://localhost:5000/Account/Register", User);
    }
    
    var _login = function(LoginData) {
            return $http.post("http://localhost:5000/Account/Login", LoginData).success(function (response) {
                localStorageService.set('authorizationData', { userName: LoginData.Email, authenticationUser: true });
                _authentication.isAuth = true;
                _authentication.userName = LoginData.Email;
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
            _authentication.useRefreshTokens = false;
        }
    };
    
    var _checkAuthentication = function () {
        var aut = localStorageService.get('authorizationData');
        if(aut == null){
            $location.path('/login');
        }
    }
    
    var _restoreIdActivity = function() {
        var idData = localStorageService.get('idActivityData');
        return idData.idActivity;
    }
    
    var _restoreIdTime = function(){
        var idTimeData = localStorageService.get('idTimeData');
        return idTimeData.idTime;
    }
    
    var _continueActivity = function(Activity)
    {
       localStorageService.set('continueActivity', { Link: Activity.Link, Observation: Activity.Observation});
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
        checkAuthentication: _checkAuthentication,
        updateActivity: _updateActivity,
        recuperarIdActivity: _restoreIdActivity,
        continuarActivity: _continueActivity,
        updateTime: _updateTime,
        saveTime: _saveTime,
        recuperarIdTime: _restoreIdTime,
        deleteTime: _deleteTime,
        getActivityUser: _getActivityUser,
        getActivityId: _getActivityId
	};
});