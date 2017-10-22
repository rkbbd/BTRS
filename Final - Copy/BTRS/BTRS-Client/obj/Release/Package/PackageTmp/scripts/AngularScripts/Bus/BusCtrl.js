/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="BusSvc.js" />
/// <reference path="../BusSchedule/BusScheduleCtrl.js" />


app.controller('BusCtrl', function ($scope, BusService, BusScheduleService) {
    clear();
    GetBus();
    function GetBus() {
        BusService.GetSvc().then(function (res) {
            $scope.BusesList = res.data.value;
        })
        BusScheduleService.GetBusScheduleSvc().then(function (BusSchedule) {
            $scope.BusScheduleList = BusSchedule.data.value;
        })

    }
    $scope.SaveBus = function () {
        if ($scope.Action == 'Save') {
            BusService.AddSvc($scope.Bus).then(function () {
                alert('Inserted');
                GetBus();
                clear();
            })
        }
        else {
            BusService.UpdateSvc($scope.Bus).then(function () {
                alert('Updated');
                GetBus();
                clear();
            })
        }
    }

    $scope.DeleteBus = function (Id) {

        BusService.DeleteSvc(Id).then(function () {
            alert('Deleted');
            GetBus();
        })
    }
    $scope.EditBus = function (b) {
        $scope.Bus = b;
        $scope.Action = 'Update';
        $scope.Back = true;
    }
    function clear() {
        $scope.Bus = null;
        $scope.Action = 'Save';
        $scope.Back = false;
    }
    $scope.BackBus = clear;
})