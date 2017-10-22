angular.module("myApp")
.factory("apiCallSvc", function ($http) {
    return {
        get: function (url, header, data) {
            return $http({
                url: url,
                method: "GET",
                headers: header,
                params: data
            });
        },
        post: function (url, header, data) {
            return $http({
                url: url,
                method: "POST",
                headers: header,
                data: data
            });
        },
        put: function (url, header, data) {
            return $http({
                url: url,
                method: "PUT",
                headers: header,
                data: data
            });
        },
        remove: function (url, header, data) {
            return $http({
                url: url,
                method: "DELETE",
                headers: header,
                data: data
            });
        }
    };
})