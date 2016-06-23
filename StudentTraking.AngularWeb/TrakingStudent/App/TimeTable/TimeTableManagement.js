(function (module) {
    'use strict';
    var timetableManagement = function ($rootScope, $location, $scope, TimeTableService, LoginService, CommonService, $state, $filter) {
        var vm = $scope;
        $scope.IsTimeTableUpdateClick = false;
        $scope.showAddtimeTable = false;
        $scope.TimeTableDetails = {};
        $scope.TimeTablelist = {};
        $scope.Classlist = {};
        $scope.Divisionlist = {};
        $scope.Sectionlist = {};
        $scope.filterOption = {
            classId: 0,
            sectionId: 0
        };
        $scope.clearFilter = function () {
            $scope.filterOption = {
                classId: 0,
                sectionId: 0
            };
        };
        $scope.validateFilterOption = function (filterObj) {
            if (filterObj.classId == 0 && filterObj.sectionId == 0) {
                return false;
            }
            else {
                return true;
            }

        };

        $scope.TimeTableDetails.SchoolId = $rootScope.User.SchoolId;

        $scope.loadCommonData = function () {
            CommonService.getCommonData().then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.Classlist = result.data.Classes;
                        $scope.Sectionlist = result.data.Sections;
                    }
                    //$scope.TagDetails = result.data.TagDetails;
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }

        $scope.loadTimeTableBySearch = function (filterOption) {
            var isFilterValid = $scope.validateFilterOption(filterOption);
            if (!isFilterValid) {
                alert("Please select atleast one filter option");
                return false;
            }
            $scope.TimeTablelist = {};
            TimeTableService.getTimeTableListBySearch(filterOption).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.TimeTablelist = result.data.TimeTables;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };

        $scope.AddTimeTable = function (isvalid, timeTableDetail) {
            $scope.clearFilter();
            StaffService.addTimeTable(timeTableDetail).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.loadTimeTableList($rootScope.User.SchoolId);
                        $scope.AddTimeTableCancel();
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }

        $scope.loadCommonData();

        $scope.AddTimeTableCancel = function () {
            $scope.TimeTableDetails = {};
            $scope.TimeTableDetails.SchoolId = $rootScope.User.SchoolId;
            $scope.IsTimeTableUpdateClick = false;
            $scope.showAddTimeTable = false;
        }

        $scope.ShowAddTimeTableForm = function () {
            $rootScope.ajaxError = false;
            $scope.TimeTableDetails = TimeTableService.TimeTableDetails;
            $scope.TimeTableDetails.SchoolId = $rootScope.User.SchoolId;
            $scope.showAddTimeTable = true;
            $scope.IsTimeTableUpdateClick = false;
        }

        $scope.ShowStaffEditForm = function () {
            $scope.loadStates($scope.StaffDetails.Country);
            $scope.TimeTableDetails.SchoolId = $rootScope.User.SchoolId;
            $scope.TimeTableDetails.ParentMobileNo = parseInt($scope.StaffDetails.ParentMobileNo);
            $scope.TimeTableDetails.DateOfBirthh = new Date($scope.StaffDetails.DateOfBirthh);
            $scope.TimeTableDetails.ZipCode = parseInt($scope.StaffDetails.ZipCode);
            $('#myStudModal').modal('hide');
            $scope.showAddStaff = true;
            $scope.IsStaffUpdateClick = true;
        }

        $scope.TimeTablegrid = {
            data: 'TimeTablelist',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            columnDefs: [{ field: "Lecture", displayName: "Lecture", width: 150 },
                         { field: "Monday", displayName: "Monday" },
                         { field: "Tuesday", displayName: "Tuesday" },
                         { field: "Wednessday", displayName: "Wednessday" },
                         { field: "Thursday", displayName: "Thursday" },
                         { field: "Friday", displayName: "Friday" }]
        };

        $scope.UpdateTimeTable = function (isvalid, objTimeTable) {
            $scope.TimeTableDetails.SchoolId = $rootScope.User.SchoolId;
            $scope.AddTimeTable(isvalid, objTimeTable);
        }
    }
    module.controller('TimeTableManagement', ['$rootScope', '$location', '$scope', 'TimeTableService', 'LoginService', 'CommonService', '$state', '$filter', timetableManagement]);
}(angular.module('StudentTracking.timetable')));