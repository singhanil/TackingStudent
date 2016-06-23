﻿(function (module) {
    'use strict';
    var dasboard = function ($rootScope, $location, $scope, DashBoardService, $state) {
        var vm = $scope;
        $scope.DailyReportData = {};
        $scope.hasDailyData = true;
        $scope.getDailyReportData = function () {
            DashBoardService.getDailyStudentReport().then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        if (result.data.Reports.length == 0) {
                            $scope.hasDailyData = false;
                        }
                        else {
                            $scope.hasDailyData = true;
                            $scope.DailyReportData = result.data.Reports;
                        }
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        $scope.getDailyReportData();
        $scope.dailyreportgrid = {
            data: 'DailyReportData',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            columnDefs: [{ field: "Attendence", displayName: "Attendence" },
                         { field: "StudentName", displayName: "Student Name" },
                         { field: "StudentId", displayName: "Student Id" },
                         { field: "Class", displayName: "Class" },
                         { field: "Section", displayName: "Section" }
                        ]
        };
    }
    module.controller('Dasboard', ['$rootScope', '$location', '$scope', 'DashBoardService', '$state', dasboard]);
}(angular.module('StudentTracking.dashboard')));