define(['models/mark', 'repositories/boardRepository'], function (Mark, boardRepository) {
    return {
        map,
        mapToViewModel
    }

    function map(src) {
        var board = boardRepository.getOpenedBoard();

        var criterion;

        if (_.isUndefined(board.Criterions)) {
            criterion = board.criterions.find(function (criterion) {
                return criterion.id == src.CriterionId;
            });
        } else {
            criterion = board.Criterions.find(function (criterion) {
                return criterion.Id == src.CriterionId;
            });
        }

        return new Mark({
            id: src.Id,
            value: src.Value,
            criterionId: src.CriterionId,
            isBenefit: criterion.IsBenefit || criterion.isBenefit,
            weight: criterion.Weight || criterion.weight,
            itemId: src.ItemId
        })
    }

    function mapToViewModel(src) {
        if (src.value === undefined)
            src = map(src);

        return new Mark({
            id: src.id,
            value: ko.observable(src.value).extend({
                validMarkValue: 'Mark'
            }),
            criterionId: src.criterionId,
            isBenefit: src.isBenefit,
            weight: src.weight,
            itemId: src.itemId
        })
    }
});