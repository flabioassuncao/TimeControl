angular.module("timeControl").controller("projectController", function ($scope, activityAPI, serialGenerator, addHour, $timeout, $location, $q, localStorageService) {
   $scope.activities = [];
   $scope.authentication = activityAPI.authentication;   
   
   var carregarActivity = function () {
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
			carregarActivity();
		});
    }
    
    $scope.filterByUserNameCurrentContext = function (activity) { 
        return activity.Responsible === activityAPI.authentication.userName;
    };
    
    $scope.ordenarPor = function (campo) {
		$scope.criterioDeOrdenacao = campo;
		$scope.direcaoDaOrdenacao = !$scope.direcaoDaOrdenacao;
	};
    
    $scope.TempoTotalAtividades = function(){
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
    
    $scope.ContinuarAtividade = function(activity){
        // activityAPI.continuarActivity(activity);
        // localStorageService.set('idActivityData', { idActivity: activity.activityId});
        
        var time = {};
        time.TimeId = serialGenerator.generate();
        time.StartDate = moment().format();
        time.ActivityId = activity.activityId;
        activityAPI.saveTime(time);
        
        $location.path('/timer');
        
    }
    
    var authen = function (){
        activityAPI.verificarAutenticacao();
    }
    
    $scope.DeletarTempo = function (timeId){
        activityAPI.deleteTime(timeId).success(function (data) {
			toastr["success"]("Deletado!");
			carregarActivity();
		});
    }
    
    authen();
    carregarActivity(); 
});