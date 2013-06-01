angular.module("app").controller("TodoController", (function () {
    var x = function($scope, $window, $q, api, createTaskService, editTaskService, landingPageUrl, adminPageUrl) {
        $scope.userEmail = "";
        $scope.editingTask = null;
        $scope.tasks = [];
        $scope.isAdmin = false;
        $scope.adminPageUrl = "";

        var sessionToken = api.getSessionToken();
        if (sessionToken === undefined) {
            $window.location.href = landingPageUrl;
            return;
        }

        api.getUserInfo(function(result) {
            $scope.userEmail = result.Payload.Email;
            $scope.isAdmin = result.Payload.IsAdmin;
            $scope.adminPageUrl = adminPageUrl;

            api.getWorkspace(function(result) {
                $scope.tasks = _.union($scope.tasks, result.Payload.Tasks);
            });
        });

        $scope.getNotDone = function() {
            var filtered = _.filter($scope.tasks, function(task) {
                return task.TaskStatus === 0;
            });
            var sorted = _.sortBy(filtered, function(task) {
                return task.TaskId;
            });
            return sorted;
        };

        $scope.getInProgress = function() {
            var filtered = _.filter($scope.tasks, function(task) {
                return task.TaskStatus === 1;
            });
            var sorted = _.sortBy(filtered, function(task) {
                return task.TaskId;
            });
            return sorted;
        };

        $scope.getDone = function() {
            var filtered = _.filter($scope.tasks, function(task) {
                return task.TaskStatus === 2;
            });
            var sorted = _.sortBy(filtered, function(task) {
                return task.TaskId;
            });
            return sorted;
        };

        $scope.CreateTaskClicked = function() {
            createTaskService.requestTaskDescription(function(taskDescription) {
                var deferred = $q.defer();
                api.createTask(taskDescription, function(result) {
                    if (!result.Ok) {
                        deferred.reject(result.FieldsInError);
                        return;
                    }

                    $scope.tasks = _.union($scope.tasks, [result.Payload]);
                    deferred.resolve();
                });

                return deferred.promise;
            });
        };

        $scope.EditClicked = function(task) {
            editTaskService.requestTaskUpdate(task.TaskDescription, function(taskDescription) {
                var deferred = $q.defer();
                api.updateTask(task.TaskId, taskDescription, function(result) {
                    if (!result.Ok) {
                        deferred.reject(result.FieldsInError);
                        return;
                    }

                    var updatedTask = result.Payload;
                    replaceTask(updatedTask);
                    deferred.resolve();
                });

                return deferred.promise;
            });
        };

        var replaceTask = function(newTask) {
            var taskId = newTask.TaskId;
            var localTask = _.find($scope.tasks, function(t) {
                return t.TaskId === taskId;
            });

            $scope.tasks = _.without($scope.tasks, localTask);
            $scope.tasks = _.union($scope.tasks, newTask);
        };

        var progressTask = function(task) {
            api.progressTask(task.TaskId, function(result) {
                var updatedTask = result.Payload;
                replaceTask(updatedTask);
            });
        };

        var unprogressTask = function(task) {
            api.unprogressTask(task.TaskId, function(result) {
                var updatedTask = result.Payload;
                replaceTask(updatedTask);
            });
        };

        $scope.WontDoClicked = function(task) {
            api.deleteTask(task.TaskId, function(result) {
                $scope.tasks = _.without($scope.tasks, task);
            });
        };

        $scope.StartClicked = function(task) {
            progressTask(task);
        };

        $scope.PostponeClicked = function(task) {
            unprogressTask(task);
        };

        $scope.DoneClicked = function(task) {
            progressTask(task);
        };

        $scope.NotDoneClicked = function(task) {
            unprogressTask(task);
        };

        $scope.CompleteClicked = function(task) {
            progressTask(task);
        };
    };

    x.$inject = ["$scope", "$window", "$q", "api", "createTaskService", "editTaskService", "landingPageUrl", "adminPageUrl"];
    return x;
})());