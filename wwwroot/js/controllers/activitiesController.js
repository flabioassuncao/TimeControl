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
        activityAPI.getActivityId(Activity.activityId).success(function (data) {
            $scope.onlyActivity = data;
        }).error(function (data, status) {
        });
    }
    
    $scope.deletarActivity = function (activityId) {
        activityAPI.deleteActivity(activityId).success(function (data) {
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
                   if(temp.status){
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
                    if(objts[item].Times[obj].status == false && objts[item].Responsible == authData.userName){
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
                time.ActivityId = activity.activityId;
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
    
    $scope.onlyWeekendsPredicate = function(date) {
        var day = date.getDay();
        return day === 0 || day === 6;
    }
    
    $scope.options = {
        customClass: getDayClass,
        showWeeks: true
    };
    
    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date(tomorrow);
    afterTomorrow.setDate(tomorrow.getDate() + 1);
    $scope.events = [
        {
            date: tomorrow,
            status: 'full'
        },
        {
            date: afterTomorrow,
            status: 'partially'
        }
    ];
    
    function getDayClass(data) {
    var date = data.date,
      mode = data.mode;
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0,0,0,0);
            
            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.events[i].date).setHours(0,0,0,0);

                if (dayToCheck === currentDay) {
                    return $scope.events[i].status;
                }
            }
        }

        return '';
    }
      
    $scope.open1 = function() {
        $scope.popup1.opened = true;
        $scope.filterteste = document.getElementById('timetime').value.substring(1, 11);
    };
    
    $scope.popup1 = {
        opened: false
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