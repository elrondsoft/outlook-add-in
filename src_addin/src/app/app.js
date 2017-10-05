(function () {

    var app = angular.module('myApp', ['templates', 'ngMaterial']);

    // =======================================================================//
    // Constants                                                              //
    // =======================================================================//

    var clientId = 'cd1488fa-849d-4f93-8558-f85ca902cf61';
    var clientSecret = 'scY9Ymn7jtGWdfvWiiedUmq';
    var helisSecretToken = 'QUVBMkUyQTQtMkU2RS00MzNFLUEyOTAtOUMzNzYyOUI3MzU5OiFIZWxpb3NXZWJBcHBQYXNzIQ==';
    var apiUrl = 'https://dev-helios-api.azurewebsites.net';
    var redirectUri = 'https://dev-helios-addin.azurewebsites.net/auth.html';

    /* Comment to deploy on prod */
    var apiUrl = 'http://localhost:32748';
    var redirectUri = 'http://localhost:3000/auth.html';

    app.value('clientId', clientId);
    app.value('clientSecret', clientSecret);
    app.value('helisSecretToken', helisSecretToken);
    app.value('apiUrl', apiUrl);
    app.value('redirectUri', redirectUri);

    // =======================================================================//
    // Custom Routing                                                         //
    // =======================================================================//

    var customRouting = {
        hsAuth: true,
        hsSync: false,
        hsOptions: false
    };

    app.value('customRouting', customRouting);

    app.component('app', {
        controller: function ($scope, $window, customRouting) {
            var self = this;

            $scope.customRouting = customRouting;
            $scope.isAuthorized = false;

            this.$onInit = function () {
                self.heliosUserEntityId = $window.localStorage.getItem('heliosUserEntityId');
                self.microsoftToken = $window.localStorage.getItem('microsoftToken');
            };

            $scope.navigate = function (component) {
                for (var key in $scope.customRouting) {
                    $scope.customRouting[key] = false;
                }

                customRouting[component] = true;
            };
        },
        templateUrl: 'src/app/app.tpl.html',
        bindings: {
        }
    });

    // =======================================================================//
    // App Initialization                                                     //
    // =======================================================================//

    // var heliosUserEntityId = window.localStorage.getItem('heliosUserEntityId');
    // var microsoftToken = window.localStorage.getItem('microsoftToken');

    // app.value('heliosUserEntityId', heliosUserEntityId);
    // app.value('microsoftToken', microsoftToken);
})();