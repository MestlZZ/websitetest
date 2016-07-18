define(['constants', 'storageContext', 'http/storageHttpWrapper', 'mappers/itemMapper', 'mappers/markMapper'],
    function (constants, storage, storageHttpWrapper, itemMapper, markMapper) {

        return {
            setValue,
            createMark
        }

        function setValue(value, markId, itemId, criterionId) {
            if (_.isNull(value) || _.isUndefined(value) || _.isNaN(value) || !_.isFinite(value))
                throw "Invalid value"


            if (_.isNull(markId) || _.isUndefined(markId) || _.isEmpty(markId)) {
                throw "Invalid mark id"
            }

            return storageHttpWrapper.post(constants.storage.setMarkValueUrl, { value: value, id: markId }).then(function () {
                var items = storage.openedBoard.items;
                var item = items.find(function (item) { return item.id == itemId });
                var mark = item.marks[criterionId];

                mark.value = value;
            });
        }

        function createMark(itemId, criterionId) {
            return storageHttpWrapper.post(constants.storage.createMarkUrl, { itemId: itemId, criterionId: criterionId }).then(function (mark) {
                var mark = markMapper.map(mark);
                var items = storage.openedBoard.items;
                var item = items.find(function (item) { return item.id == itemId });

                item.marks[criterionId] = mark;

                return mark;
            })
        }
    });