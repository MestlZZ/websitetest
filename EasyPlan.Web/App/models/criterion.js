define(function () {
    function Criterion(opt){
        this.id = opt.id,
        this.title = opt.title,
        this.marks = opt.marks,
        this.isBenefit = opt.isBenefit,
        this.weight = opt.weight
    }

    return Criterion;
});