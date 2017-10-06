(function () {
    angular.module('myApp')
        .component('hsAuthModal', {
            controller: function ($scope, $window, $timeout, $interval) {
                $scope.outlook_token = null;

                var interval = $interval(function () {
                    var url = new URL(window.location);
                    var microsoft_code = url.searchParams.get("code");

                    if (microsoft_code) {

                        debugger;
                        $window.localStorage.setItem('microsoft_code', microsoft_code);
                        $interval.cancel(interval);
                        $window.close();
                    }
                }, 500);
            },
            templateUrl: 'src/app/components/authorization/microsoft/auth-modal.tpl.html',
            bindings: {}
        });
})();

