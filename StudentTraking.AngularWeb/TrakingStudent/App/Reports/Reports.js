(function (module) {
    'use strict';
    var reports = function ($rootScope, $location, $scope, ReportsService, CommonService, $state) {
        var vm = $scope;
        var stateName = $state.current.name == 'dashboard.monthlyattendance'?'Month':'Year';
        $scope.ReportData = {};
        $scope.hasReportData = true;
        $scope.loadClassSection = function () {
            $rootScope.ajaxError = false;
            CommonService.getCommonData().then(function (result) {
                debugger
                //$rootScope.ajaxError = false;
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
        $scope.getReportData = function () {
            $rootScope.ajaxError = false;
            ReportsService.getMonthalyYearlyAttendance(stateName).then(function (result) {

                if (result != null) {
                    if (result.data.ErrorMessage == "Invalide security token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        if (result.data.Reports.length == 0) {
                            $scope.hasReportData = false;
                        }
                        else {
                            $scope.ReportData = result.data.Reports;
                            $scope.hasReportData = true;
                        }
                    }
                }
            }, function (result) {
                $scope.hasReportData = false;
                $rootScope.ajaxError = true;
            });
        };
        $scope.getReportDataBySearch = function (classId, sectionId) {
            $rootScope.ajaxError = false;
            ReportsService.getReportDataBySearch(stateName, classId, sectionId).then(function (result) {

                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        if (result.data.Reports.length == 0) {
                            $scope.hasReportData = false;
                        }
                        else {
                            $scope.hasReportData = true;
                            $scope.DailyReportData = result.data.Reports;
                        }
                    }
                }
            }, function (result) {
                $scope.hasReportData = false;
                $rootScope.ajaxError = true;
            });
        };
        $scope.clearReportFilter = function () {
            $scope.selctedClass = null;
            $scope.selectedDivision = null;
            $scope.getReportData(stateName);
        }
        //$scope.getDailyReportDataBySearch = function (classId, sectionId) {
        //    $scope.getDailyReportData(classId, sectionId);
        //};
        if ($rootScope.Classlist == undefined || $rootScope.Sectionlist == undefined) {
            $scope.loadClassSection();
        }
        $scope.getReportData();
        $scope.reportgrid = {
            data: 'ReportData',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            columnDefs: [{ field: "StudentName", displayName: "Student Name" },
                         { field: "TotalPresentDays", displayName: "Present Day(s)" },
                         { field: "TotalAbsentDays", displayName: "Absent Day(s)" },
                         { field: "TotalSchoolDays", displayName: "School Day(s)" },
                         { field: "StudentId", displayName: "Student Id" },
                         { field: "Class", displayName: "Class" },
                         { field: "Section", displayName: "Section" }
            ]
        };
        $('.ngViewport').height($('.ngViewport').height() + 2);
    }
    module.controller('Reports', ['$rootScope', '$location', '$scope', 'ReportsService', 'CommonService', '$state', reports]);
}(angular.module('StudentTracking.report')));