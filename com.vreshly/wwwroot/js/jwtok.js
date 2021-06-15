$(function () {
    var loggedIn = {};
    var authItem = JSON.parse(localStorage.getItem("auth"));
    $('#login_btn').on('click', function (e) {
        e.preventDefault();
        var _email = $('#login_email').val();
        var _password = $('#login_password').val()

        if (isEmail(_email) && validateForm(_password, 'Password')) {
            loginDto = {
                email: _email,
                password: _password
            };

            $.ajax({
                type: 'POST',
                url: `${baseUrl}Account/Logon`,
                data: JSON.stringify(loginDto),
                contentType: "application/json;charset=utf-8",
                traditional: true,
            }).done(function (response) {
                loggedIn.email = response.email;
                loggedIn.displayName = response.displayName;
                loggedIn.token = response.token;
                localStorage.setItem('auth', JSON.stringify(loggedIn));
                window.location.href = "/Home/Index";
                
            })
                .fail(function (data) {
                    alert('Username or Password is incorrect');
            });
        }
    });

    var isEmail = function (email) {
        var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!regex.test(email)) {
            alert("Please provide a valid mail");
            return false;
        } else {
            return true;
        }
    }

    validateForm = function (x, field) {
        if (x == "") {
            alert(`${field} must be filled out`);
            return false;
        } else {
            return true
        }
    }

    var checkAuthStat = function (_authItem) {
        if (_authItem != null) {
            $('.login_register').css('display', 'none');
            $('.welcome_msg').css('display', 'block');
            $('.welcome_msg').text(`Hello ${_authItem.displayName}`)
        } else {
            $('.welcome_msg').css('display', 'none');
            $('.login_register').css('display', 'block');
        }
    }

    checkAuthStat(authItem);

/** toggle login on checkout  **/
    var checkoutLoginToggle = function (_authItem) {
        if (_authItem != null) {
            $('#billing-address-div').css('display', 'block');
            $('#checkout-login-div').css('display', 'none');
            $('#completed-transaction').css('display', 'none');
        } else {
            $('#billing-address-div').css('display', 'none');
            $('#checkout-login-div').css('display', 'block')
        }
    }

    checkoutLoginToggle(authItem);

    $('#login_btn_chkout').on('click', function (e) {
        e.preventDefault();
        var _email = $('#login_email_chkout').val();
        var _password = $('#login_password_chkout').val()

        if (isEmail(_email) && validateForm(_password, 'Password')) {
            loginDto = {
                email: _email,
                password: _password
            };

            $.ajax({
                type: 'POST',
                url: `${baseUrl}Account/Logon`,
                data: JSON.stringify(loginDto),
                contentType: "application/json;charset=utf-8",
                traditional: true,
            }).done(function (response) {
                loggedIn.email = response.email;
                loggedIn.displayName = response.displayName;
                loggedIn.token = response.token;
                localStorage.setItem('auth', JSON.stringify(loggedIn));
                checkAuthStat(loggedIn);
                checkoutLoginToggle(loggedIn)
                //window.location.href = "/Home/Index";

            })
               .fail(function (data) {
                    alert('Username or Password is incorrect');
                });
        }
    });
})