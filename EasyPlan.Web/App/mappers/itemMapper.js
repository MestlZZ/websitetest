define(['models/item', 'mappers/markMapper'],
    function (Item, markMapper) {
    return {
        map: map,
    }

    function map(src) {
        return new Item({
            id: src.id,
            title: src.title,
            marks: _.map(src.marks, markMapper.map)
        })
    }   
});