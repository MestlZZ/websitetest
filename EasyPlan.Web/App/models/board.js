﻿define(function () {
    function Board(opt){
        this.id = opt.id,
        this.title = opt.title,
        this.criterions = opt.criterions,
        this.items = opt.items
    }

    return Board;
});