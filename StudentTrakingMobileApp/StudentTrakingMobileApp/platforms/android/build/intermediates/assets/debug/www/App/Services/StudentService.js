(function (module) {
    'use strict';

    var StudentService = function ($http, $rootScope, Constant) {
        var service = {};
        var _constant = new Constant();
        var getResult = function () {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("resulturl") + $rootScope.User.SecurityToken + '/' + 0 + '/' + 0;
            return $http({
                url: url,
                method: "GET"
            })
        };
        var getMonthalyAttendance = function () {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("attendanceurl") + $rootScope.User.SecurityToken + '/' + 0+ '/' + 0;
            return $http({
                url: url,
                method: "GET"
            })
        };
        var getNotifications = function () {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("notificationurl") + $rootScope.User.SecurityToken + '/' + $rootScope.User.SchoolId + '/' + $rootScope.User.Id;
            return $http({
                url: url,
                method: "GET"
            })
        };        
        var sendNotifications = function (notificationText) {
            var NotificationRequest = {
                SecurityToken: $rootScope.User.SecurityToken,
                NotificationModel: {
                                        MessageId:"",
                                        SchoolId:$rootScope.User.SchoolId,
                                        To:"",
                                        From:$rootScope.User.Id,
                                        Message:notificationText,
                                        RelatedMSGId:"",
                                        IsNew:true
                                    }
            };
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("savenotificationurl") + $rootScope.User.SecurityToken + '/' + $rootScope.User.SchoolId + '/' + $rootScope.User.Id;
            return $http({
                url: url,
                method: "POST",
                data: NotificationRequest
            })
        };  
        service.getResult = getResult;
        service.getMonthalyAttendance = getMonthalyAttendance;
        service.getNotifications = getNotifications;
        service.sendNotifications = sendNotifications;
        return service;
    };
    module.service("StudentService", ['$http', '$rootScope', 'Constant', StudentService]);
}(angular.module('StudentTracking.student')));