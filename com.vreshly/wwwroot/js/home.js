$(function () {
    var productWrapperDiv = $('.product-wrapper');
    productWrapperDiv.on('click', '.detail-view', function (e) {
        e.preventDefault();
        var $this = $(this);
        var _id = $this.attr('data-id');
        $.ajax({
            type: 'GET',
            url: `${baseUrl}Product/GetProduct/${_id}`
        }).done(function (response) {
            $('.modal-body').html("<div class='container-fluid'>\
                    <div class='row'>\
                        <div class='col-lg-6 col-md-6 text-center'>\
                            <div class='product-image'>\
                                <img src='"+ response.mainImage + "' alt='Product Image'>\
                            </div>\
                        </div>\
                        <div class='col-lg-6 col-md-6'>\
                            <div class='modal-product'>\
                                <div class='product-content'>\
                                    <div class='product-title'>\
                                        <h4 class='title'>"+ response.productName + "</h4>\
                                    </div>\
                                    <div class='price-box'>\
                                         <span class='regular-price '>NGN "+ response.discountPrice + "</span>\
									     <span class='old-price'><del>NGN "+ response.sellingPrice + "</del></span>\
                                    </div>\
                                    <div class='product-rating'>\
                                        <i class='fa fa-star'></i>\
                                        <i class='fa fa-star'></i>\
                                        <i class='fa fa-star'></i>\
                                        <i class='fa fa-star-o'></i>\
                                        <i class='fa fa-star-o'></i>\
                                        <span>1 Review</span>\
                                    </div>\
                                    <p class='desc-content'>"+ response.productDescription + "</p>\
                                    <div class='quantity-with_btn'>\
                                        <div class='quantity'>\
                                            <div class='cart-plus-minus'>\
                                                <input class='cart-plus-minus-box' value='0' type='text'>\
                                                <div class='dec qtybutton'>-</div>\
                                                <div class='inc qtybutton'>+</div>\
                                            </div>\
                                        </div>\
                                        <div class='add-to_cart'>\
                                             <a class='add-to-cart-modal btn obrien-button primary-btn' data-id='"+ response.id +
                "' data-productName='" + response.productName +
                "' data-price='" + response.discountPrice +
                "' data-quantity='1' data-pictureUrl='" + response.mainImage +
                "' data-category='" + response.category +
                "' data-brand='" + response.brand + "' title = 'Add To cart' >\
                                                            Add To cart</a>\
                                        </div>\
                                    </div>\
                                </div>\
                            </div>\
                        </div>\
                    </div>\
                </div>");
            $('#exampleModalCenter').modal({
                show: true
            })
        }).fail(function (data) {
            // Make sure that the formMessages div has the 'error' class.
            console.log(data);
        });
        //alert(_id);

    });

    $('.modal-body').on('click', '.qtybutton', function () {
        var $button = $(this);
        var oldValue = $button.parent().find('input').val();
        if ($button.hasClass('inc')) {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            // Don't allow decrementing below zero
            if (oldValue > 1) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 1;
            }
        }
        $button.parent().find('input').val(newVal);
    });
});
