angular.module("timeControl").controller("projectController", function ($scope, $sce, activityAPI, serialGenerator, addHour, $timeout, $location, $q, localStorageService) {
   $scope.activities = [];
   $scope.authentication = activityAPI.authentication;   
   
   var loadActivity = function () {
        var user = activityAPI.authentication.userName;
		activityAPI.getActivityUser(user).success(function (data) {
			$scope.activities = data;
		}).error(function (data, status) {
			$scope.message = "Aconteceu um problema: " + status;
		});
        
	};
    
    $scope.deletarActivity = function (activityId) {
        activityAPI.deleteActivity(activityId).success(function (data) {
			toastr["success"]("Deletado!");
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
    
    $scope.totalTimeActivities = function(){ //olhar aqui tempo quando estiver atv em exe...
        var total = "00:00:00";
        var temp = "00:00:00";
        for(var i = 0; i < $scope.act.length; i++){
            var hora = $scope.act[i];
            if(hora.Times.length > 0){
                for(var x = 0; x < hora.Times.length; x++){
                   temp = hora.Times[x];
                   total = addHour.addHoras(total, temp.ActivityTime, false);
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
                toastr["warning"]("Existe atv em execução!")
            }else{
                var time = {};
                time.TimeId = serialGenerator.generate();
                time.StartDate = moment().format();
                time.ActivityId = activity.activityId;
                activityAPI.saveTime(time);
                
                toastr.options = {
                    "progressBar": true,
                    "timeOut": "2000",
                }
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
    
    var authentication = function (){
        activityAPI.verificarAutenticacao();
    }
    
    $scope.DeleteTime = function (timeId){
        activityAPI.deleteTime(timeId).success(function (data) {
			toastr["success"]("Deletado!");
			loadActivity();
		});
    }
    
    $scope.SkipValidation = function(value) {
        return $sce.trustAsHtml(value);
    };
    
    authentication();
    loadActivity(); 
});