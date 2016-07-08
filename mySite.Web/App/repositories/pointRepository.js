define(['constants', 'storageContext', 'http/storageHttpWrapper', ],
    function (constants, storage, storageHttpWrapper) {

        return {
            setTitle: setTitle,
            remove, remove
        }

        function setTitle(title, pointId, boardId){
            return storageHttpWrapper.post(constants.storage.host + constants.storage.setItemTitleUrl, {title: title, id: pointId}).then(function () {
                var board = storage.boards.find(function (elem) { return elem.id === boardId });
                var point = board.points.find(function (elem) { return elem.id === pointId })

                point.title = title;
            });
        }

        function remove(item, boardId) {
            return storageHttpWrapper.post(constants.storage.host + constants.storage.removeItemUrl, { id: item.id }).then(function () {
                var board = storage.boards.find(function (elem) { return elem.id === boardId });
                var point = board.points.find(function (elem) { return elem.id === item.d })

                board.points.splice(board.points.indexOf(point), 1);

                console.log(points);
            });
        }
    });