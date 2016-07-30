(function (module) {
    "use strict";
    function config($urlRouterProvider) {
        $urlRouterProvider.when("/dashboard", '/dashboard/main');
        $urlRouterProvider.otherwise('/login');
    }
    module.config(config);
}(angular.module("StudentTracking")));


(function (module) {
    'use strict';
    var trackingcontroller = function ($rootScope, $location, $scope, $window, LoginService, StudentService, $state) {
        var vm = $scope;
        $scope.ShowDashboard=function(){
            $state.go('dashboard.main');
        }
        $scope.ShowSyllabus=function(){
            $state.go('dashboard.syllabus');
        }
        $scope.ShowTimetable=function(){
            $state.go('dashboard.timetable');
        }
        $scope.ShowAttendance=function(){
            $state.go('dashboard.monthlyattendance');
        }
        $scope.ShowResults=function(){
            $state.go('dashboard.results');
        }
        $scope.ShowNotification=function(){
            $state.go('dashboard.notification');
        }
        $scope.ShowBlogs=function(blogUrl){
            $window.open(blogUrl, "_blank", "location=no");
        }
    }
    module.controller('trackingController', ['$rootScope', '$location', '$scope', '$window', 'LoginService', 'StudentService', '$state', trackingcontroller]);
}(angular.module('StudentTracking')));


angular.module("StudentTracking").run(function ($rootScope, $state, $location, LoginService) {
    $rootScope.Logout = function () {
        localStorage.clear();
        $location.path('/login');
    };

    $rootScope.$on('$stateChangeStart', function (e, toState, toParams, fromState, fromParams) {
        $rootScope.ajaxError = false;
    });
    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        if (!LoginService.authorized && (toState.data['authorization'] != undefined) && (toState.data['redirectTo'] != undefined)) {
            $state.transitionTo(toState.data.redirectTo);
        }
    })
});




