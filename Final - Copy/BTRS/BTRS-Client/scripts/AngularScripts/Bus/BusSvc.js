/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="../Main.js" />

//Buses
app.service('BusService', function ($http) {
    this.GetSvc = function () {
        return $http.get(busAddress);
    }
    this.AddSvc = function (obj) {
        return $http.post(busAddress, obj);
    }
    this.UpdateSvc = function (obj) {
        return $http.put(busAddress + '(' + obj.Id + ')', obj);
    }
    this.DeleteSvc = function (id) {
        return $http.delete(busAddress + '(' + id + ')');
    }
})
