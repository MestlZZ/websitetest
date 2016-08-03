$('.see-password').on('mousedown', function () {
    $(this).next('input').attr('type', 'text');

    $(this).on('mouseup', function () {
        $(this).next('input').attr('type', 'password');
    });
})