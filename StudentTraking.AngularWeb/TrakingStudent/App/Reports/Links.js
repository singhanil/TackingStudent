(function (module) {
    'use strict';
    var linksController = function ($rootScope, $location, $scope, SchoolService, LoginService, $state) {
        $scope.importantLinks = {};
        $scope.getLinks = function () {
            $scope.importantLinks = {};
            SchoolService.getImportantLinks().then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.importantLinks = result.data.ImportantLinks;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }
        $scope.getLinks();
    }
    module.controller('linksController', ['$rootScope', '$location', '$scope', 'SchoolService', 'LoginService', '$state', linksController]);
}(angular.module('StudentTracking.report')));