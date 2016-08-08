define(['constants', 'http/storageHttpWrapper'],
    function (constants, storageHttpWrapper) {

        return {
            setWeight: setWeight,
            setTitle: setTitle,
            remove: remove,
            getNewCriterion: getNewCriterion
        };

        function setWeight(weight, criterionId, boardId) {
            if (_.isInvalidNumber(weight)) {
                throw 'Invalid weight';
            }

            if (_.isInvalidText(criterionId)) {
                throw 'Invalid criterion id';
            }

            if (_.isInvalidText(boardId)) {
                throw 'Invalid board id';
            }

            return storageHttpWrapper.post(constants.storage.setCriterionWeightUrl, { weight: weight, criterionId: criterionId, boardId: boardId });
        }

        function setTitle(title, criterionId, boardId) {
            if (_.isInvalidText(title)) {
                throw 'Invalid criterion title';
            }

            if (_.isInvalidText(criterionId)) {
                throw 'Invalid criterion id';
            }

            if (_.isInvalidText(boardId)) {
                throw 'Invalid board id';
            }

            return storageHttpWrapper.post(constants.storage.setCriterionTitleUrl, { title: title, criterionId: criterionId, boardId: boardId });
        }

        function remove(criterionId, boardId) {
            if (_.isInvalidText(criterionId)) {
                throw 'Invalid criterion id';
            }

            if (_.isInvalidText(boardId)) {
                throw 'Invalid board id';
            }

            return storageHttpWrapper.post(constants.storage.removeCriterionUrl, { criterionId: criterionId, boardId: boardId });
        }

        function getNewCriterion(isBenefit, boardId) {
            if (_.isInvalidBoolean(isBenefit)) {
                throw 'Is invalid isBenefit';
            }

            if (_.isInvalidText(boardId)) {
                throw 'Invalid board id';
            }

            return storageHttpWrapper.post(constants.storage.createNewCriterionUrl, { isBenefit: isBenefit, boardId: boardId });
        }
    });