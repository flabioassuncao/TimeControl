angular.module("timeControl").controller("registerController", function ($scope, activityAPI, functionsForHours, $timeout, $location, $q, localStorageService) {
    
    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.alert = true;
    
    
    $scope.signUp = function (registration) {
        
        activityAPI.signUp(registration).success(function (response) {
            $scope.savedSuccessfully = true;
            $scope.message = "Registration successful, wait!";
            startTimer();
        }).error(function (data) {
            $scope.alert = false;
			$scope.message = data;
		});
    };
    
    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }
});