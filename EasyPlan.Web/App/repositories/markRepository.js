define(['constants', 'http/storageHttpWrapper'],
    function (constants, storageHttpWrapper) {

        return {
            setValue: setValue,
            createMark: createMark
        }

        function setValue(value, markId, boardId) {
            if (_.isInvalidNumber(value)) {
                throw 'Invalid value';
            }

            if (_.isInvalidText(markId)) {
                throw 'Invalid mark id';
            }

            if (_.isInvalidText(boardId)) {
                throw 'Invalid board id';
            }

            return storageHttpWrapper.post(constants.storage.setMarkValueUrl, { value: value, markId: markId, boardId: boardId });
        }

        function createMark(itemId, criterionId, boardId) {
            if (_.isInvalidText(itemId)) {
                throw 'Invalid item id';
            }

            if (_.isInvalidText(criterionId)) {
                throw 'Invalid criterion id';
            }

            if (_.isInvalidText(boardId)) {
                throw 'Invalid board id';
            }

            return storageHttpWrapper.post(constants.storage.createMarkUrl, { itemId: itemId, criterionId: criterionId, boardId: boardId });
        }
    });