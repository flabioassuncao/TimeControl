angular.module("timeControl").controller("timeControlCtrl", function ($scope, activityAPI, operadorasAPI, serialGenerator, addHour, $timeout) {
	$scope.app = "Time Control";
	$scope.activities = [];
    $scope.timeTotal;
    $scope.counter = 0;
    $scope.stopped = true;
    var vm = this;
    vm.Total = 0;
    $scope.onTimeout = function(){
        if(!$scope.stopped){
            $scope.counter++;
            mytimeout = $timeout($scope.onTimeout,1000);
        }
    }

	var carregarActivity = function () {
		activityAPI.getActivity().success(function (data) {
			$scope.activities = data;
		}).error(function (data, status) {
			$scope.message = "Aconteceu um problema: " + status;
		});
        
	};

	$scope.adicionarActivity = function (activity) {
		  activity.activityId = serialGenerator.generate();
          activity.EndDate = moment().format();
          activity.StartDate = document.getElementById('startDate').value;
          var tempo = document.getElementById('tempo').innerText;
          tempo = tempo.replace("H ", ":");
          tempo = tempo.replace("M ", ":");
          tempo = tempo.replace("S", "");
          activity.Time = tempo
          activity.Responsible = document.getElementById('usuario').innerText;
		  activityAPI.saveActivity(activity).success(function (data) {
			 delete $scope.activity;
             document.getElementById('tempo').innerText  = '00H 00M 00S';
			 carregarActivity();
		});
	};
    
    $scope.deletarActivity = function (activityId) {
        activityAPI.deleteActivity(activityId).success(function (data) {
			toastr["success"]("Deletado!");
			carregarActivity();
		});
    }
    
	$scope.ordenarPor = function (campo) {
		$scope.criterioDeOrdenacao = campo;
		$scope.direcaoDaOrdenacao = !$scope.direcaoDaOrdenacao;
	};
    
    var mytimeout = $timeout($scope.onTimeout,1000);
    $scope.takeAction = function(){
        if(!$scope.stopped){
            $timeout.cancel(mytimeout);
             $scope.tagAfinish = true;
        }
        else
        {
            mytimeout = $timeout($scope.onTimeout,1000);
            $scope.tagAfinish = false;
        }
            $scope.stopped=!$scope.stopped;
    }
    
    $scope.timeTotal = function(){
        var total = "00:00:00";
        for(var i = 0; i < $scope.act.length; i++){
            var hora = $scope.act[i];
            total = addHour.addHoras(total, hora.Time, false);
        }
        return total;
    }
    
    $scope.HideStart = function(){
        if($scope.divStart == true){
            $scope.divStart = false;
            $scope.tagAstart = false;
            $scope.tagAfinish = false;
        }
        else{
            $scope.divStart = true;
            $scope.tagAstart = true;
            
        }  
    }
    
    $scope.valueStartDate = function (){
        document.getElementById('startDate').value = moment().format();
    }
    
	carregarActivity();
});