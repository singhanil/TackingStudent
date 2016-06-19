(function (module) {
    'use strict';

    var DashBoardService = function ($http, $rootScope, Constant, $state) {
        var service = {};
        var _constant = new Constant();
        var getDailyStudentReport = function () {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("getstuddailyreportlurl") + "/" + $rootScope.User.SecurityToken + "/" + $rootScope.User.SchoolId;

            return $http({
                url: url,
                method: "GET"
            })
        };
        service.getDailyStudentReport = getDailyStudentReport;
        return service;
    };
    module.service("DashBoardService", ['$http', '$rootScope', 'Constant', DashBoardService]);
}(angular.module('StudentTracking.dashboard')));