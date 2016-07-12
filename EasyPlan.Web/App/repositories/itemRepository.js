define(['constants', 'storageContext', 'http/storageHttpWrapper', 'mappers/itemMapper'],
    function (constants, storage, storageHttpWrapper, itemMapper) {

        return {
            setTitle,
            remove,
            getNewItem
        }

        function setTitle(title, itemId, boardId){
            return storageHttpWrapper.post(constants.storage.host + constants.storage.setItemTitleUrl, {title: title, id: itemId});
        }

        function remove(itemId, boardId) {
            return storageHttpWrapper.post(constants.storage.host + constants.storage.removeItemUrl, { id: itemId });
        }

        function getNewItem(boardId) {
            return storageHttpWrapper.post(constants.storage.host + constants.storage.createNewItemUrl, { id: boardId }).then(function (item) {

                return itemMapper.map(item);
            });
        }
    });