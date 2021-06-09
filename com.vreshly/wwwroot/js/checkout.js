$(function () {
    var baseUrl = "https://localhost:5201/";
    let checkoutItemTable = $('#checkoutitems');
    let deliveryMethods = $('#deliveryMethod');
    let placeOrder = $('#place_order');
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

    placeOrder.on('click', function (e) {
        e.preventDefault();
        var isAllFieldValid = true;
        let firstName = $('#firstName').val();
        let lastName = $('#lastName').val();
        let address = $('#address').val();
        let city = $('#city').val();
        let state = $('#state').val();
        let zipcode = $('#zipcode').val();
        let phoneNumber = $('#phoneNumber').val();

        isAllFieldValid = validateForm(firstName, 'First Name');
        isAllFieldValid = validateForm(lastName, 'Last Name');
        isAllFieldValid = validateForm(address, 'Address');
        isAllFieldValid = validateForm(city, 'City');
        isAllFieldValid = validateForm(state, 'State');
        //isAllFieldValid = validateForm(zipcode, 'Zip Code');
        if (parseInt(deliveryMethods.val()) == 0) {
            alert('Please select Delivery Method');
            isAllFieldValid = false;
        }

        isAllFieldValid = validatePhone(phoneNumber, 'Phone Number');
        if (isAllFieldValid) {
            basket.deliveryMethod = parseInt(deliveryMethods.val());
            basket.paymentIntent = basket.id;
            basket.shippingPrice = $('#deliveryMethod option:selected').attr('data-price');
            localStorage.setItem("cart", JSON.stringify(basket));
            callApi('UpdateBasket', basket);
            var _shipToAddress =
            {
                firstName: firstName,
                lastName: lastName,
                street: address,
                city: city,
                state: state,
                zipCode: zipcode,
                phoneNumber: phoneNumber
            }

            var orderDto =
            {
                basketId: basket.id,
                deliveryMethod: basket.deliveryMethod,
                shipToAddress: _shipToAddress
            }

            $.ajax({
                type: 'POST',
                url: `${baseUrl}Orders/CreateOrder`,
                data: JSON.stringify(orderDto),
                contentType: "application/json;charset=utf-8",
                traditional: true,
            }).done(function (response) {
                console.log(response);
            })
                .fail(function (data) {
                    console.log(data);
                });
        }

    });

    var validatePhone = function (txtPhone,field) {
        var filter = /^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$/;
        if (filter.test(txtPhone)) {
            return true;
        }
        else {
            alert(`${field} must be filled out`);
            return false;
        }
    }

    validateForm = function (x, field) {
        if (x == "") {
            alert(`${field} must be filled out`);
            return false;
        }
    }

    var callApi = function (endpoint, _data) {
        $.ajax({
            type: 'POST',
            url: `${baseUrl}Basket/${endpoint}`,
            data: JSON.stringify(_data),
            contentType: "application/json;charset=utf-8",
            traditional: true,
        }).done(function (response) {
            console.log(response);
        })
            .fail(function (data) {
                console.log(data);
            });
    }

   

    

    getDeliveryMethods();

    loadCheckoutItems();



});