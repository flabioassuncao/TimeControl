angular.module("timeControl").controller("activitiesForProjectsController", function ($scope, $sce, activityAPI, $location) {
    
    $scope.searchName = function () {
        if($scope.search)
            $scope.search = false;
        else   
            $scope.search = true;
    }
    
    $scope.getNameProjects = function () {
        activityAPI.getNameProjects().success(function (data) {
            $scope.project = data;
        });
    }
    
    $scope.loadActivity = function (ProjectId) {
        activityAPI.getAllActivityByProjectId(ProjectId).success(function (data) {
            $scope.activities = data;
        }).error(function (data, status) {  });
	};
    
    $scope.SkipValidation = function(value) {
        return $sce.trustAsHtml(value);
    };
    
    $scope.getBelongProject = function(projectId){
        activityAPI.getBelongProject(projectId).success(function (data) {
            $scope.users = data;
            activityAPI.getAllActivityByProjectId(projectId).success(function (data) {
                $scope.activities = data;
            });
        }).error(function (data, status) { });
    }
    
    activityAPI.checkAuthentication();
});