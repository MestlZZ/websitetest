define(['constants', 'storageContext', 'http/storageHttpWrapper', 'mappers/itemMapper'],
    function (constants, storage, storageHttpWrapper, itemMapper) {

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


            return storageHttpWrapper.post(constants.storage.host + constants.storage.setItemTitleUrl, {title: title, id: itemId});
        }

        function remove(itemId) {
            if (_.isNull(itemId) || _.isUndefined(itemId) || _.isEmpty(itemId))
                throw "Invalid item id"

            return storageHttpWrapper.post(constants.storage.host + constants.storage.removeItemUrl, { id: itemId });
        }

        function getNewItem(boardId) {
            if (_.isNull(boardId) || _.isUndefined(boardId) || _.isEmpty(boardId))
                throw "Invalid board id"

            return storageHttpWrapper.post(constants.storage.host + constants.storage.createNewItemUrl, { id: boardId }).then(function (item) {

                if (_.isNull(item) || _.isUndefined(item))
                    throw "Failed to load item"

                return itemMapper.map(item);
            });
        }
    });