(function (module) {
    'use strict';
    var calendarController = function ($rootScope, $location, $scope, $compile, SchoolService, LoginService, $state) {
        $scope.eventSources = {};
        $scope.events = [];
        $scope.getEventList = function (callback) {
            $scope.events = [];
           
            SchoolService.getEventList().then(function (result) {
                $rootScope.ajaxError = false;
                if (result != null) {
                    if (result.data.ErrorMessage == "Invalid or expired token") {
                        alert(result.data.ErrorMessage);
                        $rootScope.Logout();
                    }
                    else {
                        $scope.events = result.data.eventlist;
                        callback(result.data.eventlist);
                        callback = null;
                        //$scope.events = $scope.mydata;
                    }
                }
            }, function (result) {
                $rootScope.ajaxError = true;
                callback([]);
                callback = null;
            });
        }
        //$scope.getEventList();
        var date = new Date();
        var d = date.getDate();
        var m = date.getMonth();
        var y = date.getFullYear();
        /* event source that calls a function on every view switch */
        $scope.eventsF = function (start, end, timezone, callback) {
            var s = new Date(start).getTime() / 1000;
            var e = new Date(end).getTime() / 1000;
            var m = new Date(start).getMonth();
            //var events = [{ title: 'Feed Me ' + m, start: s + (50000), end: s + (100000), allDay: false, className: ['customFeed'] }];
            $scope.getEventList(function (response) {
                callback($scope.events);
            });
            
        };
        
        /* Change View */
        $scope.changeView = function (view, calendar) {
            uiCalendarConfig.calendars[calendar].fullCalendar('changeView', view);
        };
        /* Change View */
        $scope.renderCalender = function (calendar) {
            if (uiCalendarConfig.calendars[calendar]) {
                uiCalendarConfig.calendars[calendar].fullCalendar('render');
            }
        };
        /* Render Tooltip */
        $scope.eventRender = function (event, element, view) {
            element.attr({
                'tooltip': event.title,
                'tooltip-append-to-body': true
            });
            $compile(element)($scope);
        };
        
        /* config object */
        $scope.uiConfig = {
            calendar: {
                height: 600,
                editable: true,
                header: {
                    left: 'title',
                    center: '',
                    right: 'today prev,next'
                },
                eventClick: $scope.alertOnEventClick,
                eventDrop: $scope.alertOnDrop,
                eventResize: $scope.alertOnResize,
                eventRender: $scope.eventRender
            }
        };

        /* event sources array*/
        $scope.eventSources = [$scope.events, $scope.eventSource, $scope.eventsF];
    }
    module.controller('calendarController', ['$rootScope', '$location', '$scope', '$compile', 'SchoolService', 'LoginService', '$state', calendarController]);
}(angular.module('StudentTracking.report')));