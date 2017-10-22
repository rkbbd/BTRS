/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="TransactionSvc.js" />

//Transaction
app.controller('TransactionCtrl', function ($scope, TransactionService) {
    GetTransaction();
    clear();
    function GetTransaction() {
        TransactionService.GetTransactionSvc().then(function (Transaction) {
            $scope.TransactionList = Transaction.data.value;
        })
    }
    $scope.SaveTransaction = function () {
        if ($scope.Action == 'Save')
        {
            TransactionService.AddTransactionSvc($scope.Transaction).then(function ()
            {
                alert('Insert');
                GetTransaction();
                clear();
            })
        } else
        {

            TransactionService.UpdateTransactionSvc($scope.Transaction).then(function ()
            {
                alert('Update');
                GetTransaction();
                clear();
            })
        }
        
    }
    $scope.DeleteTransaction = function (Id) {

        TransactionService.DeleteTransactionSvc(Id).then(function () {
            alert('Delete');
            GetTransaction();
        })
    }
    $scope.EditTransaction = function (F) {
        $scope.Transaction = F;
        $scope.Action = 'Update';
        $scope.Back = true;
    }
    function clear() {
        $scope.Transaction = null;
        $scope.Action = 'Save';
        $scope.Back = false;
    }
    $scope.BackTransaction = clear;
})