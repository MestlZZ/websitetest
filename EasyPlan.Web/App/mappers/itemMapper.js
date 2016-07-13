define(['models/item', 'mappers/markMapper', 'services/boardService'],
    function (Item, markMapper, boardService) {
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

        var item =  new Item({
            id: src.id,
            marks: ko.observableArray(_.map(src.marks, markMapper.mapToObservable)),
            title: ko.observable(src.title).extend({
                validItemTitle: 'Title'
            })
        });

        item.rank = ko.observable();

        item.score = ko.computed(function () {
            return boardService.computeScore(item.marks());
        });

        return item;
    }
});