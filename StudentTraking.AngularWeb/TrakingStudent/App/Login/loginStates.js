(function (module) {
    "use strict";
    debugger
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
        .state('login', {
            url: "/login",
            templateUrl: 'App/Login/Login.html',
            controller: 'Login  as vm'
       })
    }
    module.run(function ($state) { })
    module.config(config);//.run(function ($state) { }); // fixes ui-view in ng-include
}(angular.module('StudentTracking.login', ['ui.router'])));