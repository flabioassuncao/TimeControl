var app = angular.module("timeControl", ["ngMessages", 'ngRoute', 'LocalStorageModule', 'ngSanitize', 'ngMaterial', 'ngAnimate', 'ui.bootstrap'] );


app.run(['activityAPI', function (activityAPI) {
    activityAPI.fillAuthData();
}]);

app.directive('autoComplete', function($timeout) {
    return function(scope, iElement, iAttrs) {
            iElement.autocomplete({
                source: scope[iAttrs.uiItems],
                select: function() {
                    $timeout(function() {
                      iElement.trigger('input');
                    }, 0);
                }
            });
    };
});
