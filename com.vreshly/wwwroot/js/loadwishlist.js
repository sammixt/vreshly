$(function () {
    var productTable = $("#wishlist_rows");
    var authItem = JSON.parse(localStorage.getItem("auth"));
 

    var isAuthenticated = function () {
        if (authItem != null) {
            return true;
        } else {
            alert('Login to view wishlist');
            false;
        }
    }

    function loadWishList() {
        if (isAuthenticated) {
            $.ajax({
                type: 'GET',
                url: `${baseUrl}Shop/GetWishList`,
                headers: {
                    Authorization: 'Bearer ' + authItem.token
                },
            }).done(function (response) {
                //console.log(response);
                $.each(response, function (key, value) {

                    productTable.append(`<tr>\
                                        <td class='pro-thumbnail'><a href='#'><img class='img-fluid' src='${value.product.mainImage}' alt='Product' /></a></td>\
                                        <td class='pro-title'><a href='#'>${value.product.productName}</td>\
                                        <td class='pro-price'><span>NGN ${value.product.discountPrice}</span></td>\
                                        <td class='pro-stock'><span><strong>${value.product.soldOut ? 'Out of Stock' : 'In Stock'}</strong></span></td>\
                                        <td class='pro-cart'>\
                                                <a class='add-to-cart btn obrien-button primary-btn text-uppercase' data-id='${value.productId}'
                                                    data-productName='${value.product.productName}'
                                                    data-price='${value.product.discountPrice}'
                                                    data-quantity='${value.product.soldOut ? '0' : '1'}' data-pictureUrl='${value.product.mainImage}'
                                                    data-category='${value.product.category}' data-brand='${value.product.brand }' title = 'Add To cart' >\
                                                    Add To cart
                                                </a>\
                                            </td>\
                                        <td class='pro-remove'><a href='javascript:void()' class='delete' data-id='${value.id}'><i class='ion-trash-b'></i></a></td>\
                                    </tr>`);
                });
                //setTimeout(, 3000);

                //sliderInit('featured-product-slider')

            }).fail(function (data) {
                // Make sure that the formMessages div has the 'error' class.
                console.log(data);
            });
        }   
    }

    productTable.on('click', '.delete', function (e) {
        e.preventDefault();
        var $this = $(this);
        var _id = $this.attr('data-id');
        var wishList = {
            id: _id
        };
        $.ajax({
            type: 'DELETE',
            url: `${baseUrl}Shop/DeleteWishlist`,
            //headers: {
            //    Authorization: 'Bearer ' + authItem.token
            //},

            data: JSON.stringify(wishList),
            contentType: "application/json;charset=utf-8",
            traditional: true,
        }).done(function (response) {
            window.location.reload();
        })
        .fail(function (data) {
            alert(data.message);
        });
    })

    loadWishList();
})