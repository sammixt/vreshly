$(function () {
    var updateAccount = $('#update-account');
    var updateAccountBtn = $('.update-account-btn');
    var personalInformation = $('#personal-information');
    var personalInformationBtn = $('.personal-information-btn');
    var updatePassword = $('#update-password');
    var updatePasswordBtn = $('.update-password-btn');

    var _id = $('#id-type').val();
    // Form Validation
    if (updateAccount.length) {
        updateAccount.validate({
            errorClass: 'error',
            rules: {
                'Username': {
                    required: true
                },
                'FullName': {
                    required: true
                },
                'Email': {
                    required: true
                },
                'Status': {
                    required: true
                },
                'Role': {
                    required: true
                }
            }
        });

        updateAccount.on('submit', function (e) {
            var isValid = updateAccount.valid();
            e.preventDefault();
            if (isValid) {

                var _username = $('#Username').val();
                var _fullname = $('#FullName').val();
                var _email = $('#Email').val();
                var _status = $('#Status option:selected').val();
                var _role = $('#Role option:selected').val();

                updateAccountBtn.html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

                axios.put(`${url}Users/UpdateUserInfo`, { fullName: _fullname, roleId: _role, username: _username, email: _email, status: _status, id: _id })
                    .then(response => {
                        const addedUser = response.data;
                        toastr['success'](`👋 ${response.data}`, 'Success!', {
                            closeButton: true,
                            tapToDismiss: true,
                            rtl: false
                        });
                        window.location.reload();
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

                        updateAccount.text("Submit");
                    });
            }
        });
    }

    if (personalInformation.length) {
        //personalInformation.validate({
        //    errorClass: 'error',
        //    rules: {
        //        'user-fullname': {
        //            required: true
        //        },
        //        'user-name': {
        //            required: true
        //        },
        //        'user-email': {
        //            required: true
        //        }
        //    }
        //});

        personalInformation.on('submit', function (e) {
            var isValid = updateAccount.valid();
            e.preventDefault();
            if (isValid) {

                var _dob = $('#UserInformation_DateOfBirth').val();
                var _phone = $('#UserInformation_PhoneNumber').val();
                var _gender = $('#UserInformation_Gender').val();
                var _addressOne = $('#UserInformation_AddressLineOne').val();
                var _addressTwo = $('#UserInformation_AddressLineTwo').val();
                var _city = $('#UserInformation_City').val();
                var _state = $('#UserInformation_State').val();
                var _country = $('#UserInformation_Country').val();
                var _userId = _id;

                personalInformationBtn.html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

                axios.put(`${url}Users/UpdateUserAddressInfo`, {
                    dateOfBirth: _dob,
                    phoneNumber: _phone,
                    gender: _gender,
                     addressLineOne: _addressOne,
                     addressLineTwo: _addressTwo,
                     city: _city,
                     state: _state,
                     country: _country,
                     userId: _userId
                })
                    .then(response => {
                        const addedUser = response.data;
                        toastr['success'](`👋 ${response.data}`, 'Success!', {
                            closeButton: true,
                            tapToDismiss: true,
                            rtl: false
                        });
                        window.location.reload();
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

                        personalInformationBtn.text("Submit");
                    });
            }
        });
    }

    if (updatePassword.length) {
        updatePassword.validate({
            errorClass: 'error',
            rules: {
                'password': {
                    required: true,
                    minlength: 6
                },
                'confirmpassword': {
                    required: true,
                    minlength: 6,
                    equalTo: "#password"
                }
            }
        });

        updatePassword.on('submit', function (e) {
            var isValid = updatePassword.valid();
            e.preventDefault();
            if (isValid) {

                var _password = $('#password').val();
                var _confirmPassword = $('#confirmpassword').val();
                

                updatePasswordBtn.html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

                axios.put(`${url}Users/UpdatePassword`, { password: _password, confirmpassword: _confirmPassword, id: _id })
                    .then(response => {
                        const addedUser = response.data;
                        toastr['success'](`👋 ${response.data}`, 'Success!', {
                            closeButton: true,
                            tapToDismiss: true,
                            rtl: false
                        });
                        window.location.reload();
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

                        updatePasswordBtn.text("Submit");
                    });
            }
        });
    }
})