(function () {
    angular.module('myApp')
        .component('hsAuthHelios', {
            controller: function ($window, api) {
                var self = this;

                self.login = "";
                self.password = "";

                self.isLoading = false;
                self.isError = false;
                self.errorText = null;

                self.clearHeliosUserEntityId = function () {
                    $window.localStorage.removeItem('heliosUserEntityId');
                    self.heliosUserEntityId = null;

                    $window.localStorage.removeItem('microsoft_code');
                    $window.localStorage.removeItem('microsoftToken');
                    self.microsoftToken = null;
                };

                self.authHelios = function () {
                    self.isLoading = true;
                    self.isError = false;

                    api.authHelios(self.login, self.password).then(function(data) {
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

                        if (data.entityId) {
                            self.heliosUserEntityId = data.entityId;
                            $window.localStorage.setItem('heliosUserEntityId', data.entityId);
                        }
                    });
                }
            },
            templateUrl: 'src/app/components/authorization/helios/auth-helios.tpl.html',
            bindings: {
                heliosUserEntityId: '=',
                microsoftToken: '='
            }
        });
})();

