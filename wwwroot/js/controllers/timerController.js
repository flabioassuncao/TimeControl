angular.module("timeControl").controller("timerController", function ($scope, activityAPI, serialGenerator, addHour, $timeout, $location, $q, localStorageService) {
    
    $scope.timeTotal;
    $scope.counter = 0;
    $scope.teste = false;
    
    
    var continuarActivity = function() {
        var idData = localStorageService.get('continueActivity');
        if(idData != null){
            // console.log(idData);
            $scope.activity = idData; 
        }
    }
    
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
             document.getElementById('endDate').value = moment().format();
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
    /////////////////////////////////
    $scope.createActivity = function(activity){
        
        activity.activityId = serialGenerator.generate();
        activity.StartDate = document.getElementById('startDate').value;
        activity.Responsible = activityAPI.authentication.userName;
        activity.Time = "00:00:00";
        activityAPI.saveActivity(activity);
    }
    
    $scope.updateActivity = function(activity)
    {
          activity.activityId = activityAPI.recuperarIdActivity();
        //   console.log(activity.activityId);
          activity.Status = false;
          activity.StartDate = document.getElementById('startDate').value;
          activity.EndDate = document.getElementById('endDate').value;
          var tempo = document.getElementById('tempo').innerText;
          tempo = tempo.replace("H ", ":");
          tempo = tempo.replace("M ", ":");
          tempo = tempo.replace("S", "");
          activity.Time = tempo 
        //   console.log(activity);
          activityAPI.updateActivity(activity);
    }
    
    $scope.updateFinishActivity = function(activity)
    {
          activity.activityId = activityAPI.recuperarIdActivity();
          
          activity.Status = true;
          activity.status = true;
          activity.StartDate = document.getElementById('startDate').value;
          activity.EndDate = document.getElementById('endDate').value;
          var tempo = document.getElementById('tempo').innerText;
          tempo = tempo.replace("H ", ":");
          tempo = tempo.replace("M ", ":");
          tempo = tempo.replace("S", "");
          activity.Time = tempo 
          
          activityAPI.updateActivity(activity).success(function (data) {
			 delete $scope.activity;
             document.getElementById('tempo').innerText  = '00H 00M 00S';
             localStorageService.remove('continueActivity');
		});
    }
    
    $scope.objFalse = function () {
        activityAPI.getActivity().success(function (data) {
			var objts = data;
            var resul;
            // alert(objts.length);
            for(var item in objts){
                if(objts[item].Status == false){
                    resul = objts[item];
                }
            }
            console.log(resul);
            if(resul){
                if(resul.Time == "00:00:00"){
                    var dt1 = moment(resul.StartDate, "YYYY/MM/DD hh:mm:ss");
                    // console.log(dt1);
                    var dt2 = moment(moment().format(), "YYYY/MM/DD hh:mm:ss");
                    // console.log(dt2);
                    var diferenca = dt2.diff(dt1, 'seconds');
                    // console.log(diferenca);
                    x = moment.duration(diferenca,'seconds')
                    var h = x.hours().toString().length == 2? x.hours() : ("0" + x.hours());
                    var m = x.minutes().toString().length == 2? x.minutes() : ("0" + x.minutes());
                    var s = x.seconds().toString().length == 2? x.seconds() : ("0" + x.seconds());
                    // console.log(h + ":" + m + ":" + s);
                    
                }
                    
                
            }else{
                alert("sem reg.");
            }
            // console.log(resul);
		}).error(function (data, status) {
			alert("Aconteceu um problema");
		});
    }
    
    var atividadeAberta = function (){
        activityAPI.getActivity().success(function (data) {
			var objts = data;
            var resul;
            // alert(objts.length);
            var authData = localStorageService.get('authorizationData');
                // console.log(authData.userName);
            for(var item in objts){
                if(objts[item].Status == false && objts[item].Responsible == authData.userName){
                    resul = objts[item];
                }
            }
            
            // console.log(resul);
            if(resul){
                if(resul.Time == "00:00:00"){
                    var dt1 = moment(resul.StartDate, "YYYY/MM/DD hh:mm:ss");
                    // console.log(dt1);
                    var dt2 = moment(moment().format(), "YYYY/MM/DD hh:mm:ss");
                    // console.log(dt2);
                    var diferenca = dt2.diff(dt1, 'seconds');
                    // console.log(diferenca);
                    x = moment.duration(diferenca,'seconds')
                    var h = x.hours().toString().length == 2? x.hours() : ("0" + x.hours());
                    var m = x.minutes().toString().length == 2? x.minutes() : ("0" + x.minutes());
                    var s = x.seconds().toString().length == 2? x.seconds() : ("0" + x.seconds());
                    // console.log(h + ":" + m + ":" + s);
                    document.getElementById('tempo').innerText  = h + "H " + m + "M " + s + "S";
                    $scope.activity = resul;
                    $scope.counter = diferenca;
                    $scope.stopped = false;
                    
                    $scope.divStart = true;
                    $scope.tagAstart = true;
                    // console.log(resul.activityId)
                    var qualquer = resul.activityId;
                    var id = function(qualquer){
                        activityAPI.salvarId(resul);
                    }
                    
                    id();
                }
                    
                
            }else{
                console.log("sem reg");
            }
            // console.log(resul);
		}).error(function (data, status) {
			// alert("Aconteceu um problema");
		});
    }
    
    ///////////////////////////////////////
    authen();
    continuarActivity();
    atividadeAberta();
    
});