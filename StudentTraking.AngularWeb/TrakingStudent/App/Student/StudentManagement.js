(function (module) {
    'use strict';
    var studentManagement = function ($rootScope, $location, $scope, StudentService, LoginService, $state) {
        var vm = $scope;
        $scope.IsStuUpdateClick = false;
        $scope.showAddStudent = false;
        $scope.StudentDetails = {};
        $scope.Studentlist = {};
        $scope.Classlist = {};
        $scope.Divisionlist = {};
        $scope.StudentDetails.SchoolBranchId = $rootScope.User.SchoolId;
        $scope.Country = [
                            { CountryCode: "091", CountryName: "India" }
        ];
        $scope.primaryTaglist = [
                            { PrimaryTagId: "0001", PrimaryTagName: "PT001" },
                            { PrimaryTagId: "0002", PrimaryTagName: "PT002" },
                            { PrimaryTagId: "0003", PrimaryTagName: "PT003" },
                            { PrimaryTagId: "0004", PrimaryTagName: "PT004" },
                            { PrimaryTagId: "0005", PrimaryTagName: "PT005" },
                            { PrimaryTagId: "0006", PrimaryTagName: "PT006" },
                            { PrimaryTagId: "0007", PrimaryTagName: "PT007" },
                            { PrimaryTagId: "0008", PrimaryTagName: "PT008" },
                            { PrimaryTagId: "0009", PrimaryTagName: "PT009" },
                            { PrimaryTagId: "0010", PrimaryTagName: "PT010" }
        ];
        $scope.secondaryTagList = [
                            { SecondaryTagId: "0001", SecondaryTagName: "ST001" },
                            { SecondaryTagId: "0002", SecondaryTagName: "ST002" },
                            { SecondaryTagId: "0003", SecondaryTagName: "ST003" },
                            { SecondaryTagId: "0004", SecondaryTagName: "ST004" },
                            { SecondaryTagId: "0005", SecondaryTagName: "ST005" },
                            { SecondaryTagId: "0006", SecondaryTagName: "ST006" },
                            { SecondaryTagId: "0007", SecondaryTagName: "ST007" },
                            { SecondaryTagId: "0008", SecondaryTagName: "ST008" },
                            { SecondaryTagId: "0009", SecondaryTagName: "ST009" },
                            { SecondaryTagId: "0010", SecondaryTagName: "ST010" }
        ];

        StudentService.getStudentList($scope.StudentDetails.SchoolBranchId).then(function (result) {
            $rootScope.ajaxError = false;
            if (result != null) {
                $scope.Studentlist = result.data;
            }
        }, function (result) {
            $rootScope.ajaxError = true;
        });

        $scope.getStates = function (countryid) {
            $scope.States = {};
            if (countryid == null || countryid == undefined) {
                return false;
            }
            StudentService.getStates(countryid).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    $scope.States = result.data;
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        StudentService.getClassList($scope.StudentDetails.SchoolBranchId).then(function (result) {
            $rootScope.ajaxError = false;
            if (result != null) {
                $scope.Classlist = result.data;
            }
        }, function (result) {
            $rootScope.ajaxError = true;
        });
        $scope.getDivisions = function (classID) {
            $scope.Divisionlist = {};
            if (classID == null || classID == undefined) {
                return false;
            }
            StudentService.getDivisionList(classID).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    $scope.Divisionlist = result.data;
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }

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
    module.controller('StudentManagement', ['$rootScope', '$location', '$scope', 'StudentService', 'LoginService', '$state', studentManagement]);
}(angular.module('StudentTracking.student')));