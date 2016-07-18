define(['models/item', 'models/Mark', 'mappers/markMapper', 'services/boardService', 'repositories/boardRepository', 'services/validateService'],
    function (Item, Mark, markMapper, boardService, boardRepository, validateService) {
    return {
        map,
        mapToViewModel
    }

    function map(src) {
        var marks = _.map(src.Marks, markMapper.map);
        var mappedMarks = {};

        var board = boardRepository.getOpenedBoard();

        if (board.Criterions !== undefined) {
            _.each(board.Criterions, function (criterion) {
                mappedMarks[criterion.Id] = markMapper.map({
                    Value: 0,
                    CriterionId: criterion.Id,
                    ItemId: src.Id
                })
            });
        } else {
            _.each(board.criterions, function (criterion) {
                mappedMarks[criterion.id] = markMapper.map({
                    Value: 0,
                    CriterionId: criterion.id,
                    ItemId: src.Id
                })
            });
        }       

        _.each(marks, function (mark) {
            mappedMarks[mark.criterionId] = mark;
        })

        return new Item({
            id: src.Id,
            title: src.Title,
            marks: mappedMarks
        })
    }

    function mapToViewModel(src) {
        if (src.id === undefined)
            src = Map(src);

        var mappedMarks = {};

        _.each(_.keys(src.marks), function (key) {
            mappedMarks[key] = markMapper.mapToViewModel(src.marks[key]);
        })

        var item =  new Item({
            id: src.id,
            marks: mappedMarks,
            title: ko.observable(src.title).extend({
                validate: validateService.validateObservableItemTitle
            })
        });

        item.rank = ko.observable();

        item.score = ko.computed(function () {
            return boardService.computeScore(item.marks);
        });

        return item;
    }
});