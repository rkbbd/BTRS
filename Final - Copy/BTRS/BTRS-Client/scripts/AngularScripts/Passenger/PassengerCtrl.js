/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="PassengerSvc.js" />

app.controller('PassengerCtrl', function ($scope, PassengerService)
{
    GetPassenger();
    clear()
    function GetPassenger() {
        PassengerService.GetPassengerSvc().then(function (Passenger)
        {
            $scope.PassengerList = Passenger.data.value;
        });
        
    }
    $scope.SavePassenger = function () {

        if ($scope.Action == 'Save')
        {
            PassengerService.AddPassengerSvc($scope.Passenger).then(function ()
            {
                alert('Insert');
                GetPassenger();
                clear();
            })
        }
        else
        {
            PassengerService.UpdatePassengerSvc($scope.Passenger).then(function ()
            {
                alert('Update');
                GetPassenger();
                clear();
            })
        }
       
    }
    $scope.DeletePassenger = function (Id) {

        PassengerService.DeletePassengerSvc(Id).then(function () {
            alert('Delete');
            GetPassenger();
        })
    }
    $scope.EditPassenger = function (p) {
        $scope.Passenger = p;
        $scope.Action = 'Update';
        $scope.Back = true;
    }
    function clear()
    {
        $scope.Action = 'Save';
        $scope.Passenger = null;
        $scope.Back = false;
        
    }
    $scope.BackPassenger = clear;
})