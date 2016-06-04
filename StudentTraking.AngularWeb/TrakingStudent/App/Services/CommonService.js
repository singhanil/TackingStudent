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

        var getCountries = function () {
            return [{ CountryCode: "IND", CountryName: "India" }];
        };
        
        service.getStates = getStates;
        service.getSchools = getSchools;
        service.getOrganisations = getOrganisations;
        service.getCountries = getCountries;
        return service;
    };
    module.service("CommonService", ['$http', '$rootScope', 'Constant', CommonService]);
}(angular.module('StudentTracking')));