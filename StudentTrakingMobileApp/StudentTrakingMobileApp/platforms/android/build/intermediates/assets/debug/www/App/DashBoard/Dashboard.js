(function (module) {
    'use strict';
    var dasboard = function ($rootScope, $location, $scope, StudentService, $state) {
        var vm = $scope;
    }
    module.controller('Dasboard', ['$rootScope', '$location', '$scope', 'StudentService', '$state', dasboard]);
}(angular.module('StudentTracking.dashboard')));