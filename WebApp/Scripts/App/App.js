angular.module("app", ["ngCookies"], ["$httpProvider", function($httpProvider) {
    $httpProvider.responseInterceptors.push("ErrorHandlingResponseInterceptor");
}]);