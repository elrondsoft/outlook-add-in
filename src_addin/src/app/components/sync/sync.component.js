(function () {
    angular.module('myApp')
        .component('hsSync', {
            controller: function ($scope, api) {
                var self = this;

                self.idLoading = false;
                self.idLocked = false;
                self.isError = false;
                self.errorText = '';
                self.isSyncEnabled = null;
                self.syncInfo = null;

                self.$onInit = function() {
                    self.idLoading = true;
                    self.idLocked = true;

                    api.getSynchronizationStatus().then(function(data) {
                        self.idLoading = false;
                        self.idLocked = false;

                        if (data == null) {
                            self.isError = true;
                            self.errorText = "Helios API network is unreachable";
                            return;
                        }

                        if (data.error) {
                            self.isError = true;
                            self.errorText = data.error;
                        }

                        if (data.isSyncEnabled != null) {
                            self.isSyncEnabled = data.isSyncEnabled;
                        }

                        if (data.syncInfo != null) {
                            self.syncInfo = data.syncInfo;
                        }
                    });
                };

                self.disableSync = function () {
                    self.idLoading = true;
                    self.idLocked = true;
                    api.setSynchronizationStatus(false).then(function(data) {
                        self.idLoading = false;
                        self.idLocked = false;

                        if (data == null) {
                            self.isError = true;
                            self.errorText = "Helios API network is unreachable";
                            return;
                        }

                        if (data.error) {
                            self.isError = true;
                            self.errorText = data.error;
                        }

                        if (data.isSyncEnabled != null) {
                            self.isSyncEnabled = data.isSyncEnabled;
                        }

                        if (data.syncInfo != null) {
                            self.syncInfo = JSON.parse(data.syncInfo);
                            console.log(self.syncInfo);
                        }
                    });
                };

                self.enableSync = function () {
                    self.idLoading = true;
                    self.idLocked = true;
                    api.setSynchronizationStatus(true).then(function(data) {
                        self.idLoading = false;
                        self.idLocked = false;

                        if (data == null) {
                            self.isError = true;
                            self.errorText = "Helios API network is unreachable";
                            return;
                        }

                        if (data.error) {
                            self.isError = true;
                            self.errorText = data.error;
                        }

                        if (data.isSyncEnabled != null) {
                            self.isSyncEnabled = data.isSyncEnabled;
                        }
                    });
                };
            },
            templateUrl: 'src/app/components/sync/sync.tpl.html',
            bindings: {

            }
        });
})();

