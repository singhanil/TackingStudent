(function (module) {
    "use strict";
    function config($urlRouterProvider) {
        $urlRouterProvider.when("/dashboard", '/dashboard/main');
        $urlRouterProvider.otherwise('/login');
    }
    module.config(config);
}(angular.module("StudentTracking")));


angular.module("StudentTracking").run(function ($rootScope, $state, $location, LoginService) {
    $rootScope.Logout = function () {
        localStorage.clear();
        $location.path('/login');
    }

    $rootScope.$on('$stateChangeStart', function (e, toState, toParams, fromState, fromParams) {
        $rootScope.ajaxError = false;
    });
    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        if (!LoginService.authorized && (toState.data['authorization'] != undefined) && (toState.data['redirectTo'] != undefined)) {
            $state.transitionTo(toState.data.redirectTo);
        }
    })
});




