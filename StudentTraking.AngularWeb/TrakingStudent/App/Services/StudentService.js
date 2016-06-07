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
            SchoolBranchId: null,
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
            ParentName: "",
            PrimaryTagDetail: "",
            SecondaryTagDetail: "",
            SchoolName: "",
            ClassName: "",
            SectionName: ""
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
        
        var getStudentListBySearch = function (searchObj) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("studentlist") + $rootScope.User.SecurityToken + '/' + $rootScope.User.SchoolId;
            if (searchObj.name != "") {
                url = url + '/' + searchObj.name;
            }
            
                url = url + '/' +searchObj.classId;
            
                url = url + '/' +searchObj.sectionId;
            return $http({
                url: url,
                method: "GET"
            })
        };

        var addStudent = function (objStudent) {
            var StudentSaveRequest = {
                SecurityToken: $rootScope.User.SecurityToken,
                Student: objStudent
            };
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("savestudent");
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: StudentSaveRequest
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

        service.getStudentList = getStudentList;
        service.getStudentListBySearch = getStudentListBySearch;
        service.addStudent = addStudent;
        return service;
    };
    module.service("StudentService", ['$http', '$rootScope', 'Constant', StudentService]);
}(angular.module('StudentTracking.student')));