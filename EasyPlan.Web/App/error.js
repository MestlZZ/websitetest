define(['viewmodels/error', 'durandal/app', 'spinner', 'plugins/router', 'constants'], function (errorViewModel, app, spinner, router, constants) {

    return {
        'throw': throwError
    }

    function throwError(reason, statusCode){
        spinner.hide();

        errorViewModel.statusCode(statusCode);
        errorViewModel.reason(reason);

        router.navigate('#error');
    }
})