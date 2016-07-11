define(['constants', 'storageContext'],
    function (constants, storage) {

    return {
        getCollection: getCollection
    }

    function getCollection()
    {
        return storage.boards;
    }
});