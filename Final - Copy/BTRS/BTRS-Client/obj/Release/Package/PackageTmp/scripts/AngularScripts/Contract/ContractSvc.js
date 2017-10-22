/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="../Main.js" />

//Contract
app.service('ContractService', function ($http)
{
    this.GetContractSve = function ()
    {
        return $http.get(ContractAddress);
    }
    this.AddContractSve = function (obj)
    {
        return $http.post(ContractAddress, obj);
    }
    this.UpdateContractSve = function (obj)
    {
        return $http.put(ContractAddress + '(' + obj.Id + ')', obj);
    }
    this.DeleteContractSve = function (id)
    {
        return $http.delete(ContractAddress + '(' + id + ')');
    }

})