define(['models/mark'], function (Mark) {
    return {
        map: map,
    }

    function map(src) {
        return new Mark({
            id: src.id,
            value: src.value,
            criterionId: src.criterionId,
            itemId: src.itemId
        })
    }
});