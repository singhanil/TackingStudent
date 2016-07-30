(function (module) {
    'use strict';
    var schoolManagement = function ($rootScope, $location, $scope, $state, $window, SchoolService) {
        var vm = $scope;
        var currentstate = $state.current.name == 'dashboard.timetable'?'timetable':'syllabus';
        $scope.selectedDay = "";
        $scope.Timetable={};
        $scope.TimetableDetails={};
        $rootScope.Syllabus={};
        $scope.getSyllabus=function(){
             SchoolService.getSyllabusResult().then(function (result) {
                $scope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalide security token" || result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $scope.Logout();
                    }
                    else {
                        $rootScope.Syllabus = result.data.Syllabus;
                    }
               }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        $scope.getTimetable=function(){
            SchoolService.getTimeTable().then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalide security token" || result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.Timetable = result.data;
                    }
               }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        if(currentstate=='timetable'){
                $scope.getTimetable();
        }
        if(currentstate=='syllabus'){
                $scope.getSyllabus();
        }
        $scope.showTimeTable=function(dayVal){
            $scope.selectedDay=dayVal;
            $scope.TimetableDetails=$scope.Timetable[dayVal];
            $('#timeTableModal').modal('show');
        };
        $scope.openPdf=function(fileUrl){
            $window.open(fileUrl, "_system", "location=no");
        };  
    }
    module.controller('SchoolManagement', ['$rootScope', '$location', '$scope', '$state', '$window', 'SchoolService', schoolManagement]);
}(angular.module('StudentTracking.school')));