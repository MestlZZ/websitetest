define(['models/item', 'mappers/markMapper'],
    function (Item, markMapper) {
    return {
        map,
        //mapToViewModel
    }

    function map(src) {
       /* var marks = _.map(src.marks, markMapper.map);
        var mappedMarks = {};

        var board = boardRepository.getOpenedBoard();

        _.each(board.criterions, function (criterion) {
            mappedMarks[criterion.id] = markMapper.map({
                value: 0,
                criterionId: criterion.id,
                itemId: src.Id
            })
        });

        _.each(marks, function (mark) {
            mappedMarks[mark.criterionId] = mark;
        })
        */
        return new Item({
            id: src.id,
            title: src.title,
            marks: _.map(src.marks, markMapper.map)
        })
    }

   /* function mapToViewModel(src) {
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
    }*/
});