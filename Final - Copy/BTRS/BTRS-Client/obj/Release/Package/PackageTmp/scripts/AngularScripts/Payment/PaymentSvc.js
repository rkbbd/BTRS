/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="../Main.js" />

//Payment
app.service('PaymentService', function ($http) {
    this.GetPaymentSvc = function () {
        return $http.get(PaymentAddress);
    }
    this.AddPaymentSvc = function (obj) {
        return $http.post(PaymentAddress,obj);
    }
    this.UpdatePaymentSvc = function (obj) {
        return $http.put(PaymentAddress + '('+ obj.Id + ')', obj)
    }
    this.DeletePaymentSvc = function (id) {
        return $http.delete(PaymentAddress + '(' + id + ')');
    }
})
