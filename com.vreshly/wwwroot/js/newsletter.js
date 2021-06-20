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
    language: {
        paginate: {
            // remove previous & next text from pagination
            previous: '&nbsp;',
            next: '&nbsp;'
        }
    }
});

$("#subscribers").on('click', 'a.delete-record', function (e) {
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
            axios.delete(`${url}Newsletter/DeleteSubscription/${id}`)
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
                text: 'Your Subscriber is still intact :)',
                icon: 'error',
                customClass: {
                    confirmButton: 'btn btn-success'
                }
            });
        }
    });

});