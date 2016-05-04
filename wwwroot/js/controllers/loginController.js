angular.module("timeControl").controller("loginController", function ($scope, activityAPI, functionsForHours, $timeout, $location, $q, localStorageService) {

    $scope.alert = true;

    $scope.login = function (loginData) {
        activityAPI.login(loginData).success(function (response) {
            activityAPI.recuperarIdUser(loginData).success(function (idUser) {
                localStorageService.set('authorizationData', { userName: loginData.Email, authenticationUser: true, userId: idUser.UserId  });
                $location.path('/timer');
            });
        }).error(function (data) {
            $scope.alert = false;
			$scope.message = data.Error[0];
		});
    };
});