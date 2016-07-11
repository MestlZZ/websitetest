﻿define(['constants', 'storageContext', 'http/storageHttpWrapper', 'mappers/itemMapper'],
    function (constants, storage, storageHttpWrapper, itemMapper) {

        return {
            setTitle,
            remove,
            getNewItem
        }

        function setTitle(title, itemId, boardId){
            return storageHttpWrapper.post(constants.storage.host + constants.storage.setItemTitleUrl, {title: title, id: itemId}).then(function () {
                var board = storage.boards.find(function (elem) { return elem.id === boardId });
                var item = board.items.find(function (elem) { return elem.id === itemId })

                item.title = title;
            });
        }

        function remove(itemId, boardId) {
            return storageHttpWrapper.post(constants.storage.host + constants.storage.removeItemUrl, { id: itemId }).then(function () {
                var board = storage.boards.find(function (elem) { return elem.id === boardId });
                var item = board.items.find(function (elem) { return elem.id === itemId })

                board.items.splice(board.items.indexOf(item), 1);
            });
        }

        function getNewItem(boardId) {
            return storageHttpWrapper.post(constants.storage.host + constants.storage.createNewItemUrl, { id: boardId }).then(function (item) {
                var board = storage.boards.find(function (elem) { return elem.id === boardId });
                var newItem = itemMapper.map(item);
                board.items.push(newItem);
                
                return newItem;
            });
        }
    });