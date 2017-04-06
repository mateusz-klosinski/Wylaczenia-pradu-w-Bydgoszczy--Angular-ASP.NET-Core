(function () {

    "use strict";

    angular.module("enea-app", ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'ngPassword']).config(function ($routeProvider) {

        $routeProvider.when("/", {
            controller: "homeController",
            controllerAs: "vm",
            templateUrl: "/views/index.html"
        })
        .when("/about", {
            templateUrl: "/views/about.html"
        })
        .when("/:userName/subscription", {
            controller: "subscriptionController",
            controllerAs: "vm",
            templateUrl: "/views/subscription.html"
        })
        .when("/:userName/keywords", {
            controller: "keywordsController",
            controllerAs: "vm",
            templateUrl: "/views/keywords.html"
        })
        .when("/:userName/settings", {
            controller: "settingsController",
            controllerAs: "vm",
            templateUrl: "/views/settings.html"
        })
        .when("/logout", {
            controller: "logoutController",
            template: ''
        })
        .otherwise({ redirectTo: "/" });


    });

}());