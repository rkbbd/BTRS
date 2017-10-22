/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="../Main.js" />

//BusSchedules
app.service('PassengerService', function ($http) {
    this.GetPassengerSvc = function () {
        return $http.get(PassengerAddress);
    }
    this.AddPassengerSvc = function (obj) {
        return $http.post(PassengerAddress, obj);
    }
    this.UpdatePassengerSvc = function (obj) {
        return $http.put(PassengerAddress + '(' + obj.Id + ')', obj);
    }
    this.DeletePassengerSvc = function (id) {
        return $http.delete(PassengerAddress + '(' + id + ')');
    }
})
