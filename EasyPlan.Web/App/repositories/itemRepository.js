define(['constants', 'http/storageHttpWrapper'],
    function (constants, storageHttpWrapper) {

        return {
            setTitle: setTitle,
            remove: remove,
            getNewItem: getNewItem
        };

        function setTitle(boardId, title, itemId) {
            if (_.isNull(title) || _.isUndefined(title) || _.isEmpty(title))
                throw "Invalid title id";

            if (_.isNull(itemId) || _.isUndefined(itemId) || _.isEmpty(itemId))
                throw "Invalid item id";

            return storageHttpWrapper.post(constants.storage.setItemTitleUrl, { boardId: boardId, title: title, itemId: itemId });
        }

        function remove(itemId, boardId) {
            if (_.isNull(itemId) || _.isUndefined(itemId) || _.isEmpty(itemId))
                throw "Invalid item id";

            return storageHttpWrapper.post(constants.storage.removeItemUrl, { itemId: itemId, boardId: boardId });
        }

        function getNewItem(boardId) {
            return storageHttpWrapper.post(constants.storage.createNewItemUrl, { boardId: boardId });
        }
    });