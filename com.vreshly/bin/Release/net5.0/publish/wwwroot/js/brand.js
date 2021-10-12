$('#dt_basic').DataTable({
    columnDefs: [
        {
            // For Responsive
            className: 'control',
            orderable: false,
            responsivePriority: 2,
            targets: 0
        },
        //{
        //    targets: 2,
        //    visible: true
        //},
        //{
        //   responsivePriority: 1,
        //   targets: 3
        //}
    ],
    //order: [[2, 'desc']],
    language: {
        paginate: {
            // remove previous & next text from pagination
            previous: '&nbsp;',
            next: '&nbsp;'
        }
    }
});

$('#add-new-record-form').on('submit', function (e) {
    e.preventDefault();
    var add_brand = $('#add_brand');
    var _brandName = $('#brand_name').val();
    var _files = $('#customFile')[0].files[0];
    var formData = new FormData();
    formData.append('brandName', _brandName);
    formData.append('uploadImage', _files);
    var textvalue = $(this).text();
    add_brand.html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

    axios.post(`${url}Brand/AddBrand`, formData)
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

            add_brand.text("Submit");
        });
});

$("#brands").on('click', '.item-edit', function (e) {
    e.preventDefault();
    var current_item = $(this);
    var id = current_item.attr('data-id');
    axios.get(`${url}Brand/GetBrand/${id}`)
        .then((response) => {
            $("#id").val(id);
            $("#brandName").val(response.data.brandName)
            $('#brand-image').prop("src", response.data.brandLogo);
        })
        .catch((error) => { console.log });
    

    $("#edit-brand").modal(
        {
            show: true
        })

});

$('#update-brand').on('submit', function (e) {
    e.preventDefault();
    var add_brand = $('#update-brand_btn');
    var _brandName = $('#brandName').val();
    var _files = $('#customFileEdit')[0].files[0];
    var id = $('#id').val();
    debugger;
    var formData = new FormData();
    formData.append('brandName', _brandName);
    formData.append('id', id);
    formData.append('uploadImage', _files);
    var textvalue = $(this).text();
    add_brand.html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

    axios.put(`${url}Brand/UpdateBrand`, formData)
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

            add_brand.text("Submit");
        });
});

$("#brands").on('click', 'a.delete-record', function (e) {
    e.preventDefault();

    var current_item = $(this);
    var id = current_item.attr('data-id');

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        customClass: {
            confirmButton: 'btn btn-primary',
            cancelButton: 'btn btn-outline-danger ml-1'
        },
        buttonsStyling: false
    }).then(function (result) {
        if (result.value) {
            axios.delete(`${url}Brand/DeleteBrand/${id}`)
                .then(response => {
                    Swal.fire({
                        icon: 'success',
                        title: 'Deleted!',
                        text: `${response.data.message}`,
                        customClass: {
                            confirmButton: 'btn btn-success'
                        }
                    }).then(function (result_deleted) {
                        if (result_deleted.value) {
                            window.location.reload();
                        }
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

        } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire({
                title: 'Cancelled',
                text: 'Your imaginary file is safe :)',
                icon: 'error',
                customClass: {
                    confirmButton: 'btn btn-success'
                }
            });
        }
    });

});