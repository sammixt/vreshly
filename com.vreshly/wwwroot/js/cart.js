﻿$(function () {
    var featuredProductDiv = $('#shop_wrapper');
    var cartItemCount = $('.cart-item_count');
    var cartItemWrapper = $('.cart-item-wrapper');
    var cartTable = $('#cart-table');
    const basketItems = [];
    const basketItem = {
        id: 0,
        productName: '',
        price: 0,
        quantity: 0,
        pictureUrl: '',
        brand: '',
        category: ''
    };

    var addToCart = function (_id, _productName, _price, _quantity, _prictureUrl, _brand, _category) {
        if (localStorage.getItem("cart") == null) {
            basketItem.id = _id;
            basketItem.productName = _productName;
            basketItem.price = _price;
            basketItem.quantity = _quantity;
            basketItem.pictureUrl = _prictureUrl;
            basketItem.category = _category;
            basketItem.brand = _brand;

            basketItems.push(basketItem);
            localStorage.setItem("cart", JSON.stringify(basketItems));
        } else {
            var carts = JSON.parse(localStorage.getItem("cart"));
            var product = carts.find(function (cart) {
                return cart.id === _id;
            });
            if (product == null ) {
                basketItem.id = _id;
                basketItem.productName = _productName;
                basketItem.price = _price;
                basketItem.quantity = _quantity;
                basketItem.pictureUrl = _prictureUrl;
                basketItem.category = _category;
                basketItem.brand = _brand;
                carts.push(basketItem);
                localStorage.setItem("cart", JSON.stringify(carts));
            } else {
                for (var i in carts) {
                    if (carts[i].id == _id) {
                        carts[i].quantity += _quantity;
                        break; //Stop this loop, we found it!
                    }
                }
                localStorage.setItem("cart", JSON.stringify(carts));
            }
            
            console.log(carts);
        }

        cartCount();
        loadCartDropdown();

    }

    var getUUID = function () {
        var dt = new Date().getTime();
        var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = (dt + Math.random() * 16) % 16 | 0;
            dt = Math.floor(dt / 16);
            return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
        });
        return uuid;
    }


    var cartCount = function () {
        var tempCart = JSON.parse(localStorage.getItem("cart"));
        var cartCount = tempCart.length;
        cartItemCount.text(cartCount);
    }



    featuredProductDiv.on('click', '.add-to-cart', function (e) {
        e.preventDefault();
        var $this = $(this);
        var _id = $this.attr('data-id');
        var _productName = $this.attr('data-productName');
        var _price = parseInt($this.attr('data-price'));
        var _quantity = parseInt($this.attr('data-quantity'));
        var _pictureUrl = $this.attr('data-pictureUrl');
        var _brand = $this.attr('data-brand');
        var _category = $this.attr('data-category');
        addToCart(_id, _productName, _price, _quantity, _pictureUrl, _brand, _category)
    })

    var loadCartDropdown = function () {
        cartItemWrapper.empty();
        var tempCart = JSON.parse(localStorage.getItem("cart"));

        $.each(tempCart, function (key, value) {
            cartItemWrapper.prepend(
                "<div class='single-cart-item'>\
            <div class='cart-img'>\
                <a href='cart.html'><img src='"+ value.pictureUrl + "' alt=''></a>\
            </div>\
            <div class='cart-text'>\
                <h5 class='title'>"+ value.productName + "</h5>\
                <div class='cart-text-btn'>\
                    <div class='cart-qty'>\
                        <span>"+ value.quantity + "×</span>\
                        <span class='cart-price'>NGN "+ value.price + "</span>\
                    </div>\
                    <button type='button'class='trash-cart' data-id='"+ value.id+"'><i class='ion-trash-b'></i></button>\
                </div>\
            </div>\
        </div>"
            );
        });

        var total = getTotal();

        cartItemWrapper.append("<div class='cart-price-total d-flex justify-content-between'>\
        <h5>Total :</h5>\
        <h5 class='total'>NGN "+ total+"</h5>\
    </div>\
    <div class='cart-links d-flex justify-content-center'>\
        <a class='obrien-button white-btn' href='/Shop/Cart'>View</a>\
        <a class='obrien-button white-btn' href='checkout.html'>Checkout</a>\
    </div>")
        
    }

    var getTotal = function () {
        var tempCart = JSON.parse(localStorage.getItem("cart"));
        var totalCost = 0;
        for (var i in tempCart) {
            totalCost = totalCost + (tempCart[i].price * tempCart[i].quantity)
        }

        return totalCost;
    }

    var deleteItemFromCart = function (_id) {
        var tempCart = JSON.parse(localStorage.getItem("cart"));
        var product = tempCart.filter(cart => cart.id !== _id);
        localStorage.setItem("cart", JSON.stringify(product));
    }

    cartItemWrapper.on('click', '.trash-cart', function (e) {
        e.preventDefault();
        let $this = $(this);
        let _id = $this.attr('data-id');
        deleteItemFromCart(_id);
        cartCount();
        loadCartDropdown();
    });

    cartCount();
    loadCartDropdown();

    var loadCartsOnCartPage = function () {
        cartTable.empty();
        let tr = "";
        let carts = JSON.parse(localStorage.getItem("cart"));
        if (carts != null) {
            $.each(carts, function (key, value) {
                tr += "<tr>\
                    <td class='pro-thumbnail'><a href='#'><img class='img-fluid' src='"+ value.pictureUrl + "' alt='Product' /></a></td>\
                    <td class='pro-title'><a href='#'>"+ value.productName + "</a></td>\
                    <td class='pro-price'><span>NGN "+ value.price + "</span></td>\
                    <td class='pro-quantity'>\
                        <div class='quantity'>\
                            <div class='cart-plus-minus'>\
                                <input class='cart-plus-minus-box' data-id='"+value.id+"' value='"+ value.quantity +"' type='text'>\
                                <div class='dec qtybutton'>-</div>\
                                <div class='inc qtybutton'>+</div>\
                                <div class='dec qtybutton'><i class='fa fa-minus'></i></div>\
                                <div class='inc qtybutton'><i class='fa fa-plus'></i></div>\
                            </div>\
                        </div>\
                    </td>\
                    <td class='pro-subtotal'>NGN<span>"+ value.price * value.quantity +"</span></td>\
                    <td class='pro-remove'><a href='#' class='remove-item' data-id='"+ value.id +"'><i class='ion-trash-b'></i></a></td>\
                </tr>"
            });
            cartTable.append(tr);
            let total = getTotal();
            $('.sub-total').text(total);
        }
        
    }

    loadCartsOnCartPage();

    /*----------------------------------------*/
    /*  Cart Plus Minus Button
    /*----------------------------------------*/
    $('.cart-plus-minus').append(
        '<div class="dec qtybutton"><i class="fa fa-minus"></i></div><div class="inc qtybutton"><i class="fa fa-plus"></i></div>'
    );
    $('#cart-table').on('click', '.qtybutton', function () {
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

    $('#update-cart').on('click', function (e) {
        e.preventDefault();
        var carts = JSON.parse(localStorage.getItem("cart"));
        
        $('#cart-table tr').each(function (i, row) {
            let $row = $(row);
            let $inputStage = $row.find('input');
            let qty = $inputStage.val();
            let _id = $inputStage.attr('data-id');

            for (var i in carts) {
                if (carts[i].id == _id) {
                    carts[i].quantity = qty;
                    break; //Stop this loop, we found it!
                }
            }
            //localStorage.setItem("cart", JSON.stringify(carts));
            //alert(qty)
            //alert(_id)

        });
        localStorage.setItem("cart", JSON.stringify(carts));
        loadCartsOnCartPage();
        cartCount();
        loadCartDropdown();
    });

    cartTable.on('click', '.remove-item', function (e) {
        let $this = $(this);
        let _id = $this.attr('data-id');
        deleteItemFromCart(_id);
        cartCount();
        loadCartDropdown();
        loadCartsOnCartPage();
    })

})