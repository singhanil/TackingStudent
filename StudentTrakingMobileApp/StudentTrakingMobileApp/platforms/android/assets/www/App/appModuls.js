﻿var app = angular
  .module('StudentTracking', [
    "StudentTracking.login",
    "StudentTracking.dashboard",
    "StudentTracking.constant",
    "StudentTracking.school",
    "StudentTracking.student",
    "StudentTracking.report",
    "StudentTracking.acadamic",
    "StudentTracking.ngLoadingSpinner",
    "ngCordova"
  ]);


app.directive('jqdatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            $(element).datepicker({
                dateFormat: 'dd/mm/yy',
                onSelect: function (date) {
                    scope.StudentDetails.DateOfBirthh = date;
                    scope.$apply();
                }
            });
        }
    };
});




