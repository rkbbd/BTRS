/// <reference path="../../angular.js" />
/// <reference path="PaymentMethodSvc.js" />
/// <reference path="../../angular-route.js" />

//PaymentMethod

app.controller('PaymentMethodCtrl', function ($scope, PaymentMethodService) {
    GetPaymentMethod();
    clear();
    function GetPaymentMethod() {
        PaymentMethodService.GetPaymentMethodSvc().then(function (PaymentMethod) {
            $scope.PaymentMethodList = PaymentMethod.data.value;
        })
    }
    $scope.SavePaymentMethod = function () {
        if ($scope.Action == 'Save')
        {
            PaymentMethodService.AddPaymentMethodSvc($scope.PaymentMethod).then(function ()
            {
                alert('Insert');
                GetPaymentMethod();
                clear();
            })
        } else
        {
            PaymentMethodService.UpdatePaymentMethodSvc($scope.PaymentMethod).then(function ()
            {
                alert('Update');
                GetPaymentMethod();
                clear();
            })
        }
        
    }
    $scope.DeletePaymentMethod = function (Id) {

        PaymentMethodService.DeletePaymentMethodSvc(Id).then(function () {
            alert('Delete');
            GetPaymentMethod();
        })
    }
    $scope.EditPaymentMethod = function (F) {
        $scope.PaymentMethod = F;
        $scope.Action = 'Update';
        $scope.Back = true;
    }
    function clear() {
        $scope.PaymentMethod = null;
        $scope.Action = 'Save';
        $scope.Back = false;
    }
    $scope.BackPaymentMethod = clear;
})