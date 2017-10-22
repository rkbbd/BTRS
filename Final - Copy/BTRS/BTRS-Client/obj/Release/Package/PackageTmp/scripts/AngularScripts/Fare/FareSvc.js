/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="../Main.js" />

//Fare
app.service('FareService', function ($http) {
    this.GetFareSvc = function () {
        return $http.get(FareAddress);
    };
    this.AddFareSvc = function (obj) {
        return $http.post(FareAddress, obj);
    };
    this.UpdateFareSvc = function (obj) {
        return $http.put(FareAddress + '(' + obj.Id + ')', obj);
    };
    this.DeleteFareSvc = function (Id) {
        return $http.delete(FareAddress + '(' + Id + ')');
    };

})

