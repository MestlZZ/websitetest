define(['constants', 'http/storageHttpWrapper'],
    function (constants, storageHttpWrapper) {

        return {
            setTitle,
            remove,
            getNewItem
        }

        function setTitle(title, itemId) {
            if (_.isNull(title) || _.isUndefined(title) || _.isEmpty(title))
                throw "Invalid title id"

            if (_.isNull(itemId) || _.isUndefined(itemId) || _.isEmpty(itemId))
                throw "Invalid item id"


            return storageHttpWrapper.post(constants.storage.setItemTitleUrl, { title: title, itemId: itemId })
        }

        function remove(itemId) {
            if (_.isNull(itemId) || _.isUndefined(itemId) || _.isEmpty(itemId))
                throw "Invalid item id"

            return storageHttpWrapper.post(constants.storage.removeItemUrl, { itemId: itemId })
        }

        function getNewItem(boardId) {
            return storageHttpWrapper.post(constants.storage.createNewItemUrl, { boardId: boardId })
        }
    });