(function () {

    angular.module("enea-app").controller("deleteModalController", function ($http, $uibModalInstance, keyWord) {

        var vm = this;
        vm.keyWord = keyWord;
        vm.errorMessage = "";

        vm.delete = function () {
            $http.post("/api/keywords/delete", vm.keyWord).then(function () {
                //if deleted
                $uibModalInstance.close(vm.keyWord);
            }, function () {
                //if failed
                vm.errorMessage = "Nie udało się usunąć słowa";
            });
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

    });


}());