define(['constants', 'http/storageHttpWrapper'],
    function (constants, storageHttpWrapper) {

        return {
            setValue: setValue
        }

        function setValue(itemId, criterionId, boardId, value) {
            if (_.isInvalidNumber(value)) {
                throw 'Invalid value';
            }

            if (_.isInvalidText(itemId)) {
                throw 'Invalid item id';
            }

            if (_.isInvalidText(boardId)) {
                throw 'Invalid board id';
            }

            if (_.isInvalidText(criterionId)) {
                throw 'Invalid criterion id';
            }

            return storageHttpWrapper.post(constants.storage.createMarkUrl, { itemId: itemId, criterionId: criterionId, boardId: boardId, value: value });
        }
    });