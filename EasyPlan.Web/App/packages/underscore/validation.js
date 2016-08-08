(function () {
    _.mixin({
        isInvalidText: function (text) {
            if (_.isNull(text) || _.isUndefined(text) || _.isEmpty(text)) {
                return true;
            }

            return false;
        },
        isInvalidNumber: function (number) {
            if (_.isNull(number) || _.isUndefined(number) || _.isNaN(number) || !_.isFinite(number)) {
                return true;
            }

            return false;
        },
        isInvalidBoolean: function (bool) {
            if (_.isNull(bool) || _.isUndefined(bool)) {
                return true;
            }

            return false;
        }
    });
})();