angular.module("app").factory("api", (function () {
    var x = function (
        $http,
        $cookies,
        pingPeriod,
        loadingIndicatorService,
        getUserInfoUrl,
        pingUrl,
        createTaskUrl,
        updateTaskUrl,
        progressTaskUrl,
        unprogressTaskUrl,
        deleteTaskUrl,
        getWorkspaceUrl) {

        $http.defaults.transformRequest = function (data) {
            loadingIndicatorService.notifyRequestStarted();
            if (data === undefined) {
                return data;
            }

            return angular.toJson(data);
        };

        return {
            getSessionToken: function () {
                return $cookies.sessionToken;
            },

            getUserInfo: function (onSuccess) {
                $http.get(getUserInfoUrl, {
                    "params": {
                        "sessionToken": $cookies.sessionToken
                    }
                }).success(function (result) {
                    onSuccess(result);
                });
            },

            ping: function (onSuccess) {
                $http.post(pingUrl, null, {
                    "params": {
                        "sessionToken": $cookies.sessionToken
                    }
                });
            },

            createTask: function (taskDescription, onSuccess) {
                $http.post(createTaskUrl, {
                    "taskDescription": taskDescription
                }, {
                    "params": {
                        "sessionToken": $cookies.sessionToken
                    }
                }).success(function (result) {
                    onSuccess(result);
                });
            },

            updateTask: function (taskId, taskDescription, onSuccess) {
                $http.post(updateTaskUrl, {
                    "taskDescription": taskDescription
                }, {
                    "params": {
                        "sessionToken": $cookies.sessionToken,
                        "taskId": taskId
                    }
                }).success(function (result) {
                    onSuccess(result);
                });
            },

            progressTask: function (taskId, onSuccess) {
                $http.post(progressTaskUrl, null, {
                    "params": {
                        "sessionToken": $cookies.sessionToken,
                        "taskId": taskId
                    }
                }).success(function (result) {
                    onSuccess(result);
                });
            },

            unprogressTask: function (taskId, onSuccess) {
                $http.post(unprogressTaskUrl, null, {
                    "params": {
                        "sessionToken": $cookies.sessionToken,
                        "taskId": taskId
                    }
                }).success(function (result) {
                    onSuccess(result);
                });
            },

            deleteTask: function (taskId, onSuccess) {
                $http.post(deleteTaskUrl, null, {
                    "params": {
                        "sessionToken": $cookies.sessionToken,
                        "taskId": taskId
                    }
                }).success(function (result) {
                    onSuccess(result);
                });
            },

            getWorkspace: function (onSuccess) {
                $http.get(getWorkspaceUrl, {
                    "params": {
                        "sessionToken": $cookies.sessionToken
                    }
                }).success(function (result) {
                    onSuccess(result);
                });
            }
        };
    };

    x.$inject = [
        "$http",
        "$cookies",
        "pingPeriod",
        "loadingIndicatorService",
        "getUserInfoUrl",
        "pingUrl",
        "createTaskUrl",
        "updateTaskUrl",
        "progressTaskUrl",
        "unprogressTaskUrl",
        "deleteTaskUrl",
        "getWorkspaceUrl"];

    return x;
})());