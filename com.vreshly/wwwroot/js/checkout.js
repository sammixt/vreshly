$(function () {
    var baseUrl = "https://localhost:5201/";
    let checkoutItemTable = $('#checkoutitems');
    let deliveryMethods = $('#deliveryMethod');
    let basket = JSON.parse(localStorage.getItem("cart"));

    var loadCheckoutItems = function () {
        if (basket != null) {

            $.each(basket.items, function (key, value) {
                checkoutItemTable.prepend(
                    "<tr class= 'cart_item'>\
                    <td class='cart-product-name'>"+ value.productName + "<strong class='product-quantity'> × "+ value.quantity + "</strong></td>\
                    <td class='cart-product-total text-center'><span class='amount'>NGN "+ value.price + "</span></td>\
                </tr >"
                );
            });

            $('.checkoutTotal').text(getTotal());
        }

        
    }

    var getTotal = function () {
        var tempCart = JSON.parse(localStorage.getItem("cart"));
        var totalCost = 0;
        for (var i in tempCart.items) {
            totalCost = totalCost + (tempCart.items[i].price * tempCart.items[i].quantity)
        }

        return totalCost;
    }

    var getDeliveryMethods = function () {
        $.ajax({
            type: 'GET',
            url: `${baseUrl}Orders/GetDeliveryMethod`
        }).done(function (response) {
            $.each(response, function (key, value) {
                deliveryMethods.append(`<option value='${value.id}' data-price='${value.price}'>${value.shortName} -- ${value.deliveryTime}</option>`)
            })
        })
    }

    deliveryMethods.on('change', function () {
        var opt = $(this).val();
        if (parseInt(opt) != 0) {
            var price = $('option:selected', this).attr('data-price');
            $('.shippingAmount').text(price);
            var totalAmount = getTotal() + parseInt(price);
            $('.totalAmount').text(totalAmount);
        } else {
            alert('Please select delivery method');
        }
        
    });

    getDeliveryMethods();

    loadCheckoutItems();



});