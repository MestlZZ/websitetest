define(['constants', 'http/storageHttpWrapper'],
    function (constants, storageHttpWrapper) {

        return {
            setValue: setValue,
            createMark: createMark
        }

        function setValue(value, markId, boardId) {
            if (_.isNull(value) || _.isUndefined(value) || _.isNaN(value) || !_.isFinite(value))
                throw "Invalid value"


            if (_.isNull(markId) || _.isUndefined(markId) || _.isEmpty(markId)) {
                throw "Invalid mark id"
            }

            return storageHttpWrapper.post(constants.storage.setMarkValueUrl, { value: value, markId: markId, boardId: boardId })
        }

        function createMark(itemId, criterionId, boardId) {
            return storageHttpWrapper.post(constants.storage.createMarkUrl, { itemId: itemId, criterionId: criterionId, boardId: boardId });
        }
    });