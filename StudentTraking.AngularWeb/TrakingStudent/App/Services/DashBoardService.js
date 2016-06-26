(function (module) {
    'use strict';
    var DashBoardService = function ($http, $rootScope, Constant, $state) {
        var service = {};
        var _constant = new Constant();
        var getDailyStudentReport = function () {
            var classId = $rootScope.User.ClassId;
            var sectionId = $rootScope.User.SectionId;
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("getreporturl") + "/" + $rootScope.User.SecurityToken + "/" + $rootScope.User.SchoolId;
            if (classId != undefined && sectionId != undefined)
                url = url + '/' + classId + '/' + sectionId;
            return $http({
                url: url,
                method: "GET"
            })
        };
        var getDailyStudentReportBySearch = function (classId, sectionId) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("getreporturl") + "/" + $rootScope.User.SecurityToken + "/" + $rootScope.User.SchoolId;
            if (classId != undefined && sectionId != undefined)
                url = url + '/' + classId + '/' + sectionId;
            return $http({
                url: url,
                method: "GET"
            })
        };
        
        service.getDailyStudentReport = getDailyStudentReport;
        service.getDailyStudentReportBySearch = getDailyStudentReportBySearch;
        return service;
    };
    module.service("DashBoardService", ['$http', '$rootScope', 'Constant', DashBoardService]);
}(angular.module('StudentTracking.dashboard')));