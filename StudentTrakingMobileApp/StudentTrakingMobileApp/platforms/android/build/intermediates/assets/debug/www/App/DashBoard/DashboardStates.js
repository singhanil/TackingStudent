(function (module) {
    "use strict";
    function config($stateProvider) {
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
        .state('dashboard.syllabus', {
                   url: "/syllabus",
                   templateUrl: 'App/School/Syllabus.html',
                   controller: 'SchoolManagement  as vm'
        })
        .state('dashboard.timetable', {
                   url: "/timetable",
                   templateUrl: 'App/School/Timetable.html',
                   controller: 'SchoolManagement  as vm'
        })
        .state('dashboard.monthlyattendance', {
            url: "/monthlyattendance",
            templateUrl: 'App/Reports/MonthlyAttendance.html',
            controller: 'Reports  as vm'
        })
        .state('dashboard.results', {
            url: "/results",
            templateUrl: 'App/Acadamic/Result.html',
            controller: 'Reports  as vm'
        })
        .state('dashboard.notification', {
            url: "/notifications",
            templateUrl: 'App/Reports/Notifications.html',
            controller: 'Notification as vm'
        })
    }
    module.run(function ($state) { })
    module.config(config);
}(angular.module('StudentTracking.dashboard', ['ui.router'])));