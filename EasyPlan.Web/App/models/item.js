define(['models/entity'], function (Entity) {

    function Item(opt) {
        Entity.call(this, opt.id);

        this.title = opt.title,
        this.marks = opt.marks
    }

    Item.prototype = Object.create(Entity.prototype);
    Item.prototype.constructor = Item;

    return Item;
});