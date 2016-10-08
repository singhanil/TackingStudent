(function (module) {
    'use strict';
    var dasboard = function ($rootScope, $location, $scope, DashBoardService, CommonService, $state) {
        var vm = $scope;
        $scope.TotalStudentCount = 0;
        $scope.presentCount = 0;
        $scope.absentCount = 0;
        $scope.DailyReportData = {};
        $scope.hasDailyData = true;
                
       $scope.loadClassSection = function () {
            $rootScope.ajaxError = false;
            CommonService.getCommonData().then(function (result) {
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
        $scope.getDailyReportData = function () {
            $rootScope.ajaxError = false;
            DashBoardService.getDailyStudentReport().then(function (result) {
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
                            $scope.TotalStudentCount = result.data.Reports.length;
                            var presentList = $.grep(result.data.Reports, function (n, i) {
                                return n.Attendence === 'Present';
                            });
                            $scope.presentCount = presentList.length;
                            $scope.absentCount = ($scope.TotalStudentCount-$scope.presentCount);
                            $scope.DailyReportData = result.data.Reports;
                            $scope.hasDailyData = true;
                        }
                    }
                }
            }, function (result) {
                $scope.hasDailyData = false;
                $rootScope.ajaxError = true;
            });
        };
        $scope.getDailyReportDataBySearch = function (classId, sectionId) {
            $rootScope.ajaxError = false;
            DashBoardService.getDailyStudentReportBySearch(classId, sectionId).then(function (result) {
               
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
                $scope.hasDailyData = false;
                $rootScope.ajaxError = true;
            });
        };
        $scope.clearReportFilter = function () {
            $scope.selctedClass = null;
            $scope.selectedDivision = null;
            $scope.getDailyReportData();
        }
        
        if ($rootScope.Classlist == undefined || $rootScope.Sectionlist == undefined) {
            $scope.loadClassSection();
        }
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