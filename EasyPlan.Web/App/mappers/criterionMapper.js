define(['models/criterion', 'mappers/markMapper', 'services/validateService'], function (Criterion, markMapper, validateService) {
    return {
        map,
        mapToViewModel
    }

    function map(src) {
        return new Criterion({
            id: src.Id,
            title: src.Title,
            isBenefit: src.IsBenefit,
            weight: src.Weight
        })
    }
    function mapToViewModel(src) {
        if (src.id === undefined)
            src = Map(src);

        return new Criterion({
            id: src.id,
            title: ko.observable(src.title),
            isBenefit: src.isBenefit,
            weight: ko.observable(src.weight).extend({
                validate: validateService.validateObservableWeightValue
            })
        });
    }
});