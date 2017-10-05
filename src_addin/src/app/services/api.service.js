(function () {
    'use strict';

    angular.module('myApp')
        .service('api', function ($http, $window, apiUrl) {

            this.authHelios = function (login, password) {
                var req = {
                    method: 'POST',
                    url: apiUrl + "/auth/helios",
                    headers: {
                        'content-type': 'application/json'
                    },
                    data: {
                        "login": login,
                        "password": password
                    }
                };

                var promise = $http(req).then(function (responce) {
                    return responce.data;
                }, function () {
                });

                return promise;
            };

            this.authMicrosoft = function (code) {
                var req = {
                    method: 'POST',
                    url: apiUrl + '/auth/microsoft',
                    headers: {
                        'content-type': 'application/json'
                    },
                    data: {
                        'Code': code,
                        'UserEntityId': $window.localStorage.getItem('heliosUserEntityId')
                    }
                };

                var promise = $http(req).then(function (responce) {
                    return responce.data;
                }, function () {
                });

                return promise;
            };

            this.getSynchronizationStatus = function () {
                var req = {
                    method: 'POST',
                    url: apiUrl + '/setting/get-sync-status',
                    headers: {
                        'content-type': 'application/json'
                    },
                    data: {
                        'UserEntityId': $window.localStorage.getItem('heliosUserEntityId')
                    }
                };

                var promise = $http(req).then(function (responce) {
                    return responce.data;
                }, function () {
                });

                return promise;
            };

            this.setSynchronizationStatus = function (status) {
                var req = {
                    method: 'POST',
                    url: apiUrl + '/setting/update-sync-status',
                    headers: {
                        'content-type': 'application/json'
                    },
                    data: {
                        'IsSyncEnabled': status,
                        'UserEntityId': $window.localStorage.getItem('heliosUserEntityId')
                    }
                };

                var promise = $http(req).then(function (responce) {
                    return responce.data;
                }, function () {
                });

                return promise;
            };

        });
})();