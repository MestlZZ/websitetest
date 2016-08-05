define(['constants', 'http/storageHttpWrapper'],
    function (constants, storageHttpWrapper) {
        return {
            setWeight,
            setTitle,
            remove,
            getNewCriterion
        }

        function setWeight(weight, criterionId, boardId) {
            if (_.isNull(weight) || _.isUndefined(weight) || _.isNaN(weight) || !_.isFinite(weight))
                throw "Invalid weight"

            if (_.isNull(criterionId) || _.isUndefined(criterionId) || _.isEmpty(criterionId))
                throw "Invalid item id";

            return storageHttpWrapper.post(constants.storage.setCriterionWeightUrl, { weight: weight, criterionId: criterionId, boardId: boardId })
        }

        function setTitle(title, criterionId, boardId) {
            if (_.isNull(title) || _.isUndefined(title) || _.isEmpty(title))
                throw "Invalid title id"

            if (_.isNull(criterionId) || _.isUndefined(criterionId) || _.isEmpty(criterionId))
                throw "Invalid item id"


            return storageHttpWrapper.post(constants.storage.setCriterionTitleUrl, { title: title, criterionId: criterionId, boardId: boardId })
        }

        function remove(criterionId, boardId) {
            if (_.isNull(criterionId) || _.isUndefined(criterionId) || _.isEmpty(criterionId))
                throw "Invalid item id"

            return storageHttpWrapper.post(constants.storage.removeCriterionUrl, { criterionId: criterionId, boardId: boardId })
        }

        function getNewCriterion(isBenefit, boardId) {            
            if (_.isNull(isBenefit) || _.isUndefined(isBenefit))
                throw "Is invalid isBenefit"

            return storageHttpWrapper.post(constants.storage.createNewCriterionUrl, { isBenefit: isBenefit, boardId: boardId })
        }
})