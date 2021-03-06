﻿(function (module) {
    'use strict';

    function constant($rootScope) {
        //#region Api's
        //var studentTrakingApiUrl = "http://localhost/Learn";
        var studentTrakingApiUrl = "http://www.octosoftlab.com";
        //var studentTrakingApiUrl = "http://localhost/Learn";
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
        var getStudReportlUrl = "/api/Report";
        var getStaffUrl = "/api/Staff/";
        var saveStaffUrl = "/api/Staff";
        var getTimeTableUrl = "/api/TimeTable/";
        var saveTimeTableUrl = "/api/TimeTable";
        var getSyllabusUrl = "/api/Syllabus/";
        var saveSyllabusUrl = "/api/Syllabus/SaveBulk";
        var getDepartmentUrl = "/api/Staff/Department/";
        var saveAttachmentUrl = "/api/Notification";
        var getNotificationUrl = "/api/Notification/Mobile/";
        var getFileUploadUrl = "/api/Common/UploadFile";
        var getHolidayListUrl = "/api/School/holidaylist/";
        var getEventListUrl = "/api/School/calendar/";
        var getLinksUrl = "/api/School/importantlinks/";
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
                case 'getreporturl':
                    _constant = getStudReportlUrl;
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
                case 'timetablelist':
                    _constant = getTimeTableUrl;
                    break;
                case 'savetimetable':
                    _constant = saveTimeTableUrl;
                    break;
                case 'syllabuslist':
                    _constant = getSyllabusUrl;
                    break;
                case 'savesyllabus':
                    _constant = saveSyllabusUrl;
                    break;
                case 'saveattachment':
                    _constant = saveAttachmentUrl;
                    break;
                case 'getnotifications':
                    _constant = getNotificationUrl;
                    break; 
                case 'getresult':
                    _constant = getResultUrl;
                    break;
                case 'uploadurl':
                    _constant = getFileUploadUrl;
                    break; 
                case 'getholidaylist':
                    _constant = getHolidayListUrl;
                    break;
                case 'geteventlist':
                    _constant = getEventListUrl;
                    break;
                case 'getimplinks':
                    _constant = getLinksUrl;
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