/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="../Main.js" />

//Transaction
app.service('SearchService', function ($http) {
    this.GetSearchSvc = function () {
        return $http.get(TransactionAddress);
    }
    this.AddSearchSvc = function (obj) {
        return $http.post(TransactionAddress, obj);
    }
})
