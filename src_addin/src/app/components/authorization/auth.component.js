(function () {
    angular.module('myApp')
        .component('hsAuth', {
            controller: function () {
                var self = this;




            },
            templateUrl: 'src/app/components/authorization/auth.tpl.html',
            bindings: {
                heliosUserEntityId: '=',
                microsoftToken: '='
            }
        });
})();

