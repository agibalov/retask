angular.module("app").factory("editTaskService", (function () {
    var x = function() {
        return {
            registerController: function(controller) {
                this.controller = controller;
            },
            requestTaskUpdate: function(description, onSave) {
                this.controller.show(description, onSave);
            }
        };
    };

    x.$inject = [];
    return x;
})());