define(['models/mark'],function (Mark) {
    return {
        map: map,
        mapToObservable: mapToObservable
    }

    function map(src) {
        return new Mark({
            id: src.Id,
            value: src.Value,
            isBenefit: src.IsBenefit
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
            isBenefit: src.isBenefit
        })
    }
});