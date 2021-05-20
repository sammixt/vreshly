﻿$(function () {
    var baseUrl = "https://localhost:5201/";
    var featuredProductDiv = $('#shop_wrapper');
    var categorySidebar = $('#sidebar-categories');
    var productSidebar = $('#sidebar-products');
	function loadProducts(_pageIndex, _pageSize) {
		$.ajax({
			type: 'GET',
			url: `${baseUrl}Product/GetProducts`,
			data: { pageIndex: _pageIndex, pageSize: _pageSize}
		}).done(function (response) {
            featuredProductDiv.empty();
            $('#pagination').empty();
            $('#pagination-content').empty();
			$.each(response.data, function (key, value) {

                featuredProductDiv.append("<div class='col-md-6 col-sm-6 col-lg-4 col-custom product-area'>\
                    <div class= 'single-product position-relative'>\
                    <div class='product-image'>\
                        <a class='d-block' href='product-details.html'>\
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
                                    <h4 class='title-2'> <a href='product-details.html'>"+ value.productName + "</a></h4>\
                                </div>\
                                <div class='price-box'>\
                                    <span class='regular-price '>NGN "+ value.discountPrice + "</span>\
									<span class='old-price'><del>NGN "+ value.sellingPrice + "</del></span>\
                                </div>\
                            </div>\
                            <div class='add-action d-flex position-absolute'>\
                                <a href='cart.html' title='Add To cart'>\
                                    <i class='ion-bag'></i>\
                                </a>\
                                <a href='compare.html' title='Compare'>\
                                    <i class='ion-ios-loop-strong'></i>\
                                </a>\
                                <a href='wishlist.html' title='Add To Wishlist'>\
                                    <i class='ion-ios-heart-outline'></i>\
                                </a>\
                                <a href='#exampleModalCenter' data-toggle='modal' title='Quick View'>\
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
                                    <h4 class='title-2'> <a href='product-details.html'>"+ value.productName + "</a></h4>\
                                </div>\
                                <div class='price-box'>\
                                    <span class='regular-price '>NGN "+ value.discountPrice + "</span>\
									<span class='old-price'><del>NGN "+ value.sellingPrice + "</del></span>\
                                </div>\
                                <div class='add-action-listview d-flex'>\
                                    <a href='cart.html' title='Add To cart'>\
                                        <i class='ion-bag'></i>\
                                    </a>\
                                    <a href='compare.html' title='Compare'>\
                                        <i class='ion-ios-loop-strong'></i>\
                                    </a>\
                                    <a href='wishlist.html' title='Add To Wishlist'>\
                                        <i class='ion-ios-heart-outline'></i>\
                                    </a>\
                                    <a href='#exampleModalCenter' data-toggle='modal' title='Quick View'>\
                                        <i class='ion-eye'></i>\
                                    </a>\
                                </div>\
                                <p class='desc-content'>"+ value.productSummary +"</p>\
                            </div>\
                        </div>\
                    </div>");
                
            });
            $('#pagination-content').text(`Showing ${_pageIndex} - ${(_pageSize * _pageIndex)} of ${response.count} result `)

            var totalpages = response.count / _pageSize;
            
            var li = "";
            li += "<li class='disabled prev'><i class='ion-ios-arrow-thin-left' ></i></li>"
            for (i = 1; i <= totalpages; i++) {
                li += `<li class='`+ (_pageIndex === i ? 'active':'') +`'><a data-index='${i}' data-size='${_pageSize}'>${i}</a></li>`
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

    $('#pagination').on('click', 'li a', function (e) {
        e.preventDefault();
        var $this = $(this);
       var _pageIndex = $this.attr('data-index');
        var _pageSize = $this.attr('data-size');

        //alert(_pageIndex);
        //alert(_pageSize);
        loadProducts(_pageIndex, _pageSize);
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
                                    <a href='product-details.html' class='image'>\
                                        <img src='"+ value.mainImage + "' alt='product'>\
                                    </a>\
                                    <div class='product-content'>\
                                        <div class='product-title'>\
                                            <h4 class='title-2'> <a href='product-details.html'>"+ value.productName + "</a></h4>\
                                        </div>\
                                        <div class='price-box'>\
                                            <span class='regular-price '>NGN "+ value.discountPrice + "</span>\
									        <span class='old-price'><del>NGN "+ value.sellingPrice + "</del></span>\
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

                categorySidebar.append("<li><a href='#'>" + value.categoryName+"</a></li>");
            });
            //setTimeout(, 3000);

            //sliderInit('featured-product-slider')

        }).fail(function (data) {
            // Make sure that the formMessages div has the 'error' class.
            console.log(data);
        });
    }

    loadProducts(1, 4);
    loadCategory(5);
    loadSidebarProduct('dateDesc', 1, 5);
});