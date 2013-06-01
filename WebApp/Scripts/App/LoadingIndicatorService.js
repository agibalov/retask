angular.module("app").factory("loadingIndicatorService", (function () {
    var x = function() {
        var state = {
            isLoading: false,
            pendingRequestCount: 0
        };

        var updateState = function() {
            if (state.pendingRequestCount < 0) {
                state.pendingRequestCount = 0;
            }

            state.isLoading = state.pendingRequestCount > 0;
        };

        return {
            getState: function() {
                return state;
            },

            notifyRequestStarted: function() {
                ++state.pendingRequestCount;
                updateState();
            },

            notifyRequestFinished: function() {
                --state.pendingRequestCount;
                updateState();
            }
        };
    };

    x.$inject = [];
    return x;
})());