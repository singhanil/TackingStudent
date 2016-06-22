(function (module) {
    'use strict';

    var StaffService = function ($http, $rootScope, Constant) {
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

        var getDepartmentList = function (branchId) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("departmentlist") + $rootScope.User.SecurityToken;
            return $http({
                url: url,
                method: "GET"
            })
        };

        var getStaffList = function (branchId) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("stafflist") + $rootScope.User.SecurityToken + '/' + branchId;
            //var params = { countryId: countryid };
            return $http({
                url: url,
                method: "GET"
                //params: params
            })
        };

        var getStaffListBySearch = function (searchObj) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("stafflist") + $rootScope.User.SecurityToken + '/' + $rootScope.User.SchoolId;
            if (searchObj.name != "") {
                url = url + '/' + searchObj.name;
            }
            url = url + '/' + searchObj.departmentId;

            return $http({
                url: url,
                method: "GET"
            })
        };

        var addStaff = function (objStaff) {
            var StaffSaveRequest = {
                SecurityToken: $rootScope.User.SecurityToken,
                Staff: objStaff
            };
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("savestaff");
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: StaffSaveRequest
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

        service.getStaffList = getStaffList;
        service.getStaffListBySearch = getStaffListBySearch;
        service.addStaff = addStaff;
        service.getDepartmentList = getDepartmentList;
        return service;
    };
    module.service("StaffService", ['$http', '$rootScope', 'Constant', StaffService]);
}(angular.module('StudentTracking.staff')));