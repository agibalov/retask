angular.module("app").factory("createTaskService", (function () {
    var x = function() {
        return {
            registerController: function(controller) {
                this.controller = controller;
            },
            requestTaskDescription: function(onSave) {
                this.controller.show(onSave);
            }
        };
    };

    x.$inject = [];
    return x;
})());