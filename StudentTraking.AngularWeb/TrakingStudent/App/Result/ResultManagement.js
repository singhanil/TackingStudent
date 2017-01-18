(function (module) {
    'use strict';
    var resultManagement = function ($rootScope, $location, $scope, ResultService, LoginService, CommonService, StudentService, $state, $filter) {
        var vm = $scope;
        $scope.IsResultUpdateClick = false;
        $scope.showAddResult = false;
        $scope.Studentlist = {};
        $scope.ResultDetails = {};
        $scope.Resultlist = {};
        $scope.Classlist = {};
        $scope.Sectionlist = {};
        $scope.filterOption = {
            name: "",
            classId: 0,
            sectionId: 0
        };
        $scope.clearFilter = function () {
            $scope.filterOption = {
                name: "",
                classId: 0,
                sectionId: 0
            };
        };
        $scope.validateFilterOption = function (filterObj) {
            if (filterObj.name == "" && filterObj.departmentId == 0) {
                return false;
            }
            else {
                return true;
            }

        };

        $scope.ResultDetails.SchoolId = $rootScope.User.SchoolId;

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

                        $scope.loadStudentList($rootScope.User.SchoolId);
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }

        $scope.loadStudentList = function (brabchId) {
            $scope.clearFilter();
            $scope.Studentlist = {};
            if (brabchId == null || brabchId == undefined) {
                return false;
            }
            StudentService.getStudentList(brabchId).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        if (result.data.Students == null || result.data.Students.length == 0) {
                            $scope.hasStudData = false;
                        }
                        else {
                            $scope.hasStudData = true;
                            $scope.Studentlist = result.data.Students;
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
            $scope.loadStudentList($rootScope.User.SchoolId);
        }
        $scope.loadStudentListBySearch = function (classId, sectionId) {
            $scope.Studentlist = {};
            $scope.filterOption.classId = classId;
            $scope.filterOption.sectionId = sectionId;
            StudentService.getStudentListBySearch($scope.filterOption).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        if (result.data.Students.length == 0) {
                            $scope.hasStudData = false;
                        }
                        else {
                            $scope.hasStudData = true;
                            $scope.Studentlist = result.data.Students;
                        }
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };

        $scope.AddResult = function (isvalid, resultDetail) {
            $scope.clearFilter();
            StaffService.addStaff(resultDetail).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.loadStaffList($rootScope.User.SchoolId);
                        $scope.AddStaffCancel();
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }

        $scope.loadCommonData();

        $scope.AddStaffCancel = function () {
            $scope.StaffDetails = {};
            $scope.StaffDetails.SchoolId = $rootScope.User.SchoolId;
            $scope.IsStaffUpdateClick = false;
            $scope.showAddStaff = false;
        }

        $scope.ShowAddStaffForm = function () {
            $rootScope.ajaxError = false;
            $scope.StaffDetails = StaffService.StaffDetails;
            $scope.StaffDetails.SchoolId = $rootScope.User.SchoolId;
            $scope.showAddStaff = true;
            $scope.IsStaffUpdateClick = false;
        }

        $scope.ShowResult = function (row) {
            $scope.ResultDetails = [];
            var rowindex = row.rowIndex;
            row.entity.DateOfBirthh = $filter('date')(new Date(row.entity.DateOfBirthh), 'MM/dd/yyyy');
            //row.entity.DateOfBirthh = new Date(Date.parse(row.entity.DateOfBirthh, "dd/mm/yyyy"));
            ResultService.getResultBySchoolIdStudentId(row.entity.StudentId).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.ResultDetails = result.data.Results;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
            var winHeight = $(window).height() * (0.7);
            var winWidth = $(window).width() * (0.5);
            $('#myResultModal').height($(window).height() * (0.8));
            $('#myResultModal .modal-dialog').height(winHeight);
            $('#myResultModal .modal-dialog').width(winWidth);
            $('#myResultModal').modal('show');
        }

        $scope.ShowStaffEditForm = function () {
            $scope.loadStates($scope.StaffDetails.Country);
            $scope.StaffDetails.SchoolId = $rootScope.User.SchoolId;
            $scope.StaffDetails.ParentMobileNo = parseInt($scope.StaffDetails.ParentMobileNo);
            $scope.StaffDetails.DateOfBirthh = new Date($scope.StaffDetails.DateOfBirthh);
            $scope.StaffDetails.ZipCode = parseInt($scope.StaffDetails.ZipCode);
            $('#myStudModal').modal('hide');
            $scope.showAddStaff = true;
            $scope.IsStaffUpdateClick = true;
        }

        $scope.Studentgrid = {
            data: 'Studentlist',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            columnDefs: [{ field: "StudentName", displayName: "Student Name", cellTemplate: '<a href="" class=\"HighlightColumn\" ng-click="ShowResult(row);"><div class=\"ngCellText\">{{row.getProperty(col.field)}}</div></a>' },
                         { field: "StudentId", displayName: "Student Id" },
                         { field: "ClassName", displayName: "Class" },
                         { field: "SectionName", displayName: "Section" },
                         { field: "ParentName", displayName: "Parent Name" },
                         { field: "ParentMobileNo", displayName: "Parent MobileNo" }]
        };

        $scope.UpdateStaff = function (isvalid, objStaff) {
            $scope.StaffDetails.SchoolId = $rootScope.User.SchoolId;
            $scope.AddStaff(isvalid, objStaff);
        }
    }
    module.controller('ResultManagement', ['$rootScope', '$location', '$scope', 'ResultService', 'LoginService', 'CommonService', 'StudentService', '$state', '$filter', resultManagement]);
}(angular.module('StudentTracking.result')));