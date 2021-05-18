$(function () {
	/*---------------------------------------*/
	/* Load Featured Products
	 * --------------------------------------*/
	var baseUrl = "https://localhost:5201/";
	var featuredProductDiv = $('#featured-product-slider');
	var bestSellerProductDiv = $('#best-seller-slider');
	function loadFeaturedProducts() {
		$.ajax({
			type: 'GET',
			url: `${baseUrl}Product/GetFeaturedProducts`,
		}).done(function (response) {
			console.log(response);
			$.each(response, function (key, value) {

				featuredProductDiv.append("<div class='single-item'>\
					<div class='single-product position-relative mb-30'>\
						<div class='product-image'>\
							<a class='d-block' href='product-details.html'>\
								<img src='"+ value.mainImage + "' alt='' style='width:480px;height:480px' class='product-image-1 w-100'>\
									<img src='"+ value.imageOne + "' alt='' style='width:480px;height:480px' class='product-image-2 position-absolute w-100'>\
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
										<h4 class='title-2'> <a href='product-details.html'>"+ value.productName+"</a></h4>\
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
                                </div>\
							<div class='single-product position-relative'>\
								<div class='product-image'>\
									<a class='d-block' href='product-details.html'>\
										<img src='"+ value.imageTwo + "' alt='' style='width:480px;height:480px' class='product-image-1 w-100'>\
											<img src='"+ value.imageThree + "' alt='' style='width:480px;height:480px' class='product-image-2 position-absolute w-100'>\
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
												<h4 class='title-2'> <a href='product-details.html'>"+ value.productName+"</a></h4>\
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

	function loadBestSellersProducts() {
		$.ajax({
			type: 'GET',
			url: `${baseUrl}Product/GetBestSellers`,
		}).done(function (response) {
			console.log(response);
			$.each(response, function (key, value) {

				bestSellerProductDiv.append("<div class='single-item'>\
					<div class='single-product position-relative mb-30'>\
						<div class='product-image'>\
							<a class='d-block' href='product-details.html'>\
								<img src='"+ value.mainImage + "' alt='' style='width:480px;height:480px' class='product-image-1 w-100'>\
									<img src='"+ value.imageOne + "' alt='' style='width:480px;height:480px' class='product-image-2 position-absolute w-100'>\
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
                                </div>\
							<div class='single-product position-relative'>\
								<div class='product-image'>\
									<a class='d-block' href='product-details.html'>\
										<img src='"+ value.imageTwo + "' alt='' style='width:480px;height:480px' class='product-image-1 w-100'>\
											<img src='"+ value.imageThree + "' alt='' style='width:480px;height:480px' class='product-image-2 position-absolute w-100'>\
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
                                </div>\
								</div>");

			});
			//setTimeout(, 3000);
			//sliderInit('best-seller-slider')


		}).fail(function (data) {
			// Make sure that the formMessages div has the 'error' class.
			console.log(data);
		});
	}

	function loadProducts() {
		loadFeaturedProducts();
		loadBestSellersProducts();
		sliderInit('best-seller-slider')
		//setTimeout(sliderInit(), 3000);
    }

	window.onload = loadProducts();
	function sliderInit(sliderId) {
		$(`.product-wrapper .product-slider`).not('.slick-initialized').slick({
			"slidesToShow": 4,
			"slidesToScroll": 1,
			"infinite": true,
			"arrows": false,
			"dots": false,
			"responsive": [
				{
					"breakpoint": 1200, "settings": {
						"slidesToShow": 3
					}
				},
				{
					"breakpoint": 992, "settings": {
						"slidesToShow": 2
					}
				},
				{
					"breakpoint": 576, "settings": {
						"slidesToShow": 1
					}
				}
			]
		});

		//$('#best-seller-slider').not('.slick-initialized').slick({
		//	"slidesToShow": 4,
		//	"slidesToScroll": 1,
		//	"infinite": true,
		//	"arrows": false,
		//	"dots": false,
		//	"responsive": [
		//		{
		//			"breakpoint": 1200, "settings": {
		//				"slidesToShow": 3
		//			}
		//		},
		//		{
		//			"breakpoint": 992, "settings": {
		//				"slidesToShow": 2
		//			}
		//		},
		//		{
		//			"breakpoint": 576, "settings": {
		//				"slidesToShow": 1
		//			}
		//		}
		//	]
		//});

		var $html = $('html');
		var $body = $('body');
		var $elementCarousel = $('.obrien-slider, .product-slider');
		// Check if element exists
		$.fn.elExists = function () {
			return this.length > 0;
		};

		/*For RTL*/
		if ($html.attr('dir') == 'rtl' || $body.attr('dir') == 'rtl') {
			$elementCarousel.attr('dir', 'rtl');
		}
		if ($elementCarousel.elExists()) {
			var slickInstances = [];

			/*For RTL*/
			if ($html.attr('dir') == 'rtl' || $body.attr('dir') == 'rtl') {
				$elementCarousel.attr('dir', 'rtl');
			}

			$elementCarousel.each(function (index, element) {
				var $this = $(this);

				// Carousel Options

				var $options = typeof $this.data('slick-options') !== 'undefined' ? $this.data('slick-options') : '';

				var $spaceBetween = $options.spaceBetween ? parseInt($options.spaceBetween, 10) : 0,
					$spaceBetween_xl = $options.spaceBetween_xl ? parseInt($options.spaceBetween_xl, 10) : 0,
					$rowSpace = $options.rowSpace ? parseInt($options.rowSpace, 10) : 0,
					$rows = $options.rows ? $options.rows : false,
					$vertical = $options.vertical ? $options.vertical : false,
					$focusOnSelect = $options.focusOnSelect ? $options.focusOnSelect : false,
					$pauseOnHover = $options.pauseOnHover ? $options.pauseOnHover : false,
					$pauseOnFocus = $options.pauseOnFocus ? $options.pauseOnFocus : false,
					$asNavFor = $options.asNavFor ? $options.asNavFor : '',
					$fade = $options.fade ? $options.fade : false,
					$autoplay = $options.autoplay ? $options.autoplay : false,
					$autoplaySpeed = $options.autoplaySpeed ? parseInt($options.autoplaySpeed, 10) : 5000,
					$swipe = $options.swipe ? $options.swipe : true,
					$swipeToSlide = $options.swipeToSlide ? $options.swipeToSlide : true,
					$touchMove = $options.touchMove ? $options.touchMove : false,
					$verticalSwiping = $options.verticalSwiping ? $options.verticalSwiping : true,
					$draggable = $options.draggable ? $options.draggable : true,
					$arrows = $options.arrows ? $options.arrows : false,
					$dots = $options.dots ? $options.dots : false,
					$adaptiveHeight = $options.adaptiveHeight ? $options.adaptiveHeight : true,
					$infinite = $options.infinite ? $options.infinite : false,
					$centerMode = $options.centerMode ? $options.centerMode : false,
					$centerPadding = $options.centerPadding ? $options.centerPadding : '',
					$variableWidth = $options.variableWidth ? $options.variableWidth : false,
					$speed = $options.speed ? parseInt($options.speed, 10) : 500,
					$appendArrows = $options.appendArrows ? $options.appendArrows : $this,
					$prevArrow =
						$arrows === true ?
							$options.prevArrow ?
								'<span class="' +
								$options.prevArrow.buttonClass +
								'"><i class="' +
								$options.prevArrow.iconClass +
								'"></i></span>' :
								'<button class="tty-slick-text-btn tty-slick-text-prev"><i class="ion-chevron-left"></i></span>' :
							'',
					$nextArrow =
						$arrows === true ?
							$options.nextArrow ?
								'<span class="' +
								$options.nextArrow.buttonClass +
								'"><i class="' +
								$options.nextArrow.iconClass +
								'"></i></span>' :
								'<button class="tty-slick-text-btn tty-slick-text-next"><i class="ion-chevron-right"></i></span>' :
							'',
					$rows = $options.rows ? parseInt($options.rows, 10) : 1,
					$rtl = $options.rtl || $html.attr('dir="rtl"') || $body.attr('dir="rtl"') ? true : false,
					$slidesToShow = $options.slidesToShow ? parseInt($options.slidesToShow, 10) : 1,
					$slidesToScroll = $options.slidesToScroll ? parseInt($options.slidesToScroll, 10) : 1;

				/*Responsive Variable, Array & Loops*/
				var $responsiveSetting =
					typeof $this.data('slick-responsive') !== 'undefined' ? $this.data('slick-responsive') : '',
					$responsiveSettingLength = $responsiveSetting.length,
					$responsiveArray = [];
				for (var i = 0; i < $responsiveSettingLength; i++) {
					$responsiveArray[i] = $responsiveSetting[i];
				}

				// Adding Class to instances
				$this.addClass('slick-carousel-' + index);
				$this.parent().find('.slick-dots').addClass('dots-' + index);
				$this.parent().find('.slick-btn').addClass('btn-' + index);

				if ($spaceBetween != 0) {
					$this.addClass('slick-gutter-' + $spaceBetween);
				}
				if ($spaceBetween_xl != 0) {
					$this.addClass('slick-gutter-xl-' + $spaceBetween_xl);
				}
				var $slideCount = null;
				$this.on('init', function (event, slick) {
					$this.find('.slick-active').first().addClass('first-active');
					$this.find('.slick-active').last().addClass('last-active');
					$slideCount = slick.slideCount;
					if ($slideCount <= $slidesToShow) {
						$this.children('.slick-dots').hide();
					}
					var $firstAnimatingElements = $('.slick-slide').find('[data-animation]');
					doAnimations($firstAnimatingElements);
				});

				$this.slick({
					slidesToShow: $slidesToShow,
					slidesToScroll: $slidesToScroll,
					asNavFor: $asNavFor,
					autoplay: $autoplay,
					autoplaySpeed: $autoplaySpeed,
					speed: $speed,
					infinite: $infinite,
					rows: $rows,
					arrows: $arrows,
					dots: $dots,
					adaptiveHeight: $adaptiveHeight,
					vertical: $vertical,
					focusOnSelect: $focusOnSelect,
					pauseOnHover: $pauseOnHover,
					pauseOnFocus: $pauseOnFocus,
					centerMode: $centerMode,
					centerPadding: $centerPadding,
					variableWidth: $variableWidth,
					swipe: $swipe,
					swipeToSlide: $swipeToSlide,
					touchMove: $touchMove,
					draggable: $draggable,
					fade: $fade,
					appendArrows: $appendArrows,
					prevArrow: $prevArrow,
					nextArrow: $nextArrow,
					rtl: $rtl,
					customPaging: function (slider, i) {
						var thumb = $(slider.$slides[i]).data();
						var number = i + 1;
						if (number < 10) {
							return '<button type="button" class="dot">' + '0' + number + '</button>';
						}
						return '<button type="button" class="dot">' + number + '</button>';
					},
					responsive: $responsiveArray
				});

				$this.on('beforeChange', function (e, slick, currentSlide, nextSlide) {
					$this.find('.slick-active').first().removeClass('first-active');
					$this.find('.slick-active').last().removeClass('last-active');
					var $animatingElements = $('.slick-slide[data-slick-index="' + nextSlide + '"]').find(
						'[data-animation]'
					);
					doAnimations($animatingElements);
				});

				function doAnimations(elements) {
					var animationEndEvents = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
					elements.each(function () {
						var $el = $(this);
						var $animationDelay = $el.data('delay');
						var $animationDuration = $el.data('duration');
						var $animationType = 'animated ' + $el.data('animation');
						$el.css({
							'animation-delay': $animationDelay,
							'animation-duration': $animationDuration
						});
						$el.addClass($animationType).one(animationEndEvents, function () {
							$el.removeClass($animationType);
						});
					});
				}

				$this.on('afterChange', function (e, slick) {
					$this.find('.slick-active').first().addClass('first-active');
					$this.find('.slick-active').last().addClass('last-active');
				});

				// Updating the sliders in tab
				$('body').on('shown.bs.tab', 'a[data-toggle="tab"], a[data-toggle="pill"]', function (e) {
					$this.slick('setPosition');
				});
			});
			// Added mousewheel for specific slider
			$('.single-blog_slider, .mousewheel-slider').on('wheel', function (e) {
				e.preventDefault();

				if (e.originalEvent.deltaY < 0) {
					$(this).slick('slickNext');
				} else {
					$(this).slick('slickPrev');
				}
			});
		};
	}


	//sliderInit();

});
