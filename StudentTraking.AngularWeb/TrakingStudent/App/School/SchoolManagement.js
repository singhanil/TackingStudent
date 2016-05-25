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
        SchoolService.getSchools().then(function (result) {
            debugger
            $scope.Schools = {};
            $rootScope.ajaxError = false;
            if (result != null) {
                //console.log(result.data);
                $scope.Schools = result.data;
            }
        }, function (result) {
            debugger
            $rootScope.ajaxError = true;
        });
        $scope.Schoolsgrid = {
            data: 'Schools',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize:true,
            columnDefs: [{ field: "SchoolName", displayName: "School Name", width:400, cellTemplate: '<a href="" class=\"HighlightColumn\" ng-click="ShowDetails(row);"><div class=\"ngCellText\">{{row.getProperty(col.field)}}</div></a>' },
                         { field: "BranchName", displayName: "Branch Name" },
                         { field: "StateName", displayName: "State Name" },
                         { field: "City", displayName: "City" },
                         { field: "Address1", displayName: "Address" },
                         { field: '', displayName: 'Action', cellTemplate: '<a ng-click="deleteSchool(row.getProperty(col.id))"  id="delete"  data-toggle="tooltip">Delete</i></a>' }]
        };

        $scope.deleteSchool = function (schoolId) {
            alert(schoolId);
        }

        $scope.AddSchool = function (isValid, objSchool) {
            debugger
            $scope.AddSchoolCancel();
        };
        $scope.ShowEditForm = function () {
            $scope.getStates($scope.school.SchoolId);
            $scope.showAddForm = true;
            $scope.IsUpdateClick = true;
            $scope.SelectedSchool = $scope.school.SchoolId;
            $scope.SelectedCountry = "091";
            $scope.SelectedState = $scope.school.StateId;
            $scope.schooldisabled = true;
            $scope.countrydisabled = true;
            $scope.statedisabled = true;
            $('#myModal').modal('hide');
        }
        $scope.ShowAddForm = function () {
            $scope.showAddForm = true;
        };
        $scope.AddSchoolCancel = function () {
            $scope.school = {};
            $scope.SelectedSchool = "";
            $scope.SelectedCountry = "";
            $scope.SelectedState = "";
            $scope.showAddForm = false;
            $scope.IsUpdateClick = false;
            $scope.schooldisabled = false;
            $scope.countrydisabled = false;
            $scope.statedisabled = false;
        };
        $scope.Country = [
                            { CountryCode: "091", CountryName: "India" }
        ];

        $scope.getStates = function (countryid) {
            $scope.States = {};
            if (countryid == null || countryid == undefined) {
                return false;
            }
            SchoolService.getStates(countryid).then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    $scope.States = result.data;
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        $scope.ShowDetails = function (row) {
            var rowindex = row.rowIndex;
            $scope.school = row.entity;
            $('#myModal').modal('show');
        };
    }
    module.controller('SchoolManagement', ['$rootScope', '$location', '$scope', 'SchoolService', 'LoginService', '$state', schoolManagement]);
}(angular.module('StudentTracking.school')));