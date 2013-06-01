angular.module("app").factory("errorHandlingService", (function () {
    var x = function() {
        var state = {
            errorCount: 0,
            inError: false
        };

        var updateState = function() {
            state.inError = state.errorCount > 0;
        };

        return {
            getState: function() {
                return state;
            },

            handleHttpError: function(httpStatusCode) {
                ++state.errorCount;
                updateState();
            },

            handleServiceError: function(serviceErrorCode) {
                ++state.errorCount;
                updateState();
            }
        };
    };

    x.$inject = [];
    return x;
})());