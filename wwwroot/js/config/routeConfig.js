angular.module("timeControl").config(function ($routeProvider) {
   
   $routeProvider.when("/timer", {
		templateUrl: "js/views/timer.html",
		controller: "timerController",
	});
    
    $routeProvider.when("/projects", {
		  templateUrl: "js/views/projects.html",
		  controller: "projectController"
	});
    
    $routeProvider.when("/login", {
		templateUrl: "js/views/login.html",
		controller: "loginController"
    });
    
    $routeProvider.when("/register", {
		templateUrl: "js/views/register.html",
		controller: "registerController"
    });
    
    $routeProvider.otherwise(
    { 
        redirectTo: '/login' 
    });
});

