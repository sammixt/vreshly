$(function () {
    let processOrder = $('#recurring-items');
    

    processOrder.on('click','.process-item', function (e) {
        e.preventDefault();
        var _id = $(this).attr("data-id");
        var _parentDiv = $(this).parent().find('select');
        var optionSelected = _parentDiv.val();
        if (parseInt(optionSelected) == 0) {
            toastr['error']('Delivery method is required', 'Error!', {
                closeButton: true,
                tapToDismiss: false,
                rtl: false
            });
        } else {
            callendpoint(_id, parseInt(optionSelected));
        }
        
    });

    processOrder.on('change', '.delivery-method', function (e) {
        e.preventDefault();
        var _price = $('option:selected', this).attr('data-price');
        var _parentDiv = $(this).parent().find('h4');
        var h4price = _parentDiv.attr('data-price');

        var curPrice = parseInt(_price) + parseInt(h4price);
        _parentDiv.text(`₦${curPrice}`);
        

    });

    

    var callendpoint = function (_id,delivery) {
        axios.put(`${url}AdminRecurring/ProcessOrder?id=${_id}&delivery=${delivery}`)
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

    var getDeliveryMethods = function () {

        axios.get(`${url}Orders/GetDeliveryMethod`)
            .then(response => {
                const res = response.data;
                $.each(res, function (key, value) {
                    $('.delivery-method').append(`<option value='${value.id}' data-price='${value.price}'>${value.shortName} -- ${value.deliveryTime}</option>`)
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

    getDeliveryMethods();
})
