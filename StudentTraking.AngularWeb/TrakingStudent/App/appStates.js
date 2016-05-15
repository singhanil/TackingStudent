(function (module) {
    "use strict";
    function config($urlRouterProvider) {
        $urlRouterProvider.when("/dashboard", '/dashboard');
        $urlRouterProvider.otherwise('/login');
    }
    module.config(config);
   
}(angular.module("StudentTracking")));


angular.module("StudentTracking").run(function ($rootScope, $state, $location, LoginService) {
    debugger
    $rootScope.$on('$stateChangeStart', function (e, toState, toParams, fromState, fromParams) {
        $rootScope.ajaxError = false;
        if (toState.name != "dashboard" && toState.name != "login") {
            LoginService.SetLocalStorage("MainDashboard", false);
            $rootScope.MainDashboard = localStorage.getItem("MainDashboard") == "true" ? true : false;
        }
        else {
            LoginService.SetLocalStorage("MainDashboard", true);
            $rootScope.MainDashboard = localStorage.getItem("MainDashboard") == "true" ? true : false;
        }
        
    });
    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        debugger
        if (!LoginService.authorized && (toState.data['authorization'] != undefined) && (toState.data['redirectTo'] != undefined)) {
            $state.go(toState.data.redirectTo);
            
        }

    })
});




