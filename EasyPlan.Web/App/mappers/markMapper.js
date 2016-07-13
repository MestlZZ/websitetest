define(['models/mark', 'mappers/criterionMapper'],function (Mark, criterionMapper) {
    return {
        map: map,
        mapToObservable: mapToObservable
    }

    function map(src) {
        return new Mark({
            id: src.Id,
            value: src.Value,
            criterion: criterionMapper.map(src.Criterion)
        })
    }

    function mapToObservable(src) {
        if (src.id === undefined)
            src = Map(src);

        return new Mark({
            id: src.id,
            value: ko.observable(src.value).extend({
                validMarkValue: 'Mark'
            }),
            criterion: criterionMapper.mapToObservable(src.criterion)
        })
    }
});