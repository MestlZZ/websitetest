define(['models/board', 'mappers/itemMapper', 'mappers/criterionMapper', 'services/boardService'],
    function (Board, itemMapper, criterionMapper, boardService) {

    return {
        map,
        mapToObservable,
        mapInfo
    }

    function map(src) {
        var board = new Board({
            id: src.Id,
            title: src.Title,
            items: _.map(src.Items, itemMapper.map),
            criterions: [],
            marks: {}
        });

        var marks = board.items[0].marks;

        for (var i = 0; i < marks.length; i++)
            board.criterions.push(marks[i].criterion);

        for (var i = 0; i < board.items.length; i++) {
            board.marks[board.items[i].id] = {};

            for (var j = 0; j < board.criterions.length; j++) {
                var mark = (board.items[i].marks).find(function(elem){
                    return elem.criterion.id == board.criterions[j].id;
                });

                board.marks[board.items[i].id][board.criterions[j].id] = mark;
            }
        }

        return board;
    }

    function mapInfo(src) {
        return new Board({
            id: src.Id,
            title: src.Title,
        });
    }

    function mapToObservable(src) {
        if (src.id === undefined)
            src = map(src);

        var board = new Board({
            id: src.id,
            title: ko.observable(src.title),
            items: ko.observableArray(_.map(src.items, itemMapper.mapToObservable)),
            criterions: ko.observableArray(),
            marks: ko.observable({})
        });

        var marks = board.items()[0].marks;

        for (var i = 0; i < marks().length; i++)
            board.criterions.push(marks()[i].criterion);

        for (var i = 0; i < board.items().length; i++) {
            console.log(board.marks());
            board.marks()[board.items()[i].id] = ko.observable({});

            for (var j = 0; j < board.criterions().length; j++) {
                var mark = (board.items()[i].marks()).find(function (elem) {
                    return elem.criterion.id == board.criterions()[j].id;
                });

                board.marks()[board.items()[i].id]()[board.criterions()[j].id] = mark;
            }
        }

        return board;
    }
});