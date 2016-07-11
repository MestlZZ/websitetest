define(function () {
    function Item(opt){
        this.id = opt.id,
        this.title = opt.title,
        this.marks = opt.marks
    }

    return Item;
});