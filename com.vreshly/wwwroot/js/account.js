$(function () {
    let orderHistoryTable = $('#order-history');
    var authItem = JSON.parse(localStorage.getItem("auth"));
    if (authItem == null) { window.location.href = "/Home/Index"; }

    $('#name').text(authItem.displayName);

    var getOrdersForUser = function () {

        $.ajax({
            type: 'GET',
            url: `${baseUrl}Orders/GetOrdersForUser`,
            headers: {
                Authorization: 'Bearer ' + authItem.token
            }
        }).done(function (response) {
            let counter = 1;
            $.each(response, function (key, value) {
                orderHistoryTable.append(
                    `<tr>\
                        <td>${counter++}</td>\
                        <td>${value.orderDate}</td>\
                        <td>${value.status}</td>\
                        <td>NGN ${value.total}</td>\
                        <td><a href='javascript:void()' data-id='${value.id}' class='btn obrien-button-2 primary-color rounded-0 details'>View</a></td>\
                    </tr>`
                )
            })
        }).fail(function (err) {
            console.log(err);
        })
    };

    getOrdersForUser();

    orderHistoryTable.on('click', '.details', function(){
        var _this = $(this);
        var orderId = _this.attr('data-id');

        $.ajax({
            type: 'GET',
            url: `${baseUrl}Orders/GetOrderByIdForUser?id=${orderId}`,
            headers: {
                Authorization: 'Bearer ' + authItem.token
            } 
        }).done(function (response) {
            $('#items').empty();
            $('.order-id').text(response.paymentIntentId);
            $('.total-items').text(response.totalItems);
            $('.order-date').text(response.orderDateFormated);
            $('.sub-total').text(response.total);
            $('.payment-method').html(response.paymentMethod);
            $('.item-total').text(response.subtotal);
            $('.shipping-fee').text(response.shippingPrice);
            $('.delivery-method').text(response.deliveryMethod);
            $('.delivery-address').html(`${response.shipToAddress.street} <br/> 
            ${response.shipToAddress.city}, ${response.shipToAddress.state}<br/>
            ${response.shipToAddress.zipCode} <br/>
            Tel. : ${response.shipToAddress.phoneNumber}`);
            
            $.each(response.orderItems, function(key,value){
                $('#items').append(
                    `
                    <article style='margin-bottom:3px'>\
                        <div class='card'>\
                            <div>\
                                <header style='text-align: left; padding: 8px 4px  3px;'>\
                                    <badge class='tag' style='background-color: #6dbd28;color:#fff;' >${response.status}</badge>\
                                    <p class='-m -mtxs'>On <span class=''>${response.orderDateFormated}</span></p>\
                                </header>\
                                <div class='-df -pvs __web-inspector-hide-shortcut__' style='display: flex; flex-direction: row; margin-top: 8px;'>\ 
                                        <a class='-dif -fsh0 -fs0 -pr'><img data-src='${value.pictureUrl}' src='${value.pictureUrl}' width='104' height='104' style='border-radius: 4px' alt='${value.productName}'></a>\
                                        <div class='-df -d-co -oh -plm' style='text-align: left; margin: 8px 0 8px 0;'>${value.productName}<br/><br/>\
                                        <div class='-mta -mba -ptxs' style='text-align: left; margin-bottom: 8px;'><p><span class='-gy5'>QTY:</span>${value.quantity}</p>\
                                        </div><p class='-ptxs -fs16' style='text-align: left;;'><span class='-gy5 -lthr -mls -ltr -tal'>₦ ${value.price}</span></p>\
                                    </div>\
                                </div>\
                            </div>\
                        </div>\
                    </article>
                    `)
            })
            $('#order-list').css('display','none');
            $('#order-details').css('display','block');
        }).fail(function (data) {
           
        });
    })

})