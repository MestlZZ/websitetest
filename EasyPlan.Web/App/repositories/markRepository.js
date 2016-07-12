define(['constants', 'storageContext', 'http/storageHttpWrapper', 'mappers/itemMapper'],
    function (constants, storage, storageHttpWrapper, itemMapper) {

        return {
            setValue
        }

        function setValue(value, markId, boardId){
            return storageHttpWrapper.post(constants.storage.host + constants.storage.setMarkValueUrl, {value: value, id: markId});
        }
    });