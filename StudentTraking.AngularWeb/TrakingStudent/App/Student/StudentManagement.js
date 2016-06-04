(function (module) {
    'use strict';
    var studentManagement = function ($rootScope, $location, $scope, StudentService, LoginService, CommonService, $state) {
        var vm = $scope;
        $scope.IsStuUpdateClick = false;
        $scope.showAddStudent = false;
        $scope.StudentDetails = {};
        $scope.Studentlist = {};
        $scope.Classlist = {};
        $scope.Divisionlist = {};
        $scope.Sectionlist = {};
        $scope.Country
        $scope.States = {};
        $scope.StudentDetails.SchoolBranchId = $rootScope.User.SchoolId;
        $scope.loadCountries = function () {
            $scope.Country = CommonService.getCountries();
        };
        $scope.loadStates = function (countryid) {
            $scope.States = {};
            if (countryid == null || countryid == undefined) {
                return false;
            }
            CommonService.getStates(countryid).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    $scope.States = result.data.States;
                    if ($scope.IsUpdateClick) {
                        $scope.statedisabled = true;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        $scope.loadClassList = function () {
            $scope.Classlist = StudentService.getClassList();
        };
        $scope.loadSectionList = function () {
            $scope.Sectionlist = StudentService.getSectionList();
        };

        $scope.loadStudentList = function (brabchId) {
            $scope.Studentlist = {};
            if (brabchId == null || brabchId == undefined) {
                return false;
            }
            StudentService.getStudentList(brabchId).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    $scope.Studentlist = result.data.Students;
                    if ($scope.IsStuUpdateClick) {
                        $scope.statedisabled = true;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };

        $scope.primaryTaglist = [
                            { Id: 1, TagId: "PT001" },
                            { Id: 2, TagId: "PT002" },
                            { Id: 3, TagId: "PT003" },
                            { Id: 4, TagId: "PT004" },
                            { Id: 5, TagId: "PT005" },
                            { Id: 6, TagId: "PT006" },
                            { Id: 7, TagId: "PT007" },
                            { Id: 8, TagId: "PT008" },
                            { Id: 9, TagId: "PT009" },
                            { Id: 0, TagId: "PT010" }
        ];

        $scope.secondaryTagList = [
                            { Id: "0001", TagId: "ST001" },
                            { Id: "0002", TagId: "ST002" },
                            { Id: "0003", TagId: "ST003" },
                            { Id: "0004", TagId: "ST004" },
                            { Id: "0005", TagId: "ST005" },
                            { Id: "0006", TagId: "ST006" },
                            { Id: "0007", TagId: "ST007" },
                            { Id: "0008", TagId: "ST008" },
                            { Id: "0009", TagId: "ST009" },
                            { Id: "0010", TagId: "ST010" }
        ];

        $scope.loadStudentList($rootScope.User.SchoolId);

        $scope.AddStudent = function (isvalid, studentDetail) {
            return false;
        }
        $scope.AddStudentCancel = function () {
            $scope.StudentDetails = {};
            $scope.StudentDetails.SchoolBranchId = $rootScope.User.SchoolId;
            $scope.IsStuUpdateClick = false;
            $scope.showAddStudent = false;
        }
        $scope.ShowAddStudentForm = function () {
            $scope.StudentDetails = {};
            $scope.StudentDetails.SchoolBranchId = $rootScope.User.SchoolId;
            $scope.showAddStudent = true;
        }
        $scope.ShowStudDetails = function (row) {
            var rowindex = row.rowIndex;
            $scope.StudentDetails = row.entity;
            $('#myStudModal').modal('show');
        }

        $scope.ShowStudEditForm = function () {
            $scope.StudentDetails.SchoolBranchId = $rootScope.User.SchoolId;
            $('#myStudModal').modal('hide');
            $scope.showAddStudent = true;
            $scope.IsStuUpdateClick = true;
        }

        $scope.Studentgrid = {
            data: 'Studentlist',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            columnDefs: [{ field: "StudentName", displayName: "Student Name", width: 400, cellTemplate: '<a href="" class=\"HighlightColumn\" ng-click="ShowStudDetails(row);"><div class=\"ngCellText\">{{row.getProperty(col.field)}}</div></a>' },
                         { field: "RollNumber", displayName: "Roll Number" },
                         { field: "PrimaryTagId", displayName: "Primary TagId" },
                         { field: "ParentName", displayName: "Parent Name" },
                         { field: "EmailId", displayName: "EmailId" },
                         { field: '', displayName: 'Action', cellTemplate: '<a style="cursor: pointer;" ng-click="deleteStudent(row.entity.Id);"  id="delete"  data-toggle="tooltip">Delete</i></a>' }]
        };
        $scope.UpdateStudent = function (objStud) {
            $scope.StudentDetails = {};
            $scope.StudentDetails.SchoolBranchId = $rootScope.User.SchoolId;
            $scope.IsStuUpdateClick = false;
            $scope.showAddStudent = false;
        }
    }
    module.controller('StudentManagement', ['$rootScope', '$location', '$scope', 'StudentService', 'LoginService', 'CommonService', '$state', studentManagement]);
}(angular.module('StudentTracking.student')));