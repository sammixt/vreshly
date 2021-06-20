$(function () {
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
                    <td class='cart-product-total text-center'><span class='amount'>₦ "+ value.price + "</span></td>\
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
            $('.myniceselectTest').niceSelect();
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
            toast('error', `Please select delivery method`);
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
            toast('error', `Please select Delivery Method`);
            isAllFieldValid = false;
        }

        isAllFieldValid = validatePhone(phoneNumber, 'Phone Number');
        var authItem = JSON.parse(localStorage.getItem("auth"));
        if (isAllFieldValid) {
            if (authItem != null)
            {
                basket.deliveryMethod = parseInt(deliveryMethods.val());
                basket.paymentIntent = basket.id;
                basket.shippingPrice = $('#deliveryMethod option:selected').attr('data-price');
                localStorage.setItem("cart", JSON.stringify(basket));
                callApi('UpdateBasket', basket);
                var _paymentMethod = parseInt($('#paymentMethod option:selected').val())
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
                    paymentMethod: _paymentMethod,
                    shipToAddress: _shipToAddress
                }

                $.ajax({
                    type: 'POST',
                    url: `${baseUrl}Orders/CreateOrder`,
                    headers: {
                        Authorization: 'Bearer ' + authItem.token
                    },
                    data: JSON.stringify(orderDto),
                    contentType: "application/json;charset=utf-8",
                    traditional: true,
                }).done(function (response) {
                    if (_paymentMethod == 1) {
                        payWithPaystack(response.info, response.pubKey, response.total, authItem);
                    } else {
                        $('#msg').css('color', 'green');
                        $('#msg').text(`Transaction was successful. Reference : ${basket.id}`);
                        $('#completed-transaction').css('display', 'block');
                        localStorage.removeItem('cart');
                    }
                    
                })
                .fail(function (data) {
                    console.log(data);
                });
            }
            
        }

    });

    var payWithPaystack = function(order,pubKey,total, _authItem) {
        var handler = PaystackPop.setup({
            key: `${pubKey}`, 
            email: order.buyerEmail,
            amount: total * 100, 
            currency: 'NGN', 
            ref: order.paymentIntentId,
            callback: function (response) {
                //this happens after the payment is completed successfully

                var reference = response.reference;
                verifyTransaction(reference, _authItem);
                localStorage.removeItem('cart');
                //alert('Payment complete! Reference: ' + reference);
                // Make an AJAX call to your server with the reference to verify the transaction

            },
            onClose: function () {
                deleteOrder(order.paymentIntentId);
            },
        });
        handler.openIframe();
    }


    var verifyTransaction = function (ref,auth) {
        $.ajax({
            type: 'GET',
            url: `${baseUrl}Orders/VerifyTransaction?reference=${ref}`,
            headers: {
                Authorization: 'Bearer ' + auth.token
            } 
        }).done(function (response) {
            $('#billing-details').css('display', 'none');
            if (response.status == "success") {
                $('#msg').css('color', 'green');
                $('#msg').text(`Transaction was successful. Reference : ${ref}`);
                $('#completed-transaction').css('display', 'block');
            } else {
                $('#msg').css('color', 'red');
                $('#msg').text(`Transaction was unsuccessful. Reference : ${ref}`);
                $('#completed-transaction').css('display', 'block');
            }
        }).fail(function (data) {
            $('#billing-address-div').css('display', 'none');
            $('#msg').css('color', 'yellow');
            $('#msg').text(`${data.message}`);
            $('#completed-transaction').css('display', 'block');
        });
    }

    var deleteOrder = function (ref) {
        $.ajax({
            type: 'DELETE',
            url: `${baseUrl}Orders/DeleteOrder?reference=${ref}`,
            //headers: {
            //    Authorization: 'Bearer ' + authItem.token
            //},

        }).done(function (response) {
            
        })
        .fail(function (data) {
            alert(data.message);
        });
    }

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