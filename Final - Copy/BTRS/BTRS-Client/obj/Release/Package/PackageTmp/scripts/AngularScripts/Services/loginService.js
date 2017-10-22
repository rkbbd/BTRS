angular.module("myApp")
.factory("loginSvc", function ($http) {
    return {
        login: function (u, p) {
            return $http({
                url:"http://btms-server.azurewebsites.net/Token",
                method: "POST",
                data: $.param({ grant_type: 'password', username: u, password: p }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
        },
        register: function (e, p, cp) {
            return $http({
                url: "http://btms-server.azurewebsites.net/api/Account/Register",
                method: "POST",
                data: {
                    Email: e,
                    Password: p,
                    ConfirmPassword: cp
                }
            });
        }

    };
});