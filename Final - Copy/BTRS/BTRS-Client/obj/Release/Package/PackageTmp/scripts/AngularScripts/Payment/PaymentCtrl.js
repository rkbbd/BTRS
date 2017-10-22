/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="PaymentSvc.js" />
//Payment
app.controller('PaymentCtrl', function ($scope, PaymentService, PaymentMethodService) {
    GetPayment();
    clear();
    function GetPayment() {
        PaymentService.GetPaymentSvc().then(function (Payment) {
            $scope.PaymentList = Payment.data.value;
        })
        PaymentMethodService.GetPaymentMethodSvc().then(function (PaymentMethod) {
            $scope.PaymentMethodList = PaymentMethod.data.value;
        })
    }
    $scope.SavePayment = function () {

        if ($scope.Action == 'Save')
        {
            PaymentService.AddPaymentSvc($scope.Payment).then(function ()
            {
                alert('Insert');
                GetPayment();
                clear();
            })
        } else
        {
            PaymentService.UpdatePaymentSvc($scope.Payment).then(function ()
            {
                alert('Update');
                GetPayment();
                clear();
            })
        }
      
    }
    $scope.DeletePayment = function (Id) {

        PaymentService.DeletePaymentSvc(Id).then(function () {
            alert('Delete');
            GetPayment();
        })
    }
    $scope.EditPayment = function (F) {
        $scope.Payment = F;
        $scope.Action = 'Update';
        $scope.Back = true;
    }
    function clear() {
        $scope.Payment = null;
        $scope.Action = 'Save';
        $scope.Back = false;
    }
    $scope.BackPayment = clear;
})