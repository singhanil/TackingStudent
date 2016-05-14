(function (module) {
    'use strict';

    var SchoolService = function ($http, $rootScope, $state) {
        var service = {};
        service.School = {
            Name: "",
            BranchName: "",
            Address1: "",
            Address2: "",
            State: "",
            County: "",
            City: "",
            ZipCode: "",
            ContactPerson: "",
            ContactTitle: "",
            Email: "",
            Phone: "",
            Discription:"",
        }
        var getSchool = function () {

            var url = _constant.get("getstates");
            var params = {  };
            return $http({
                url: url,
                method: "GET",
                params: params
            })
        };
        service.getSchool = getSchool;
        return service;
    };
    module.service("SchoolService", ['$http', '$rootScope', SchoolService]);
}(angular.module('StudentTracking.login')));