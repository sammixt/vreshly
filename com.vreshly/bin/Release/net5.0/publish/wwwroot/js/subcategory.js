
$('#dt_basic').DataTable({
    columnDefs: [
        {
            // For Responsive
            className: 'control',
            orderable: false,
            responsivePriority: 2,
            targets: 0
        },
        
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

var populateSelectOptions = function (categories) {

    $.each(categories, function (key, value) {
        $('#category_options').append("<option value='" + value.id + "'>" + value.categoryName + "</option>");
        $('#category_options_edit').append("<option value='" + value.id + "'>" + value.categoryName + "</option>");
    });
    
}

const getCategories = function() {
   
        axios.get(`${url}Category/GetCategories`)
        .then((response) => {
             populateSelectOptions(response.data);
        })
    .catch((error) => {
        console.log(error);
        });

    
};

window.onload = getCategories();

$('#add_category').on('click', function (e) {
    var add_category = $(this);
    var _subcategoryName = $('#sub_category_name').val();
    var _categoryId = $('#category_options option:selected').val();
    var textvalue = $(this).text();
    $(this).html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

    axios.post(`${url}Subcategory/AddSubCategory`, { subCategoryName: _subcategoryName, categoryId: _categoryId})
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

$("#subcategories").on('click', '.item-edit', function (e) {
    e.preventDefault();
    var current_item = $(this);
    var id = current_item.attr('data-id');
    var cat_name = current_item.attr('data-cat');
    var sub_cat_name = current_item.attr('data-sub-cat');
    $("#subCategoryName").val(sub_cat_name);
    $("#id").val(id);
    
    $("#category_options_edit option").filter(function () {
        //may want to use $.trim in here
        return $(this).text() == cat_name;
    }).prop('selected', true);

    $("#edit-subcategory").modal(
        {
            show: true
        })
});

$('#update-sub-category').on('click', function (e) {
    var add_category = $(this);
    var _subcategoryName = $('#subCategoryName').val();
    var _id = $('#id').val();
    var _categoryId = $('#category_options_edit option:selected').val();
    var textvalue = $(this).text();
    $(this).html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

    axios.put(`${url}Subcategory/UpdateSubCategory`, { id: _id, subCategoryName: _subcategoryName, categoryId: _categoryId })
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

$("#subcategories").on('click', 'a.delete-record', function (e) {
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
            axios.delete(`${url}Subcategory/DeleteSubCategory/${id}`)
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