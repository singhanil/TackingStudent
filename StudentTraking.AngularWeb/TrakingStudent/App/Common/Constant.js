(function (module) {
    'use strict';

    function constant($rootScope) {
        //#region Api's
        var studentTrakingApiUrl1 = "http://localhost/studentTrackingAPI"; 
        var studentTrakingApiUrl = "http://localhost/Learn";
        var getLoginUrl = "/api/Security/";
        var getStatesUrl = "/api/Common/GetStates/";
        var getOrganisationUrl = "/api/Organization/";
        var getSchoolsUrl = "/api/School/";
        var getClassesUrl = "/School/GetClassList";
        var getDivisionsUrl = "/School/GetDivisionList"; 
        var getStudentsUrl = "/api/Student/";
        var saveSchoolUrl = "/api/School";
        var getCommonDataUrl = "/api/Common/";
        var saveStudentlUrl = "/api/Student";
        var getStudDailyReportlUrl = "/api/Report";
        var getStaffUrl = "/api/Staff/";
        var saveStaffUrl = "/api/Staff";
        var getDepartmentUrl = "/api/Staff/Department/";
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
                case 'getcommondata':
                    _constant = getCommonDataUrl;
                    break; 
                case 'savestudent':
                    _constant = saveStudentlUrl;
                    break;
                case 'getstuddailyreportlurl':
                    _constant = getStudDailyReportlUrl;
                    break;
                case 'stafflist':
                    _constant = getStaffUrl;
                    break;
                case 'savestaff':
                    _constant = saveStaffUrl;
                    break;
                case 'departmentlist':
                    _constant = getDepartmentUrl;
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