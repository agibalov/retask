angular.module("app").controller("EditTaskController", (function () {
    var x = function($scope, editTaskService) {
        editTaskService.registerController($scope);

        $scope.show = function(taskDescription, onSaveClicked) {
            $scope.taskDescription = taskDescription;
            $scope.taskDescriptionErrors = [];
            $("#edit-todo-modal").modal("show");
            $scope.onSaveClicked = onSaveClicked;
        };

        $scope.saveClicked = function() {
            $scope.taskDescriptionErrors = [];
            var deferred = $scope.onSaveClicked($scope.taskDescription);
            deferred.then(function() {
                $("#edit-todo-modal").modal("hide");
            }, function(fieldsInError) {
                $scope.taskDescriptionErrors = fieldsInError.TaskDescription;
            });
        };

        $scope.cancelClicked = function() {
            $("#edit-todo-modal").modal("hide");
        };
    };

    x.$inject = ["$scope", "editTaskService"];
    return x;
})());