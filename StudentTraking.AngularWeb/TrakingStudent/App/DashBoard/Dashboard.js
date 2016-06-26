(function (module) {
    'use strict';
    var dasboard = function ($rootScope, $location, $scope, DashBoardService, CommonService, $state) {
        var vm = $scope;
        debugger;
        $scope.selctedClass = null;
        $scope.selectedDivision = null;
        $scope.DailyReportData = {};
        $scope.hasDailyData = true;
        $scope.loadClassSection = function () {
            CommonService.getCommonData().then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $rootScope.Classlist = result.data.Classes;
                        $rootScope.Sectionlist = result.data.Sections;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        $scope.getDailyReportData = function (classId, sectionId) {
            debugger
            if (classId == undefined || sectionId == undefined) {
                classId = $rootScope.User.ClassId; 
                sectionId = $rootScope.User.SectionId;
            }
            $rootScope.User.SecurityToken
            DashBoardService.getDailyStudentReport(classId, sectionId).then(function (result) {
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
        $scope.clearReportFilter = function () {
            $scope.selctedClass = null;
            $scope.selectedDivision = null;
            $scope.getDailyReportData();
        }
        $scope.getDailyReportDataBySearch = function (classId, sectionId) {
            $scope.getDailyReportData(classId, sectionId);
        };
        $scope.loadClassSection();
        $scope.getDailyReportData();
        $scope.dailyreportgrid = {
            data: 'DailyReportData',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            columnDefs: [{ field: "StudentName", displayName: "Student Name" },
                         { field: "StudentId", displayName: "Student Id" },
                         { field: "Class", displayName: "Class" },
                         { field: "Section", displayName: "Section" },
                         { field: "Attendence", displayName: "Attendence" }
            ]
        };
        $('.ngViewport').height($('.ngViewport').height() + 2);
    }
    module.controller('Dasboard', ['$rootScope', '$location', '$scope', 'DashBoardService', 'CommonService', '$state', dasboard]);
}(angular.module('StudentTracking.dashboard')));