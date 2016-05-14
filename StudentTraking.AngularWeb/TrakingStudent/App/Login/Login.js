(function (module) {
    'use strict';
    debugger
    var login = function ($rootScope, $location, $scope, $state, LoginService) {
        debugger
        var vm = $scope;
        //vm.validate = Validate();
        $scope.validate = function (User) {
            var factory = LoginService.authenticateUser(User)
         .then(onLoginSuccess, onLoginError);
        }
        var onLoginSuccess = function (data) {
            debugger
            LoginService.SetLocalStorage("User", JSON.stringify(data.data))
            LoginService.SetLocalStorage("authorized", true);
            LoginService.SetLocalStorage("MainDashboard", true);
            LoginService.User = JSON.parse(localStorage.getItem("User"));
            
            //$rootScope.User = service.User;
            LoginService.authorized = localStorage.getItem("authorized") == "true" ? true : false;
            $location.path('/dashboard')
        }
        var onLoginError = function (data) {
            debugger;
            LoginService.SetLocalStorage("authorized", false);
        }
        $scope.hasError = function (field) {
            return $scope.form[field].$invalid && $scope.form[field].$dirty;
        };
    }
    module.controller('Login', ['$rootScope', '$location', '$scope', '$state', 'LoginService', login]);
}(angular.module('StudentTracking.login')));