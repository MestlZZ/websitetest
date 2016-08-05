define(['models/board', 'mappers/itemMapper', 'mappers/criterionMapper'],
    function (Board, itemMapper, criterionMapper) {

    return {
        map,
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
});