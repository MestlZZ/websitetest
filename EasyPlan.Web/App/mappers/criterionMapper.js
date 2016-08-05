define(['models/criterion', 'mappers/markMapper'], function (Criterion, markMapper) {
    return {
        map,
    }

    function map(src) {
        return new Criterion({
            id: src.id,
            title: src.title,
            isBenefit: src.isBenefit,
            weight: src.weight
        })
    }
});