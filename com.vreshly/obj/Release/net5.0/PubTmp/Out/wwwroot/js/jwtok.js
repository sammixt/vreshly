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



    $('#registration-form').on('submit', function (e) {
           
        e.preventDefault();
        var _username = $('#username').val();
        var _email = $('#email').val();
        var _password = $('#password').val();

        if (isEmail(_email) && validateForm(_password, 'Password') && validateForm(_username, 'Username'))  {

            var _data = { displayName: _username, email: _email, password: _password }
                
            $.ajax({
                type: 'POST',
                url: `${baseUrl}Account/CreateCustomer`,
                data: JSON.stringify(_data),
                contentType: "application/json;charset=utf-8",
                traditional: true,
            }).done(function (response) {
                loggedIn.email = response.email;
                loggedIn.displayName = response.displayName;
                loggedIn.token = response.token;
                localStorage.setItem('auth', JSON.stringify(loggedIn));
                toast('success', `Account Created`);

                setTimeout(window.location.href = "/Home/Index", 2000);
                

            })
                .fail(function (data) {
                    var res = JSON.parse(data.responseText);
                    toast('error', `${res.errors}`);
                    console.log(data.responseText)
                });
            }
        })
    

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
            $('.logout').css('display', 'block');
            $('.welcome_msg').text(`Hello ${_authItem.displayName}`)
        } else {
            $('.welcome_msg').css('display', 'none');
            $('.logout').css('display', 'none');
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

    $('.logout-btn').on('click', function (e) {
        localStorage.removeItem("auth");
        $.ajax({
            type: 'GET',
            url: `${baseUrl}Account/Logout`,
        }).done(function (response) { })
            .fail(function (data) {
                console.log(data);
            });
        window.location.href = "/Home/Index";
    })

    function loadCategory(_take) {
        var category = $('.category-one');
        $.ajax({
            type: 'GET',
            url: `${baseUrl}Category/GetIndexedCategories`,
            data: { take: _take }
        }).done(function (response) {
            const maxCategoryCount = 15;
            let totalCategory = response.length;
            let initCount = 0;
            let splitCount = 5;
            var li = ""
            $.each(response, function (key, value) {
                
                li += "<li><a class='loadcart' href='#' data-cart='" + value.id + "'>" + value.categoryName + "</a></li>";
                initCount = initCount + 1;
                if ((initCount % splitCount == 0) && initCount < totalCategory) {
                    category.append(`
                        <div class='menu-colum'>
                            <ul>
                                ${li}
                            </ul>
                        </div>
                        `);
                    li = "";
                } else if (initCount == totalCategory) {
                    category.append(`
                        <div class='menu-colum'>
                            <ul>
                                ${li}
                            </ul>
                        </div>
                        `);
                    li = "";
                }
            });
            //setTimeout(, 3000);

            //sliderInit('featured-product-slider')

        }).fail(function (data) {
            // Make sure that the formMessages div has the 'error' class.
            console.log(data);
        });
    }

    $('.category-one').on('click', '.menu-colum .loadcart', function (e) {
        e.preventDefault();

        var catId = $(this).attr('data-cart');
        var _path = window.location.pathname.split('/');

        if (_path[1] == 'Shop' && (_path[2] == 'Index' || _path[2] === undefined)) {
            loadProducts(1, 8, catId, 'nil', 'nil');
        } else {
            var _hash = `#ct$d$${catId}`;
            window.location.href = `/Shop/Index${_hash}`;
        }
        
    })

    loadCategory(15);

    $('#mc-form').submit(function (e) {
        e.preventDefault();
        var _email = $('#mc-email').val();
        var subsctiption = {
            email: _email
        }
        $.ajax({
            type: 'POST',
            url: `${baseUrl}Newsletter/Subscribe`,
            data: JSON.stringify(subsctiption),
            contentType: "application/json;charset=utf-8",
            traditional: true,
        }).done(function (response) {
            toast('success', `${response.message}`);

        })
            .fail(function (data) {
                toast('error', `${response.message}`);
            });
    })
})