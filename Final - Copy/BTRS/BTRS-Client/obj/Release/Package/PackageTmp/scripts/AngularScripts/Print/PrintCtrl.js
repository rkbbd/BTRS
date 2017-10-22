/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="../Transaction/TransactionSvc.js" />

app.controller('PrintCtrl', function ($scope, $http, TransactionService)
{
    $scope.CancelTicket = function (Id)
    {
        if (confirm("Are you sure?"))
        {
            TransactionService.DeleteTransactionSvc(Id).then(function ()
            {
                alert('Cancel Ticket Successfully!!!');
            })
        } else
        {
            alert("Thanks for your choices, Stay with us!!")
        }
        
    }
   
    $scope.Print = function ()
    {
        var Ticket = $scope.Search.Ticket;

        var TransAddress = 'http://btms-server.azurewebsites.net/odata/Transactions()?$filter=TicketNo%20eq%20%27' + Ticket + '%27';
        var BusAddress = 'http://btms-server.azurewebsites.net/odata/Buses';
        var LAddress = 'http://btms-server.azurewebsites.net/odata/Locations';

        //Search with TicketNo
        $http.get(TransAddress).then(function (res)
        {
            if (res.data.value == "")
            {
                alert("Ticket no doesn't match!!")
            }
            else
            {
                $scope.TransactionList = res.data.value;
            }
            
        });

        //Location
        $http.get(LAddress).then(function (res)
        {
            $scope.LList = res.data.value;
        });

        //BusService
        $http.get(BusAddress).then(function (res)
        {
            $scope.PBusList = res.data.value;
        });

    }
})