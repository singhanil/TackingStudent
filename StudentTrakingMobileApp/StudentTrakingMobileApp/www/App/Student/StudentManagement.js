(function (module) {
    'use strict';
    var studentManagement = function ($rootScope, $location, $scope, StudentService, LoginService, CommonService, $state, $filter) {
        var vm = $scope;
        $scope.IsStuUpdateClick = false;
        $scope.showAddStudent = false;
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
            $scope.loadStudentList();
        };

        $scope.loadStates = function (countryid) {
           
        };

        $scope.loadStudentList = function (brabchId) {
            $scope.Studentlist=[{"Id":17,"StudentId":"ST008","ParentMobileNo":"7412589632","PrimaryTagId":11,"SecondaryTagId":13,"EmailId":"surya@surya.com","StudentName":"Suryapratam Thakur","SchoolBranchId":10,"ClassId":3,"SectionId":1,"Address1":"Dehu Road","Address2":"","City":"Pune","State":"MH","ZipCode":"789456","Country":"IND","Gender":"M","DateOfBirthh":"2011-05-16T00:00:00","ParentName":"Uttkarsh","PrimaryTagDetail":"Primary Tag6","SecondaryTagDetail":"Primary Tag6","SchoolName":"Euro School, Wadgaon Branch","ClassName":"Senior KG","SectionName":"A"},{"Id":18,"StudentId":"ST010","ParentMobileNo":"7412589633","PrimaryTagId":4,"SecondaryTagId":7,"EmailId":"a@a.com","StudentName":"karan","SchoolBranchId":10,"ClassId":12,"SectionId":4,"Address1":"kale wadi","Address2":"","City":"pune","State":"MH","ZipCode":"852396","Country":"IND","Gender":"M","DateOfBirthh":"2001-05-16T00:00:00","ParentName":"a","PrimaryTagDetail":"Primary Tag4","SecondaryTagDetail":"Primary Tag4","SchoolName":"Euro School, Wadgaon Branch","ClassName":"Class IX","SectionName":"D"}];
        };

        $scope.loadStudentListBySearch = function (filterOption) {
            $scope.Studentlist = $.grep($scope.Studentlist, function (n, i) {
            if(filterOption.name != "" && filterOption.classId != 0 && filterOption.sectionId != 0){
                return ( n.StudentName === filterOption.name && n.ClassId == filterOption.classId && n.SectionId && filterOption.sectionId);
            }
            if(filterOption.name != "" && filterOption.classId == 0 && filterOption.sectionId == 0){
                return ( n.StudentName === filterOption.name);
            }
            if(filterOption.name == "" && filterOption.classId != 0 && filterOption.sectionId != 0){
                return ( n.ClassId == filterOption.classId && n.SectionId && filterOption.sectionId);
            }
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
            //var rowindex = row.rowIndex;
            //row.entity.DateOfBirthh = $filter('date')(new Date(row.entity.DateOfBirthh), 'MM/dd/yyyy');
            ////row.entity.DateOfBirthh = new Date(Date.parse(row.entity.DateOfBirthh, "dd/mm/yyyy"));
            //$scope.StudentDetails = row.entity;
            row.DateOfBirthh = $filter('date')(new Date(row.DateOfBirthh), 'MM/dd/yyyy');
            //row.entity.DateOfBirthh = new Date(Date.parse(row.entity.DateOfBirthh, "dd/mm/yyyy"));
            $scope.StudentDetails = row;
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