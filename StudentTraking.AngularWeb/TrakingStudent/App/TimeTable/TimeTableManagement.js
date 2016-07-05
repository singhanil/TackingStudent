(function (module) {
    'use strict';
    var timetableManagement = function ($rootScope, $location, $scope, TimeTableService, LoginService, CommonService, $state, $filter) {
        var vm = $scope;
        $scope.IsTimeTableUpdateClick = false;
        $scope.showAddtimeTable = false;
        $scope.showSaveTimeTable = false;
        $scope.IsAddClicked = false;
        $scope.TimeTableDetails = {};
        $scope.TimeTablelist = {};
        $scope.Classlist = {};
        $scope.Divisionlist = {};
        $scope.Sectionlist = {};
        $scope.Subjectlist = {};
        $scope.Lecturelist = {};
        $scope.TimeTableDetailsList = [];
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
                        //$scope.Subjectlist = result.data.Subjects;
                    }
                    $scope.loadTTCommonData();

                    //$scope.TagDetails = result.data.TagDetails;
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }

        $scope.loadTTCommonData = function()
        {
            TimeTableService.getTTCommonData().then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.Subjectlist = result.data.Subjects;
                        $scope.Lecturelist = result.data.Lectures;
                    }
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

            var BulkTimeTableRequest = {
                SecurityToken: $rootScope.User.SecurityToken,
                SchoolId: $rootScope.User.SchoolId,
                ClassId: timeTableDetail.ClassId,
                SectionId: timeTableDetail.SectionId,
                TimeTable: $scope.TimeTableDetailsList
            };

            TimeTableService.addTimeTable(BulkTimeTableRequest).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        //$scope.loadTimeTableList($rootScope.User.SchoolId);
                        $scope.AddTimeTableCancel();
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }
        $scope.AddTT = function () {
            //alert(TimeTableService.TimeTableDetails);
            $scope.IsAddClicked = true;
            var obj = TimeTableService.TimeTableDetails;
            $scope.TimeTableDetailsList.push({
                Id: 0, Lecture: obj.Lecture, Monday: obj.Monday, Tuesday: obj.Tuesday, Wednessday: obj.Wednessday, Thursday: obj.Thursday, Friday: obj.Friday
            });
            $scope.showSaveTimeTable = true;
            //alert($scope.TimeTableDetailsList);
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
            $scope.IsAddClicked = false;
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
            plugins: [new ngGridFlexibleHeightPlugin()],
            columnDefs: [{ field: "Lecture", displayName: "Lecture", width: 150 },
                         { field: "Monday", displayName: "Monday" },
                         { field: "Tuesday", displayName: "Tuesday" },
                         { field: "Wednessday", displayName: "Wednessday" },
                         { field: "Thursday", displayName: "Thursday" },
                         { field: "Friday", displayName: "Friday" }]
        };

        $scope.TimeTableSavegrid = {
            data: 'TimeTableDetailsList',
            enableSorting: false,
            enableRowSelection: false,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
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