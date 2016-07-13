define(['models/board', 'mappers/itemMapper', 'mappers/criterionMapper'], function (Board, itemMapper, criterionMapper) {
    return {
        map,
        mapToObservable,
        mapInfo
    }

    function map(src) {
        return new Board({
            id: src.Id,
            title: src.Title,
            items: _.map(src.Items, itemMapper.map),
            criterions: _.map(src.Criterions, criterionMapper.map)
        });
    }

    function mapInfo(src) {
        return new Board({
            id: src.Id,
            title: src.Title,
        });
    }

    function mapToObservable(src) {
        if (src.id === undefined)
            src = map(src);

        return new Board({
            id: src.id,
            title: ko.observable(src.title),
            items: ko.observableArray(_.map(src.items, itemMapper.mapToObservable)),
            criterions: ko.observableArray(_.map(src.criterions, criterionMapper.mapToObservable))
        });
    }
});