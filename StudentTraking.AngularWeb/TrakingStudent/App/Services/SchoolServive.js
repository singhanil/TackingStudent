(function (module) {
    'use strict';

    var SchoolService = function ($http, $rootScope, Constant) {
        var service = {};
        var _constant = new Constant();
        service.School = {
            Name: "",
            BranchName: "",
            Address1: "",
            Address2: "",
            CountryCode: "",
            CountryName: "",
            StateId: "",
            StateName: "",
            City: "",
            ZipCode: "",
            ContactPerson: "",
            ContactTitle: "",
            Email: "",
            Phone: "",
            Discription: "",
        };
        var getStates = function (countryid) {
            debugger
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("states");
            var params = { countryId: countryid };
            return $http({
                url: url,
                method: "GET",
                params: params
            })
        };
        var getSchools = function () {
            debugger
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("schools");
            var params = { };
            return $http({
                url: url,
                method: "GET",
                params: params
            })
        };
        service.getStates = getStates;
        service.getSchools = getSchools;
        return service;
    };
    module.service("SchoolService", ['$http', '$rootScope', 'Constant', SchoolService]);
}(angular.module('StudentTracking.school')));