angular.module("timeControl").controller("activitiesForProjectsController", function ($scope, $sce, activityAPI, $location) {
    
    $scope.searchName = function () {
        if($scope.search)
            $scope.search = false;
        else   
            $scope.search = true;
    }
    
    $scope.testeProje = function () {
        activityAPI.getNameProjects().success(function (data) {
            $scope.project = data;
        });
    }
    
    var loadActivity = function () {
            activityAPI.getActivity().success(function (data) {
                $scope.activities = data;
            }).error(function (data, status) {
            });
        
	};
    
    $scope.SkipValidation = function(value) {
        return $sce.trustAsHtml(value);
    };
    
    $scope.getOptions2 = function(projectId){
        
        activityAPI.getBelongProject(projectId).success(function (data) {
                $scope.users = data;
            }).error(function (data, status) {
            });
    }
    
    loadActivity();
    
});