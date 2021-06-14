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
                                        <td class='pro-cart'><a href='checkout.html' class='btn obrien-button primary-btn text-uppercase'>Add to Cart</a></td>\
                                        <td class='pro-remove'><a href='#'><i class='ion-trash-b'></i></a></td>\
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

    loadWishList();
})