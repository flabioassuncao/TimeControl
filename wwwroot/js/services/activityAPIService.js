angular.module("timeControl").factory("activityAPI", function ($timeout, $route, $http, config, $q, localStorageService, $location) {
    var _authentication = {
        isAuth: false,
        userName: "",
        idUser: "",
        useRefreshTokens: false
    };
    
    var _getActivity = function () {
		return $http.get(config.baseUrl + "/Activity");
	};
    
    var _getActivityUser = function (UserId) {
		return $http.get(config.baseUrl + "/Activity/GetAllUser/" + UserId);
	};
    
    var _getActivityId = function (Id) {
		return $http.get(config.baseUrl + "/Activity/" + Id);
	};
    
    var _updateActivity = function (Activity) {
		return $http.put(config.baseUrl + "/Activity", Activity);
	};
    
    var _updateTime = function(Time) {
        return $http.put(config.baseUrl + "/Time/UpdateTime", Time);
    }
    
	var _saveActivity = function (Activity) {
		return $http.post(config.baseUrl + "/Activity", Activity).success(function (response) {
            localStorageService.set('idActivityData', { idActivity: response.ActivityId});
        }).error(function (err, status) { });
	};
    
    var _saveTime = function (Time) {
        return $http.post(config.baseUrl + "/Time/SaveTime", Time).success(function (response) {
            localStorageService.set('idTimeData', { idTime: response.TimeId});
        }).error(function (err, status) { });
    }
    
    var _deleteActivity = function(Activity){
        return $http.delete(config.baseUrl + "/Activity/" + Activity);
    }
    
    var _deleteBelong = function(memberId, projectId){
        var belong ={
            MemberId : memberId,
            ProjectId : projectId
        };
        return $http.post(config.baseUrl + "/Project/DeleteBelong", belong);
    }
    
    var _deleteTime = function(TimeId){
        return $http.delete(config.baseUrl + "/Time/DeleteTime/" + TimeId);
    }
    
    var _singUp = function(User){
        return $http.post("http://localhost:5000/Account/Register", User);
    }
    
    var _login = function(LoginData) {
            return $http.post("http://localhost:5000/Account/Login", LoginData).success(function (response) {
        }).error(function (err, status) {
            _logOut();
        });
    };
    
    var _recuperarIdUser = function(user){
         return $http.get(config.baseUrl + "/User/" + user.Email).success(function (data) {
                    _authentication.isAuth = true;
                    _authentication.userName = user.Email;
                    _authentication.idUser = data.UserId;
                });
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
            _authentication.idUser = authData.userId;
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
        var userId = _authentication.idUser;
        _getActivityUser(userId).success(function (data) {
			var objts = data, resul, timer, item, obj;
            for(item in objts){
                for(obj in objts[item].Times){
                    if(!objts[item].Times[obj].Status){
                        timer = objts[item].Times[obj];
                        resul = objts[item];
                    }
                }
            }
            if(resul){
                toastr["warning"]("There is already an activity running!")
            }else{
                var time = {
                    StartDate: moment().format(),
                    ActivityTime: "00:00:00",
                    ActivityId: Activity.ActivityId,
                };
                _saveTime(time);
                toastr.options = {"progressBar": true, "timeOut": "2000",}
                toastr["info"]("Wait!!")
                var timer = $timeout(function () {
                    $timeout.cancel(timer);
                    $location.path('/timer');
                }, 2000);
            }
		}).error(function (data, status) {
		});
    }
    
    var _getNameProjects = function(){
        var UserId = _authentication.idUser;
        return $http.get(config.baseUrl + "/Project/GetAllNamesProjects/" + UserId);
    }
    
    var _getAllProjects = function() {
        var userId = _authentication.idUser;
        return $http.get(config.baseUrl + "/Project/GetAll/" + userId) ;
    }
    
    var _getAllActivityByProjectId = function(ProjectId) {
        return $http.get(config.baseUrl + "/Activity/GetAllProject/" + ProjectId);
    }
    
    var _saveProject = function(project){
        project.AdministratorId = _authentication.idUser;
        return $http.post(config.baseUrl + "/Project", project);
    }
    
    var _getNameUsers = function(){
        return $http.get(config.baseUrl + "/User/GetAllUsers");
    }
    
    var _getBelongProject = function(projectId){
        return $http.get(config.baseUrl + "/User/GetAllBelongProject/" + projectId);
    }
    
    var _getProjectsParticipating = function(){
        var UserId = _authentication.idUser;
        return $http.get(config.baseUrl + "/Project/GetProjectsParticipating/" + UserId);
    }
    
    var _saveBelongProject = function(IdProjectAndUser){
        return $http.post(config.baseUrl + "/Project/SaveBelong", IdProjectAndUser);
    }
    
    var _createUser = function(user) {
        return $http.post(config.baseUrl + "/User", user);
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
        continueActivity: _continueActivity,
        updateTime: _updateTime,
        saveTime: _saveTime,
        recuperarIdTime: _restoreIdTime,
        deleteTime: _deleteTime,
        getActivityUser: _getActivityUser,
        getActivityId: _getActivityId,
        getNameProjects: _getNameProjects,
        saveProject: _saveProject,
        getNameUsers: _getNameUsers,
        getBelongProject: _getBelongProject,
        saveBelongProject: _saveBelongProject,
        getAllProjects: _getAllProjects,
        getAllActivityByProjectId: _getAllActivityByProjectId,
        getProjectsParticipating: _getProjectsParticipating,
        createUser: _createUser,
        recuperarIdUser: _recuperarIdUser,
        deleteBelong: _deleteBelong
	};
});