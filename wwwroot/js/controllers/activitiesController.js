angular.module("timeControl").controller("activitiesController", function ($scope, $sce, $log, activityAPI, functionsForHours, $timeout, $location, $q, localStorageService) {
   $scope.activities = [];
   $scope.authentication = activityAPI.authentication
   $scope.todos = [];
    
    $scope.addTodo = function(){
      var todo =[{desc:'byActivity',done:false}]
      $scope.todos = todo.concat($scope.todos);  
      $scope.newTodo = '';
      $scope.search = true;
    }
    
    $scope.removeTodo = function(index) {
      $scope.todos.splice(index,1);
      delete $scope.searchCriteria;
      delete $scope.initialDate
      if($scope.todos.length == 0)
            $scope.search = false;
    }
   
   var loadActivity = function () {
        var userId = activityAPI.authentication.idUser;
        if(userId){
            activityAPI.getActivityUser(userId).success(function (data) {
                $scope.activities = data;
                var timer = $timeout(function () {
                    $timeout.cancel(timer);
                         activityAPI.getProjectsParticipating().success(function (data) {
                        $scope.project = data;
                     });
                }, 2000);
            }).error(function (data, status) {
            });
        }
	}
    
    $scope.ShowActivity = function (Activity) {
        $scope.searchCriteria = Activity.Link;
    }
    
    $scope.deletarActivity = function (ActivityId) {
        activityAPI.deleteActivity(ActivityId).success(function (data) {
			toastr["success"]("Deleted!");
			loadActivity();
		});
    }
    
    $scope.totalTimeActivities = function(){
        var total = "00:00:00";
        var temp = "00:00:00";
        for(var i = 0; i < $scope.act.length; i++){
            var hora = $scope.act[i];
            if(hora.Times.length > 0){
                for(var x = 0; x < hora.Times.length; x++){
                   temp = hora.Times[x];
                   if(temp.Status){
                        total = functionsForHours.addHoras(total, temp.ActivityTime, false);
                   }
                }
            } 
        }
        return total;
    }
    
    $scope.ContinueActivity = function(activity){
        activityAPI.continueActivity(activity);
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
    
    $scope.searchName = function () {
        if($scope.search)
            $scope.search = false;
        else   
            $scope.search = true;
    }
    
    activityAPI.checkAuthentication();
    loadActivity(); 
});