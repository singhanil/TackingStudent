﻿(function (module) {
    "use strict";
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider

        .state('dashboard', {
            abstract: true,
            url: "/dashboard",
            templateUrl: 'App/DashBoard/Dasboard.html',
            //controller: 'Dasboard  as vm',
            data: {
                authorization: true,
                redirectTo: 'login'
            }
        })
        .state('dashboard.main', {
            url: "/main",
            templateUrl: 'App/DashBoard/DashBoardMain.html',
            controller: 'Dasboard  as vm'
        })
       .state('dashboard.school', {
           url: "/school",
           templateUrl: 'App/School/School.html',
           controller: 'SchoolManagement  as vm'
       })
        .state('dashboard.student', {
            url: "/student",
            templateUrl: 'App/Student/Student.html',
            controller: 'StudentManagement  as vm'
        })
        .state('dashboard.staff', {
            url: "/staff",
            templateUrl: 'App/Staff/Staff.html',
            controller: 'StaffManagement  as vm'
        })
        .state('dashboard.timetable', {
            url: "/timetable",
            templateUrl: 'App/TimeTable/TimeTable.html',
            controller: 'TimeTableManagement  as vm'
        })
        .state('dashboard.syllabus', {
            url: "/syllabus",
            templateUrl: 'App/Syllabus/Syllabus.html',
            controller: 'SyllabusManagement  as vm'
        })
        .state('dashboard.result', {
            url: "/result",
            templateUrl: 'App/Result/Result.html',
            controller: 'ResultManagement  as vm'
        })
        .state('dashboard.monthlyattendance', {
            url: "/monthlyattendance",
            templateUrl: 'App/Reports/MonthlyAttendance.html',
            controller: 'Reports as vm'
        })
        .state('dashboard.yearlyattendance', {
            url: "/yearlyattendance",
            templateUrl: 'App/Reports/YearlyAttendance.html',
            controller: 'Reports as vm'
        })
        .state('dashboard.otherreport', {
            url: "/otherreport",
            templateUrl: 'App/Reports/OtherReport.html'
            //controller: 'StudentManagement  as vm'
        })
        .state('dashboard.studentreport', {
            url: "/studentreport",
            templateUrl: 'App/Reports/StudentReport.html'
            //controller: 'StudentManagement  as vm'
        })
        .state('dashboard.notification', {
            url: "/notifications",
            templateUrl: 'App/Notification/Notifications.html',
            controller: 'Notification  as vm'
        })
        .state('dashboard.hollidays', {
            url: "/hollidays",
            templateUrl: 'App/Reports/HollidayList.html',
            controller: 'hollidayController  as vm'
        })
        .state('dashboard.links', {
            url: "/implinks",
            templateUrl: 'App/Reports/MyLinks.html',
            controller: 'linksController  as vm'
        })
        .state('dashboard.upload', {
            url: "/upload",
            templateUrl: 'App/Reports/UploadFile.html',
            controller: 'uploadController  as vm'
        })
        .state('dashboard.calendar', {
            url: "/calender",
            templateUrl: 'App/Reports/MyCalendar.html',
            controller: 'calendarController  as vm'
        })

    }
    module.run(function ($state) { })
    module.config(config);
}(angular.module('StudentTracking.dashboard', ['ui.router'])));