(function (module) {
    'use strict';

    var CommonService = function ($http, $rootScope, Constant) {
        var service = {};
        var _constant = new Constant();
        var getOrganisations = function (countryid) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("orglist") + $rootScope.User.SecurityToken;

            var params = {};
            return $http({
                url: url,
                method: "GET",
                params: params
            })
        };
        var getStates = function (countryid) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("states") + $rootScope.User.SecurityToken + '/' + countryid;
            //var params = { countryId: countryid };
            return $http({
                url: url,
                method: "GET"
                //params: params
            })
        };
        var getSchools = function () {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("schools") + $rootScope.User.SecurityToken;
            var params = {};
            return $http({
                url: url,
                method: "GET"
                //params: params
            })
        };

        var getCommonData = function () {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("getcommondata") + $rootScope.User.SecurityToken;
            var params = {};
            return $http({
                url: url,
                method: "GET"
            })
        };

        var getCountries = function () {
            return [{ CountryCode: "IND", CountryName: "India" }];
        };

        var uploadFile = function (fd) {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("uploadurl");
            return $http({
                url: url,
                method: "POST",
                data: fd,
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
        };
        
        service.getCommonData = getCommonData;
        service.getStates = getStates;
        service.getSchools = getSchools;
        service.getOrganisations = getOrganisations;
        service.getCountries = getCountries;
        service.uploadFile = uploadFile;
        return service;
    };
    module.service("CommonService", ['$http', '$rootScope', 'Constant', CommonService]);
}(angular.module('StudentTracking')));