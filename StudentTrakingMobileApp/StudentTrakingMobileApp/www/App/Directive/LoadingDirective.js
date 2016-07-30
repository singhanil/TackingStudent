(function () {
    "use strict";
    angular.module("StudentTracking.ngLoadingSpinner", ['angularSpinner'])
     .directive('trakingSpinner', ['$http', '$rootScope', '$state', function ($http, $rootScope, $state) {
         return {
             link: function (scope, elm, attrs) {
                 $rootScope.spinnerActive = false;
                 scope.isLoading = function () {
                     return $http.pendingRequests.length > 0;
                 };

                 scope.$watch(scope.isLoading, function (loading) {
                 $(document).ready(function () {
                    var winHeight = $(window).height() - 50;
                    //setTimeout(function () {
                         $('body.nav-md .container.body .right_col').css({ 'min-height': winHeight });
                         $('#sidebar-menu li ul').slideUp();
                         $('#sidebar-menu li').removeClass('active');
                     //}, 1000);
                     
                 });
                
                     $rootScope.spinnerActive = loading;
                     if (loading) {
                         elm.removeClass('ng-hide');
                     } else {
                         elm.addClass('ng-hide');

                     }
                 });
             }
         };

     }]);
}()).call(this);

