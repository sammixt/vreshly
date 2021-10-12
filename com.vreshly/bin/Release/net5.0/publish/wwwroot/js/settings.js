$(function () {
    $('#banner1, #banner2, #banner3, #banner4').on('click', function (e) {
        e.preventDefault();
        let attr = $(this).attr("id");
        switch (attr) {
            case "banner1":
                $('#slide-type').val(1);
                break;
            case "banner2":
                $('#slide-type').val(2);
                break;
            case "banner3":
                $('#slide-type').val(3);
                break;
            case "banner4":
                $('#slide-type').val(4);
                break;
            default:
                $('#slide-type').val(0);
                break;
        }
        $('#inlineForm').modal({
            show: true,
        });
    });

    $('#add-banner').submit(function (e) {
        e.preventDefault();
        let _subTitle = $('#small-title').val();
        let _title = $('#large-title').val();
        let _sliderTypes = $('#slide-type').val();
        var _files = $(`#customFile`)[0].files[0];
        var formData = new FormData();
        formData.append('title', _title);
        formData.append('uploadImage', _files);
        formData.append('subTitle', _subTitle);
        formData.append('sliderTypes', _sliderTypes);

        upload(formData);
    });

    $('#contact-form').submit(function (e) {
        e.preventDefault();
        let data = {
            address: $("#Address").val(),
            city: $("#City").val(),
            state: $("#State").val(),
            country: $("#Country").val(),
            email: $("#Email").val(),
            phoneNumber: $("#PhoneNumber").val(),
        }
        alert(data);
        updateContact(`${url}Settings/AddAddress`, data);
    })

    $('#social-form').submit(function (e) {
        e.preventDefault();
        let data = {
            facebook: $("#Facebook").val(),
            twitter: $("#Twitter").val(),
            instagram: $("#Instagram").val(),
            youtube: $("#Youtube").val()
        }
        alert(data)
        updateContact(`${url}Settings/AddSocialMedia`, data);
    })


});

var uploadBannerImage = function (imageId, imageType) {
    var _id = $('#data-id').val();
    var _files = $(`#${imageId}`)[0].files[0];
    var formData = new FormData();
    formData.append('uploadImage', _files);
    formData.append('sliderTypes', 5);
    upload(formData);
}

function upload(formData) {
    axios.post(`${url}Settings/AddSlider`, formData)
        .then(response => {
            const addedUser = response.data;
            toastr['success'](`👋 ${addedUser.message}`, 'Success!', {
                closeButton: true,
                tapToDismiss: true,
                rtl: false
            });
            window.setTimeout(window.location.reload(), 2000);
        })
        .catch(function(error) {
            if (error.response) {
                toastr['error'](error.response.data.message, 'Error!', {
                    closeButton: true,
                    tapToDismiss: false,
                    rtl: false
                });
            }
        });
}

function updateContact(url, data) {
    axios.post(url, data)
        .then(response => {
            const _contact = response.data;
            toastr['success'](`👋 ${_contact.message}`, 'Success!', {
                closeButton: true,
                tapToDismiss: true,
                rtl: false
            });
            
        })
        .catch(function (error) {
            if (error.response) {
                toastr['error'](error.response.data.message, 'Error!', {
                    closeButton: true,
                    tapToDismiss: false,
                    rtl: false
                });
            }
        });
}
