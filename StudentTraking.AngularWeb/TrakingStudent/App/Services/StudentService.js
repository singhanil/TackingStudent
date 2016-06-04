(function (module) {
    'use strict';

    var StudentService = function ($http, $rootScope, Constant) {
        var service = {};
        var _constant = new Constant();
        service.StudentDetails = {
            Id: null,
            StudentId: "",
            StudentName: "",
            ParentMobileNo: "",
            PrimaryTagId: null,
            SecondaryTagId: null,
            EmailId: "",
            SchoolBranchId: "",
            ClassId: "",
            SectionId: "",
            Address1: "",
            Address2: "",
            Country: "",
            State: "",
            City: "",
            ZipCode: "",
            Gender: "",
            DateOfBirthh: null,
            ParentName: "",
            PrimaryTagDetail: "",
            SecondaryTagDetail: "",
            SchoolName: "",
            ClassName: "",
            SectionName: ""
        };

        var getClassList = function () {
            return [
                            { Id: 1, Name: "Nursery" },
                            { Id: 2, Name: "Junior KG" },
                            { Id: 3, Name: "Senior KG" },
                            { Id: 4, Name: "Class I" },
                            { Id: 5, Name: "Class II" },
                            { Id: 6, Name: "Class III" },
                            { Id: 7, Name: "Class IV" },
                            { Id: 8, Name: "Class V" },
                            { Id: 9, Name: "Class VI" },
                            { Id: 10, Name: "Class VII" },
                            { Id: 11, Name: "Class VIII" },
                            { Id: 12, Name: "Class IX" },
                            { Id: 13, Name: "Class X" },
                            { Id: 14, Name: "Class XI" },
                            { Id: 15, Name: "Class XII" }
            ];
        };
        var getSectionList = function () {
            return [
                            { Id: 1, Name: "A" },
                            { Id: 2, Name: "B" },
                            { Id: 3, Name: "C" },
                            { Id: 4, Name: "D" },
                            { Id: 5, Name: "E" },
                            { Id: 6, Name: "F" }
            ];
        };

        var getStudentList = function (branchId) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("studentlist") + $rootScope.User.SecurityToken + '/' + branchId;
            //var params = { countryId: countryid };
            return $http({
                url: url,
                method: "GET"
                //params: params
            })
        };

        var addSchools = function (objSchool) {
            var SchoolSaveRequest = {
                SecurityToken: $rootScope.User.SecurityToken,
                School: objSchool
            };
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("schools");
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: SchoolSaveRequest
            })
        };

        var deleteSchool = function (schoolid) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("saveschool");
            var requiredparams = { schoolId: schoolid, securityToken: $rootScope.User.SecurityToken };
            return $http({
                url: url,
                method: "GET",
                params: requiredparams
            })
        };

        service.getClassList = getClassList;
        service.getSectionList = getSectionList;
        return service;
    };
    module.service("StudentService", ['$http', '$rootScope', 'Constant', StudentService]);
}(angular.module('StudentTracking.student')));