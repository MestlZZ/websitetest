define(['durandal/app'], function (app) {
    return {
        computeScore,
        setRanks,
        itemsChanged
    }

    function computeScore(marks) {
        var result = 0;

        _.each(_.keys(marks), function (key) {
            var mark = marks[key];

            if(mark.isBenefit){
                result += ko.unwrap(mark.value);
            } else {
                result -= ko.unwrap(mark.value) - 5;
            }
        });

        return result;
    }

    function sortItemsByScore(items) {
        return _.sortBy(items, function (item) {
            return item.score();
        }).reverse();
    }

    function itemsChanged(items) {
        setRanks(items());

        app.trigger('board:item-changed', items());
    }

    function setRanks(items) {
        var items = sortItemsByScore(items);

        for(var i = 0; i < items.length; i++)
        {
            if (i == 0 || items[i - 1].score() != items[i].score())
                items[i].rank(i + 1);
            else
                items[i].rank(items[i - 1].rank());
        }
    }    
})