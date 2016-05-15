(function (module) {
    "use strict";
    debugger
    function config($stateProvider, $urlRouterProvider) {
        debugger
        $stateProvider

        .state('dashboard', {
            url: "/dashboard",
            templateUrl: 'App/DashBoard/Dasboard.html',
            controller: 'Dasboard  as vm',
            data: {
                authorization: true,
                redirectTo: 'login'
            }
        })
       .state('dashboard.school', {
           url: "/school",
           templateUrl: 'App/School/School.html',
           controller: 'SchoolManagement  as vm',
       })
    }
    module.run(function ($state) { })
    module.config(config);//.run(function ($state) { }); // fixes ui-view in ng-include
}(angular.module('StudentTracking.dashboard', ['ui.router'])));