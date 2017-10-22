/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="../Main.js" />

//Location



app.controller('LocationCtrl', function ($scope, LocationService) {
    GetLocation();
    clear();
    function GetLocation() {
        LocationService.GetLocationSve().then(function (Location)
        {
            $scope.LocationList = Location.data.value;
        })
    }
    $scope.SaveLocation = function () {

        if ($scope.Action == 'Save')
        {
            LocationService.AddLocationSve($scope.Location).then(function ()
            {
                alert('Insert');
                GetLocation();
                clear();
            })
        }
        else
        {
            LocationService.UpdateLocationSve($scope.Location).then(function ()
            {
                alert('Update');
                GetLocation();
                clear();
            })
        }
       
    }
    $scope.DeleteLocation = function (Id) {

        LocationService.DeleteLocationSve(Id).then(function () {
            alert('Delete');
            GetLocation();
        })
    }
    $scope.EditLocation = function (F) {
        $scope.Location = F;
        $scope.Action = 'Update';
        $scope.Back = true;
    }
    function clear() {
        $scope.Location = null;
        $scope.Action = 'Save';
        $scope.Back = false;
    }
    $scope.BackLocation = clear;
})