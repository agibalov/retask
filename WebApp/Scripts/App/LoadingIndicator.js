angular.module("app").directive("loadingIndicator", (function () {
    var x = function(loadingIndicatorService) {
        return {
            restrict: "A",
            link: function(scope, element, attrs) {
                var loadingClass = attrs.loadingClass;

                var getIsLoading = function() {
                    return loadingIndicatorService.getState().isLoading;
                };

                scope.$watch(getIsLoading, function(isLoading) {
                    if (isLoading) {
                        element.addClass(loadingClass);
                    } else {
                        element.removeClass(loadingClass);
                    }
                });
            }
        };
    };

    x.$inject = ["loadingIndicatorService"];
    return x;
})());