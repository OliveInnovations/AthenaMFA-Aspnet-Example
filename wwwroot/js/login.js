function success() {
    alert("Your login request has been approved");
}

$(function () {
    AthenaMfa.Init({
        checkCallbackUrl: '/Home/LoginMfaCheck',
        validateResponseCodeUrl: '/Home/ValidateMfaResponseCode',
        onSuccess: success,
        csrfToken: null,
        timeout: 1000
    });

    $('#btnLogin').click(function () {
        $.ajax({
            url: '/Home/Login',
            type: 'post',
            dataType: 'json',
            data: {
                Email: $('#Email').val(),
                Password: $('#Password').val()
            }
        }).done(function (data) {
            if (data.isValid) {
                AthenaMfa.ValidateMfa(data.response);
            }
        });
    });
});