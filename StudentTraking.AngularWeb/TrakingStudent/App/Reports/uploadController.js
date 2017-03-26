(function (module) {
    'use strict';
    var uploadController = function ($rootScope, $location, $scope, CommonService, LoginService, $state) {
        $scope.uploadDocument = function () {
            var objFiles = $("#document").prop("files");
            var fd = new FormData();
            fd.append("file", objFiles[0]); 
            fd.append("SecurityToken", $rootScope.User.SecurityToken);
            fd.append("UserId", $rootScope.User.Id);
            fd.append("SchoolId", $rootScope.User.SchoolId);
            CommonService.uploadFile(fd).then(function (result) {
                $scope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalide security token" || result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $scope.Logout();
                    }
                    else if (result.data.ErrorMessage == "Document upload failed.") {
                        alert(result.data.ErrorMessage);
                    }
                    else {
                        alert("Document uploaded successfully.");
                        $("#document").val('');
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        }
    }
    module.controller('uploadController', ['$rootScope', '$location', '$scope', 'CommonService', 'LoginService', '$state', uploadController]);
}(angular.module('StudentTracking.report')));