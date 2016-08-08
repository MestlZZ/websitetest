define(['durandal/app'], function (app) {

    return {
        computeScore: computeScore,
        setRanks: setRanks
    };

    function computeScore(marks) {
        var result = 0;

        _.each(_.keys(marks), function (key) {
            var mark = marks[key];

            if (mark.isBenefit) {
                result += +ko.unwrap(mark.value) * ko.unwrap(mark.weight);
            } else {
                result -= (+ko.unwrap(mark.value) - 5) * ko.unwrap(mark.weight);
            }
        });

        return result;
    }

    function sortItemsByScore(items) {
        return _.sortBy(items, function (item) {
            return item.score();
        }).reverse();
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