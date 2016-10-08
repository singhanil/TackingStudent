(function () {
    "use strict";

    angular.module("StudentTracking.notification", ['ngGrid']);

}());
(function (module) {
    'use strict';
    var notification = function ($rootScope, $scope, NotificationService, CommonService, StudentService) {
        var vm = $scope;
        $scope.Notification = {
            MessageType: '',
            ClassId: 0,
            SectionId: 0,
            StudentId: 0,
            Subject: null,
            MessageText: null
        };
        $scope.filterOption = {
            name: "",
            classId: 0,
            sectionId: 0
        };
        $scope.notificationText = "";
        $scope.myNotificationText = "";
        $scope.Classlist = {};
        $scope.Sectionlist = {};
        $scope.Studentlist = {};
        $scope.loadCommonData = function () {
            CommonService.getCommonData().then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.Classlist = result.data.Classes;
                        $scope.Sectionlist = result.data.Sections;
                        $scope.loadStudentList($rootScope.User.SchoolId);
                    }
                    //$scope.TagDetails = result.data.TagDetails;
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        $scope.loadStudentList = function (brabchId) {
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
                        $scope.Studentlist = result.data.Students;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        $scope.loadStudentListBySearch = function (filterOption) {

            if (filterOption.classId == undefined && filterOption.sectionId != undefined) {
                filterOption.classId = 0;
            }
            if (filterOption.classId != undefined && filterOption.sectionId == undefined) {
                //$scope.loadStudentList($rootScope.User.SchoolId);
                filterOption.sectionId = 0;
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
        $scope.getNotificationList = function () {
            NotificationService.getNotifications().then(function (result) {
                $scope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalide security token" || result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $scope.Logout();
                    }
                    else {
                        $rootScope.Notifications = result.data.Notifications;
                        var newNotification = $.grep(result.data.Notifications, function (n, i) {
                            return n.IsNew === true;
                        });
                        $rootScope.NotificationCount = $(newNotification).length;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
                $scope.loadCommonData();
            });
        };
        //$scope.sendNotification = function (notificationText) {
        //    NotificationService.sendNotifications().then(function (result) {
        //        $scope.ajaxError = false;
        //        if (result != null) {
        //            if (result.data.ErrorMessage == "Invalide security token") {
        //                alert(result.data.ErrorMessage);
        //                $scope.Logout();
        //            }
        //            else {
        //                alert("notification send.");
        //                $scope.notificationtext = "";
        //                $('#mynotificationmodal').modal('hide');
        //                $scope.getNotificationList();
        //            }
        //        }
        //    }, function (result) {
        //        $rootScope.ajaxError = true;
        //    });
        //};
        //if ($rootScope.Notifications == null) {
        //    $scope.getNotificationList();
        //}
        $scope.showNotificationForm = function () {
            //$('#myNotificationTextModal').modal('show');
            //$("#myModal").modal('show');
        };
        
        if ($rootScope.Classlist == undefined || $rootScope.Sectionlist == undefined) {
            $scope.loadCommonData();
            $scope.Classlist = $rootScope.Classlist;
            $scope.Sectionlist = $rootScope.Sectionlist;
        }
        else {
            $scope.loadStudentList($rootScope.User.SchoolId);
        }
        $scope.SendNotification = function (isvalid, Notification, filterOption) {
            debugger;
            Notification.ClassId = filterOption.classId;
            Notification.SectionId = filterOption.sectionId;
            var objFiles = $("#attachment").prop("files");
            var fd = new FormData();
            fd.append("file", objFiles[0]);
            fd.append("data", JSON.stringify(Notification));
            NotificationService.sendNotifications(fd).then(function (result) {
                $scope.ajaxError = false;
                if (result != null) {
                    alert("file uploaded");
                }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        $scope.ShowFullText = function (textMessage) {
            if (textMessage.length <= 65) {
                return false;
            }
            $scope.myNotificationText = textMessage;
            $('#myNotificationTextModal').modal('show');
        };
        $scope.showNotificationForm = function () {
            $('#myNotificationModal').modal('show');
        };
        $scope.notifyToAdmin = function () {
            alert("notification send.");
            $scope.notificationtext = "";
            $('#mynotificationmodal').modal('hide');
            //$scope.sendNotification($scope.notificationText);
        };
        $scope.cancelNotification = function () {
            $scope.notificationText = "";
            $('#myNotificationModal').modal('hide');
        };

        $scope.Studentgrid = {
            data: 'Studentlist',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            columnDefs: [{ field: "StudentName", displayName: "Student Name", cellTemplate: '<a href="" class=\"HighlightColumn\" ng-click="ShowResult(row);"><div class=\"ngCellText\">{{row.getProperty(col.field)}}</div></a>' },
                         { field: "StudentId", displayName: "Student Id" },
                         { field: "ClassName", displayName: "Class" },
                         { field: "SectionName", displayName: "Section" },
                         { field: "ParentName", displayName: "Parent Name" },
                         { field: "ParentMobileNo", displayName: "Parent MobileNo" }]
        };
    }
    module.controller('Notification', ['$rootScope', '$scope', 'NotificationService', 'CommonService', 'StudentService', notification]);
}(angular.module('StudentTracking.notification')));
