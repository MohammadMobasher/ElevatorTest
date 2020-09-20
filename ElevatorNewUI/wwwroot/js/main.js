$(document).ready(function () { function e() { $(".slide-progress").css({ width: "100%", transition: "width 5000ms" }) } var i, t; $("li.nav-overlay").hover(function () { $(".mega-menu-level-two").removeClass("active"), $(".nav-categories-overlay").addClass("active") }, function () { $(".nav-categories-overlay").removeClass("active") }), $(".dropdown-toggle").on("click", function (e) { e.stopPropagation(), e.preventDefault(); var t = $(this); if (t.is(".disabled, :disabled")) return !1; t.parent().toggleClass("open") }), $(document).on("click", function (e) { $(".dropdown").hasClass("open") && $(".dropdown").removeClass("open") }), $(".nav-btn.nav-slider").on("click", function () { $(".overlay").show(), $("nav").toggleClass("open") }), $(".overlay").on("click", function () { $("nav").hasClass("open") && $("nav").removeClass("open"), $(this).hide() }), $("li.active").addClass("open").children("ul").show(), $("li.has-sub > a").on("click", function () { $(this).removeAttr("href"); var e = $(this).parent("li"); e.hasClass("open") ? (e.removeClass("open"), e.find("li").removeClass("opne"), e.find("ul").slideUp(200)) : (e.addClass("open"), e.children("ul").slideDown(200), e.siblings("li").children("ul").slideUp(200), e.siblings("li").removeClass("open"), e.siblings("li").find("li").removeClass("open"), e.siblings("li").find("ul").slideUp(200)) }), $(".product-carousel").owlCarousel({ rtl: !0, margin: 10, nav: !0, navText: ['<i class="fa fa-angle-right"></i>', '<i class="fa fa-angle-left"></i>'], dots: !1, responsiveClass: !0, responsive: { 0: { items: 1, slideBy: 1 }, 576: { items: 1, slideBy: 1 }, 768: { items: 3, slideBy: 2 }, 992: { items: 4, slideBy: 2 }, 1400: { items: 4, slideBy: 3 } } }), $(".product-carousel-brand").owlCarousel({ items: 4, rtl: !0, margin: 10, nav: !0, navText: ['<i class="fa fa-angle-right"></i>', '<i class="fa fa-angle-left"></i>'], dots: !1, responsiveClass: !0, responsive: { 0: { items: 1, slideBy: 1 }, 576: { items: 1, slideBy: 1 }, 768: { items: 3, slideBy: 2 }, 992: { items: 5, slideBy: 2 }, 1400: { items: 5, slideBy: 3 } } }), $(".product-carousel-symbol").owlCarousel({ rtl: !0, items: 2, loop: !0, margin: 10, dots: !1, autoplay: !0, autoplayTimeout: 3e3, autoplayHoverPause: !0, responsiveClass: !0, responsive: { 0: { items: 1, slideBy: 1, autoplay: !0 }, 576: { items: 1, slideBy: 1, autoplay: !0 }, 768: { items: 1, slideBy: 1, autoplay: !0 }, 992: { items: 1, slideBy: 1, autoplay: !0 }, 1400: { items: 1, slideBy: 1, autoplay: !0 } } }), $("#suggestion-slider").owlCarousel({ rtl: !0, items: 1, autoplay: !0, autoplayTimeout: 5e3, loop: !0, dots: !1, onInitialized: e, onTranslate: function () { $(".slide-progress").css({ width: 0, transition: "width 0s" }) }, onTranslated: e }), $(".sticky-sidebar").length && $(".sticky-sidebar").theiaStickySidebar(), i = jQuery, t = { init: function () { t.countDown() }, countDown: function (e, s) { i(".countdown").each(function () { var e = i(this), t = i(this).data("date-time"), n = i(this).data("labels"); (s || e).countdown(t, function (e) { i(this).html(e.strftime('<div class="countdown-item"><div class="countdown-value">%D</div><div class="countdown-label">' + n["label-day"] + '</div></div><div class="countdown-item"><div class="countdown-value">%H</div><div class="countdown-label">' + n["label-hour"] + '</div></div><div class="countdown-item"><div class="countdown-value">%M</div><div class="countdown-label">' + n["label-minute"] + '</div></div><div class="countdown-item"><div class="countdown-value">%S</div><div class="countdown-label">' + n["label-second"] + "</div></div>")) }) }) } }, i(function () { t.init() }); var n = (new Date).getFullYear() + 1; $("#countdown").countdown({ year: n }), $(".showcoupon").on("click", function () { $(".checkout-coupon").slideToggle(200) }), $("ul.gallery-actions li .add-product-wishes").on("click", function () { $(this).toggleClass("active") }), $(".custom-select-ui").length && $(".custom-select-ui select").niceSelect(); var s, o = document.getElementById("slider-non-linear-step"); $("#slider-non-linear-step").length && (noUiSlider.create(o, { start: [0, 5e6], connect: !0, direction: "rtl", format: wNumb({ decimals: 0, thousand: "," }), range: { min: [0], "10%": [500, 500], "50%": [4e4, 1e3], max: [1e7] } }), s = document.getElementById("slider-non-linear-step-value"), o.noUiSlider.on("update", function (e) { s.innerHTML = e.join(" - ") })), jQuery('<div class="quantity-nav"><div class="quantity-button quantity-up">+</div><div class="quantity-button quantity-down">-</div></div>').insertAfter(".quantity input"), jQuery(".quantity").each(function () { var n = jQuery(this), s = n.find('input[type="number"]'), e = n.find(".quantity-up"), t = n.find(".quantity-down"), i = s.attr("min"), o = s.attr("max"); e.click(function () { var e, t = parseFloat(s.val()); e = o <= t ? t : t + 1, n.find("input").val(e), n.find("input").trigger("change") }), t.click(function () { var e, t = parseFloat(s.val()); e = t <= i ? t : t - 1, n.find("input").val(e), n.find("input").trigger("change") }) }); var a = document.querySelector(".progress-wrap path"), l = a.getTotalLength(); a.style.transition = a.style.WebkitTransition = "none", a.style.strokeDasharray = l + " " + l, a.style.strokeDashoffset = l, a.getBoundingClientRect(), a.style.transition = a.style.WebkitTransition = "stroke-dashoffset 10ms linear"; function r() { var e = $(window).scrollTop(), t = $(document).height() - $(window).height(), n = l - e * l / t; a.style.strokeDashoffset = n } r(), $(window).scroll(r); var d; jQuery(window).on("scroll", function () { 50 < jQuery(this).scrollTop() ? jQuery(".progress-wrap").addClass("active-progress") : jQuery(".progress-wrap").removeClass("active-progress") }), jQuery(".progress-wrap").on("click", function (e) { return e.preventDefault(), jQuery("html, body").animate({ scrollTop: 0 }, 1500), !1 }), $("#countdown-verify-end").length && (d = $("#countdown-verify-end")).countdown({ date: (new Date).getTime() + 18e4, text: '<span class="day">%s</span><span class="hour">%s</span><span>: %s</span><span>%s</span>', end: function () { d.html("<a href='' class='link-border-verify form-account-link'>ارسال مجدد</a>") } }), $(".line-number-account").keyup(function () { $(this).next().focus() }), $(".mask-handler").click(function (e) { e.preventDefault(); var t = $(this).parents(".content-expert-summary"); t.find(".mask-text").toggleClass("active"), t.find(".shadow-box").fadeToggle(0), $(this).find(".show-more").fadeToggle(0), $(this).find(".show-less").fadeToggle(0) }), $(".content-expert-button").click(function (e) { e.preventDefault(); var t = $(this).parents(".content-expert-article"); t.find(".content-expert-article").toggleClass("active"), t.find(".content-expert-text").slideToggle(), $(this).find(".show-more").fadeToggle(0), $(this).find(".show-less").fadeToggle(0) }), $("#gallery-slider").owlCarousel({ rtl: !0, margin: 10, nav: !0, navText: ['<i class="fa fa-angle-right"></i>', '<i class="fa fa-angle-left"></i>'], dots: !1, responsiveClass: !0, responsive: { 0: { items: 4, slideBy: 1 } } }), $(".back-to-top").click(function (e) { e.preventDefault(), $("html, body").animate({ scrollTop: 0 }, 800, "easeInExpo") }), $("#img-product-zoom").length && $("#img-product-zoom").ezPlus({ zoomType: "inner", containLensZoom: !0, gallery: "gallery_01f", cursor: "crosshair", galleryActiveClass: "active", responsive: !0, imageCrossfade: !0, zoomWindowFadeIn: 500, zoomWindowFadeOut: 500 }); var c = $("#custom-events"); c.lightGallery(); var u = ["#21171A", "#81575E", "#9C5043", "#8F655D"]; c.on("onBeforeSlide.lg", function (e, t, n) { $(".lg-outer").css("background-color", u[n]) }) });