(function () {

    angular.module("enea-app").controller("homeController", function ($http) {

        var vm = this;
        vm.errorMessage = "";
        vm.disconnections = {};
        vm.isBusy = true;

        $http.get("/api/disconnections")
        .then(function (response) {

            //if succeeded
            angular.copy(response.data, vm.disconnections);

        }, function (error) {

            //if failed
            vm.errorMessage = "Nie udało się pobrać wyłączeń ze strony Enea, spróbuj ponownie później.";

        }).finally(function () {
            vm.isBusy = false;
        });

    });


}());