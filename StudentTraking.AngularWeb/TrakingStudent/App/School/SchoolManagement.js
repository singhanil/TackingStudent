(function (module) {
    'use strict';
    var schoolManagement = function ($rootScope, $location, $scope, SchoolService, LoginService, $state) {
        var vm = $scope;
        $scope.showAddForm = false;
        $scope.IsUpdateClick = false;
        $scope.school = SchoolService.School;
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
            $rootScope.ajaxError = true;
        });
        $scope.Schoolsgrid = {
            data: 'Schools',
            columnDefs: [{ field: "SchoolName", displayName: "School Name" },
                         { field: "BranchName", displayName: "Branch Name" },
                         { field: "StateName", displayName: "State Name" },
                         { field: "City", displayName: "City" },
                         { field: "Address1", displayName: "Address" }]
        };

        $scope.AddSchool = function (isValid, objSchool) {
            debugger
        };
        $scope.ShowEditForm = function () {
            $scope.showAddForm = true;
            $scope.IsUpdateClick = true;
        }
        $scope.ShowAddForm = function () {
            $scope.showAddForm = true;
        };
        $scope.AddSchoolCancel = function () {
            $scope.school = {};
            $scope.showAddForm = false;
            $scope.IsUpdateClick = false;
        };
        $scope.Country = [
                            { CountryCode: "091", CountryName: "India" }
        ];

        $scope.getStates = function (countryid) {
            debugger;
            $scope.States = {};
            if (countryid == null || countryid == undefined) {
                return false;
            }
            SchoolService.getStates(countryid).then(function (result) {
                debugger
                $rootScope.ajaxError = false;
                if (result != null) {
                    //console.log(result.data);
                    $scope.States = result.data;
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
    }
    module.controller('SchoolManagement', ['$rootScope', '$location', '$scope', 'SchoolService', 'LoginService', '$state', schoolManagement]);
}(angular.module('StudentTracking.school')));