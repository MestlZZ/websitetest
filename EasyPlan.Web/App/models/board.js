define(['models/entity'], function (Entity) {

    function Board(opt) {
        Entity.call(this, opt.id);

        this.title = opt.title;
        this.criterions = opt.criterions;
        this.items = opt.items;
    }

    Board.prototype = Object.create(Entity.prototype);
    Board.prototype.constructor = Board;

    return Board;
});