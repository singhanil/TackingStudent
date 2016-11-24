(function () {
    "use strict";

    angular.module("StudentTracking.notification", ['ngGrid']);

}());
(function (module) {
    'use strict';
    var notification = function ($rootScope, $scope, NotificationService, CommonService, StudentService) {
        var vm = $scope;
        $scope.Notification = {
            MessageType: 'Message',
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
                        //$rootScope.Notifications = result.data.Notifications;
                        var newNotification = $.grep(result.data.List, function (n, i) {
                            return n.IsNew === true;
                        });
                        $scope.MessageList = $.grep(newNotification, function (n, i) {
                            return n.MessageType === 'Message';
                        });
                        $scope.NotificationList = $.grep(newNotification, function (n, i) {
                            return n.MessageType === 'Notification';
                        });
                        $scope.HomeworkList = $.grep(newNotification, function (n, i) {
                            return n.MessageType === 'Homework';
                        });
                        //$rootScope.NotificationCount = $(newNotification).length;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
                //$scope.loadCommonData();
            });
        };
        $scope.showNotificationForm = function () {
            //$('#myNotificationTextModal').modal('show');
            //$("#myModal").modal('show');
        };
        $scope.getNotificationList();
        if ($rootScope.Classlist == undefined || $rootScope.Sectionlist == undefined) {
            $scope.loadCommonData();
        }
        else {
            $scope.Classlist = $rootScope.Classlist;
            $scope.Sectionlist = $rootScope.Sectionlist;
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
        $scope.showNotificationForm = function () {
            $scope.Notification = {
                MessageType: 'Message',
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
            $('#myNotificationModal').modal('show');
        };
        $scope.cancelNotification = function () {
            $scope.notificationText = "";
            $('#myNotificationModal').modal('hide');
        };

        $scope.Messagegrid = {
            data: 'MessageList',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            columnDefs: [{ field: "Sender", displayName: "From" },
                         { field: "Subject", displayName: "Subject" },
                         { field: "SentDate", displayName: "Date", cellFilter: 'date:\'MM/dd/yyyy\'' }]
        };
        $scope.Notificationgrid = {
            data: 'NotificationList',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            columnDefs: [{ field: "Sender", displayName: "From" },
                         { field: "Subject", displayName: "Subject" },
                         { field: "SentDate", displayName: "Date" }]
        };
        $scope.Homeworkgrid = {
            data: 'HomeworkList',
            enableSorting: true,
            enableRowSelection: false,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            columnDefs: [{ field: "Sender", displayName: "From" },
                         { field: "Subject", displayName: "Subject" },
                         { field: "SentDate", displayName: "Date" }]
        };
    }
    module.controller('Notification', ['$rootScope', '$scope', 'NotificationService', 'CommonService', 'StudentService', notification]);
}(angular.module('StudentTracking.notification')));
