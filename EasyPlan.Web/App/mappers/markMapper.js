define(['models/mark'], function (Mark) {
    return {
        map,
        //mapToViewModel
    }

    function map(src) {
        /*var board = boardRepository.getOpenedBoard();

        var criterion;
               
        criterion = board.criterions.find(function (criterion) {
            return criterion.id == src.criterionId;
        });
        */
        return new Mark({
            id: src.id,
            value: src.value,
            criterionId: src.criterionId,
            //isBenefit: criterion.isBenefit || criterion.isBenefit,
            //weight: criterion.weight || criterion.weight,
            itemId: src.itemId
        })
    }
/*
    function mapToViewModel(src) {
        if (src.value === undefined)
            src = map(src);

        var res = new Mark({
            id: src.id,
            value: ko.observable(src.value).extend({
                validate: validateService.validateObservableMarkValue
            }),
            criterionId: src.criterionId,
            isBenefit: src.isBenefit,
            weight: src.weight,
            itemId: src.itemId
        })

        app.on('board:criterion-changed', function (criterion) {
            if(res.criterionId == criterion.id)
                res.weight = criterion.weight();
        });

        return res;
    }*/
});