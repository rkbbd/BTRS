/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="BusScheduleSvc.js" />

app.controller('BusScheduleCtrl', function ($scope, BusScheduleService, LocationService)
{
    GetBusSchedule();
    clear()
    function GetBusSchedule() {
        BusScheduleService.GetBusScheduleSvc().then(function (BusSchedule)
        {
            $scope.BusScheduleList = BusSchedule.data.value;
        });
        LocationService.GetLocationSve().then(function (Location)
        {
            $scope.LocationList = Location.data.value;
        });
    }
    $scope.SaveBusSchedule = function () {

        if ($scope.Action == 'Save')
        {
            BusScheduleService.AddBusScheduleSvc($scope.BusSchedule).then(function ()
            {
                alert('Insert');
                GetBusSchedule();
                clear();
            })
        }
        else
        {
            BusScheduleService.UpdateBusScheduleSvc($scope.BusSchedule).then(function ()
            {
                alert('Update');
                GetBusSchedule();
                clear();
            })
        }
       
    }
    $scope.DeleteBusSchedule = function (Id) {

        BusScheduleService.DeleteBusScheduleSvc(Id).then(function () {
            alert('Delete');
            GetBusSchedule();
        })
    }
    $scope.EditBusSchedule = function (b) {
        $scope.BusSchedule = b;
        $scope.Action = 'Update';
        $scope.Back = true;
    }
    function clear()
    {
        $scope.Action = 'Save';
        $scope.BusSchedule = null;
        $scope.Back = false;
        
    }
    $scope.BackBusSchedule = clear;
})