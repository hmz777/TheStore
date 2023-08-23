import Swiper from 'https://cdn.jsdelivr.net/npm/swiper@10/swiper-bundle.min.mjs';

export function InitCarousel() {
    let swiper = new Swiper('#branches-carousel', {
        autoplay: {
            pauseOnMouseEnter: true,
            disableOnInteraction: false
        },
        speed: 400,
        direction: 'horizontal',
        loop: true,
        slidesPerView: 1,
        preloadImages: false,
        lazy: {
            enabled: true,
            checkInView: true,
            loadOnTransitionStart: true,
        },
        pagination: {
            el: '.swiper-pagination',
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        }
    });

    let swiperCssId = "swiper-css";

    if (document.getElementById(swiperCssId) == null) {
        let carouselCss = document.createElement('link');
        carouselCss.setAttribute('rel', 'stylesheet');
        carouselCss.setAttribute('href', 'https://cdn.jsdelivr.net/npm/swiper@10/swiper-bundle.min.css');
        carouselCss.setAttribute('id', swiperCssId);
        document.head.append(carouselCss);
    }
}