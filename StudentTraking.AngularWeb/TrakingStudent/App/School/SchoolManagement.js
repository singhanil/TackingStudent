(function (module) {
    'use strict';
    var schoolManagement = function ($rootScope, $location, $scope, SchoolService, LoginService, $state) {
        var vm = $scope;
        $scope.showAddForm = false;
        $scope.school = SchoolService.School;
        $scope.AddSchool = function (isValid) {
            debugger
        };
        $scope.ShowAddForm = function () {
            $scope.showAddForm = true;
        }
        $scope.AddSchoolCancel = function () {
            $scope.school = {};
            $scope.showAddForm = false;
        }
    }
    module.controller('SchoolManagement', ['$rootScope', '$location', '$scope', 'SchoolService', 'LoginService', '$state', schoolManagement]);
}(angular.module('StudentTracking.school')));