(function (module) {
    'use strict';
    var staffManagement = function ($rootScope, $location, $scope, StaffService, LoginService, CommonService, $state, $filter) {
        var vm = $scope;
        $scope.IsStaffUpdateClick = false;
        $scope.showAddStaff = false;
        $scope.StaffDetails = {};
        $scope.Stafflist = {};
        $scope.Classlist = {};
        $scope.Divisionlist = {};
        $scope.Departmentlist = {};
        $scope.Sectionlist = {};
        $scope.primaryTaglist = {};
        $scope.Countrylist = {};
        $scope.Statelist = {};
        $scope.filterOption = {
            name: "",
            departmentId: "0"
           
        };
        $scope.clearFilter = function () {
            $scope.filterOption = {
                name: "",
                departmentId: "0"
                
            };
        };
        $scope.validateFilterOption = function (filterObj) {
            if (filterObj.name == "" && (filterObj.departmentId == 0 || filterObj.departmentId == undefined)) {
                return false;
            }
            else {
                return true;
            }

        };

        $scope.StaffDetails.SchoolId = $rootScope.User.SchoolId;

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
                        $scope.loadStaffList($rootScope.User.SchoolId);
                        $scope.loadDepartmentList();
                    }
                    //$scope.TagDetails = result.data.TagDetails;
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }

        $scope.loadDepartmentList = function () {
            StaffService.getDepartmentList().then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        debugger
                        $scope.Departmentlist = result.data.Departments;
                        //$scope.Departmentlist.add();
                    }
                    //$scope.TagDetails = result.data.TagDetails;
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }

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

        $scope.loadStaffList = function (brabchId) {
            $scope.clearFilter();
            $scope.Stafflist = {};
            if (brabchId == null || brabchId == undefined) {
                return false;
            }
            StaffService.getStaffList(brabchId).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.Stafflist = result.data.Staffs;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };

        $scope.loadStaffBySearch = function (filterOption) {
            var isFilterValid = $scope.validateFilterOption(filterOption);
            if (!isFilterValid) {
                alert("Please select atleast one filter option");
                return false;
            }
            $scope.Stafflist = {};
            StaffService.getStaffListBySearch(filterOption).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.Stafflist = result.data.Staffs;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };

        $scope.AddStaff = function (isvalid, staffDetail) {
            $scope.clearFilter();
            StaffService.addStaff(staffDetail).then(function (result) {
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

        $scope.ShowStaffDetails = function (row) {
            var rowindex = row.rowIndex;
            row.entity.DateOfBirthh = $filter('date')(new Date(row.entity.DateOfBirthh), 'MM/dd/yyyy');
            //row.entity.DateOfBirthh = new Date(Date.parse(row.entity.DateOfBirthh, "dd/mm/yyyy"));
            $scope.StaffDetails = row.entity;

            $('#myStaffModal').modal('show');
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

        $scope.Staffgrid = {
            data: 'Stafflist',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            columnDefs: [{ field: "StaffName", displayName: "Name", width: 400, cellTemplate: '<a href="" class=\"HighlightColumn\" ng-click="ShowStaffDetails(row);"><div class=\"ngCellText\">{{row.getProperty(col.field)}}</div></a>' },
                         { field: "StaffId", displayName: "Id" },
                         { field: "DepartmentName", displayName: "Department" },
                         { field: "ClassName", displayName: "Class" },
                         { field: "SectionName", displayName: "Section" },
                         { field: "ReportingEmailId", displayName: "Email Id" },
                         { field: "StaffMobileNo", displayName: "Mobile No." },
                         { field: '', displayName: 'Action', cellTemplate: '<a style="cursor: pointer;" ng-click="deleteStaff(row.entity.Id);"  id="delete"  data-toggle="tooltip">Delete</i></a>' }]
        };

        $scope.UpdateStaff = function (isvalid, objStaff) {
            $scope.StaffDetails.SchoolId = $rootScope.User.SchoolId;
            $scope.AddStaff(isvalid, objStaff);
        }
    }
    module.controller('StaffManagement', ['$rootScope', '$location', '$scope', 'StaffService', 'LoginService', 'CommonService', '$state', '$filter', staffManagement]);
}(angular.module('StudentTracking.staff')));