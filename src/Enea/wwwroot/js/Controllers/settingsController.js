(function () {
    
    angular.module("enea-app").controller("settingsController", function ($http, $location) {

        var vm = this;
        vm.isBusy = false;

        vm.newCredentials = {};

        vm.submitCredentials = function (credentialType) {

            vm.isBusy = true;

            vm.emailSuccess = "";
            vm.passwordSuccess = "";
            vm.userNameSuccess = "";
            vm.emailFailed = "";
            vm.passwordFailed = "";
            vm.userNameFailed = "";

            $http.post("/api/settings/setNewCredentials", vm.newCredentials)
                .then(function () {
                    vm.newCredentials = {};

                    if (credentialType === 'email') {
                        vm.emailSuccess = "Zmieniono adres e-mail";
                    }
                    else if (credentialType === 'password') {
                        vm.passwordSuccess = "Zmieniono hasło";
                    }
                    else if (credentialType === 'username') {
                        vm.userNameSuccess = "Zmieniono nazwę użytkownika";
                    }

                    $(".nav-button-text.right").click();

                }, function () {

                    if (credentialType === 'email') {
                        vm.emailFailed = "Nie udało się zmienić adresu e-mail";
                    }
                    else if (credentialType === 'password') {
                        vm.passwordFailed = "Nie udało się zmienić hasła";
                    }
                    else if (credentialType === 'username') {
                        vm.userNameFailed = "Nie udało się zmienić nazwy użytkownika";
                    }


                }).finally(function () {
                    vm.isBusy = false;
                });
        };

    });


}());