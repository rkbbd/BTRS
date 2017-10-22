/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="../Main.js" />

//Contract
app.controller('ContractCtrl', function ($scope, ContractService)
{
    GetContract();
    clear();
    function GetContract()
    {
        ContractService.GetContractSve().then(function (Contract)
        {
            $scope.ContractList = Contract.data.value;
        })
    }
    $scope.SaveContract = function ()
    {

        if ($scope.Action == 'Send')
        {
            ContractService.AddContractSve($scope.Contract).then(function ()
            {
                alert('Thank you for your opinion!');
                GetContract();
                clear();
            })
        }
        else
        {
            ContractService.UpdateContractSve($scope.Contract).then(function ()
            {
                alert('Update');
                GetContract();
                clear();
            })
        }

    }
    $scope.DeleteContract = function (Id)
    {

        ContractService.DeleteContractSve(Id).then(function ()
        {
            alert('Delete');
            GetContract();
        })
    }
    $scope.EditContract = function (F)
    {
        $scope.Contract = F;
        $scope.Action = 'Update';
        $scope.Back = true;
    }
    function clear()
    {
        $scope.Contract = null;
        $scope.Action = 'Send';
        $scope.Back = false;
    }
    $scope.BackContract = clear;
})