var app = angular.module("timeControl", ["ngMessages", 'ngRoute', 'LocalStorageModule', 'ngSanitize', 'ngMaterial', 'ngAnimate', 'ui.bootstrap'] );


app.run(['activityAPI', function (activityAPI) {
    activityAPI.fillAuthData();
}]);
