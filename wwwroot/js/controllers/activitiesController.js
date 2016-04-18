angular.module("timeControl").controller("activitiesController", function ($scope, $sce, $log, activityAPI, functionsForHours, $timeout, $location, $q, localStorageService) {
   $scope.activities = [];
   $scope.authentication = activityAPI.authentication;   
   $scope.dt = new Date();
   $scope.filterteste;
   $scope.datee = $scope.dt;
   
   var loadActivity = function () {
        var user = activityAPI.authentication.userName;
        if(user){
            activityAPI.getActivityUser(user).success(function (data) {
                $scope.activities = data;
            }).error(function (data, status) {
            });
        }
	};
    
    $scope.ShowActivity = function (Activity) {
        activityAPI.getActivityId(Activity.ActivityId).success(function (data) {
            $scope.onlyActivity = data;
        }).error(function (data, status) {
        });
    }
    
    $scope.deletarActivity = function (ActivityId) {
        activityAPI.deleteActivity(ActivityId).success(function (data) {
			toastr["success"]("Deleted!");
			loadActivity();
		});
    }
    
    $scope.filterByUserNameCurrentContext = function (activity) { 
        return activity.Responsible === activityAPI.authentication.userName;
    };
    
    $scope.ordenarPor = function (campo) {
		$scope.criterioDeOrdenacao = campo;
		$scope.direcaoDaOrdenacao = !$scope.direcaoDaOrdenacao;
	};
    
    $scope.totalTimeActivities = function(){
        var total = "00:00:00";
        var temp = "00:00:00";
        for(var i = 0; i < $scope.act.length; i++){
            var hora = $scope.act[i];
            if(hora.Times.length > 0){
                for(var x = 0; x < hora.Times.length; x++){
                   temp = hora.Times[x];
                   if(temp.Status){
                        total = functionsForHours.addHoras(total, temp.ActivityTime, false);
                   }
                }
            } 
        }
        return total;
    }
    
    $scope.ContinueActivity = function(activity){
        activityAPI.getActivity().success(function (data) {
			var objts = data, resul, timer, item, obj;
            var authData = localStorageService.get('authorizationData');
            for(item in objts){
                for(obj in objts[item].Times){
                    if(objts[item].Times[obj].Status == false && objts[item].Responsible == authData.userName){
                        timer = objts[item].Times[obj];
                        resul = objts[item];
                    }
                }
            }
            if(resul){
                toastr["warning"]("There is already an activity running!")
            }else{
                var time = {};
                time.StartDate = moment().format();
                time.ActivityTime = "00:00:00";
                time.ActivityId = activity.ActivityId;
                activityAPI.saveTime(time);
                
                toastr.options = {"progressBar": true, "timeOut": "2000",}
                toastr["info"]("Wait!!")
                startTimer();
            }
            
		}).error(function (data, status) {
		});
    }
    
    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/timer');
        }, 2000);
    }
    
    $scope.DeleteTime = function (timeId){
        activityAPI.deleteTime(timeId).success(function (data) {
			toastr["success"]("Deleted!");
			loadActivity();
		});
    }
    
    $scope.SkipValidation = function(value) {
        return $sce.trustAsHtml(value);
    };
    
    activityAPI.checkAuthentication();
    loadActivity(); 
    
    $scope.searchName = function () {
        
        if($scope.search)
            $scope.search = false;
           
        else   
            $scope.search = true;
    }
    
});