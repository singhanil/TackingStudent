(function (module) {
    'use strict';
    var syllabusManagement = function ($rootScope, $location, $scope, SyllabusService, LoginService, CommonService, $state, $filter) {
        var vm = $scope;
        $scope.IsSyllabusUpdateClick = false;
        $scope.showAddSyllabus = false;
        $scope.SyllabusDetails = {};
        $scope.Syllabuslist = {};
        $scope.Classlist = {};
        $scope.Divisionlist = {};
        $scope.Sectionlist = {};
        $scope.hasSyllabusData = false;
        $scope.filterOption = {
            classId: 0
        };
        $scope.clearFilter = function () {
            $scope.filterOption = {
                classId: 0,
                sectionId: 0
            };
        };
        $scope.validateFilterOption = function (filterObj) {
            if ((filterObj.classId == 0 || filterObj.classId == undefined)) {
                return false;
            }
            else {
                return true;
            }

        };

        $scope.SyllabusDetails.SchoolId = $rootScope.User.SchoolId;

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

        $scope.loadSyllabusBySearch = function (filterOption) {
            $scope.hasSyllabusData = false;
            var isFilterValid = $scope.validateFilterOption(filterOption);
            if (!isFilterValid) {
                alert("Please select class filter to search");
                return false;
            }
            $scope.Syllabuslist = {};
            SyllabusService.getSyllabusListBySearch(filterOption).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        debugger
                        $scope.Syllabuslist = result.data.Syllabus;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };

        $scope.AddSyllabus = function (isvalid, syllabusDetail) {
            $scope.clearFilter();
            SyllabusService.addSyllabus(syllabusDetail).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.loadsyllabusDetailList($rootScope.User.SchoolId);
                        $scope.AddSyllabusDetailCancel();
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }

        $scope.loadCommonData();
        $scope.loadSyllabusBySearch(0,0);
        $scope.AddSyllabusCancel = function () {
            $scope.SyllabusDetail = {};
            $scope.SyllabusDetail.SchoolId = $rootScope.User.SchoolId;
            $scope.IsSyllabusUpdateClick = false;
            $scope.showSyllabus = false;
        }

        $scope.ShowAddSyllabusForm = function () {
            $rootScope.ajaxError = false;
            $scope.SyllabusDetail = SyllabusService.SyllabusDetail;
            $scope.SyllabusDetail.SchoolId = $rootScope.User.SchoolId;
            $scope.showAddSyllabus = true;
            $scope.IsSyllabusUpdateClick = false;
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

        $scope.Syllabusgrid = {
            data: 'Syllabuslist',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            columnDefs: [{ field: "Semester", displayName: "Semester", width: 150 },
                         { field: "TotalMarks", displayName: "Total Marks", width: 150 },
                         { field: "Detail", displayName: "Syllabus Details" }]
        };

        $scope.UpdateSyllabus = function (isvalid, objTimeTable) {
            $scope.TimeTableDetails.SchoolId = $rootScope.User.SchoolId;
            $scope.AddTimeTable(isvalid, objTimeTable);
        }
    }
    module.controller('SyllabusManagement', ['$rootScope', '$location', '$scope', 'SyllabusService', 'LoginService', 'CommonService', '$state', '$filter', syllabusManagement]);
}(angular.module('StudentTracking.syllabus')));