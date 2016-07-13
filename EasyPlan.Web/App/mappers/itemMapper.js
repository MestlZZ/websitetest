define(['models/item', 'mappers/markMapper'], function (Item, markMapper) {
    return {
        map: map,
        mapToObservable: mapToObservable
    }

    function map(src) {
        return new Item({
            id: src.Id,
            title: src.Title,
            marks: _.map(src.Marks, markMapper.map)
        })
    }

    function mapToObservable(src) {
        if (src.id === undefined)
            src = Map(src);

        return new Item({
            id: src.id,
            marks: ko.observableArray(_.map(src.marks, markMapper.mapToObservable)).extend({
                marksScore: '123'
            }),
            title: ko.observable(src.title).extend({
                validItemTitle: 'Title'
            })
        });
    }
});