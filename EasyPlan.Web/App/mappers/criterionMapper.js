define(['models/criterion', 'mappers/markMapper'], function (Criterion, markMapper) {
    return {
        map: map,
        mapToObservable: mapToObservable
    }

    function map(src) {
        return new Criterion({
            id: src.Id,
            title: src.Title,
            isBenefit: src.IsBenefit,
            weight: src.Weight
        })
    }
    function mapToObservable(src) {
        if (src.id === undefined)
            src = Map(src);

        return new Criterion({
            id: src.id,
            title: ko.observable(src.title),
            isBenefit: src.isBenefit,
            weight: ko.observable(src.weight)
        });
    }
});