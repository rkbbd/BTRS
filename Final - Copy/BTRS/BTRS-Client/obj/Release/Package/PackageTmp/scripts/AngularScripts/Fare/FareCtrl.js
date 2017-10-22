/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="FareSvc.js" />

//Fare
app.controller('FareCtrl', function ($scope, FareService, BusService, LocationService)
{
    GetFare();
    clear();
    function GetFare() {
        FareService.GetFareSvc().then(function (Fare) {
            $scope.FareList = Fare.data.value;
        })
        BusService.GetSvc().then(function (res){
            $scope.BusesList = res.data.value;
        })
        LocationService.GetLocationSve().then(function (Location) {
            $scope.LocationList = Location.data.value;
        })
    }
    $scope.SaveFare = function () {

        if ($scope.Action == 'Save')
        {
            FareService.AddFareSvc($scope.Fare).then(function ()
            {
                alert('Insert');
                GetFare();
                clear();
            })
        }
        else
        {
            FareService.UpdateFareSvc($scope.Fare).then(function ()
            {
                alert('Update');
                GetFare();
                clear();
            })
        }
      
    }

    $scope.DeleteFare = function (Id) {

       FareService.DeleteFareSvc(Id).then(function () {
            alert('Delete');
            GetFare();
        })
    }
    $scope.EditFare = function (F) {
        $scope.Fare = F;
        $scope.Action = 'Update'
        $scope.Back = true;
    }

    function clear() {
        $scope.Fare = null;
        $scope.Action = 'Save'
        $scope.Back = false;
    }
    $scope.BackFare = clear;
})