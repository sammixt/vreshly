$(function () {
    'use strict';

    var pageLoginForm = $('.auth-login-form');

    // jQuery Validation
    // --------------------------------------------------------------------
    if (pageLoginForm.length) {
        pageLoginForm.validate({
            /*
            * ? To enable validation onkeyup
            onkeyup: function (element) {
              $(element).valid();
            },*/
            /*
            * ? To enable validation on focusout
            onfocusout: function (element) {
              $(element).valid();
            }, */
            rules: {
                'login-email': {
                    required: true,
                    email: true
                },
                'login-password': {
                    required: true
                }
            }
        });

        pageLoginForm.on('submit', function (e) {
            var isValid = pageLoginForm.valid();
            var _this = $(this);
            e.preventDefault();
            if (isValid) {
              
                var _email = $('#login-email').val();
                var _password = $('#login-password').val();

                _this.html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

                axios.post(`${url}Auth/Login`, { email: _email, password: _password })
                    .then(response => {
                        const addedUser = response.data;
                        if (response.data.statusCode === 200) {
                            toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                                closeButton: true,
                                tapToDismiss: true,
                                rtl: false
                            });
                            window.location.href = "/Auth/Dashboard";
                        } else {
                            toastr['error'](`👋 ${response.data.message}`, 'Error!', {
                                closeButton: true,
                                tapToDismiss: true,
                                rtl: false
                            });
                            _this.html("<b>Submit</b>");
                        }
                       
                       // window.location.reload();
                        console.log(response.data);
                    })
                    .catch(function (error) {
                        if (error.response) {
                            toastr['error'](error.response.data.message, 'Error!', {
                                closeButton: true,
                                tapToDismiss: false,
                                rtl: false
                            });
                        }

                        _this.html("<b>Submit</b>");
                    });
            }
        });
    }
});