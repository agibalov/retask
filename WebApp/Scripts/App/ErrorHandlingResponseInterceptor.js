angular.module("app").factory("ErrorHandlingResponseInterceptor", (function () {
    var x = function($q, loadingIndicatorService, errorHandlingService) {
        return function(promise) {
            return promise.then(function(response) {
                loadingIndicatorService.notifyRequestFinished();

                var contentType = response.headers()["content-type"];
                if (contentType.indexOf("application/json") !== -1) {
                    var serviceResult = response.data;
                    if (!serviceResult.Ok && serviceResult.FieldsInError === null) {
                        errorHandlingService.handleServiceError(serviceResult.Error);
                        return $q.reject(response);
                    }
                }

                return response;
            }, function(response) {
                loadingIndicatorService.notifyRequestFinished();
                errorHandlingService.handleHttpError(response.status);

                return $q.reject(response);
            });
        };
    };

    x.$inject = ["$q", "loadingIndicatorService", "errorHandlingService"];
    return x;
})());