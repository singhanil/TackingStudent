var app = angular
  .module('StudentTracking', [
    "StudentTracking.login",
    "StudentTracking.dashboard",
    "StudentTracking.constant",
    "StudentTracking.school",
    "StudentTracking.student",
    "StudentTracking.report",
    "StudentTracking.ngLoadingSpinner"
  ]);


app.directive('jqdatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            debugger
            $(element).datepicker({
                dateFormat: 'dd/mm/yy',
                onSelect: function (date) {
                    debugger
                    scope.StudentDetails.DateOfBirthh = date;
                    scope.$apply();
                }
            });
        }
    };
});




