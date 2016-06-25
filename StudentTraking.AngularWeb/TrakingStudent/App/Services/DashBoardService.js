(function (module) {
    'use strict';

    var DashBoardService = function ($http, $rootScope, Constant, $state) {
        var service = {};
        var _constant = new Constant();
        var getDailyStudentReport = function (classId, sectionId) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("getstuddailyreportlurl") + "/" + $rootScope.User.SecurityToken + "/" + $rootScope.User.SchoolId;
            if (classId != undefined && sectionId != undefined)
                url = url + '/' + classId + '/' + sectionId;
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