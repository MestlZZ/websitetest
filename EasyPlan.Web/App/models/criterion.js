define(['models/entity'], function (Entity) {

    function Criterion(opt) {
        Entity.call(this, opt.id);

        this.title = opt.title,
        this.isBenefit = opt.isBenefit,
        this.weight = opt.weight
    }

    Criterion.prototype = Object.create(Entity.prototype);
    Criterion.prototype.constructor = Criterion;

    return Criterion;
});