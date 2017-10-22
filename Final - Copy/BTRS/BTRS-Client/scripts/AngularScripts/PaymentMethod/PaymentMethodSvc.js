/// <reference path="../../angular.js" />
/// <reference path="../Main.js" />
/// <reference path="../../angular-route.js" />

//PaymentMethod
app.service('PaymentMethodService', function ($http) {
    this.GetPaymentMethodSvc = function () {
        return $http.get(PaymentMethodAddress);
    }
    this.AddPaymentMethodSvc = function (obj) {
        return $http.post(PaymentMethodAddress, obj);
    }
    this.UpdatePaymentMethodSvc = function (obj) {
        return $http.put(PaymentMethodAddress + '(' + obj.Id + ')', obj);
    }
    this.DeletePaymentMethodSvc = function (id) {
        return $http.delete(PaymentMethodAddress + '(' + id + ')');
    }
})
