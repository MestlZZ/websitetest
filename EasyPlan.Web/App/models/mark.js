define(['models/entity'], function (Entity) {

    function Mark(opt){
        Entity.call(this, opt.id);

        this.value = opt.value;
        this.criterionId = opt.criterionId;
        this.itemId = opt.itemId;
        this.isBenefit = opt.isBenefit;
        this.weight = opt.weight;
    }

    Mark.prototype = Object.create(Entity.prototype);
    Mark.prototype.constructor = Mark;

    return Mark;
});