// angular.module("timeControl", ["ngMessages", 'ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

var app = angular.module("timeControl", ["ngMessages", 'ngRoute', 'LocalStorageModule'] );


app.run(['activityAPI', function (activityAPI) {
    activityAPI.fillAuthData();
}]);
