define(['models/board', 'mappers/itemMapper', 'mappers/criterionMapper'],
    function (Board, itemMapper, criterionMapper) {

    return {
        map,
        //mapToViewModel,
        mapToShortInfo
    }

    function map(src) {
        return new Board({
            id: src.id,
            title: src.title,
            criterions: _.map(src.criterions, criterionMapper.map),
            items: _.map(src.items, itemMapper.map)
        });
    }

    function mapToShortInfo(src) {
        return new Board({
            id: src.id,
            title: src.title,
        });
    }

   /* function mapToViewModel(src) {
        if (src.id === undefined)
            src = map(src);

        var board = new Board({
            id: src.id,
            title: ko.observable(src.title),
            criterions: ko.observableArray(_.map(src.criterions, criterionMapper.mapToViewModel)),
            items: ko.observableArray(_.map(src.items, itemMapper.mapToViewModel)),
        });

        board.items.countVisible = ko.observable(board.items().length);

        return board;
    }*/
});