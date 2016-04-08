angular.module("timeControl").controller("loginController", function ($scope, activityAPI, serialGenerator, addHour, $timeout, $location, $q, localStorageService) {

    $scope.alert = true;

    $scope.login = function (loginData) {
        activityAPI.login(loginData).success(function (response) {

            $location.path('/timer');

        }).error(function (data) {
            $scope.alert = false;
			$scope.message = "Verifique seus dados!";
		});
    };
});