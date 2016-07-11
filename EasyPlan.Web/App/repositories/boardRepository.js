define(['constants', 'storageContext'],
    function (constants, storage) {

    return {
        getCollection: getCollection,
        getFirstBoard: getFirstBoard
    }

    function getCollection()
    {
        return storage.boards;
    }

    function getFirstBoard()
    {
        if (!storage.boards[0])
            throw "Failed to load board";

        return storage.boards[0];
    }
});