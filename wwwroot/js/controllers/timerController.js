angular.module("timeControl").controller("timerController", function ($scope, $log, $sce, $route, activityAPI, functionsForHours, $timeout, $location, $q, localStorageService) {
    $scope.activities = [];
    $scope.counter = 0;
    $scope.Duration = "00:00:00";
    $scope.activity = {};
    $scope.tagRestart = false;
    $scope.initialDate = moment().format('YYYY/MM/DD');
    $scope.finalDate = moment().add(1, 'days').format('YYYY/MM/DD');
    
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
    
    $scope.HideStart = function(){
        if($scope.divStart == true){
            $scope.divStart = false; $scope.tagStop = false;
        }else
            $scope.divStart = true; $scope.tagAstart = true; 
    }
    
    $scope.valueStartDate = function (){
        $scope.StartDate = moment().format(); 
    }
    
    $scope.createActivity = function(activity, proj){
        var userId = activityAPI.authentication.idUser;
        activity.ResponsibleId = userId;
        activity.ProjectId = proj;
        activity.Link = $scope.searchText;
        activityAPI.saveActivity(activity).success(function (data) {
            var time = {
                StartDate : $scope.StartDate,
                ActivityId : data.ActivityId,
                ActivityTime : "00:00:00"    
            };
            activityAPI.saveTime(time);
            localStorageService.set('lastActivity', { lastIdActivity: data.ActivityId});
		});
    }
    
    $scope.createTime = function (){
        var time = {
            StartDate: $scope.StartDate,
            ActivityId: activityAPI.recuperarIdActivity()
        };
        activityAPI.saveTime(time);
    }
    
    $scope.updateActivity = function(activity)
    {
        var help = {
            ResponsibleId : activityAPI.authentication.idUser,
            ProjectId : document.getElementById('projeId').value,
            Observation : activity.Observation,
            ActivityId : activityAPI.recuperarIdActivity(),
            LastTimeWorked : moment().format(),
        };
        activityAPI.updateActivity(help).success(function (data) {
            updateTime();  
			$scope.activity = {};
            $scope.Duration = '00:00:00';
            $scope.counter = 0;
            $scope.searchText = "";
            localStorageService.remove('continueActivity');
		});
    }
    
    var updateTime = function () {
        var time = {
            TimeId : activityAPI.recuperarIdTime(),
            StartDate : $scope.StartDate,
            EndDate : $scope.EndDate,
            ActivityTime : functionsForHours.transformingSeconds(functionsForHours.turningForSeconds($scope.StartDate, $scope.EndDate)),
            Status : true,
        };
        activityAPI.updateTime(time).success(function () {
            atividadeAberta();
        });
    }
    
    var atividadeAberta = function (){
        var userId = activityAPI.authentication.idUser;
        if(userId){
            activityAPI.getActivityUser(userId).success(function (data) {
                $scope.states = loadAll(data);
                $scope.activities = data;
                var objts = data, resul, timer, item, obj;
                var authData = localStorageService.get('authorizationData');
                if(authData){
                    for(item in objts){
                        for(obj in objts[item].Times){
                            if(objts[item].Times[obj].Status == false){
                                timer = objts[item].Times[obj];
                                resul = objts[item];
                            }
                        }
                    }
                    if(resul){
                        $scope.activity = resul;
                        var difference = functionsForHours.turningForSeconds(timer.StartDate, moment().format());
                        $scope.Duration = functionsForHours.convertToFormatView(moment.duration(difference,'seconds'));
                        document.getElementById('projeId').value =  resul.ProjectId;
                        $scope.counter = difference;
                        $scope.divStart = true; $scope.tagStop = true; $scope.stopped = false;
                        $scope.StartDate = timer.StartDate;
                        localStorageService.set('idActivityData', { idActivity: resul.ActivityId});
                        localStorageService.set('idTimeData', { idTime: timer.TimeId});
                        $scope.searchText = resul.Link;
                    }
                }
                var timer = $timeout(function () {
                    $timeout.cancel(timer);
                    activityAPI.getProjectsParticipating().success(function (data) {
                        $scope.project = data;
                     });
                }, 2000);
            });
        }
    }
    
    $scope.SkipValidation = function(value) {
        return $sce.trustAsHtml(value);
    };
    
    $scope.simulateQuery = false;
    $scope.isDisabled = false;
    $scope.querySearch = querySearch;
    $scope.selectedItemChange = selectedItemChange;
    $scope.searchTextChange   = searchTextChange;
    
    function querySearch (query) {
      var results = query ? $scope.states.filter( createFilterFor(query) ) : $scope.states,
          deferred;
      if ($scope.simulateQuery) {
        deferred = $q.defer();
        $timeout(function () { deferred.resolve( results ); }, Math.random() * 1000, false);
        return deferred.promise;
      }else 
            return results;
    }

    function searchTextChange(text) { }

    function selectedItemChange(item) {
        var time = {
            StartDate: moment().format(),
            ActivityId: item.ActivityId,
        };
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
    
    $scope.ContinueActivity = function(activity){
        activityAPI.continueActivity(activity);
        var timer = $timeout(function () {
                    $timeout.cancel(timer);
                    $route.reload();
                }, 2000);
    }
    
    activityAPI.checkAuthentication();
    atividadeAberta();
});