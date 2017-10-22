/// <reference path="../../angular.js" />
/// <reference path="../../angular-route.js" />
/// <reference path="TransactionSvc.js" />

//Transaction

app.controller('SearchCtrl', function ($scope, $http, filterFilter, SearchService, BusService, LocationService, BusScheduleService, FareService, PaymentService)
{
    GetSearch();
    clear();
    var searchResult;                                   //variable use in block seats
    function GetSearch()                                //Get All Table Information for Select Dropdown
    {
        SearchService.GetSearchSvc().then(function (Search)
        {
            $scope.SearchList = Search.data.value;
            searchResult = Search.data.value;
        });
        BusService.GetSvc().then(function (res)
        {
            $scope.BusesList = res.data.value;
        });
        LocationService.GetLocationSve().then(function (Location)
        {
            $scope.LocationList = Location.data.value;
        });
        BusScheduleService.GetBusScheduleSvc().then(function (BusSchedule)
        {
            $scope.BusScheduleList = BusSchedule.data.value;
        });
        FareService.GetFareSvc().then(function (Fare)
        {
            $scope.FareList = Fare.data.value;
        });
        PaymentService.GetPaymentSvc().then(function (Payment)
        {
            $scope.PaymentList = Payment.data.value;
        });
       
    }
    $scope.SaveSearch = function ()                                 //Purchase Ticket save in transaction table
    {
        var Destination = $scope.Search.DestinationLocationId;
        var Departure = $scope.Search.DepartureLocationId;
        var Bus = $scope.Search.BusId;

        var FareAddressSearch = 'http://btms-server.azurewebsites.net/odata/Fares()?$filter=DepartureLocationId%20eq%20' + Departure + '%20and%20DestinationLocationId%20eq%20' + Destination + '%20and%20BusId%20eq%20' + Bus; //Search From OdataController for fare id

        $http.get(FareAddressSearch).then(function (res)
        {
            $scope.SearchFareList = res.data.value;                    // fare list filter
        });

        $scope.Search.FareId = $scope.SearchFareList[0].Id;            //FairId From Search

        if ($scope.Search.ReservedSeat == null | $scope.Search.ReservedSeat == "") // check Reserve seat empty or null
        {
            alert('You forget to select seat')
        }
        else if ($scope.Search.ReservedSeat.length <= 8)                  //Check seat not more than 4
        {
            PaymentService.AddPaymentSvc($scope.Payment).then(function (res) //if Payment data insert successfully, than save seachTable data with Payment Table's Id
            {
                $scope.Search.PaymentId = res.data.Id; //PaymentId from PaymentTable

                SearchService.AddSearchSvc($scope.Search).then(function (res) //Save SeachData
                {
                    $scope.TicketNoPrint = res.data.TicketNo;                // http.post success callback data for ticket Print (without value)
                    $scope.TicketPurchase = true;                            //Hide and show Div
                    GetSearch();
                    clear();

                    //Ticket Print
                    var Ticket = res.data.TicketNo;

                    var TransAddress = 'http://btms-server.azurewebsites.net/odata/Transactions()?$filter=TicketNo%20eq%20%27' + Ticket + '%27'; //Filter by Ticket No
                    var BusAddress = 'http://btms-server.azurewebsites.net/odata/Buses';
                    var LAddress = 'http://btms-server.azurewebsites.net/odata/Locations';

                    //Search with TicketNo
                    $http.get(TransAddress).then(function (res)
                    {
                        $scope.TransactionList = res.data.value; //Filter row get
                    });

                    //Location
                    $http.get(LAddress).then(function (res)     //filter on html
                    {
                        $scope.LList = res.data.value;
                    });

                    //BusService
                    $http.get(BusAddress).then(function (res)
                    {
                        $scope.PBusList = res.data.value;
                    });

                })

            })

           
        }
        else
        {
            alert('You select more than 4 seat, Please cancel seat and try again')
        }
        
    }
    //=======Block seat========
    $scope.busSeat = function ()
    {
        
        //New
        var seatAll;
        if ($scope.Search.DepartureLocationId == "1" && $scope.Search.DestinationLocationId == "3") //Dhaka- Cox's bazar
        {
            var Result = filterFilter(searchResult, { BusId: $scope.Search.BusId, Time: $scope.Search.Time, DepartureLocationId: 1, DestinationLocationId: 2, TravelDate: $scope.Search.TravelDate }) //search all bookseat
            //var seatAll = "";
            for (var i = 0; i < Result.length; i++) {
                seatAll += Result[i].ReservedSeat;                                  //all seat store in variable
                //alert(SeatAll);
            }
        }
        else if ($scope.Search.DepartureLocationId == "3" && $scope.Search.DestinationLocationId == "1") //Cox's bazar- Dhaka
        {
            var Result = filterFilter(searchResult, { BusId: $scope.Search.BusId, Time: $scope.Search.Time, DepartureLocationId: 2, DestinationLocationId: 1, TravelDate: $scope.Search.TravelDate }) //search all bookseat
            //var seatAll = "";
            for (var i = 0; i < Result.length; i++) {
                seatAll += Result[i].ReservedSeat;                                  //all seat store in variable
                //alert(SeatAll);
            }
        }
        else if ($scope.Search.DepartureLocationId == "1" && $scope.Search.DestinationLocationId == "2") //Dhaka - Chittagong
        {
            var Result = filterFilter(searchResult, { BusId: $scope.Search.BusId, Time: $scope.Search.Time, DepartureLocationId: 1, DestinationLocationId: 3, TravelDate: $scope.Search.TravelDate }) //search all bookseat
            //var seatAll = "";
            for (var i = 0; i < Result.length; i++) {
                seatAll += Result[i].ReservedSeat;                                  //all seat store in variable
                //alert(SeatAll);
            }
        }
        else if ($scope.Search.DepartureLocationId == "2" && $scope.Search.DestinationLocationId == "1") //Chittagong - Dhaka
        {
            var Result = filterFilter(searchResult, { BusId: $scope.Search.BusId, Time: $scope.Search.Time, DepartureLocationId: 3, DestinationLocationId: 1, TravelDate: $scope.Search.TravelDate }) //search all bookseat
            //var seatAll = "";
            for (var i = 0; i < Result.length; i++) {
                seatAll += Result[i].ReservedSeat;                                  //all seat store in variable
                //alert(SeatAll);
            }
        }
        else if ($scope.Search.DepartureLocationId == "2" && $scope.Search.DestinationLocationId == "3") //Chittagong - Cox's bazar
        {
            var Result = filterFilter(searchResult, { BusId: $scope.Search.BusId, Time: $scope.Search.Time, DepartureLocationId: 1, DestinationLocationId: 3, TravelDate: $scope.Search.TravelDate }) //search all bookseat
            //var seatAll = "";
            for (var i = 0; i < Result.length; i++) {
                seatAll += Result[i].ReservedSeat;                                  //all seat store in variable
                //alert(SeatAll);
            }
        }
        else if ($scope.Search.DepartureLocationId == "3" && $scope.Search.DestinationLocationId == "2") //Cox's bazar - Chittagong
        {
            var Result = filterFilter(searchResult, { BusId: $scope.Search.BusId, Time: $scope.Search.Time, DepartureLocationId: 3, DestinationLocationId: 1, TravelDate: $scope.Search.TravelDate }) //search all bookseat
            //var seatAll = "";
            for (var i = 0; i < Result.length; i++) {
                seatAll += Result[i].ReservedSeat;                                  //all seat store in variable
                //alert(SeatAll);
            }
        }


        //Dhaka = 1,  Chittagong = 2,  Cox's Bazar = 3

        //End New


        var Result = filterFilter(searchResult, { BusId: $scope.Search.BusId, Time: $scope.Search.Time,  DepartureLocationId: $scope.Search.DepartureLocationId, DestinationLocationId: $scope.Search.DestinationLocationId, TravelDate: $scope.Search.TravelDate }) //search all bookseat
        
        //var seatAll = "";    // comment out for new implementation
        for (var i = 0; i < Result.length; i++)
        {
            seatAll += Result[i].ReservedSeat;                                  //all seat store in variable
        }
        $scope.ShowDiv2 = true;                                                 //div show and hide
        //=======Check A========
        if (seatAll.includes("A1"))                                             //Check Seat Contains "A1"
        {
            
            $('#A1anchor').addClass('A1Style');                                 //css class for link disable
            $('#A1anchor').html("<img src='Content/Seat/booked_seat_img.gif'>") //Replace anchor image
        }
        if (seatAll.includes("A2"))
        {
            $('#A2anchor').addClass('A1Style');
            $('#A2anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("A3"))
        {
            $('#A3anchor').addClass('A1Style');
            $('#A3anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("A4"))
        {
            $('#A4anchor').addClass('A1Style');
            $('#A4anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        //=======Check B========
        if (seatAll.includes("B1"))
        {
            $('#B1anchor').addClass('A1Style');
            $('#B1anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("B2"))
        {
            $('#B2anchor').addClass('A1Style');
            $('#B2anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("B3"))
        {
            $('#B3anchor').addClass('A1Style');
            $('#B3anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("B4"))
        {
            $('#B4anchor').addClass('A1Style');
            $('#B4anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        //=======Check C========
        if (seatAll.includes("C1"))
        {
            $('#C1anchor').addClass('A1Style');
            $('#C1anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("C2"))
        {
            $('#C2anchor').addClass('A1Style');
            $('#C2anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("C3"))
        {
            $('#C3anchor').addClass('A1Style');
            $('#C3anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("C4"))
        {
            $('#C4anchor').addClass('A1Style');
            $('#C4anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        //=======Check D========
        if (seatAll.includes("D1"))
        {
            $('#D1anchor').addClass('A1Style');
            $('#D1anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("D2"))
        {
            $('#D2anchor').addClass('A1Style');
            $('#D2anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("D3"))
        {
            $('#D3anchor').addClass('A1Style');
            $('#D3anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("D4"))
        {
            $('#D4anchor').addClass('A1Style');
            $('#D4anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        //=======Check E========
        if (seatAll.includes("E1"))
        {
            $('#E1anchor').addClass('A1Style');
            $('#E1anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("E2"))
        {
            $('#E2anchor').addClass('A1Style');
            $('#E2anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("E3"))
        {
            $('#E3anchor').addClass('A1Style');
            $('#E3anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("E4"))
        {
            $('#E4anchor').addClass('A1Style');
            $('#E4anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        //=======Check F========
        if (seatAll.includes("F1"))
        {
            $('#F1anchor').addClass('A1Style');
            $('#F1anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("F2"))
        {
            $('#F2anchor').addClass('A1Style');
            $('#F2anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("F3"))
        {
            $('#F3anchor').addClass('A1Style');
            $('#F3anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("F4"))
        {
            $('#F4anchor').addClass('A1Style');
            $('#F4anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        //=======Check G========
        if (seatAll.includes("G1"))
        {
            $('#G1anchor').addClass('A1Style');
            $('#G1anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("G2"))
        {
            $('#G2anchor').addClass('A1Style');
            $('#G2anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("G3"))
        {
            $('#G3anchor').addClass('A1Style');
            $('#G3anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("G4"))
        {
            $('#G4anchor').addClass('A1Style');
            $('#G4anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        //=======Check H========
        if (seatAll.includes("H1"))
        {
            $('#H1anchor').addClass('A1Style');
            $('#H1anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("H2"))
        {
            $('#H2anchor').addClass('A1Style');
            $('#H2anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("H3"))
        {
            $('#H3anchor').addClass('A1Style');
            $('#H3anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }
        if (seatAll.includes("H4"))
        {
            $('#H4anchor').addClass('A1Style');
            $('#H4anchor').html("<img src='Content/Seat/booked_seat_img.gif'>")
        }

        $scope.AllSeat = seatAll;
    }
    //=======Block seat========

    function clear()
    {
        $scope.Search = null;
    }

    //=============Next button============
    $scope.TimeFilter = function ()                             //dropdown menu time select>
    {
        var time = $scope.Search.Time;

        var QueryBusScheduleAddress = 'http://btms-server.azurewebsites.net/odata/BusSchedules()?$filter=DepartureTime%20eq%20%27' + time + '%27';

        $http.get(QueryBusScheduleAddress).then(function (res) //Search busScheduleID by time
        {
            $scope.QueryBusScheduleList = res.data.value;
        });
    }
    $scope.TimeFilterAgain = function ()                      //Go Button> busType div show and query bus
    {
        var QueryBusAddress = 'http://btms-server.azurewebsites.net/odata/Buses()?$filter=BusScheduleId%20eq%20' + $scope.QueryBusScheduleList[0].Id;
        $http.get(QueryBusAddress).then(function (res)
        {
            $scope.QueryBusList = res.data.value;
        });
        $scope.ShowDiv = true;
    }
    //=============Next button============

    $scope.ShowFare = function ()                           //Destination Location dropdown> fare show
    {
        var Destination = $scope.Search.DestinationLocationId;
        var Departure = $scope.Search.DepartureLocationId;
        var Bus = $scope.Search.BusId;
        var FareAddressSearch = 'http://btms-server.azurewebsites.net/odata/Fares()?$filter=DepartureLocationId%20eq%20' + Departure + '%20and%20DestinationLocationId%20eq%20' + Destination + '%20and%20BusId%20eq%20' + Bus; //Search From OdataController

        $http.get(FareAddressSearch).then(function (res)
        {
            $scope.SearchFareList = res.data.value;
        });
        $scope.AmountDiv = true;
    }
})