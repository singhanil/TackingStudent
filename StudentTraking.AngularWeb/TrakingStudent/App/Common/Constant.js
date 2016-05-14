(function (module) {
    'use strict';

    function constant($rootScope) {
        //#region Api's
        var studentTrakingApiUrl = "http://localhost/studentTrackingAPI";
        var getLoginUrl = "/api/LogIn";
        
        //#endregion
         var getConstant = function (key) {
            var _constant = "";
            //*******************************************************
            //please add all key in lowercase manner
            //debugger;
            //********************************************************
            switch (key.toLowerCase()) {
                case 'studenttrakingurl':
                    _constant = studentTrakingApiUrl;
                    break;
                case 'loginurl':
                    _constant = getLoginUrl;
                    break;
                default:
                    break;
            }
            return _constant;
        };
        function oConstant() { }
        oConstant.prototype = {
            get: function (key) { return getConstant(key); }
        };
        return oConstant;
    }
    module.factory('Constant', ['$rootScope',constant]);
}(angular.module('StudentTracking.constant', [])));