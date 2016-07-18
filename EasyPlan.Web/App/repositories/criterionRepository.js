define(['constants', 'storageContext', 'http/storageHttpWrapper'],
    function (constants, storage, storageHttpWrapper) {
        return {
            setWeight,
            setTitle
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
})