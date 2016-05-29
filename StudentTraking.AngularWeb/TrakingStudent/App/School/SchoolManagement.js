(function (module) {
    'use strict';
    var schoolManagement = function ($rootScope, $location, $scope, SchoolService, LoginService, $state) {
        var vm = $scope;
        $scope.showAddForm = false;
        $scope.IsUpdateClick = false;
        $scope.school = SchoolService.School;
        $scope.schooldisabled = false;
        $scope.countrydisabled = false;
        $scope.statedisabled = false;
        $scope.SelectedSchool = "";
        $scope.SelectedCountry = "";
        $scope.SelectedState = "";
        $scope.States = {};
        $scope.Schools = {};
        $scope.Organisation = {};
        $scope.count = 4;
        //define all functions
        $scope.getAllSchoolList = function () {
            SchoolService.getSchools().then(function (result) {
                $scope.Schools = {};
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.Schools = result.data.Schools;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };

        $scope.loadOrgList = function () {
            SchoolService.getOrganisations().then(function (result) {
                $scope.Organisation = {};
                $rootScope.ajaxError = false;
                if (result != null) {
                    //console.log(result.data);
                    $scope.Organisation = result.data;
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };

        $scope.getStates = function (countryid) {
            $scope.States = {};
            if (countryid == null || countryid == undefined) {
                return false;
            }
            SchoolService.getStates(countryid).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    $scope.States = result.data;
                    if ($scope.IsUpdateClick) {
                        $scope.statedisabled = true;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };

        $scope.getCountryList = function () {
            $scope.Country = [
                                { CountryCode: "Ind", CountryName: "India" },
            { CountryCode: "Usa", CountryName: "USA" }
            ];
        };

        $scope.deleteSchool = function (schoolid) {
            if (schoolid == null || schoolid == undefined) {
                return false;
            }
            SchoolService.getStates(countryid).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    $scope.States = result.data;
                    if ($scope.IsUpdateClick) {
                        $scope.statedisabled = true;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };

        //initialy get school data
        $scope.getAllSchoolList();

        $scope.Schoolsgrid = {
            data: 'Schools',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            columnDefs: [{ field: "Name", displayName: "School Name", width: 400, cellTemplate: '<a href="" class=\"HighlightColumn\" ng-click="ShowDetails(row);"><div class=\"ngCellText\">{{row.getProperty(col.field)}}</div></a>' },
                         { field: "BranchName", displayName: "Branch Name" },
                         { field: "State", displayName: "State Name" },
                         { field: "City", displayName: "City" },
                         { field: "Address1", displayName: "Address" },
                         { field: '', displayName: 'Action', cellTemplate: '<a style="cursor: pointer;" ng-click="deleteSchool(row.entity.Id);"  id="delete"  data-toggle="tooltip">Delete</i></a>' }]
        };

        $scope.deleteSchool = function (schoolId) {
            return false;
        };

        $scope.AddSchool = function (isValid, objSchool) {
            objSchool.Id = $scope.count + 1;
            SchoolService.addSchools(objSchool).then(function (result) {
                $scope.Organisation = {};
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else{
                        $scope.getAllSchoolList();
                        $scope.AddSchoolCancel();
                    }
                    //$scope.Organisation = result.data;
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
            $scope.AddSchoolCancel();
        };
        $scope.ShowEditForm = function () {
            $scope.showAddForm = true;
            $scope.IsUpdateClick = true;
            $scope.getCountryList();
            $scope.loadOrgList();
            $scope.getStates($scope.school.Country);
            $scope.school.Phone = parseInt($scope.school.Phone);
            
            //$scope.SelectedSchool = $scope.school.SchoolId;
            //$scope.SelectedCountry = "091";
            //$scope.SelectedState = $scope.school.StateId;
            $scope.schooldisabled = true;
            $scope.countrydisabled = true;
            //$scope.statedisabled = true;
            $('#myModal').modal('hide');
        };
        $scope.ShowAddForm = function () {
            $scope.getCountryList();
            $scope.showAddForm = true;
            $scope.school = {};
            $scope.school = SchoolService.School;
            $scope.loadOrgList();
        };
        $scope.AddSchoolCancel = function () {
            $scope.school = {};
            //$scope.SelectedSchool = "";
            //$scope.SelectedCountry = "";
            //$scope.SelectedState = "";
            $scope.showAddForm = false;
            $scope.IsUpdateClick = false;
            $scope.schooldisabled = false;
            $scope.countrydisabled = false;
            $scope.statedisabled = false;
        };

        $scope.ShowDetails = function (row) {
            var rowindex = row.rowIndex;
            $scope.school = row.entity;
            $('#myModal').modal('show');
        };

        $scope.UpdateSchool = function (isValid, objSchool) {
            alert("School data updated successfully!");
            $scope.getAllSchoolList();
            $scope.AddSchoolCancel();
        }


    }
    module.controller('SchoolManagement', ['$rootScope', '$location', '$scope', 'SchoolService', 'LoginService', '$state', schoolManagement]);
}(angular.module('StudentTracking.school')));