define(['repositories/studentsRepository'], function (repository) {
    return {
        title: "Hi, sorry for trouble",
        students: ko.observable([]),
        activate: function () {
            this.students(repository.getCollection());
        }
    }
});