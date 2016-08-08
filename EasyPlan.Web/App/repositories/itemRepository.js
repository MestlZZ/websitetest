define(['constants', 'http/storageHttpWrapper'],
    function (constants, storageHttpWrapper) {

        return {
            setTitle: setTitle,
            remove: remove,
            getNewItem: getNewItem
        };

        function setTitle(boardId, title, itemId) {
            if (_.isInvalidText(boardId)) {
                throw 'Invalid board id';
            }

            if (_.isInvalidText(title)) {
                throw 'Invalid title';
            }

            if (_.isInvalidText(titleId)) {
                throw 'Invalid title id';
            }

            return storageHttpWrapper.post(constants.storage.setItemTitleUrl, { boardId: boardId, title: title, itemId: itemId });
        }

        function remove(itemId, boardId) {
            if (_.isInvalidText(itemId)) {
                throw 'Invalid item id';
            }

            if (_.isInvalidText(boardId)) {
                throw 'Invalid board id';
            }

            return storageHttpWrapper.post(constants.storage.removeItemUrl, { itemId: itemId, boardId: boardId });
        }

        function getNewItem(boardId) {
            if (_.isInvalidText(boardId)) {
                throw 'Invalid board id';
            }

            return storageHttpWrapper.post(constants.storage.createNewItemUrl, { boardId: boardId });
        }
    });