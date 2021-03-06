
var featuredProductDiv = $('#shop_wrapper');
var categorySidebar = $('#sidebar-categories');
var productSidebar = $('#sidebar-products');
var brandSidebar = $('#sidebar-brands');
var click = 'click';

function loadProducts(_pageIndex, _pageSize, cart, sort, search) {
    var $data = {};
    $data = { pageIndex: _pageIndex, pageSize: _pageSize }


    if (cart > 0) { $data.categoryId = cart; }
    if (sort !== 'nil') { $data.sort = sort; }
    if (search !== 'nil') { $data.search = search; }

    $.ajax({
        type: 'GET',
        url: `${baseUrl}Product/GetProducts`,
        data: $data
    }).done(function (response) {
        featuredProductDiv.empty();
        $('#pagination').empty();
        $('#pagination-content').empty();
        $.each(response.data, function (key, value) {

            featuredProductDiv.append("<div class='col-md-6 col-sm-6 col-lg-4 col-custom product-area'>\
                    <div class= 'single-product position-relative'>\
                    <div class='product-image'>\
                        <a class='d-block' href='/Shop/ProductDetail?productId="+ value.id + "'>\
                            <img src='"+ value.mainImage + "' alt=''  class='product-image-1 w-100'>\
							<img src='"+ value.imageOne + "' alt=''  class='product-image-2 position-absolute w-100'>\
                                </a>\
                            </div>\
                            <div class='product-content'>\
                                <div class='product-rating'>\
                                    <i class='fa fa-star'></i>\
                                    <i class='fa fa-star'></i>\
                                    <i class='fa fa-star'></i>\
                                    <i class='fa fa-star-o'></i>\
                                    <i class='fa fa-star-o'></i>\
                                </div>\
                                <div class='product-title'>\
                                    <h4 class='title-2'> <a href='/Shop/ProductDetail?productId="+ value.id + "'>" + value.productName + "</a></h4>\
                                </div>\
                                <div class='price-box'>\
                                    <span class='regular-price '>₦ "+ value.discountPrice + "</span>\
									<span class='old-price'><del>₦ "+ value.sellingPrice + "</del></span>\
                                </div>\
                            </div>\
                            <div class='add-action d-flex position-absolute'>\
                                <a class='add-to-cart' data-id='"+ value.id +
                "' data-productName='" + value.productName +
                "' data-price='" + value.discountPrice +
                "' data-quantity='1' data-pictureUrl='" + value.mainImage +
                "' data-category='" + value.category +
                "' data-brand='" + value.brand + "' title = 'Add To cart' >\
                                    <i class='ion-bag'></i>\
                                </a>\
                                <a class='add-to-recurring' data-id='"+ value.id + "' data-product='" + value.productName+"' title='Recurring Order'>\
                                    <i class='ion-ios-loop-strong'></i>\
                                </a>\
                                <a class='add-to-wishlist' data-id='"+ value.id + "' title='Add To Wishlist'>\
                                    <i class='ion-ios-heart-outline'></i>\
                                </a>\
                                 <a  class='detail-view' data-id='"+ value.id + "' title='Quick View'>\
                                        <i class='ion-eye'></i>\
                                   </a>\
                            </div>\
                            <div class='product-content-listview'>\
                                <div class='product-rating'>\
                                    <i class='fa fa-star'></i>\
                                    <i class='fa fa-star'></i>\
                                    <i class='fa fa-star'></i>\
                                    <i class='fa fa-star-o'></i>\
                                    <i class='fa fa-star-o'></i>\
                                </div>\
                                <div class='product-title'>\
                                    <h4 class='title-2'> <a href='/Shop/ProductDetail?productId="+ value.id + "'>" + value.productName + "</a></h4>\
                                </div>\
                                <div class='price-box'>\
                                    <span class='regular-price '>₦ "+ value.discountPrice + "</span>\
									<span class='old-price'><del>₦ "+ value.sellingPrice + "</del></span>\
                                </div>\
                                <div class='add-action-listview d-flex'>\
                                     <a class='add-to-cart' data-id='"+ value.id +
                "' data-productName='" + value.productName +
                "' data-price='" + value.discountPrice +
                "' data-quantity='1' data-pictureUrl='" + value.mainImage +
                "' data-category='" + value.category +
                "' data-brand='" + value.brand + "' title = 'Add To cart' >\
                                            <i class='ion-bag'></i>\
                                        </a>\
                                    <a class='add-to-recurring' data-id='"+ value.id + "' data-product='" + value.productName +"' title='Recurring Order'>\
                                        <i class='ion-ios-loop-strong'></i>\
                                    </a>\
                                    <a class='add-to-wishlist' data-id='"+ value.id + "' title='Add To Wishlist'>\
                                        <i class='ion-ios-heart-outline'></i>\
                                    </a>\
                                    <a  class='detail-view' data-id='"+ value.id + "' title='Quick View'>\
                                        <i class='ion-eye'></i>\
                                    </a>\
                                </div>\
                                <p class='desc-content'>"+ value.productSummary + "</p>\
                            </div>\
                        </div>\
                    </div>");

        });
        $('#pagination-content').text(`Showing ${_pageIndex} - ${(_pageSize * _pageIndex)} of ${response.count} result `)

        var totalpages = response.count / _pageSize;

        var li = "";
        li += "<li class='disabled prev'><i class='ion-ios-arrow-thin-left' ></i></li>"
        for (i = 1; i <= totalpages; i++) {
            li += `<li class='` + (_pageIndex === i ? 'active' : '') + `'><a data-index='${i}' data-cart='${cart}'
                        data-size='${_pageSize}' data-sort='${sort}' data-search='${search}'>${i}</a></li>`
        }
        li += "<li class='next'><a href='#' title='Next >>'><i class='ion-ios-arrow-thin-right'></i></a ></li >";
        $('#pagination').append(li);

        //setTimeout(, 3000);

        //sliderInit('featured-product-slider')

    }).fail(function (data) {
        // Make sure that the formMessages div has the 'error' class.
        console.log(data);
    });
}
$(function () {

    $('#pagination').on('click', 'li a', function (e) {
        e.preventDefault();
        var $this = $(this);
       var _pageIndex = $this.attr('data-index');
        var _pageSize = $this.attr('data-size');
        var cart = parseInt($this.attr('data-cart'));

        //alert(_pageIndex);
        //alert(_pageSize);
        loadProducts(_pageIndex, _pageSize, cart);
    })

    featuredProductDiv.on('click', '.detail-view', function (e) {
        e.preventDefault();
        var $this = $(this);
        var _id = $this.attr('data-id');
        $.ajax({
            type: 'GET',
            url: `${baseUrl}Product/GetProduct/${_id}`
        }).done(function (response) {
            $('#exampleModalCenter .modal-body').html("<div class='container-fluid'>\
                    <div class='row'>\
                        <div class='col-lg-6 col-md-6 text-center'>\
                            <div class='product-image'>\
                                <img src='"+ response.mainImage +"' alt='Product Image'>\
                            </div>\
                        </div>\
                        <div class='col-lg-6 col-md-6'>\
                            <div class='modal-product'>\
                                <div class='product-content'>\
                                    <div class='product-title'>\
                                        <h4 class='title'>"+ response.productName +"</h4>\
                                    </div>\
                                    <div class='price-box'>\
                                         <span class='regular-price '>₦ "+ response.discountPrice + "</span>\
									     <span class='old-price'><del>₦ "+ response.sellingPrice + "</del></span>\
                                    </div>\
                                    <div class='product-rating'>\
                                        <i class='fa fa-star'></i>\
                                        <i class='fa fa-star'></i>\
                                        <i class='fa fa-star'></i>\
                                        <i class='fa fa-star-o'></i>\
                                        <i class='fa fa-star-o'></i>\
                                        <span>1 Review</span>\
                                    </div>\
                                    <p class='desc-content'>"+ response.productDescription+"</p>\
                                    <div class='quantity-with_btn'>\
                                        <div class='quantity'>\
                                            <div class='cart-plus-minus'>\
                                                <input class='cart-plus-minus-box' value='0' type='text'>\
                                                <div class='dec qtybutton'>-</div>\
                                                <div class='inc qtybutton'>+</div>\
                                            </div>\
                                        </div>\
                                        <div class='add-to_cart'>\
                                             <a class='add-to-cart-modal btn obrien-button primary-btn' data-id='"+ response.id +
                                                "' data-productName='" + response.productName +
                                                "' data-price='" + response.discountPrice +
                                                "' data-quantity='1' data-pictureUrl='" + response.mainImage +
                                                "' data-category='" + response.category +
                                                "' data-brand='" + response.brand + "' title = 'Add To cart' >\
                                                            Add To cart</a>\
                                        </div>\
                                    </div>\
                                </div>\
                            </div>\
                        </div>\
                    </div>\
                </div>");
            $('#exampleModalCenter').modal({
                show: true
            })
        }).fail(function (data) {
            // Make sure that the formMessages div has the 'error' class.
            console.log(data);
        });
        //alert(_id);
    })

    //function loadProduct(_pageIndex, _pageSize) {
    //    alert(_pageIndex);
    //    alert(_pageSize);
    //}

    function loadSidebarProduct(_sort,_pageIndex, _pageSize) {
        $.ajax({
            type: 'GET',
            url: `${baseUrl}Product/GetProducts`,
            data: { sort: _sort, pageIndex: _pageIndex, pageSize: _pageSize }
        }).done(function (response) {

            $.each(response.data, function (key, value) {

                productSidebar.append("<div class='sidebar-product align-items-center'>\
                                    <a href='/Shop/ProductDetail?productId="+ value.id +"' class='image'>\
                                        <img src='"+ value.mainImage + "' alt='product'>\
                                    </a>\
                                    <div class='product-content'>\
                                        <div class='product-title'>\
                                            <h4 class='title-2'> <a href='/Shop/ProductDetail?productId="+ value.id +"'>"+ value.productName + "</a></h4>\
                                        </div>\
                                        <div class='price-box'>\
                                            <span class='regular-price '>₦ "+ value.discountPrice + "</span>\
									        <span class='old-price'><del>₦ "+ value.sellingPrice + "</del></span>\
                                        </div>\
                                        <div class='product-rating'>\
                                            <i class='fa fa-star'></i>\
                                            <i class='fa fa-star'></i>\
                                            <i class='fa fa-star'></i>\
                                            <i class='fa fa-star-o'></i>\
                                            <i class='fa fa-star-o'></i>\
                                        </div>\
                                    </div>\
                                </div>");
            });
            //setTimeout(, 3000);

            //sliderInit('featured-product-slider')

        }).fail(function (data) {
            // Make sure that the formMessages div has the 'error' class.
            console.log(data);
        });
    }
    function loadCategory(_take)
    {
        $.ajax({
            type: 'GET',
            url: `${baseUrl}Category/GetIndexedCategories`,
            data: { take: _take}
        }).done(function (response) {

            $.each(response, function (key, value) {

                categorySidebar.append("<li><a class='loadcart' href='#' data-cart='"+value.id+"'>" + value.categoryName+"</a></li>");
            });
            //setTimeout(, 3000);

            //sliderInit('featured-product-slider')

        }).fail(function (data) {
            // Make sure that the formMessages div has the 'error' class.
            console.log(data);
        });
    }

    function loadBrand(_take) {
        $.ajax({
            type: 'GET',
            url: `${baseUrl}Brand/GetIndexBrands`,
            data: { take: _take }
        }).done(function (response) {

            $.each(response, function (key, value) {

                brandSidebar.append("<li><a href='#'>" + value.brandName + "</a></li>");
            });
            //setTimeout(, 3000);

            //sliderInit('featured-product-slider')

        }).fail(function (data) {
            // Make sure that the formMessages div has the 'error' class.
            console.log(data);
        });
    }

    categorySidebar.on(click, '.loadcart', function (e) {
        e.preventDefault();
        var $this = $(this);
        let category = parseInt($this.attr('data-cart'));
        
        loadProducts(1, 4, category, 'nil', 'nil');
    });
    $('#search-btn').on(click, function (e) {
        e.preventDefault();
        var searchParam = $(this).parent().parent().find('input').val();
        if (searchParam.length > 1) {
            loadProducts(1, 4, 0, 'nil', searchParam);
        } else {
            alert("Enter Product Name");
        }

    })

    $(".nice-select").on('change', function (e) {
        e.preventDefault();
        var sort = $(this).val();
        loadProducts(1, 4, 0, sort, 'nil');
    });


    $('#exampleModalCenter .modal-body').on('click', '.qtybutton', function () {
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

    if (window.location.hash == undefined) {
        loadProducts(1, 4, 0, 'nil', 'nil');
    } else {
        var _hash = window.location.hash;
        var hashArray = _hash.split('$');
        loadProducts(1, 4, hashArray[2], 'nil', 'nil');
    }
    
    loadCategory(5);
    loadSidebarProduct('dateDesc', 1, 5);
    loadBrand(5);
});