angular.module("timeControl").controller("timerController", function ($scope, activityAPI, serialGenerator, addHour, $timeout, $location, $q, localStorageService) {
    
    $scope.timeTotal;
    $scope.counter = 0;
    $scope.teste = false;
    
    
    $scope.timeTotal = function(){
        var total = "00:00:00";
        for(var i = 0; i < $scope.act.length; i++){
            var hora = $scope.act[i];
            total = addHour.addHoras(total, hora.Time, false);
        }
        return total;
    }
    
    $scope.stopped = true;
    var vm = this;
    vm.Total = 0;
    $scope.onTimeout = function(){
        if(!$scope.stopped){
            $scope.counter++;
            mytimeout = $timeout($scope.onTimeout,1000);
        }
    }
    
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
    
    $scope.adicionarActivity = function (activity) {
		  activity.activityId = serialGenerator.generate();
          activity.EndDate = moment().format();
          activity.StartDate = document.getElementById('startDate').value;
          var tempo = document.getElementById('tempo').innerText;
          tempo = tempo.replace("H ", ":");
          tempo = tempo.replace("M ", ":");
          tempo = tempo.replace("S", "");
          activity.Time = tempo
          activity.Responsible = activityAPI.authentication.userName;
		  activityAPI.saveActivity(activity).success(function (data) {
			 delete $scope.activity;
             document.getElementById('tempo').innerText  = '00H 00M 00S';
			//  carregarActivity();
		});
	};
    
    var authen = function (){
        activityAPI.verificar();
    }
    
    authen();
    
});