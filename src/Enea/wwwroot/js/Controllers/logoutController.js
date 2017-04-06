(function () {

    angular.module("enea-app").controller("logoutController", function ($location) {

        localStorage.clearAll();
        $location.path('/');
    });



}());