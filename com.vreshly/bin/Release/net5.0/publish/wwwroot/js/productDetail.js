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

$('#update-general').on('click', function (e) {
    e.preventDefault();
    var add_category = $(this);
    var _productName = $('#ProductName').val();
    var _productCode = $('#ProductCodes').val();
    var _productSummary = $('#ProductSummary').val();
    var _productDescription = $('#ProductDescription').val();
    var _id = $('#general-id').val();
    var textvalue = $(this).text();
    $(this).html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

    axios.put(`${url}Product/UpdateGeneralInfo`, {
        productName: _productName,
        productCodes: _productCode,
        productSummary: _productSummary,
        productDescription: _productDescription,
        id : _id
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

$('#update-data').on('click', function (e) {
    e.preventDefault();
    var add_category = $(this);
    var _sellingPrice = $('#SellingPrice').val();
    var _discountPrice = $('#DiscountPrice').val();
    var _quantity = $('#Quantity').val();
    var _categoryId = $('#category_options option:selected').val();
    var _subCategoryId = $('#subcategory_options option:selected').val();
    var _brandId = $('#brand_options option:selected').val();
    var _isFeaturedProduct = $('#IsFeaturedProduct').is(':checked') ? true : false;
    var _isBestSeller = $('#IsBestSeller').is(':checked') ? true : false;
    var _id = $('#data-id').val();
    var textvalue = $(this).text();
    $(this).html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>\
                                            <span class=\"ml-25 align-middle\">Loading...</span>");

    axios.put(`${url}Product/UpdateDataInfo`, {
        sellingPrice: _sellingPrice,
        discountPrice: _discountPrice,
        quantity: _quantity,
        categoryId: _categoryId,
        subCategoryId: _subCategoryId,
        brandId: _brandId,
        isFeaturedProduct: _isFeaturedProduct,
        isBestSeller: _isBestSeller,
        id : _id
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

var uploadImage = function (imageId, imageType) {
    var _id = $('#data-id').val();
    var _files = $(`#${imageId}`)[0].files[0];
    var formData = new FormData();
    formData.append('imageType', imageType);
    formData.append('uploadImage', _files);
    formData.append('id', _id);

    axios.put(`${url}Product/AddImage`, formData)
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

            
        });
}

