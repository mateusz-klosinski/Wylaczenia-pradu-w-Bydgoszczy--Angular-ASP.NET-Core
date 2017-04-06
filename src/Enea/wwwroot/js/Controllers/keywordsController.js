(function () {

    angular.module("enea-app").controller("keywordsController", function ($http, $scope, $uibModal) {

        var vm = this;

        vm.isBusy = true;
        vm.errorMessage = "";
        vm.successMessage = "";

        vm.keyWords = [];
        vm.newKeyWord = {};


        vm.keyWordChanged = function (keyWord) {

            vm.isBusy = true;


            $http.post("/api/keywords", keyWord)
                .then(function (response) {
                    //if success
                    return;

                }, function () {
                    vm.errorMessage = "Nie udało się zaktualizować słowa, spróbuj ponownie później";
                })
                .finally(function () {
                    vm.isBusy = false;
                });

        };


        vm.openModal = function (keyWord) {

            var modal = $uibModal.open({
                size: 'lg',
                controller: 'deleteModalController',
                controllerAs: 'vm',
                templateUrl: '/views/delete-modal.html',
                resolve: {
                    keyWord: function () {
                        return keyWord;
                    }
                }
            });

            modal.result.then(function (keyWord) {
                //success modal
                vm.successMessage = "";

                vm.keyWords.splice(vm.keyWords.indexOf(keyWord), 1);
                vm.successMessage = "Usunięto słowo pomyślnie";
            }, function () {
                //cancel modal
            });

        };


        vm.addKeyWord = function () {
            vm.isBusy = true;

            $http.post("/api/keywords/create", vm.newKeyWord)
            .then(function (response) {
                //if created

                vm.keyWords.push(response.data);
            }, function () {
                //if failed

                vm.errorMessage = "Nie udało się dodać słowa";
            })
            .finally(function () {
                vm.isBusy = false;
                vm.newKeyWord = {};
            });
        };


        $http.get("/api/keywords")
            .then(function (response) {
                //if success
                angular.copy(response.data, vm.keyWords);
            }, function (error) {
                //if fails
                vm.errorMessage = "Nie udało się pobrać słów kluczy";
            })
            .finally(function () {
                vm.isBusy = false;
            });



    });


}());