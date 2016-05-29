(function (module) {
    'use strict';

    function constant($rootScope) {
        //#region Api's
        var studentTrakingApiUrl1 = "http://localhost/studentTrackingAPI"; 
        var studentTrakingApiUrl = "http://localhost:58222";
        var getLoginUrl = "/api/Security/";
        var getStatesUrl = "/School/GetStateList";
        var getOrganisationUrl = "/School/GetSchoolList";
        var getSchoolsUrl = "/api/School/";
        var getClassesUrl = "/School/GetClassList";
        var getDivisionsUrl = "/School/GetDivisionList";
        var getStudentsUrl = "/School/GetStudentList";
        var saveSchoolUrl = "/api/School";

        //#endregion
        var getConstant = function (key) {
            var _constant = "";
            //*******************************************************
            //please add all key in lowercase manner
            //********************************************************
            switch (key.toLowerCase()) {
                case 'studenttrakingurl1':
                    _constant = studentTrakingApiUrl1;
                    break;
                case 'studenttrakingurl':
                    _constant = studentTrakingApiUrl;
                    break;
                case 'loginurl':
                    _constant = getLoginUrl;
                    break;
                case 'schools':
                    _constant = getSchoolsUrl;
                    break;
                case 'orglist':
                    _constant = getOrganisationUrl;
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
                case 'saveschool':
                    _constant = saveSchoolUrl;
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