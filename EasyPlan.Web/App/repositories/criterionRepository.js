define(['constants', 'storageContext', 'http/storageHttpWrapper', 'mappers/criterionMapper'],
    function (constants, storage, storageHttpWrapper, criterionMapper) {
        return {
            setWeight,
            setTitle,
            remove,
            getNewCriterion
        }

        function setWeight(weight, criterionId) {
            if (_.isNull(weight) || _.isUndefined(weight) || _.isNaN(weight) || !_.isFinite(weight))
                throw "Invalid weight"

            if (_.isNull(criterionId) || _.isUndefined(criterionId) || _.isEmpty(criterionId))
                throw "Invalid item id"


            return storageHttpWrapper.post(constants.storage.setCriterionWeightUrl, { weight: weight, criterionId: criterionId }).then(function () {
                var criterions = storage.openedBoard.criterions;

                var criterion = criterions.find(function (criterion) {
                    return criterion.id == criterionId
                });

                criterion.weight = weight;
            });
        }

        function setTitle(title, criterionId) {
            if (_.isNull(title) || _.isUndefined(title) || _.isEmpty(title))
                throw "Invalid title id"

            if (_.isNull(criterionId) || _.isUndefined(criterionId) || _.isEmpty(criterionId))
                throw "Invalid item id"


            return storageHttpWrapper.post(constants.storage.setCriterionTitleUrl, { title: title, criterionId: criterionId }).then(function () {
                var criterions = storage.openedBoard.criterions;
                var criterion = criterions.find(function (criterion) {
                    return criterion.id == criterionId
                });

                criterion.title = title;
            });
        }

        function remove(criterionId) {
            if (_.isNull(criterionId) || _.isUndefined(criterionId) || _.isEmpty(criterionId))
                throw "Invalid item id"

            return storageHttpWrapper.post(constants.storage.removeCriterionUrl, { criterionId: criterionId }).then(function () {
                var criterions = storage.openedBoard.criterions;
                var criterion = criterions.find(function (criterion) {
                    return criterion.id == criterionId
                });

                var index = criterions.indexOf(criterion);

                criterions.splice(index, 1);
            })
        }

        function getNewCriterion(isBenefit) {
            var board = storage.openedBoard;
            
            if (_.isNull(isBenefit) || _.isUndefined(isBenefit))
                throw "Is invalid isBenefit"

            return storageHttpWrapper.post(constants.storage.createNewCriterionUrl, { isBenefit: isBenefit, boardId: board.id }).then(function (criterion) {

                if (_.isNull(criterion) || _.isUndefined(criterion))
                    throw "Failed to load item"

                var criterion = criterionMapper.map(criterion);

                var criterions = board.criterions;

                criterions.unshift(criterion);

                return criterion;
            });
        }
})