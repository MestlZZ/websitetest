define(['http/storageHttpWrapper', 'constants', 'storageContext'],
    function (storageHttpWrapper, constants, storage) {

    return {
        getCollection: getCollection,
        initialize: initialize,
        clearCollection: clearCollection
    }

    function initialize()
    {
        return Q.fcall(function () {
            storageHttpWrapper.post(constants.storage.host + constants.storage.studentsUrl).then(function (students) {
                storage.set('students', students);
            })
        });
    }

    function getCollection()
    {
        return storage.get('students').Data;
    }

    function clearCollection()
    {
        storage.remove('students');
    }
});