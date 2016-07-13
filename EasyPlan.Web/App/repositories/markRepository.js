define(['constants', 'storageContext', 'http/storageHttpWrapper', 'mappers/itemMapper'],
    function (constants, storage, storageHttpWrapper, itemMapper) {

        return {
            setValue
        }

        function setValue(value, markId) {
            if (_.isNull(value) || _.isUndefined(value) || _.isNaN(value) || !_.isFinite(value))
                throw "Invalid value"

            if (_.isNull(markId) || _.isUndefined(markId) || _.isEmpty(markId))
                throw "Invalid mark id"

            return storageHttpWrapper.post(constants.storage.host + constants.storage.setMarkValueUrl, {value: value, id: markId});
        }
    });