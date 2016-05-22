(function (module) {
    'use strict';

    function constant($rootScope) {
        //#region Api's
        var studentTrakingApiUrl = "http://localhost/studentTrackingAPI"; 
        var getLoginUrl = "/api/LogIn";
        var getStatesUrl = "/School/GetStateList";
        var getSchoolsUrl = "/School/GetSchoolList";
        var getClassesUrl = "/School/GetClassList";
        var getDivisionsUrl = "/School/GetDivisionList";
        var getStudentsUrl = "/School/GetStudentList";

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
                case 'schools':
                    _constant = getSchoolsUrl;
                    break;
                case 'states':
                    _constant = getStatesUrl;
                    break;
                case 'classlist':
                    _constant = getClassesUrl;
                    break;
                case 'divisionlist':
                    _constant = getDivisionsUrl;
                    break;
                case 'studentlist':
                    _constant = getStudentsUrl;
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