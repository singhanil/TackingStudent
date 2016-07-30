(function (module) {
    'use strict';
    var notification = function ($rootScope, $location, $scope, StudentService, $state, $window) {
        var vm = $scope;
        $scope.notificationText="";
        $scope.myNotificationText="";
        $scope.getNotificationList=function(){
             StudentService.getNotifications().then(function (result) {
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
            });
        };
        $scope.sendNotification=function(notificationText){
             StudentService.sendNotifications().then(function (result) {
                $scope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalide security token") {
                        alert(result.data.ErrorMessage);
                        $scope.Logout();
                    }
                    else {
                            alert("notification send.");
                            $scope.notificationtext="";
                            $('#mynotificationmodal').modal('hide');
                            $scope.getNotificationList();
                    }
               }
            }, function (result) {
                $rootScope.ajaxError = true;
            });
        };
        if($rootScope.Notifications==null){
            $scope.getNotificationList();
        }
        $scope.ShowFullText=function(textMessage){
            if(textMessage.length<=65){
                return false;
            }
            $scope.myNotificationText=textMessage;
            $('#myNotificationTextModal').modal('show');
        };
        $scope.showNotificationForm=function(){
            $('#myNotificationModal').modal('show');
        };
        $scope.notifyToAdmin=function(){
            alert("notification send.");
            $scope.notificationtext="";
            $('#mynotificationmodal').modal('hide');
            //$scope.sendNotification($scope.notificationText);
        };
        $scope.cancelNotification=function(){
            $scope.notificationText="";
            $('#myNotificationModal').modal('hide');
        };
    }
    module.controller('Notification', ['$rootScope', '$location', '$scope', 'StudentService', '$state', '$window', notification]);
}(angular.module('StudentTracking.report')));
