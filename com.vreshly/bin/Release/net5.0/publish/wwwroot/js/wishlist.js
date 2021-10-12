$(function () {
    var loggedIn = {};
    var authItem = JSON.parse(localStorage.getItem("auth"));
    var featuredProductDiv = $('#shop_wrapper');
    var productWrapperDiv = $('.product-wrapper');
    var addProductToCart = $('#add-to_cart');

    var isAuthenticated = function () {
        if (authItem != null) {
            return true;
        } else {
            toast('error', `Login to perform task`);
            return false;
        }
    }

    var addToWishList = function (productId) {
        var wishList = {
            productId: productId
        }
        $.ajax({
            type: 'POST',
            url: `${baseUrl}Shop/CreateWishList`,
            headers: {
                Authorization: 'Bearer ' + authItem.token
            },
            data: JSON.stringify(wishList),
            contentType: "application/json;charset=utf-8",
            traditional: true,
        }).done(function (response) {
            toast('success', `${response.message}`);

        })
            .fail(function (data) {
                alert(data.message);
            });
    }

    featuredProductDiv.on('click', '.add-to-wishlist', function (e) {
        e.preventDefault();
        var $this = $(this);
        var _id = $this.attr('data-id');
        if (isAuthenticated()) {
            addToWishList(_id);
        }
    });

    productWrapperDiv.on('click', '.add-to-wishlist', function (e) {
        e.preventDefault();
        var $this = $(this);
        var _id = $this.attr('data-id');
        if (isAuthenticated()) {
            addToWishList(_id);
        }
    });

    addProductToCart.on('click', '.add-to-wishlist', function (e) {
        e.preventDefault();
        var $button = $(this);
        var _id = $button.attr('data-id');
        if (isAuthenticated()) {
            addToWishList(_id);
        }
    });

});