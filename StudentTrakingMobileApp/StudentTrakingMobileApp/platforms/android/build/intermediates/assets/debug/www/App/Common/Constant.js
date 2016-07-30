(function (module) {
    'use strict';
    function constant($rootScope) {
        //#region Api's
        var studentTrakingApiUrl = "http://octosoftlabs.com";
        var getLoginUrl = "/api/Security/";
        var getStatesUrl = "/api/Common/GetStates/";
        var getAttendanceUrl = "/api/Report/Mobile/";
        var getResultUrl = "/api/Result/Mobile/";
        var getTimeTableUrl = "/api/TimeTable/Mobile/";
        var getNotificationUrl = "/api/Notification/Mobile/";
        var getSyllabusUrl = "/api/Syllabus/";
        var getSaveNotificationUrl="/api/Notification";
        //#endregion
        var getConstant = function (key) {
            var _constant = "";
            //*******************************************************
            //please add all key in lowercase manner
            //********************************************************
            switch (key.toLowerCase()) {
               case 'studenttrakingurl':
                    _constant = studentTrakingApiUrl;
                    break;
                case 'loginurl':
                    _constant = getLoginUrl;
                    break;
                case 'resulturl':
                    _constant = getResultUrl;
                    break;
                case 'timetablelist':
                    _constant = getTimeTableUrl;
                    break;
                case 'attendanceurl':
                    _constant = getAttendanceUrl;
                    break;
                case 'notificationurl':
                    _constant = getNotificationUrl;
                    break;
                case 'syllabusurl':
                    _constant = getSyllabusUrl;
                    break;
                case 'savenotificationurl':
                    _constant = getSaveNotificationUrl;
                    break;
                default:
                    break;
            }
            return _constant;
        };
        function oConstant() { }
        oConstant.prototype = {
            get: function (key) { return getConstant(key); }
        };
        return oConstant;
    }
    module.factory('Constant', ['$rootScope', constant]);
}(angular.module('StudentTracking.constant', [])));