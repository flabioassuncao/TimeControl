angular.module("timeControl").config(function ($routeProvider) {
   
   $routeProvider.when("/timer", {
		templateUrl: "js/views/timer.html",
		controller: "timerController",
	});
    
    $routeProvider.when("/activities", {
		  templateUrl: "js/views/activities.html",
		  controller: "activitiesController"
	});
    
    $routeProvider.when("/login", {
		templateUrl: "js/views/login.html",
		controller: "loginController"
    });
    
    $routeProvider.when("/register", {
		templateUrl: "js/views/register.html",
		controller: "registerController"
    });
    
    $routeProvider.when("/activities-for-projects", {
		templateUrl: "js/views/activities-for-projects.html",
		controller: "activitiesForProjectsController"
    });
    
    $routeProvider.when("/my-projects", {
		templateUrl: "js/views/my-projects.html",
		controller: "myProjectsController"
    });
    
    $routeProvider.otherwise(
    { 
        redirectTo: '/timer' 
    });
});

