/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="../Main.js" />

//BusSchedules
app.service('BusScheduleService', function ($http) {
    this.GetBusScheduleSvc = function () {
        return $http.get(BusScheduleAddress);
    }
    this.AddBusScheduleSvc = function (obj) {
        return $http.post(BusScheduleAddress,obj);
    }
    this.UpdateBusScheduleSvc = function (obj) {
        return $http.put(BusScheduleAddress+'('+obj.Id + ')',obj);
    }
    this.DeleteBusScheduleSvc = function (id) {
        return $http.delete(BusScheduleAddress + '('+id +')');
    }
})
