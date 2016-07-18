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


            return storageHttpWrapper.post(constants.storage.setItemTitleUrl, { title: title, id: itemId }).then(function () {
                var items = storage.openedBoard.items;
                var item = items.find(function (item) {
                    return item.id == itemId;
                });

                item.title = title;
            });
        }

        function remove(itemId) {
            if (_.isNull(itemId) || _.isUndefined(itemId) || _.isEmpty(itemId))
                throw "Invalid item id"

            return storageHttpWrapper.post(constants.storage.removeItemUrl, { id: itemId }).then(function () {
                var items = storage.openedBoard.items;
                var item = items.find(function (item) {
                    return item.id == itemId;
                });

                var index = items.indexOf(item);

                items.splice(index, 1);
            });
        }

        function getNewItem() {

            var board = storage.openedBoard;

            return storageHttpWrapper.post(constants.storage.createNewItemUrl, { boardId: board.id }).then(function (item) {

                if (_.isNull(item) || _.isUndefined(item))
                    throw "Failed to load item"
                
                var item = itemMapper.map(item);

                var items = board.items;

                items.unshift(item);

                return item;
            });
        }
    });