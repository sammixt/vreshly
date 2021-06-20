$(function () {
    let confirmPayment = $('#confirm-payment');
    let declinePayment = $('#decline-payment');
    let processDelivery = $('#process-delivery');
    let completeTransaction = $('#complete');

    confirmPayment.on('click', function (e) {
        e.preventDefault();
        var _id = $(this).attr("data-id");

        callendpoint(_id, 1);
    });

    declinePayment.on('click', function (e) {
        e.preventDefault();
        var _id = $(this).attr("data-id");
        callendpoint(_id, 3);
    });
    processDelivery.on('click', function (e) {
        e.preventDefault();
        var _id = $(this).attr("data-id");
        callendpoint(_id, 4);
    });
    completeTransaction.on('click', function (e) {
        e.preventDefault();
        var _id = $(this).attr("data-id");
        callendpoint(_id, 7);
    });

    var callendpoint = function (_id, _status) {
        axios.put(`${url}Orders/UpdateOrderStatus?id=${_id}&status=${_status}`)
            .then(response => {
                const addedUser = response.data;
                toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                    closeButton: true,
                    tapToDismiss: true,
                    rtl: false
                });
                setTimeout(function () {
                    window.location.reload();
                }, 2000);   
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
})
