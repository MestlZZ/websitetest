define(['models/item', 'mappers/markMapper', 'services/boardService', 'repositories/boardRepository', 'services/validateService', 'durandal/app'],
    function (Item, markMapper, boardService, boardRepository, validateService, app) {
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
                validate: validateService.validateObservableTitle
            })
        });

        item.rank = ko.observable();
        item.score = ko.observable(boardService.computeScore(item.marks));
        item.visible = ko.observable(true);

        app.on('board:item-changed', function () {
            item.score(boardService.computeScore(item.marks));
        })
        

        return item;
    }
});