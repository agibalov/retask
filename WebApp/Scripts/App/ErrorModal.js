angular.module("app").directive("errorModal", (function () {
    var x = function(errorHandlingService) {
        return {
            restrict: "A",
            link: function(scope, element, attrs) {
                var getShouldDisplay = function() {
                    return errorHandlingService.getState().inError;
                };

                scope.$watch(getShouldDisplay, function(shouldDisplay) {
                    if (shouldDisplay) {
                        $(element).modal({ show: true });
                    } else {
                        $(element).modal({ show: false });
                    }
                });
            }
        };
    };

    x.$inject = ["errorHandlingService"];
    return x;
})());