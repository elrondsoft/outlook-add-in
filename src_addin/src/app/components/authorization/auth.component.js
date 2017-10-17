(function () {
    angular.module('myApp')
        .component('hsAuth', {
            controller: function () {
                var self = this;

                self.navigateInner = function (component) {

                    self.navigate({component: component});
                }

            },
            templateUrl: 'src/app/components/authorization/auth.tpl.html',
            bindings: {
                heliosUserEntityId: '=',
                microsoftToken: '=',
                navigate: '&'
            }
        });
})();

