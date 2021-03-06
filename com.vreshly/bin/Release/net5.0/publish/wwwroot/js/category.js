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

$('#add_category').on('click', function (e) {
    var add_category = $(this);
    var _categoryName = $('#category_name').val();
    var textvalue = $(this).text();
    $(this).html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

    axios.post(`${url}Category/AddCategory`, { categoryName: _categoryName })
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

            add_category.text("Submit");
        });
});

$("#categories").on('click', '.item-edit', function (e) {
    e.preventDefault();
    var current_item = $(this);
    var id = current_item.attr('data-id');
    var name = current_item.attr('data-name');
    $("#categoryName").val(name);
    $("#id").val(id);

    $("#edit-category").modal(
        {
            show: true
        })

});

$('#update-category').on('click', function (e) {
    var add_category = $(this);
    var _categoryName = $('#categoryName').val();
    var _id = $('#id').val();
    var textvalue = $(this).text();
    $(this).html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

    axios.put(`${url}Category/UpdateCategory`, { id: _id, categoryName: _categoryName })
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

            add_category.text("Submit");
        });
});

$("#categories").on('click', 'a.delete-record', function (e) {
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
            axios.delete(`${url}Category/DeleteCategory/${id}`)
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

