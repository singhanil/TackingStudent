(function (module) {
    'use strict';
    var ReportsService = function ($http, $rootScope, Constant, $state) {
        var service = {};
        var _constant = new Constant();
        var getMonthalyYearlyAttendance = function (stateName) {
            var classId = $rootScope.User.ClassId != undefined ? $rootScope.User.ClassId : 0;
            var sectionId = $rootScope.User.SectionId != undefined ? $rootScope.User.SectionId : 0;
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("getreporturl") + "/" + $rootScope.User.SecurityToken + "/" + $rootScope.User.SchoolId;
            url = url + '/' + classId + '/' + sectionId + '/' + stateName;
            return $http({
                url: url,
                method: "GET"
            })
        };
        var getReportDataBySearch = function (stateName, classId, sectionId) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("getreporturl") + "/" + $rootScope.User.SecurityToken + "/" + $rootScope.User.SchoolId;
            url = url + '/' + classId + '/' + sectionId + '/' + stateName;
            return $http({
                url: url,
                method: "GET"
            })
        };

        service.getMonthalyYearlyAttendance = getMonthalyYearlyAttendance;
        service.getReportDataBySearch = getReportDataBySearch;
        return service;
    };
    module.service("ReportsService", ['$http', '$rootScope', 'Constant', ReportsService]);
}(angular.module('StudentTracking.report')));