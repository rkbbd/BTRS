angular.module("myApp")
.factory("authLocalStorageSvc", function () {
    return {
        getAuthData: function () {
            if (sessionStorage.length > 0) {
                return sessionStorage.getItem("auth") == null ? { authenticated: false } : JSON.parse(sessionStorage.getItem("auth"));
            }
            else {
                return { authenticated: false };
            }
        },
        saveAuthData: function (u, a) {
            sessionStorage.setItem("auth", JSON.stringify({ user: u, accToken: a, isAuthenticated: true }));
        },
        removeAuthData: function () {
            sessionStorage.removeItem("auth");
        }
    }
});
