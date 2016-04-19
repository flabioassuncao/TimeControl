angular.module("timeControl").controller("timerController", function ($scope, $log, $sce, $route, activityAPI, functionsForHours, $timeout, $location, $q, localStorageService) {
    $scope.activities = [];
    $scope.timeTotal;
    $scope.counter = 0;
    $scope.Duration = "00H 00M 00S";
    $scope.activity = {};
    $scope.tagRestart = false;
    
    $scope.timeTotal = function(){
        var total = "00:00:00";
        for(var i = 0; i < $scope.act.length; i++){
            var hora = $scope.act[i];
            total = functionsForHours.addHoras(total, hora.Time, false);
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
             $scope.EndDate = moment().format();
             $scope.tagRestart = true;
        }
        else
        {
            mytimeout = $timeout($scope.onTimeout,1000);
            $scope.tagStart = false;
            $scope.tagStop = true;
            $scope.EndDate = moment().format();
            $scope.tagRestart = false;
        }
            $scope.stopped=!$scope.stopped;
    }
    
    $scope.timeTotal = function(){
        var total = "00:00:00";
        for(var i = 0; i < $scope.act.length; i++){
            var hora = $scope.act[i];
            total = functionsForHours.addHoras(total, hora.Time, false);
        }
        return total;
    }
    
    $scope.HideStart = function(){
        if($scope.divStart == true){
            $scope.divStart = false; $scope.tagAstart = false; $scope.tagStart = false;
        }
        else{
            $scope.divStart = true; $scope.tagAstart = true; $scope.tagStop = true;
         }
    }
    
    $scope.valueStartDate = function (){
        $scope.StartDate = moment().format(); 
    }
    
    $scope.createActivity = function(activity){
        activity.Link = $scope.searchText;
        activity.Responsible = activityAPI.authentication.userName;
        activityAPI.saveActivity(activity).success(function (data) {
            var time = {};
            time.StartDate = $scope.StartDate;
            time.ActivityId = data.ActivityId;
            time.ActivityTime = "00:00:00";
            activityAPI.saveTime(time);
            localStorageService.set('lastActivity', { lastIdActivity: data.ActivityId});
		});
    }
    
    $scope.createTime = function (){
        var time = {};
        time.StartDate = $scope.StartDate;
        time.ActivityId = activityAPI.recuperarIdActivity();
        activityAPI.saveTime(time);
    }
    
    $scope.updateActivity = function(activity)
    {
        activity.ActivityId = activityAPI.recuperarIdActivity();
        activity.Responsible = activityAPI.authentication.userName;
        activity.LastTimeWorked = moment().format();
        activityAPI.updateActivity(activity).success(function (data) {
            updateTime();  
			$scope.activity = {};
            $scope.Duration = '00H 00M 00S';
            $scope.counter = 0;
            $scope.searchText = "";
            localStorageService.remove('continueActivity');
		});
    }
    
    var updateTime = function () {
        var time = {};
        time.TimeId = activityAPI.recuperarIdTime();
        time.StartDate = $scope.StartDate;
        time.EndDate = $scope.EndDate;
        time.ActivityTime = functionsForHours.transformingSeconds(functionsForHours.turningForSeconds(time.StartDate, time.EndDate));
        time.Status = true;
        activityAPI.updateTime(time).success(function () {
            atividadeAberta();
        });
    }
    
    var atividadeAberta = function (){
        var user = activityAPI.authentication.userName;
        if(user){
            activityAPI.getActivityUser(user).success(function (data) {
                $scope.states = loadAll(data);
                $scope.activities = data;
                var objts = data, resul, timer, item, obj;
                var authData = localStorageService.get('authorizationData');
                if(authData){
                    for(item in objts){
                        for(obj in objts[item].Times){
                            if(objts[item].Times[obj].Status == false && objts[item].Responsible == authData.userName){
                                timer = objts[item].Times[obj];
                                resul = objts[item];
                            }
                        }
                    }
                    if(resul){
                        $scope.activity = resul;
                        var difference = functionsForHours.turningForSeconds(timer.StartDate, moment().format());
                        $scope.Duration = functionsForHours.convertToFormatView(moment.duration(difference,'seconds'));
                        $scope.counter = difference;
                        $scope.divStart = true; $scope.tagStop = true; $scope.stopped = false;
                        $scope.StartDate = timer.StartDate;
                        localStorageService.set('idActivityData', { idActivity: resul.ActivityId});
                        localStorageService.set('idTimeData', { idTime: timer.TimeId});
                    }
                }
            });
        }
    }
    
    $scope.SkipValidation = function(value) {
        return $sce.trustAsHtml(value);
    };
    
    $scope.simulateQuery = false;
    $scope.isDisabled    = false;

    $scope.querySearch   = querySearch;
    $scope.selectedItemChange = selectedItemChange;
    $scope.searchTextChange   = searchTextChange;
    
    function querySearch (query) {
        
      var results = query ? $scope.states.filter( createFilterFor(query) ) : $scope.states,
          deferred;
      if ($scope.simulateQuery) {
        deferred = $q.defer();
        $timeout(function () { deferred.resolve( results ); }, Math.random() * 1000, false);
        return deferred.promise;
      } else {
        return results;
      }
    }

    function searchTextChange(text) {
    //   $log.info('Text changed to ' + text);
    }

    function selectedItemChange(item) {
        var time = {};
        time.StartDate = moment().format();
        time.ActivityId = item.ActivityId;
        activityAPI.saveTime(time);
        toastr.options = {"progressBar": true, "timeOut": "2000",}
        toastr["info"]("Wait!!");
        var timer = $timeout(function () {
          $timeout.cancel(timer);
          $route.reload();
        }, 2000);
    }

    function loadAll(ok) {
      return ok.map( function (repo) {
        repo.value = repo.Link.toLowerCase();
        return repo;
      });
    }

    function createFilterFor(query) {
      var lowercaseQuery = angular.lowercase(query);

      return function filterFn(state) {
        return (state.value.indexOf(lowercaseQuery) === 0);
      };
    }
    
    $scope.ContinueLastActivity = function(){
        var IdActivity = localStorageService.get('lastActivity');
            
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
            if (resul){
                toastr["warning"]("There is already an activity running!")
            } else {
                var time = {};
                time.StartDate = moment().format();
                time.ActivityTime = "00:00:00";
                time.ActivityId = IdActivity.lastIdActivity;
                time.Status = false;
                activityAPI.saveTime(time);
                
                toastr.options = {"progressBar": true, "timeOut": "2000",}
                toastr["info"]("Wait!!");
                $scope.tagRestart = false;
                
                var timer = $timeout(function () {
                    $timeout.cancel(timer);
                    $route.reload();
                }, 2000);
            }
            
		}).error(function (data, status) {
		});
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
                var timer = $timeout(function () {
                    $timeout.cancel(timer);
                    $route.reload();
                }, 2000);
            }
		}).error(function (data, status) {
		});
    }
    
    activityAPI.checkAuthentication();
    atividadeAberta();
    
});