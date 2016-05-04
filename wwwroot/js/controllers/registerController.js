angular.module("timeControl").controller("registerController", function ($scope, activityAPI, functionsForHours, $timeout, $location, $q, localStorageService) {
    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.alert = true;
    var user = {};
    
    $scope.signUp = function (registration) {
        activityAPI.signUp(registration).success(function (response) {
            user.UserName = registration.Email;
            activityAPI.createUser(user);
            $scope.savedSuccessfully = true;
            $scope.message = "Registration successful, wait!";
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path('/login');
            }, 2000);
        }).error(function (data) {
            $scope.alert = false;
			$scope.message = data;
		});
    };
});