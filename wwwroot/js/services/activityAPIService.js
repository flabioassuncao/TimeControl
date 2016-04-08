angular.module("timeControl").factory("activityAPI", function ($http, config, $q, localStorageService, $location) {
	
    var _authentication = {
        isAuth: false,
        userName: "",
        useRefreshTokens: false
    };
    
    var _getAtividade = function () {
		return $http.get(config.baseUrl + "/Activity");
	};
    
    var _getAtividadeUsuario = function (user) {
		return $http.get(config.baseUrl + "/Activity/GetAllUser/" + user);
	};
    
    var _updateAtividade = function (activity) {
		return $http.put(config.baseUrl + "/Activity", activity);
	};
    
    var _updateTempo = function(time) {
        return $http.put(config.baseUrl + "/Activity/UpdateTime", time);
    }
    
	var _saveAtividade = function (activity) {
		return $http.post(config.baseUrl + "/Activity", activity).success(function (response) {
                localStorageService.set('idActivityData', { idActivity: activity.activityId});
        }).error(function (err, status) {
           
        });
	};
    
    var _saveTempo = function (time) {
        return $http.post(config.baseUrl + "/Activity/SaveTime", time).success(function (response) {
                localStorageService.set('idTimeData', { idTime: time.TimeId});
        }).error(function (err, status) {
           
        });
    }
    
    var _deleteAtividade = function(activity){
        return $http.delete(config.baseUrl + "/Activity/" + activity);
    }
    
    var _deleteTempo = function(timeId){
        return $http.delete(config.baseUrl + "/Activity/DeleteTime/" + timeId);
    }
    
    var _singUp = function(user){
        return $http.post("http://localhost:5000/Account/Register", user);
    }
    
    var _login = function(loginData) {
            return $http.post("http://localhost:5000/Account/Login", loginData).success(function (response) {
                localStorageService.set('authorizationData', { userName: loginData.Email, authenticationUser: true });
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
            _authentication.useRefreshTokens = false;
        }
    };
    
    var _verificarAutenticacao = function () {
        var aut = localStorageService.get('authorizationData');
        if(aut == null){
            $location.path('/login');
        }
    }
    
    var _recuperarIdActivity = function() {
        var idData = localStorageService.get('idActivityData');
        return idData.idActivity;
    }
    
    var _recuperarIdTime = function(){
        var idTimeData = localStorageService.get('idTimeData');
        return idTimeData.idTime;
    }
    
    var _continuarActivity = function(activity)
    {
       localStorageService.set('continueActivity', { Link: activity.Link, Observation: activity.Observation});
    }
    
	return {
		getActivity: _getAtividade,
		saveActivity: _saveAtividade,
        deleteActivity: _deleteAtividade,
        signUp: _singUp,
        login: _login,
        authentication: _authentication,
        logOut: _logOut,
        fillAuthData: _fillAuthData,
        verificarAutenticacao: _verificarAutenticacao,
        updateActivity: _updateAtividade,
        recuperarIdActivity: _recuperarIdActivity,
        continuarActivity: _continuarActivity,
        updateTime: _updateTempo,
        saveTime: _saveTempo,
        recuperarIdTime: _recuperarIdTime,
        deleteTime: _deleteTempo,
        getActivityUser: _getAtividadeUsuario
	};
});