/*
 * Copyright (c) Microsoft Corporation. All rights reserved. Licensed under the MIT license.
 * See LICENSE in the project root for license information.
 */

'use strict';

(function () {

    // create
    angular
        .module('helios', [])
        .controller('HomeController', [HomeController])
        .config(['$logProvider', function ($logProvider) {
            // set debug logging to on
            if ($logProvider.debugEnabled) {
                $logProvider.debugEnabled(true);
            }
        }]);

    /**
     * Home Controller
     */
    function HomeController() {
        this.title = 'Home';
        console.log(this.title + ' is ready!');

        this.run = function () {


            /**
             * Insert your Outlook code here
             */

        }
    }

    // when Office has initalized, manually bootstrap the app
    Office.initialize = function () {
        angular.bootstrap(document.body, ['helios']);
    };

})();