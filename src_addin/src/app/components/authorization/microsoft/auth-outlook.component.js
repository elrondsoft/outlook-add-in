(function () {
    angular.module('myApp')
        .component('hsAuthOutlook', {
            controller: function ($scope, $window, $interval, $timeout, $httpParamSerializer, clientId, redirectUri, api) {
                var self = this;

                self.isLoading = false;
                self.isError = false;
                self.errorText = '';

                self.authOutlook = function () {
                    self.isLoading = true;
                    self.isError = false;
                    self.errorText = '';
                    $window.localStorage.removeItem('microsoft_code');
                    $window.localStorage.removeItem('microsoftToken');
                    self.microsoftToken = null;

                    $window.open(self.buildAuthUrl(), '', 'toolbar=0,status=0,width=626,height=436');

                    var interval = $interval(function () {
                        var microsoft_code = $window.localStorage.getItem('microsoft_code');

                        if (microsoft_code) {
                            api.authMicrosoft(microsoft_code).then(function (data) {
                                self.isLoading = false;

                                if (data == null) {
                                    self.isError = true;
                                    self.errorText = "Helios API network is unreachable";
                                    return;
                                }

                                if (data.error) {
                                    self.isError = true;
                                    self.errorText = data.error;
                                }

                                if (data.accessToken) {
                                    self.microsoftToken = data.accessToken;
                                    $window.localStorage.setItem('microsoftToken', data.accessToken);

                                    $timeout(function() {
                                        self.navigateInner({component: 'hsSync'});
                                    },200);
                                }
                            });

                            $interval.cancel(interval);
                        }

                    }, 500);
                };

                self.buildAuthUrl = function () {
                    var authEndpoint = 'https://login.microsoftonline.com/common/oauth2/v2.0/authorize?';

                    var codeAuthParams = {
                        client_id: clientId,
                        response_type: 'code',
                        redirect_uri: redirectUri,
                        response_mode: 'query',
                        scope: 'openid offline_access profile Mail.Read Calendars.ReadWrite Tasks.ReadWrite',
                        state: '12345'
                    };

                    return authEndpoint + $httpParamSerializer(codeAuthParams);
                };

                self.logoutOutlook = function () {
                    self.microsoftToken = null;
                    $window.localStorage.removeItem('microsoftToken');
                };

            },
            templateUrl: 'src/app/components/authorization/microsoft/auth-outlook.tpl.html',
            bindings: {
                microsoftToken: '=',
                navigateInner: '&'
            }
        });
})();

