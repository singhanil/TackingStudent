(function (module) {
    "use strict";
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider

        .state('dashboard', {
            abstract: true,
            url: "/dashboard",
            templateUrl: 'App/DashBoard/Dasboard.html',
            controller: 'Dasboard  as vm',
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
    }
    module.run(function ($state) { })
    module.config(config);
}(angular.module('StudentTracking.dashboard', ['ui.router'])));