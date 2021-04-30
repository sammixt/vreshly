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
    var add_brand = $(this);
    var _brandName = $('#brand_name').val();
    var _files = $('#customFile')[0].files[0];
    debugger;
    var formData = new FormData();
    formData.append('brandName', _brandName);
    formData.append('uploadImage', _files);
    var textvalue = $(this).text();
    $(this).html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
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

            add_category.text("Submit");
        });
});
