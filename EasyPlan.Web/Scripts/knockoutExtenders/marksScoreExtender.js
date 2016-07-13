ko.extenders.marksScore = function (target, formula) {
    target.score = ko.computed(function () {
        return _.reduce(target(), function (memo, num) {
            if (num.isBenefit)
                return memo + num.value();
            else
                return memo - num.value();
        }, 0);
    });

    return target;
};