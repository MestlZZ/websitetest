define(['models/mark'],function (Mark) {
    return {
        map: map,
        mapToObservable: mapToObservable
    }

    function map(src) {
        return new Mark({
            id: src.Id,
            value: src.Value
        })
    }

    function mapToObservable(src) {
        if (src.id === undefined)
            src = Map(src);

        return {
            id: src.id,
            value: ko.observable(src.value).extend({
                validMarkValue: 'Mark'
            })
        }
    }
});