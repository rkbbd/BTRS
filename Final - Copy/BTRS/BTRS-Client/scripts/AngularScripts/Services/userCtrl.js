angular.module("myApp")
.controller("userCtrl", function ($scope, $http, $location, apiCallSvc, authLocalStorageSvc, loginSvc, userListUrl) {
    $scope.userError = null;
    $scope.userInfo = null;
    $scope.confirm;
    $scope.allUser = [];
    apiCallSvc.get(userListUrl, { Authorization: "Bearer " + $scope.auth.accToken }, null)
    .then(function (result) {
        //$scope.model.users = result.data;
        $scope.allUser = result.data;
        //console.log(result);
        console.log(result);
        console.log($scope.allUser);
    },function (err) {
        $scope.userError = "Error encountered";

    });
    $scope.register = function () {
        loginSvc.register($scope.userInfo.Email, $scope.userInfo.Password, $scope.userInfo.ConfirmPassword)
        .then(function () {
            $scope.userInfo = null;
            $scope.regform.$setPristine();
            $scope.regform.$setValidity();
            $scope.regform.$setUntouched();
            $scope.confirm = true;
            apiCallSvc.get(userListUrl, { Authorization: "Bearer " + $scope.auth.accToken }, null)
            .then(function (result) {
                $scope.model.users = result.data;
            },function (err) {
                console.log(err);
                $scope.confirm = false;
            });
        },function (err) {
            $scope.userError = "An error is occured while posting the new data";
            console.log(err);
        });
    }
});