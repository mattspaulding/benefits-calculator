
var app = angular.module('App', ['ui.router','ngResource', 'App.employeesControllers', 'App.employeesServices', 'LocalStorageModule', 'ngMaterial', 'ngMessages', 'ngMdIcons']);

app.config(function ($stateProvider,$urlRouterProvider) {

    $urlRouterProvider.otherwise('/');

    $stateProvider.state("home", {
        url: "/",
        controller: "homeController",
        templateUrl: "/app/views/home/index.html"
    })

    $stateProvider.state("user", {
        url: "/user", 
        controller: "userController",
        templateUrl: "/app/views/user/index.html"
    });

    $stateProvider.state("userEdit", {
        url: "/user/edit/:userId", 
        controller: "userController",
        templateUrl: "/app/views/user/edit.html"
    });

    $stateProvider.state("login", {
        url: "/login", 
        controller: "loginController",
        templateUrl: "/app/views/account/login.html"
    });

    $stateProvider.state("signup", {
        url: "/signup", 
        controller: "signupController",
        templateUrl: "/app/views/account/signup.html"
    });

    $stateProvider.state("employees", {
        url: "/employees", 
        controller: "employeesIndexController",
        templateUrl: "/app/views/employees/index.html"
    });

    $stateProvider.state("employeesAdd", {
        url: "/employees/add", 
        controller: "employeesAddController",
        templateUrl: "/app/views/employees/add.html"
    });

    $stateProvider.state("employeesEdit", {
        url: "/employees/edit/:employeeId", 
        controller: "employeesEditController",
        templateUrl: "/app/views/employees/edit.html"
    });

    $stateProvider.state("employeesDetails", {
        url: "/employees/details/:employeeId", 
        controller: "employeesDetailsController",
        templateUrl: "/app/views/employees/details.html"
    });

});

app.config(function ($mdThemingProvider) {
    $mdThemingProvider.theme('default')
      .primaryPalette('deep-purple')
      .accentPalette('amber')
      .warnPalette('red')
    .backgroundPalette('grey');
});

var serviceBase = 'http://localhost:49512/';
var clientId = 'consoleApp';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: clientId
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


