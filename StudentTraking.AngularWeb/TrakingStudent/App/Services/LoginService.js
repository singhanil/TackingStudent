(function (module) {
    'use strict';

    var LoginService = function ($http, $rootScope, Constant, $state) {
        var service = {};
        var _constant = new Constant();
        service.authorized = false,
        service.ErrorMessage = undefined;
        service.User = {
            UserId:"",
            UserName: "",
            UserRole: "",
            SchoolId:"",
            SecurityToken: ""
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

        service.authenticateUser = function (UserName, Password) {
            var requestData = {
                UserName: UserName,
                Password: Password
            };
            //var requestData = {
            //    userId: UserName,
            //    password: Password
            //};
            debugger;
            var APIURL = _constant.get("studenttrakingurl");
            var loginurl = _constant.get("loginurl");
            var loginUrl = "http://localhost:58222/api/Security/" + UserName + "/" + Password; //APIURL + loginurl;

            var promise = $http({
                cache: false,
                url: loginUrl,
                method: "GET"
                //params: requestData
            });
            return promise;
        };
        return service;
    };
    module.service("LoginService", ['$http', '$rootScope', 'Constant', LoginService]);
}(angular.module('StudentTracking.login')));