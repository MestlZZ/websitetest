define(['constants', 'storageContext'],
    function (constants, storage) {

    return {
        getCollection: getCollection,
        clearCollection: clearCollection
    }

    function getCollection()
    {
        return storage.get(constants.storage.keys.boards);
    }

    function clearCollection()
    {
        storage.remove(constants.storage.keys.boards);
    }
});