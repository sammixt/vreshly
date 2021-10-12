﻿$('#dt_basic').DataTable({
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
    var _title = $('#title').val();
    var _content = $('#content').val();
    var _videoLink = $('#video_url').val();
    var _educativeType = $('#eduType').val();
    var _files = $('#customFile')[0].files[0];
    var formData = new FormData();
    formData.append('title', _title);
    formData.append('content', _content);
    formData.append('videoLink', _videoLink);
    formData.append('educativeType', _educativeType);
    formData.append('uploadImage', _files);
    var textvalue = $(this).text();
    $('#add_educative').html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

    axios.post(`${url}Settings/AddEducative`, formData)
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

$("#educative_content").on('click', '.item-edit', function (e) {
    e.preventDefault();
    var current_item = $(this);
    var id = current_item.attr('data-id');
    axios.get(`${url}Settings/GetEducative/${id}`)
        .then((response) => {
            $("#id").val(id);
            $('#update_title').val(response.data.title);
            $('#update_content').val(response.data.content);
            $('#update_video_url').val(response.data.videoLink);
            $('#educative-image').prop("src", response.data.imageUrl);
        })
        .catch((error) => { console.log });


    $("#edit-educative").modal(
        {
            show: true
        })

});

$('#update-educative').on('submit', function (e) {
    e.preventDefault();
    var add_brand = $('#update-content-btn');

   var _id = $("#id").val();
    var _title = $('#update_title').val();
    var _content = $('#update_content').val();
    var _videoLink = $('#update_video_url').val();
   
    var _files = $('#customFileEdit')[0].files[0];
    
    var formData = new FormData();
    formData.append('id', _id);
    formData.append('title', _title);
    formData.append('content', _content);
    formData.append('videoLink', _videoLink);
    formData.append('uploadImage', _files);
    var textvalue = $(this).text();
    add_brand.html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

    axios.put(`${url}Settings/UpdateEducative`, formData)
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

$("#educative_content").on('click', 'a.delete-record', function (e) {
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
            axios.delete(`${url}Settings/DeleteEducative/${id}`)
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

$("#educative_content").on('click', 'a.change-status', function (e) {
    e.preventDefault();

    var current_item = $(this);
    var id = current_item.attr('data-id');

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to Alter Status!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, Alter it!',
        customClass: {
            confirmButton: 'btn btn-primary',
            cancelButton: 'btn btn-outline-danger ml-1'
        },
        buttonsStyling: false
    }).then(function (result) {
        if (result.value) {
            axios.put(`${url}Settings/UpdateEducativeStatus/${id}`)
                .then(response => {
                    Swal.fire({
                        icon: 'success',
                        title: 'Altered!',
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
                text: 'Unaltered :)',
                icon: 'error',
                customClass: {
                    confirmButton: 'btn btn-success'
                }
            });
        }
    });

});