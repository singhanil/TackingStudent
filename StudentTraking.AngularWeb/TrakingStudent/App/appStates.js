(function (module) {
    "use strict";
    function config($urlRouterProvider, $locationProvider) {
        debugger
        $urlRouterProvider.when("/dashboard", '/dashboard/main');
        $urlRouterProvider.otherwise('/login');
    }
    module.config(config);
}(angular.module("StudentTracking")));


angular.module("StudentTracking").run(function ($rootScope, $state, $location, LoginService) {
    $rootScope.Logout = function () {
        localStorage.clear();
        $location.path('/login');
    };

    $rootScope.$on('$stateChangeStart', function (e, toState, toParams, fromState, fromParams) {
        $rootScope.ajaxError = false;
    });
    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        $rootScope.toggleLeftMenu();
        if (!LoginService.authorized && (toState.data['authorization'] != undefined) && (toState.data['redirectTo'] != undefined)) {
            $state.transitionTo(toState.data.redirectTo);
            
        }
    })
    //for menu animation
    $rootScope.toggleLeftPannel = function () {
        if ($('body').hasClass('nav-md')) {
            $('body').removeClass('nav-md').addClass('nav-sm');
            $('.left_col').removeClass('scroll-view').removeAttr('style');
            $('.sidebar-footer').hide();

            if ($('#sidebar-menu li').hasClass('active')) {
                $('#sidebar-menu li.active').addClass('active-sm').removeClass('active');
            }
        } else {
            $('body').removeClass('nav-sm').addClass('nav-md');
            $('.sidebar-footer').show();

            if ($('#sidebar-menu li').hasClass('active-sm')) {
                $('#sidebar-menu li.active-sm').addClass('active').removeClass('active-sm');
            }
        }
    };
    $rootScope.toggleLeftMenu = function () {
        //var link = $('a', this).attr('href');
        //angular.forEach(('#sidebar-menu').find('li'), function () {

        //    if ($(this).find('li').is('.active')) {
        //        $(this).removeClass('active');
        //        $('ul', this).slideUp();
        //    } else {
        //        $(this).removeClass('active');
        //        $(this).find('ul').slideUp();

        //        $(this).addClass('active');
        //        $('ul', this).slideDown();
        //    }
        //});


        //debugger
        //var link = angular.element('#sidebar-menu');

        //    if ($(link).find('li').is('.active')) {
        //        $(link).removeClass('active');
        //        $('ul', link).slideUp();
        //    } else {
        //        $('#sidebar-menu li').removeClass('active');
        //        $('#sidebar-menu li ul').slideUp();

        //        $(link).addClass('active');
        //        $('ul', link).slideDown();
        //    }

    }
});




