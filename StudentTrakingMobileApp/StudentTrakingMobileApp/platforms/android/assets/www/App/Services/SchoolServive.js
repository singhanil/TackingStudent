(function (module) {
    'use strict';

    var SchoolService = function ($http, $rootScope, Constant) {
        var service = {};
        var _constant = new Constant();
        var getTimeTable = function () {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("timetablelist") + $rootScope.User.SecurityToken + '/' + $rootScope.User.SchoolId+ '/' + 0+ '/' + 0;
            return $http({
                url: url,
                method: "GET"
            })
        };
        var getSyllabusResult = function () {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("syllabusurl") + $rootScope.User.SecurityToken + '/' + 0+ '/' + 0;
            return $http({
                url: url,
                method: "GET"
            })
        };
        service.getTimeTable = getTimeTable;
        service.getSyllabusResult = getSyllabusResult;
        return service;
    };
    module.service("SchoolService", ['$http', '$rootScope', 'Constant', SchoolService]);
}(angular.module('StudentTracking.school')));