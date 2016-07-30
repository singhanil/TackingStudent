(function (module) {
    'use strict';
    var login = function ($rootScope, $location, $scope, $state, $timeout, LoginService, StudentService) {
        $scope.LoginError = "";
        var vm = $scope;
        $scope.getNotificationList=function(){
             StudentService.getNotifications().then(function (result) {
                $scope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalide security token" || result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $scope.Logout();
                    }
                    else {
                        $rootScope.Notifications = result.data.Notifications;
                        var newNotification = $.grep(result.data.Notifications, function (n, i) {
                            return n.IsNew === true;
                        });
                        $rootScope.NotificationCount = $(newNotification).length;
                    }
               }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        $scope.validate = function (UserName, Password) {
            $timeout(function () {
                $scope.$apply(function () {
                    $scope.LoginError = "";
                });
            }, 100);
            var factory = LoginService.authenticateUser(UserName, Password)
         .then(onLoginSuccess, onLoginError);
        };
        var onLoginSuccess = function (data) {
            if (data.data.Status == 'OK') { 
                LoginService.SetLocalStorage("User", JSON.stringify(data.data.UserContext));//data.data.UserContext
                LoginService.SetLocalStorage("authorized", true);
                LoginService.SetLocalStorage("MainDashboard", true);
                LoginService.User = JSON.parse(localStorage.getItem("User"));
                $rootScope.User = LoginService.User;
                LoginService.authorized = localStorage.getItem("authorized") == "true" ? true : false;
                $scope.getNotificationList();
                $location.path('/dashboard');
            }
            else { $scope.LoginError = data.data.ErrorMessage; }
        };
        var onLoginError = function (data) {
            console.log(data);
            $scope.LoginError = data.data.ErrorMessage;//data.data.MessageDetail;
            LoginService.SetLocalStorage("authorized", false);
        };

        $scope.hasError = function (field) {
            return $scope.form[field].$invalid && $scope.form[field].$dirty;
        };
    }
    module.controller('Login', ['$rootScope', '$location', '$scope', '$state', '$timeout', 'LoginService', 'StudentService', login]);
}(angular.module('StudentTracking.login')));