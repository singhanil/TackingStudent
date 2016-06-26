(function (module) {
    'use strict';

    var SyllabusService = function ($http, $rootScope, Constant) {
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

        var getSyllabusListBySearch = function (searchObj) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("syllabuslist") + $rootScope.User.SecurityToken + '/' + $rootScope.User.SchoolId;

            url = url + '/' + searchObj.classId + '/' + searchObj.sectionId;

            return $http({
                url: url,
                method: "GET"
            })
        };

        var addSyllabus = function (objSyllabus) {
            var BulkTimeTableRequest = {
                SecurityToken: $rootScope.User.SecurityToken,
                TimeTables: objSyllabus
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

        service.getSyllabusListBySearch = getSyllabusListBySearch;
        service.addSyllabus = addSyllabus;
        return service;
    };
    module.service("SyllabusService", ['$http', '$rootScope', 'Constant', SyllabusService]);
}(angular.module('StudentTracking.syllabus')));