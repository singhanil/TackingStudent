(function (module) {
    'use strict';
    var reports = function ($rootScope, $location, $scope, $cordovaNetwork, StudentService, $state) {
        var vm = $scope;
        var currentstate = $state.current.name == 'dashboard.monthlyattendance'?'attendance':'result';
        $scope.ResultData={};
        $scope.AttendancetData={};
        $scope.getResult=function(){
            StudentService.getResult().then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalide security token" || result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.ResultData = result.data.Results;
                    }
               }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        $scope.getAttendanceReport=function(){
            StudentService.getMonthalyAttendance().then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalide security token" || result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.AttendancetData = result.data.Attendence;
                    }
               }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        if(currentstate=='result'){
                $scope.getResult();
        }
        if(currentstate=='attendance'){
                $scope.getAttendanceReport();
        }
        
    }
    module.controller('Reports', ['$rootScope', '$location', '$scope', '$cordovaNetwork', 'StudentService', '$state', reports]);
}(angular.module('StudentTracking.report')));
