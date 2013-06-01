angular.module("app").directive("markdown", (function () {
    var x = function() {
        return {
            restrict: "E",
            scope: {
                data: "=data"
            },
            controller: ["$scope", function($scope) {
                $scope.html = function() {
                    var converter = new Markdown.Converter();
                    return converter.makeHtml($scope.data);
                };
            }],
            template: "<div ng-bind-html-unsafe='html()' class='description'></div>"
        };
    };

    x.$inject = [];
    return x;
})());