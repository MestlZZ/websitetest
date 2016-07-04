var storage = [];

function set(key, value) {
    if (_.isNullOrUndefined(key) ||
            _.isEmpty(key) ||
            _.isUndefined(value)) {
        throw 'Invalid arguments';
    }

    storage.push({ key: key, value: value });
    return value;
};

function get(key) {
    if (_.isNullOrUndefined(key) ||
        _.isEmpty(key)) {
        throw 'Invalid arguments';
    }

    return _.find(storage, function(item){
        return item.key == key;
    })
};

function remove(key) {
    if (_.isNullOrUndefined(key) ||
        _.isEmpty(key)) {
        throw 'Invalid arguments';
    }

    storage = _.without(storage, get(key));
};

export{
    set,
    get,
    remove
};