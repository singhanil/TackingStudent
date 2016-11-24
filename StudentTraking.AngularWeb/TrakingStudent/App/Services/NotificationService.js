(function (module) {
    'use strict';

    var NotificationService = function ($http, $rootScope, Constant) {
        var service = {};
        var _constant = new Constant();
       
        var getNotifications = function () {
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("getnotifications") + $rootScope.User.SecurityToken + '/' + $rootScope.User.SchoolId + '/' + $rootScope.User.Id;
            return $http({
                url: url,
                method: "GET"
            })
        };
        var sendNotifications = function (fd) {
            //var NotificationRequest = {
            //    SecurityToken: $rootScope.User.SecurityToken,
            //    NotificationModel: {
            //        MessageId: "",
            //        SchoolId: $rootScope.User.SchoolId,
            //        To: "",
            //        From: $rootScope.User.Id,
            //        Message: notificationText,
            //        RelatedMSGId: "",
            //        IsNew: true
            //    }
            //};
            var APIURL = _constant.get("studenttrakingurl");
            var url = APIURL + _constant.get("saveattachment");
            //return $http.post(url, fd, {
            //    withCredentials: true,
            //    headers: { 'Content-Type': undefined },
            //    transformRequest: angular.identity
            //})

            
            return $http({
                url: url,
                method: "POST",
                data: fd,
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
        };
        
        service.getNotifications = getNotifications;
        service.sendNotifications = sendNotifications;
        return service;
    };
    module.service("NotificationService", ['$http', '$rootScope', 'Constant', NotificationService]);
}(angular.module('StudentTracking.notification')));