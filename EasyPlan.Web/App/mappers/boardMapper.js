define(['models/board', 'mappers/itemMapper', 'mappers/criterionMapper'],
    function (Board, itemMapper, criterionMapper) {

    return {
        map,
        mapToViewModel,
        mapToShortInfo
    }

    function map(src) {
        return new Board({
            id: src.Id,
            title: src.Title,
            criterions: _.map(src.Criterions, criterionMapper.map),
            items: _.map(src.Items, itemMapper.map)
        });
    }

    function mapToShortInfo(src) {
        return new Board({
            id: src.Id,
            title: src.Title,
        });
    }

    function mapToViewModel(src) {
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
    }
});