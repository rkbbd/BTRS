/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="../Main.js" />

//Transaction
app.service('TransactionService', function ($http) {
    this.GetTransactionSvc = function () {
        return $http.get(TransactionAddress);
    }
    this.AddTransactionSvc = function (obj) {
        return $http.post(TransactionAddress, obj);
    }
    this.UpdateTransactionSvc = function (obj) {
        return $http.put(TransactionAddress + '(' + obj.Id + ')', obj);
    }
    this.DeleteTransactionSvc = function (id) {
        return $http.delete(TransactionAddress + '(' + id + ')');
    }

})
