(function () {

    // =======================================================================//
    // Environment variables                                                  //
    // =======================================================================//

    var app = angular.module('myApp', ['templates', 'ngMaterial']);

    var clientId = 'cd1488fa-849d-4f93-8558-f85ca902cf61';
    var apiUrl = 'https://dev-helios-api.azurewebsites.net';
    var redirectUri = 'https://dev-helios-addin.azurewebsites.net/auth.html';

    /* Comment to deploy on prod */
    // var apiUrl = 'http://localhost:32748';
    // var redirectUri = 'http://localhost:3000/auth.html';

    app.value('clientId', clientId);
    app.value('apiUrl', apiUrl);
    app.value('redirectUri', redirectUri);

    // =======================================================================//
    // Custom Routing                                                         //
    // =======================================================================//

    var customRouting = {
        hsAuth: true,
        hsSync: false
    };

    app.value('customRouting', customRouting);

    // =======================================================================//
    // App Initialization                                                     //
    // =======================================================================//

    app.component('app', {
        controller: function ($scope, $window, customRouting) {
            var self = this;

            self.customRouting = customRouting;
            self.isAuthorized = false;

            this.$onInit = function () {
                self.heliosUserEntityId = $window.localStorage.getItem('heliosUserEntityId');
                self.microsoftToken = $window.localStorage.getItem('microsoftToken');

                if (self.heliosUserEntityId && self.microsoftToken) {
                    self.navigate('hsSync');
                }
            };

            self.navigate = function (component) {
                for (var key in self.customRouting) {
                    self.customRouting[key] = false;
                }

                customRouting[component] = true;
            };
        },
        templateUrl: 'src/app/app.tpl.html',
        bindings: {}
    });
})();