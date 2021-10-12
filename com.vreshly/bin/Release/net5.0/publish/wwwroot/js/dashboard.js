$(function () {
    let totalSales = $('.total-sales');
    let totalCustomers = $('#total-customers');
    let totalProducts = $('#total-products');
    let totalRevenue = $('.total-revenue');
    let monthlyRevenue = $('#months-profit');
    let newOrdersTable = $('#new-orders');
    let recurringTable = $('#recurring-table');
    var loadDashboard = function () {
        axios.get(`${url}Dashboard/GetDashboardData`)
            .then(response => {
                const details = response.data;
                totalSales.text(details.sales);
                totalCustomers.text(details.customers);
                totalProducts.text(details.products);
                totalRevenue.text(`₦${details.revenues}`);
                monthlyRevenue.text(`₦${details.revenuePerMonth}`);
            })
            .catch(function (error) {
                if (error.response) {
                    toastr['error'](error.response.data.message, 'Error!', {
                        closeButton: true,
                        tapToDismiss: false,
                        rtl: false
                    });
                }

            });
    }

    loadDashboard();

    var getNewOrders = function () {
        axios.get(`${url}Dashboard/GetNewOrders`)
            .then(response => {
                const orders = response.data;
                $.each(orders, function (key, order) {
                    newOrdersTable.append(`
                        <tr>
                            <td>${order.paymentMethod}</td>
                            <td>${order.subtotal}</td>
                            <td>${order.shippingPrice}</td>
                            <td>${order.total}</td>
                            <td>${order.orderDateFormated}</td>
                            <td>
                                <a  href='/AdminOrder/OrderDetails?pi=${order.paymentIntentId}'>
                                    <svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-file-text mr-50 font-small-4'>
                                        <path d='M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z'></path>
                                        <polyline points='14 2 14 8 20 8'></polyline>
                                        <line x1='16' y1='13' x2='8' y2='13'></line>
                                        <line x1='16' y1='17' x2='8' y2='17'></line>
                                        <polyline points='10 9 9 9 8 9'></polyline>
                                    </svg>Details
                                </a>
                            </td>
                        </tr>
                    `);
                })
            })
            .catch(function (error) {
                if (error.response) {
                    toastr['error'](error.response.data.message, 'Error!', {
                        closeButton: true,
                        tapToDismiss: false,
                        rtl: false
                    });
                }

            });
    }

    var getRecurringOrdersForUser = function () {

        axios.get(`${url}Dashboard/GetReccurringOrdersDueInFiveDays`)
            .then(response => {
                const orders = response.data;
                $.each(orders, function (key, value) {
                    recurringTable.append(
                        `
                    <tr>
                        <td>
                            
                            <div class="d-flex align-items-center">
                                <div class="avatar rounded">
                                    <div class="avatar-content">
                                        <img src="${value.product.mainImage}" height="60px" width="60px" alt="Speaker svg" />
                                    </div>
                                </div>
                                <div>
                                    <div class="font-weight-bolder">${value.product.productName}</div>
                                    <div class="font-small-2 text-muted">Quantity : ${value.quantity}</div>
                                    <div class="font-small-2 text-muted">Price : ₦${value.product.discountPrice}</div>
                                </div>
                            </div>
                        </td>
                        <td>${value.userEmail}</td>
                        <td>${value.nextDeliveryDateString}</td>
                        <td>${value.previousDeliveryDate}</td>
                        <td>
                            <a  href='/AdminOrder/OrderDetails?pi=${value.id}'>
                                <svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-file-text mr-50 font-small-4'>
                                    <path d='M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z'></path>
                                    <polyline points='14 2 14 8 20 8'></polyline>
                                    <line x1='16' y1='13' x2='8' y2='13'></line>
                                    <line x1='16' y1='17' x2='8' y2='17'></line>
                                    <polyline points='10 9 9 9 8 9'></polyline>
                                </svg>Details
                            </a>
                        </td>
                    </tr>
                    `)
                })
            })
            .catch(function (error) {
                if (error.response) {
                    toastr['error'](error.response.data.message, 'Error!', {
                        closeButton: true,
                        tapToDismiss: false,
                        rtl: false
                    });
                }

            })

    }

    getNewOrders();
    getRecurringOrdersForUser();
})