define(['durandal/app'], function (app) {
    
    app.on('items:score-changed', setRanks);

    return {
        computeScore,
        sortItemsByScore,
        setRanks,
        callTriggerScoreChanged
    }

    function computeScore(marks) {
        return _.reduce(marks, function (memo, num) {
            var value = ko.utils.unwrapObservable(num.value);

            if (num.isBenefit)
                return memo + value;
            else
                return memo - value;
        }, 0);
    }

    function sortItemsByScore(items) {
        return _.sortBy(items, function (item) {
            return item.score();
        }).reverse();
    }

    function callTriggerScoreChanged(items) {
        app.trigger('items:score-changed', items);
    }

    function setRanks(items) {
        for(var i = 0; i < items.length; i++)
        {
            if (i == 0 || items[i - 1].score() != items[i].score())
                items[i].rank(i + 1);
            else
                items[i].rank(items[i - 1].rank());
        }
    }    
})