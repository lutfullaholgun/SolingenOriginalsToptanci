(function ($) {
    "use strict";

    var initSwipers = function () {
        new Swiper(".main-swiper", {
            slidesPerView: 3,
            spaceBetween: 80,
            speed: 700,
            loop: true,
            navigation: {
                nextEl: ".icon-arrow-right",
                prevEl: ".icon-arrow-left"
            },
            breakpoints: {
                300: { slidesPerView: 1, spaceBetween: 20 },
                768: { slidesPerView: 2, spaceBetween: 20 },
                1200: { slidesPerView: 3, spaceBetween: 80 }
            }
        });

        // Diðer swiper'lar
        new Swiper(".slideshow", {
            slidesPerView: 1,
            spaceBetween: 0,
            speed: 1000,
            loop: true,
            navigation: {
                nextEl: ".icon-arrow-right",
                prevEl: ".icon-arrow-left"
            }
        });

        new Swiper(".two-column-swiper .swiper", {
            slidesPerView: 1,
            loop: true,
            speed: 900,
            navigation: {
                nextEl: ".two-column-swiper .icon-arrow-right",
                prevEl: ".two-column-swiper .icon-arrow-left"
            }
        });

        new Swiper(".overlay-swiper", {
            slidesPerView: "auto",
            loop: true,
            navigation: {
                nextEl: ".icon-arrow-right",
                prevEl: ".icon-arrow-left"
            }
        });

        $(".product-carousel").each(function () {
            var id = $(this).attr("id");
            new Swiper("#" + id + " .swiper", {
                slidesPerView: 4,
                spaceBetween: 20,
                navigation: {
                    nextEl: "#" + id + " .icon-arrow-right",
                    prevEl: "#" + id + " .icon-arrow-left"
                },
                breakpoints: {
                    0: {
                        slidesPerView: 2,
                        spaceBetween: 20,
                        pagination: {
                            el: ".swiper-pagination",
                            clickable: true
                        }
                    },
                    999: { slidesPerView: 3, spaceBetween: 10 },
                    1366: { slidesPerView: 4, spaceBetween: 40 }
                }
            });
        });

        new Swiper(".testimonial-swiper", {
            effect: "coverflow",
            grabCursor: true,
            centeredSlides: true,
            loop: true,
            slidesPerView: "auto",
            coverflowEffect: { fade: true },
            pagination: {
                el: ".testimonial-swiper-pagination",
                clickable: true
            }
        });

        new Swiper(".review-swiper", {
            slidesPerView: 1,
            spaceBetween: 20,
            loop: true,
            navigation: {
                nextEl: ".icon-arrow-right",
                prevEl: ".icon-arrow-left"
            },
            pagination: {
                el: ".swiper-pagination",
                clickable: true
            }
        });

        var thumbs = new Swiper(".product-thumbnail-slider", {
            slidesPerView: 3,
            spaceBetween: 20,
            direction: "vertical",
            breakpoints: {
                0: { direction: "horizontal" },
                992: { direction: "vertical" }
            }
        });

        new Swiper(".product-large-slider", {
            slidesPerView: 1,
            spaceBetween: 0,
            effect: "fade",
            thumbs: { swiper: thumbs },
            pagination: {
                el: ".swiper-pagination",
                clickable: true
            }
        });
    };

    var initQuantityButtons = function () {
        $(".product-qty").each(function () {
            var $el = $(this);
            $el.find(".quantity-right-plus").click(function (e) {
                e.preventDefault();
                var qty = parseInt($el.find("#quantity").val()) || 0;
                $el.find("#quantity").val(qty + 1);
            });
            $el.find(".quantity-left-minus").click(function (e) {
                e.preventDefault();
                var qty = parseInt($el.find("#quantity").val()) || 0;
                if (qty > 0) $el.find("#quantity").val(qty - 1);
            });
        });
    };

    var initJarallax = function () {
        if (typeof jarallax === "function") {
            jarallax(document.querySelectorAll(".jarallax"));
            jarallax(document.querySelectorAll(".jarallax-keep-img"), { keepImg: true });
        }
    };

    var initTextEffect = function () {
        $(".txt-fx").each(function () {
            var html = "", t = 0, words = this.textContent.split(/\s/);
            $.each(words, function (i, word) {
                html += "<span class='word'>";
                for (var j = 0; j < word.length; j++) {
                    html += "<span class='letter' style='transition-delay:" + (300 + 10 * t) + "ms;'>" + word[j] + "</span>";
                    t++;
                }
                html += "</span><span class='letter' style='transition-delay:300ms;'>&nbsp;</span>";
                t++;
            });
            this.innerHTML = html;
        });
    };

    var initStickyNavbar = function () {
        if ($(window).scrollTop() >= 200) {
            $(".navbar.fixed-top").addClass("bg-black");
        } else {
            $(".navbar.fixed-top").removeClass("bg-black");
        }
    };

    var initIsotope = function () {
        $(".grid").each(function () {
            var $grid = $(".grid").isotope({
                itemSelector: ".product-item",
                layoutMode: "fitRows",
                filter: $(".button-group .is-checked").data("filter")
            });

            $(".button-group").on("click", "a", function (e) {
                e.preventDefault();
                var filterValue = $(this).attr("data-filter");
                $grid.isotope({ filter: filterValue });
                $(".button-group .is-checked").removeClass("is-checked");
                $(this).addClass("is-checked");
            });
        });
    };

    var initZoomEffect = function () {
        $(".image-zoom")
            .on("mouseover", function () {
                $(this).children(".photo").css({ transform: "scale(" + $(this).attr("data-scale") + ")" });
            })
            .on("mouseout", function () {
                $(this).children(".photo").css({ transform: "scale(1)" });
            })
            .on("mousemove", function (e) {
                var offsetX = (e.pageX - $(this).offset().left) / $(this).width() * 100;
                var offsetY = (e.pageY - $(this).offset().top) / $(this).height() * 100;
                $(this).children(".photo").css({ "transform-origin": offsetX + "% " + offsetY + "%" });
            })
            .each(function () {
                $(this).append('<div class="photo"></div>').children(".photo").css({ "background-image": "url(" + $(this).attr("data-image") + ")" });
            });
    };

    var initSearchPopup = function () {
        $(".navbar").on("click", ".search-button", function () {
            $(".search-popup").toggleClass("is-visible");
        });

        $(".navbar").on("click", ".btn-close-search", function () {
            $(".search-popup").toggleClass("is-visible");
        });

        $(".search-popup-trigger").on("click", function (e) {
            e.preventDefault();
            $(".search-popup").addClass("is-visible");
            setTimeout(function () {
                $(".search-popup").find("#search-popup").focus();
            }, 350);
        });

        $(".search-popup").on("click", function (e) {
            if ($(e.target).is(".search-popup, .search-popup-close, .search-popup-close svg, .search-popup-close path")) {
                e.preventDefault();
                $(this).removeClass("is-visible");
            }
        });

        $(document).on("keyup", function (e) {
            if (e.which === 27) {
                $(".search-popup").removeClass("is-visible");
            }
        });
    };

    // Baþlatma
    $(document).ready(function () {
        initSearchPopup();
        initJarallax();
        initTextEffect();
        initQuantityButtons();
        initSwipers();
        initZoomEffect();

        if ($.fn.colorbox) {
            $(".youtube").colorbox({
                iframe: true,
                innerWidth: 960,
                innerHeight: 585
            });
        }

        if (typeof AOS !== "undefined") {
            AOS.init({ duration: 1200, once: true });
        }

        if (typeof hcSticky === "function") {
            new hcSticky(".sticky-info", {
                stickTo: "section.single-product",
                innerTop: 200,
                responsive: { 980: { disable: true } }
            });
        }
    });

    $(window).on("scroll", initStickyNavbar);

    $(window).on("load", function () {
        $(".preloader").addClass("loaded");

        $(".btn-nav").on("click tap", function () {
            $(".nav-content").toggleClass("showNav hideNav").removeClass("hidden");
            $(this).toggleClass("animated");
        });

        initIsotope();
    });

})(jQuery);

<script>
    document.querySelector('.fa-search')?.addEventListener('click', () => {
        document.querySelector('.search-popup').style.display = 'flex';
    });

    document.querySelector('.search-popup-close')?.addEventListener('click', () => {
        document.querySelector('.search-popup').style.display = 'none';
    });
</script>
