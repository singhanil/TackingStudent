(function (module) {
    'use strict';

    var TimeTableService = function ($http, $rootScope, Constant) {
        var service = {};
        var _constant = new Constant();
                
        var getTimeTable = function () {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("timetablelist") + $rootScope.User.SecurityToken + '/' + $rootScope.User.SchoolId;

            url = url + '/' + searchObj.classId;

            url = url + '/' + searchObj.sectionId;

            return $http({
                url: url,
                method: "GET"
            })
        };

        service.getTimeTable = getTimeTable;
        return service;
    };
    module.service("TimeTableService", ['$http', '$rootScope', 'Constant', TimeTableService]);
}(angular.module('StudentTracking.timetable')));