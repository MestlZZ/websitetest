define(function () {
    function Mark(opt){
        this.id = opt.id;
        this.value = opt.value;
        this.criterionId = opt.criterionId;
        this.itemId = opt.itemId;
        this.isBenefit = opt.isBenefit;
        this.weight = opt.weight;
    }

    return Mark;
});