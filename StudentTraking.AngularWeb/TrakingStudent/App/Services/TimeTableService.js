(function (module) {
    'use strict';

    var TimeTableService = function ($http, $rootScope, Constant) {
        var service = {};
        var _constant = new Constant();
        service.StaffDetails = {
            Id: null,
            StaffId: "",
            StaffName: "",
            DepartmentId: null,
            PrimaryTagId: null,
            ReportingEmailId: "",
            StaffMobileNo: "",
            SchoolId: null,
            ClassId: null,
            SectionId: null,
            Address1: "",
            Address2: "",
            Country: "",
            State: "",
            City: "",
            ZipCode: "",
            Gender: "",
            DateOfBirthh: null,
            PrimaryTagDetail: "",
            DepartmentName: "",
            SchoolName: "",
            ClassName: "",
            SectionName: "",
            CountryName: ""
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
            var BulkTimeTableRequest = {
                SecurityToken: $rootScope.User.SecurityToken,
                TimeTables: objTimeTable
            };
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("savebulktimetable");
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: BulkTimeTableRequest
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

        service.getTimeTableListBySearch = getTimeTableListBySearch;
        service.addTimeTable = addtimeTable;
        return service;
    };
    module.service("TimeTableService", ['$http', '$rootScope', 'Constant', TimeTableService]);
}(angular.module('StudentTracking.timetable')));