angular.module("app").controller("CreateTaskController", (function () {
    var x = function($scope, createTaskService) {
        createTaskService.registerController($scope);

        $scope.show = function(onSaveClicked) {
            $scope.taskDescription = "";
            $scope.taskDescriptionErrors = [];
            $("#create-todo-modal").modal("show");
            $scope.onSaveClicked = onSaveClicked;
        };

        $scope.saveClicked = function() {
            $scope.taskDescriptionErrors = [];
            var deferred = $scope.onSaveClicked($scope.taskDescription);
            deferred.then(function() {
                $("#create-todo-modal").modal("hide");
            }, function(fieldsInError) {
                $scope.taskDescriptionErrors = fieldsInError.TaskDescription;
            });
        };

        $scope.cancelClicked = function() {
            $("#create-todo-modal").modal("hide");
        };
    };

    x.$inject = ["$scope", "createTaskService"];

    return x;
})());