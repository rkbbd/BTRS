/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="../Main.js" />

//Location
app.service('LocationService', function ($http)
{
    this.GetLocationSve = function ()
    {
        return $http.get(LocationAddress);
    }
    this.AddLocationSve = function (obj)
    {
        return $http.post(LocationAddress,obj);
    }
    this.UpdateLocationSve = function (obj)
    {
        return $http.put(LocationAddress +'(' +obj.Id + ')', obj );
    }
    this.DeleteLocationSve = function (id)
    {
        return $http.delete(LocationAddress + '('+ id + ')');
    }

})


