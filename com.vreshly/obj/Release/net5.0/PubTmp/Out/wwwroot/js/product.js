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

$('#add_product').on('click', function (e) {
    var add_category = $(this);
    var _productName = $('#category_name').val();
    var _categoryId = $('#category_options option:selected').val();
    var _subCategoryId = $('#subcategory_options option:selected').val();
    var _brandId = $('#brand_options option:selected').val();
    var textvalue = $(this).text();
    $(this).html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

    axios.post(`${url}Product/CreateProduct`, {
        productName: _productName,
        categoryId: _categoryId,
        subCategoryId: _subCategoryId,
        brandId: _brandId

    })
        .then(response => {
            const addedUser = response.data;
            toastr['success'](`👋 ${response.data}`, 'Success!', {
                closeButton: true,
                tapToDismiss: true,
                rtl: false
            });
            window.setTimeout(window.location.reload(), 2000);
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

var populateSelectOptions = function (categories) {

    $.each(categories, function (key, value) {
        $('#category_options').append("<option value='" + value.id + "'>" + value.categoryName + "</option>");
        $('#category_options_edit').append("<option value='" + value.id + "'>" + value.categoryName + "</option>");
    });

}

var populateOptions = function () {

    getCategories();
    getBrands();
}

const getCategories = function () {

    axios.get(`${url}Category/GetCategories`)
        .then((response) => {
            
            $.each(response.data, function (key, value) {
                $(`#category_options`).append("<option value='" + value.id + "'>" + value.categoryName + "</option>");
            });
        })
        .catch((error) => {
            console.log(error);
        });

};

$('.category_options').on('change', '#category_options', function () {
    var id = $('#category_options option:selected').val();
    axios.get(`${url}Subcategory/GetSubCategoriesByCategory/${id}`)
        .then((response) => {

            $.each(response.data, function (key, value) {
                $(`#subcategory_options`).append("<option value='" + value.id + "'>" + value.subCategoryName + "</option>");
            });
        })
        .catch((error) => {
            console.log(error);
        });
});



const getBrands = function () {

    axios.get(`${url}Brand/GetBrands`)
        .then((response) => {
            $.each(response.data, function (key, value) {
                $(`#brand_options`).append("<option value='" + value.id + "'>" + value.brandName + "</option>");
            });
        })
        .catch((error) => {
            console.log(error);
        });

};

window.onload = populateOptions();