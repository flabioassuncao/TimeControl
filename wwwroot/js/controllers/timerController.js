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
             $scope.tagStart = true;
             $scope.tagStop = false;
             document.getElementById('endDate').value = moment().format();
        }
        else
        {
            mytimeout = $timeout($scope.onTimeout,1000);
            $scope.tagStart = false;
            $scope.tagStop = true;
            document.getElementById('endDate').value = moment().format();
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
            $scope.tagStart = false;
        }
        else{
            $scope.divStart = true;
            $scope.tagAstart = true;
            $scope.tagStop = true;
            
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
		});
	};
    
    var authen = function (){
        activityAPI.verificarAutenticacao();
    }
    
    $scope.createActivity = function(activity){
        activity.activityId = serialGenerator.generate();
        activity.Responsible = activityAPI.authentication.userName;
        activityAPI.saveActivity(activity).success(function (data) {
			var time = {};
                time.TimeId = serialGenerator.generate();
                time.StartDate = document.getElementById('startDate').value;
                time.ActivityId = activity.activityId;
                time.ActivityTime = "00:00:00";
                activityAPI.saveTime(time);
		});
    }
    
    $scope.createTime = function (){
        var time = {};
        time.TimeId = serialGenerator.generate();
        time.StartDate = document.getElementById('startDate').value;
        time.ActivityId = activityAPI.recuperarIdActivity();
        activityAPI.saveTime(time);
    }
    
    $scope.updateActivity = function(activity)
    {
          activity.activityId = activityAPI.recuperarIdActivity();
          activity.Responsible = activityAPI.authentication.userName;
          activity.Status = true;
          activity.StartDate = document.getElementById('startDate').value;
          activity.EndDate = document.getElementById('endDate').value;
          var tempo = document.getElementById('tempo').innerText;
          tempo = tempo.replace("H ", ":");
          tempo = tempo.replace("M ", ":");
          tempo = tempo.replace("S", "");
          activity.Time = tempo  
          activityAPI.updateActivity(activity).success(function (data) {
            updateTime();  
			delete $scope.activity;
            document.getElementById('tempo').innerText  = '00H 00M 00S';
            $scope.counter = 0;
            localStorageService.remove('continueActivity');
		});
            
    }
    
    var updateTime = function () {
        var time = {};
        time.TimeId = activityAPI.recuperarIdTime();
        time.StartDate = document.getElementById('startDate').value;
        time.EndDate = document.getElementById('endDate').value;
        var dt1 = moment(time.StartDate, "YYYY/MM/DD hh:mm:ss");
        var dt2 = moment(time.EndDate, "YYYY/MM/DD hh:mm:ss");
        time.ActivityTime = converterSegundos(dt2.diff(dt1, 'seconds'));
        time.status = true;
        activityAPI.updateTime(time);
        
    }
    
    var converterSegundos = function (s){
              
        function duas_casas(numero){
            if (numero <= 9){
                numero = "0"+numero;
            }
            return numero;
        }

        var hora = duas_casas(Math.round(s/3600));
        var minuto = duas_casas(Math.floor((s%3600)/60));
        var segundo = duas_casas((s%3600)%60);
                
        var formatado = hora+":"+minuto+":"+segundo;
                
        return formatado;
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
    
    var atividadeAberta = function (){
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
                $scope.activity = resul;
                var dt1 = moment(timer.StartDate, "YYYY/MM/DD hh:mm:ss");
                var dt2 = moment(moment().format(), "YYYY/MM/DD hh:mm:ss");
                var difference = dt2.diff(dt1, 'seconds');
                var x = moment.duration(difference,'seconds')
                var h = x.hours().toString().length == 2? x.hours() : ("0" + x.hours());
                var m = x.minutes().toString().length == 2? x.minutes() : ("0" + x.minutes());
                var s = x.seconds().toString().length == 2? x.seconds() : ("0" + x.seconds());
                document.getElementById('tempo').innerText  = h + "H " + m + "M " + s + "S";
                $scope.counter = difference;
                $scope.divStart = true;
                $scope.tagStop = true;
                $scope.stopped = false;
                document.getElementById('startDate').value = timer.StartDate;
                localStorageService.set('idActivityData', { idActivity: resul.activityId});
                localStorageService.set('idTimeData', { idTime: timer.TimeId});
            }
		}).error(function (data, status) {
		});
    }
    
    authen();
    // continuarActivity();
    atividadeAberta();
    
});