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

    var addRecurring = function (_productId, _productName, _qty, _freq) {
        var order = {
            productId: _productId,
            productName: _productName,
            quantity: _qty,
            inputFrequency: _freq
        }
        $.ajax({
            type: 'POST',
            url: `${baseUrl}Shop/CreateRecurringOrder`,
            headers: {
                Authorization: 'Bearer ' + authItem.token
            },
            data: JSON.stringify(order),
            contentType: "application/json;charset=utf-8",
            traditional: true,
        }).done(function (response) {
            toast('success', `${response.message}`);

        })
            .fail(function (data) {
                alert(data.message);
            });
        $('#recurring-modal').modal({
            show: false,
        })
    }

    featuredProductDiv.on('click', '.add-to-recurring', function (e) {
        e.preventDefault();
        var $this = $(this);
        var _id = $this.attr('data-id');
        var _product = $this.attr('data-product');
        if (isAuthenticated()) {
            _showModal(_id, _product);
        }
    });

    productWrapperDiv.on('click', '.add-to-recurring', function (e) {
        e.preventDefault();
        var $this = $(this);
        var _id = $this.attr('data-id');
        var _product = $this.attr('data-product');
        if (isAuthenticated()) {
            _showModal(_id, _product);
        }
    });

    addProductToCart.on('click', '.add-to-recurring', function (e) {
        e.preventDefault();
        var $button = $(this);
        var _id = $button.attr('data-id');
        var _product = $this.attr('data-product');
        if (isAuthenticated()) {
            _showModal(_id, _product);
        }
    });

    var _showModal = function (_id, _product) {
        
        $('#recurring-product-id').val(_id)
        $('#recurring-product').val(_product)
        $('#recurring-modal').modal({
            show:true,
        })
    }

    $('#add-recurring').on('click', function (e) {
        e.preventDefault();
        var prodId = $('#recurring-product-id').val();
        var prodName = $('#recurring-product').val();
        var _qty = $('#recurring-product-qty').val();
        var freq = $('#recurring-frequency option:selected').val();
        addRecurring(prodId, prodName, _qty, freq);
    })

});