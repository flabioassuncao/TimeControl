angular.module("timeControl").controller("indexController", function ($scope, activityAPI, $location) {
    
    $scope.logOut = function () {
        activityAPI.logOut();
    }

    $scope.authentication = activityAPI.authentication;
    
    var authen = function (){
        activityAPI.verificarAutenticacao();
    }
    
    authen();
    
});