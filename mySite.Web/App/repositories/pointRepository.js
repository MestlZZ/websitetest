define(['constants', 'storageContext', 'http/storageHttpWrapper', ],
    function (constants, storage, storageHttpWrapper) {

        return {
            setTitle: setTitle,
        }

        function setTitle(title, pointId, boardId){
            return storageHttpWrapper.post(constants.storage.host + constants.storage.setTitleUrl, {title: title, id: pointId}).then(function () {
                var board = storage[constants.storage.keys.boards].find(function (elem) { return elem.id === boardId });
                var point = board.points.find(function (elem) { return elem.id === pointId });
                point.title = title;
            });
        }

        function getCollection() {
            return storage.get(constants.storage.keys.boards);
        }

        function clearCollection() {
            storage.remove(constants.storage.keys.boards);
        }
    });