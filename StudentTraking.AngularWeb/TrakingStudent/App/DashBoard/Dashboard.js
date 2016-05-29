(function (module) {
    'use strict';
    var dasboard = function ($rootScope, $location, $scope, LoginService, $state) {
        var vm = $scope;
        //$state.transitionTo('dashboard.main');
    }
    module.controller('Dasboard', ['$rootScope', '$location', '$scope', 'LoginService', '$state', dasboard]);
}(angular.module('StudentTracking.dashboard')));