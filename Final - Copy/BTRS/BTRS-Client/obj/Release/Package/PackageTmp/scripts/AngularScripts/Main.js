/// <reference path="../angular.js" />
/// <reference path="../angular-route.js" />
/// <reference path="C:\Users\MD. Rakib HASAN\Source\Workspaces\BTRS\BTRS\BTRS-Client\IndexHtml/MainPage.html" />

var app = angular.module('myApp', ['ngRoute']);
var busAddress = 'http://btms-server.azurewebsites.net/odata/Buses';
var BusScheduleAddress = 'http://btms-server.azurewebsites.net/odata/BusSchedules/';
var FareAddress = 'http://btms-server.azurewebsites.net/odata/Fares/';
var LocationAddress = 'http://btms-server.azurewebsites.net/odata/Locations/';
var PaymentAddress = 'http://btms-server.azurewebsites.net/odata/Payments/';
var PaymentMethodAddress = 'http://btms-server.azurewebsites.net/odata/PaymentMethods/';
var TransactionAddress = 'http://btms-server.azurewebsites.net/odata/Transactions/';
var PassengerAddress = 'http://btms-server.azurewebsites.net/odata/Passengers/';
var ContractAddress = 'http://btms-server.azurewebsites.net/odata/Contracts/';
//var adminUrl = "http://btms-server.azurewebsites.net/Token";
//var createUser = "http://btms-server.azurewebsites.net/api/Account/Register";

app.config(function ($locationProvider, $routeProvider) {
    $routeProvider
    .when('/', { templateUrl: '/Layout/Main.html', controller: 'SearchCtrl' })
    .when('/Contacts', { templateUrl: '/Index/Contacts.html' })
    .when('/About', { templateUrl: '/Index/About.html' })
    .when('/PrivacyPolicy', { templateUrl: '/Layout/PrivacyPolicy.html' })
    .when('/BusService', { templateUrl: '/Htmlpage/BusService.html', controller: 'BusCtrl' })
    .when('/BusSchedule', { templateUrl: '/Htmlpage/BusSchedule.html', controller: 'BusScheduleCtrl' })
    .when('/Fare', { templateUrl: '/Htmlpage/Fare.html', controller: 'FareCtrl' })
    .when('/Location', { templateUrl: '/Htmlpage/Location.html', controller: 'LocationCtrl' })
    .when('/Payment', { templateUrl: '/Htmlpage/Payment.html', controller: 'PaymentCtrl' })
    .when('/PaymentMethod', { templateUrl: '/Htmlpage/PaymentMethod.html', controller: 'PaymentMethodCtrl' })
    .when('/Transaction', { templateUrl: '/Htmlpage/Transaction.html', controller: 'TransactionCtrl' })
    .when('/Passenger', { templateUrl: '/Htmlpage/Passenger.html', controller: 'PassengerCtrl' })
    .when('/Search', { templateUrl: '/Htmlpage/Search.html', controller: 'SearchCtrl' })
    .when('/Print', { templateUrl: '/Htmlpage/PrintPage.html', controller: 'PrintCtrl' })
    .when('/About', { templateUrl: '/Htmlpage/About.html' })
    .when('/Admin', { templateUrl: '/Htmlpage/Admin.html' })
    .when('/Reg', { templateUrl: '/Layout/SignUp.html' })
    .when('/Log', { templateUrl: '/Layout/SignIn.html' })
    .when('/Contract', { templateUrl: '/Htmlpage/ContractPage.html'})
    .otherwise({ redirectTo: '/' });

    $locationProvider.hashPrefix('');
});
//Authetication
app.controller("MainCtrl", function ($scope, $http, $location, apiCallSvc, authLocalStorageSvc, loginSvc)
{
    $scope.model = {};
    $scope.loginModel = {};
    $scope.userError = null;
    $scope.userInfo = null;
    $scope.confirm;
    $scope.allUser = [];

    $scope.auth = authLocalStorageSvc.getAuthData();
    
    /////login tasks
    $scope.loginErr = "";
    $scope.signin = function () {
        //console.log("ok");
        loginSvc.login($scope.loginModel.username, $scope.loginModel.password)
        .then(function (result) {
            console.log(result);
            $scope.loginErr = "";
            authLocalStorageSvc.saveAuthData($scope.loginModel.username, result.data.access_token);
            console.log(result.data.access_token);
            $scope.loginModel = null;
            $scope.auth = authLocalStorageSvc.getAuthData();
            $location.path("/Admin");


        }, function (res) {
            console.log(res);
            $scope.loginErr = res.data.error_description;
        })

    },
    $scope.signout = function () {
        authLocalStorageSvc.removeAuthData();
        $scope.auth = {};
        $location.path("/login");
    }
    $scope.register = function () {
        console.log($scope.userInfo);
        loginSvc.register($scope.userInfo.Email, $scope.userInfo.Password, $scope.userInfo.ConfirmPassword)
        .then(function () {
            $scope.userInfo = null;
            $scope.regform.$setPristine();
            $scope.regform.$setValidity();
            $scope.regform.$setUntouched();
            $scope.confirm = true;
          
        }, function (err) {
            $scope.userError = "An error is occured while posting the new data";
            console.log(err);
        });
    }
})

