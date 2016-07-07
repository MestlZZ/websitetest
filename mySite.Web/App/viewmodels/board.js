define(['repositories/boardRepository', 'repositories/pointRepository'], function (boardRepository, pointRepository) {
    return {        
        board: ko.observable(),
        activate: function () {
            this.board(boardRepository.getCollection()[0]);
        },
        sendPoint: function (data, context) {
            var self = this;

            pointRepository.setTitle(data.title, data.id, context.board().id);
        }
    }
});