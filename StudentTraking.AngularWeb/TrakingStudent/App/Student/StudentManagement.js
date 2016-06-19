(function (module) {
    'use strict';
    var studentManagement = function ($rootScope, $location, $scope, StudentService, LoginService, CommonService, $state, $filter) {
        var vm = $scope;
        $scope.IsStuUpdateClick = false;
        $scope.showAddStudent = false;
        $scope.hasStudData = true;
        $scope.StudentDetails = {};
        $scope.Studentlist = {};
        $scope.Classlist = {};
        $scope.Divisionlist = {};
        $scope.Sectionlist = {};
        $scope.primaryTaglist = {};
        $scope.secondaryTaglist = {};
        $scope.Countrylist = {};
        $scope.Statelist = {};
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
            if (filterObj.name == "" && filterObj.classId == 0 && filterObj.sectionId == 0) {
                return false;
            }
            else {
                return true;
            }
            
        };

        $scope.StudentDetails.SchoolBranchId = $rootScope.User.SchoolId;

        $scope.loadCommonData = function () {
            CommonService.getCommonData().then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.Countrylist = result.data.Coutries;
                        $scope.Classlist = result.data.Classes;
                        $scope.Sectionlist = result.data.Sections;
                        $scope.primaryTaglist = $.grep(result.data.TagDetails, function (n, i) {
                            return n.Type === 'P';
                        });
                        $scope.secondaryTaglist = $.grep(result.data.TagDetails, function (n, i) {
                            return n.Type === 'S';
                        });
                        $scope.loadStudentList($rootScope.User.SchoolId);
                    }
                    //$scope.TagDetails = result.data.TagDetails;
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };

        $scope.loadStates = function (countryid) {
            $scope.States = {};
            if (countryid == null || countryid == undefined) {
                return false;
            }
            CommonService.getStates(countryid).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.Statelist = result.data.States;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };

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

        $scope.loadStudentListBySearch = function (filterOption) {
            var isFilterValid = $scope.validateFilterOption(filterOption);
            if (!isFilterValid) {
                alert("Please select atleast one filter option");
                return false;
            }
            $scope.Studentlist = {};
            StudentService.getStudentListBySearch(filterOption).then(function (result) {
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

        $scope.AddStudent = function (isvalid, studentDetail) {
            $scope.clearFilter();
            StudentService.addStudent(studentDetail).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.loadStudentList($rootScope.User.SchoolId);
                        $scope.AddStudentCancel();
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }

        $scope.loadCommonData();

        $scope.AddStudentCancel = function () {
            $scope.StudentDetails = {};
            $scope.StudentDetails.SchoolBranchId = $rootScope.User.SchoolId;
            $scope.IsStuUpdateClick = false;
            $scope.showAddStudent = false;
        }

        $scope.ShowAddStudentForm = function () {
            $rootScope.ajaxError = false;
            $scope.StudentDetails = StudentService.StudentDetails;
            $scope.StudentDetails.SchoolBranchId = $rootScope.User.SchoolId;
            $scope.showAddStudent = true;
            $scope.IsStuUpdateClick = false;
        }

        $scope.ShowStudDetails = function (row) {
            var rowindex = row.rowIndex;
            row.entity.DateOfBirthh = $filter('date')(new Date(row.entity.DateOfBirthh), 'MM/dd/yyyy');
            //row.entity.DateOfBirthh = new Date(Date.parse(row.entity.DateOfBirthh, "dd/mm/yyyy"));
            $scope.StudentDetails = row.entity;

            $('#myStudModal').modal('show');
        }

        $scope.ShowStudEditForm = function () {
            $scope.loadStates($scope.StudentDetails.Country);
            $scope.StudentDetails.SchoolBranchId = $rootScope.User.SchoolId;
            $scope.StudentDetails.ParentMobileNo = parseInt($scope.StudentDetails.ParentMobileNo);
            $scope.StudentDetails.DateOfBirthh = new Date($scope.StudentDetails.DateOfBirthh);
            $scope.StudentDetails.ZipCode = parseInt($scope.StudentDetails.ZipCode);
            $('#myStudModal').modal('hide');
            $scope.showAddStudent = true;
            $scope.IsStuUpdateClick = true;
        }

        $scope.Studentgrid = {
            data: 'Studentlist',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            columnDefs: [{ field: "StudentName", displayName: "Student Name", cellTemplate: '<a href="" class=\"HighlightColumn\" ng-click="ShowStudDetails(row);"><div class=\"ngCellText\">{{row.getProperty(col.field)}}</div></a>' },
                         { field: "StudentId", displayName: "Student Id" },
                         { field: "SchoolName", displayName: "School Name" },
                         { field: "ClassName", displayName: "Class" },
                         { field: "SectionName", displayName: "Section" },
                         { field: "ParentName", displayName: "Parent Name" },
                         { field: "ParentMobileNo", displayName: "Parent MobileNo" },
                         { field: '', displayName: 'Action', cellTemplate: '<a style="cursor: pointer;" ng-click="deleteStudent(row.entity.Id);"  id="delete"  data-toggle="tooltip">Delete</i></a>' }]
        };

        $scope.UpdateStudent = function (isvalid, objStud) {
            $scope.StudentDetails.SchoolBranchId = $rootScope.User.SchoolId;
            $scope.AddStudent(isvalid, objStud);
        }
    }
    module.controller('StudentManagement', ['$rootScope', '$location', '$scope', 'StudentService', 'LoginService', 'CommonService', '$state', '$filter', studentManagement]);
}(angular.module('StudentTracking.student')));