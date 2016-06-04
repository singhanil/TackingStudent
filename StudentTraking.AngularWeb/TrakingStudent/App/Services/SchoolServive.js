(function (module) {
    'use strict';

    var SchoolService = function ($http, $rootScope, Constant) {
        var service = {};
        var _constant = new Constant();
        service.School = {
            Name: "",
            Id:null,
            BranchName: "",
            Address1: "",
            Address2: "",
            //CountryCode: "",
            Country: "",
            //StateId: "",
            State: "",
            City: "",
            //ZipCode: "",
            ContactPerson: "",
            Title: "",
            EmailId: "",
            Phone: "",
            Discription: "",
            FaceBookUrl: "",
            Twitter: "",
            LinkedIn: "",
            OrganizationId: 0,
            ThemeId: 1,
            IsActive: 'Y',
            OrganizationName: "",
            ThemeDetail: "",
            ModifiedDate:null
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

        service.addSchools = addSchools;
        service.deleteSchool = deleteSchool;
        return service;
    };
    module.service("SchoolService", ['$http', '$rootScope', 'Constant', SchoolService]);
}(angular.module('StudentTracking.school')));