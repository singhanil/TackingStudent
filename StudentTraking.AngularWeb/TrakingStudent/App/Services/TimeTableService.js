﻿(function (module) {
    'use strict';

    var TimeTableService = function ($http, $rootScope, Constant) {
        var service = {};
        var _constant = new Constant();
        service.TimeTableDetails = {
            Id: null,
            SchoolId: null,
            ClassId: null,
            SectionId: null,
            Lecture: null,
            Monday: null,
            Tuesday: null,
            Wednessday: null,
            Thursday: null,
            Friday: null
        };
        
        var getTimeTableListBySearch = function (searchObj) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("timetablelist") + $rootScope.User.SecurityToken + '/' + $rootScope.User.SchoolId;

            url = url + '/' + searchObj.classId;

            url = url + '/' + searchObj.sectionId;

            return $http({
                url: url,
                method: "GET"
            })
        };

        var addtimeTable = function (objTimeTable) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("savetimetable");
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: objTimeTable
            })
        };

        var deleteStaff = function (staffid) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("deletestaff");
            var requiredparams = { staffId: staffid, securityToken: $rootScope.User.SecurityToken };
            return $http({
                url: url,
                method: "GET",
                params: requiredparams
            })
        };

        var getTTCommonData = function () {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("timetablelist") + $rootScope.User.SecurityToken + '/' + $rootScope.User.SchoolId;

            return $http({
                url: url,
                method: "GET"
            })
        };

        service.getTimeTableListBySearch = getTimeTableListBySearch;
        service.addTimeTable = addtimeTable;
        service.getTTCommonData = getTTCommonData;

        return service;
    };
    module.service("TimeTableService", ['$http', '$rootScope', 'Constant', TimeTableService]);
}(angular.module('StudentTracking.timetable')));