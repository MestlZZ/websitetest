define(['storageContext'], function (storage) {
    return {
        title: "Hi, sorry for trouble",
        students: ko.observable([]),
        activate: function () {
            this.students(storage.get('students').Data);
        }
    }
});