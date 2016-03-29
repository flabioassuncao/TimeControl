angular.module("timeControl").controller("projectController", function ($scope, activityAPI, serialGenerator, addHour, $timeout, $location, $q, localStorageService) {
   $scope.activities = [];
   $scope.authentication = activityAPI.authentication;   
   
   var carregarActivity = function () {
		activityAPI.getActivity().success(function (data) {
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
    
    $scope.timeTotal = function(){
        var total = "00:00:00";
        for(var i = 0; i < $scope.act.length; i++){
            var hora = $scope.act[i];
            total = addHour.addHoras(total, hora.Time, false);
        }
        return total;
    }
    
    var authen = function (){
        activityAPI.verificar();
    }
    
    authen();
    carregarActivity(); 
});