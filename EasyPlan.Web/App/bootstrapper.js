define(['bootstrapper.tasks'], function (tasks) {

    return {
        initialize: initialize
    };

    function initialize()
    {
        var tasksPromises = [];
        
        _.each(tasks.getTasks(), function (task) {
            tasksPromises.push(task.initialize());
        });

        return Q.all(tasksPromises);
    }
})