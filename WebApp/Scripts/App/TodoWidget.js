angular.module("app").directive("todo", (function () {
    var x = function(templatesRoot) {
        return {
            "restrict": "E",
            "templateUrl": templatesRoot + "todo.html",
            "scope": {
                "model": "=",
                "wontDoClicked": "&",
                "startClicked": "&",
                "postponeClicked": "&",
                "doneClicked": "&",
                "notDoneClicked": "&",
                "completeClicked": "&",
                "editClicked": "&"
            },
            "controller": ["$scope", function($scope) {
                $scope.getStatusClass = function() {
                    return $scope.model.status;
                };
            }]
        };
    };

    x.$inject = ["templatesRoot"];
    return x;
})());