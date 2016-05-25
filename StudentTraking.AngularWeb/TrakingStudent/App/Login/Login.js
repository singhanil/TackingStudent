(function (module) {
    'use strict';
    var login = function ($rootScope, $location, $scope, $state, $timeout, LoginService) {
        $scope.LoginError = "";
        var vm = $scope;
        $scope.validate = function (UserName, Password) {
            $timeout(function () {
                $scope.$apply(function () {
                    $scope.LoginError = "";
                });
            }, 100);
            debugger
            var factory = LoginService.authenticateUser(UserName, Password)
         .then(onLoginSuccess, onLoginError);
        }
        var onLoginSuccess = function (data) {
            debugger 
            //if (data.data.Status == 'OK') { statusText
            if (data.statusText == 'OK') {
                LoginService.SetLocalStorage("User", JSON.stringify(data.data));//data.data.UserContext
                LoginService.SetLocalStorage("authorized", true);
                LoginService.SetLocalStorage("MainDashboard", true);
                LoginService.User = JSON.parse(localStorage.getItem("User"));
                $rootScope.User = LoginService.User;
                LoginService.authorized = localStorage.getItem("authorized") == "true" ? true : false;
                $location.path('/dashboard');
            }
            else { $scope.LoginError = data.data.ErrorMessage; }
        }
        var onLoginError = function (data) {
            $scope.LoginError = data.data.MessageDetail;//data.data.ErrorMessage
            LoginService.SetLocalStorage("authorized", false);
        }
        $scope.hasError = function (field) {
            return $scope.form[field].$invalid && $scope.form[field].$dirty;
        };
    }
    module.controller('Login', ['$rootScope', '$location', '$scope', '$state', '$timeout', 'LoginService', login]);
}(angular.module('StudentTracking.login')));