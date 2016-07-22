define(['models/criterion', 'mappers/markMapper'], function (Criterion, markMapper) {
    return {
        map,
        //mapToViewModel
    }

    function map(src) {
        return new Criterion({
            id: src.id,
            title: src.title,
            isBenefit: src.isBenefit,
            weight: src.weight
        })
    }

    /*function mapToViewModel(src) {
        if (src.id === undefined)
            src = Map(src);

        return new Criterion({
            id: src.id,
            title: ko.observable(src.title).extend({
                validate: validateService.validateObservableTitle
            }),
            isBenefit: src.isBenefit,
            weight: ko.observable(src.weight).extend({
                validate: validateService.validateObservableWeightValue
            })
        });
    }*/
});