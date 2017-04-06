(function () {

    angular.module("enea-app").controller("subscriptionController", function ($scope, $http) {


        var vm = this;
        
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.subscriptionData = {};

        $http.get("/api/subscription")
            .then(function (response) {
                //if success
                angular.copy(response.data, vm.subscriptionData);
            }, function (error) {
                //if fails
                vm.errorMessage = "Nie udało się pobrać informacji o subskrypcji";
            })
            .finally(function () {
                vm.isBusy = false;
            });



        $scope.$watch('vm.subscriptionData.hasActiveSubscription', function (newValue, oldValue) {

            vm.isBusy = true;

            if (newValue === oldValue) {
                return;
            }

            $http.post("/api/subscription", { hasActiveSubscription: vm.subscriptionData.hasActiveSubscription })
                .then(function () {
                    //if success
                    return;

                }, function () {
                    //if fail
                    vm.errorMessage = "Nie udało się zmienić stanu subskrypcji, spróbuj później";
                })
                .finally(function () {
                    vm.isBusy = false;
            });


        });


    });



}());