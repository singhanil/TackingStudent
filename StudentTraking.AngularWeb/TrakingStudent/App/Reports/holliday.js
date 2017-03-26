(function (module) {
    'use strict';
    var hollidayController = function ($rootScope, $location, $scope, SchoolService, LoginService, $state) {
        $scope.holidayList = {};
        $scope.getHolidayList = function () {
            $scope.holidayList = {};
            SchoolService.getHolidayList().then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.holidayList = result.data.HolidayList;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }
        $scope.getHolidayList();
    }
    module.controller('hollidayController', ['$rootScope', '$location', '$scope', 'SchoolService', 'LoginService', '$state', hollidayController]);
}(angular.module('StudentTracking.report')));