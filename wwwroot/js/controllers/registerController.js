angular.module("timeControl").controller("registerController", function ($scope, activityAPI, serialGenerator, addHour, $timeout, $location, $q, localStorageService) {
    
    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.alert = true;
    
    
    $scope.signUp = function (registration) {
        
        activityAPI.signUp(registration).success(function (response) {
            $scope.savedSuccessfully = true;
            $scope.message = "Cadastro realizado com sucesso, aguarde!";
            startTimer();
        }).error(function (data) {
            $scope.alert = false;
			$scope.message = "Favor verificar os campo preenchidos, e certifique a senha inserida!";
		});
    };
    
    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }
});