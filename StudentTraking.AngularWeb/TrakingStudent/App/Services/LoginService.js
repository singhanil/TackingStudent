(function (module) {
    'use strict';

    var LoginService = function ($http, $rootScope, Constant, $state) {
        var service = {};
        var _constant = new Constant();
        service.authorized = false,
        service.ErrorMessage = undefined;
        service.User = {
            UserName: "",
            UserRole: "",
            Token: ""
        }
        service.User = JSON.parse(localStorage.getItem("User"));
        $rootScope.User = service.User;
        service.authorized = localStorage.getItem("authorized") == "true" ? true : false;
        service.SetLocalStorage = function (key, value) {
            localStorage.setItem(key, value);
        };

        service.GetLocalStorage = function (key) {
            if (LocalStorageService.get(key)) {
                return (localStorage.getItem(key));
            }
            return;
        };
        service.clear = function () {
            service.authorized = false;
            service.User = {};
        };

        service.go = function (Callback) {
            var targetState = service.authorized ? Callback : "login"

            $state.go(Callback);
        };

        service.authenticateUser = function (User) {
            var requestData = {
                user: User
            };
            debugger;
            var APIURL = _constant.get("studenttrakingurl");
            var loginurl = _constant.get("loginurl");
            var loginUrl = APIURL + loginurl;

            var promise = $http({
                cache: false,
                url: loginUrl,
                method: "POST",
                data: requestData
            });
            return promise;
        };
        return service;
    };
    module.service("LoginService", ['$http', '$rootScope', 'Constant', LoginService]);
}(angular.module('StudentTracking.login')));