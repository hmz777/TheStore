import Swiper from 'https://cdn.jsdelivr.net/npm/swiper@10/swiper-bundle.min.mjs';

let swiperInitialized;

let instances = [];

let autoPlayDefaultSettings = {
    pauseOnMouseEnter: true,
    disableOnInteraction: false
};

let paginationDefaultSettings = {
    el: '.swiper-pagination',
    clickable: true,
};

let navigationDefaultSettings = {
    nextEl: '.swiper-button-next',
    prevEl: '.swiper-button-prev'
}

function InitSwiperIfNot() {
    if (swiperInitialized == true) {
        return;
    }

    let swiperCssId = "swiper-css";

    if (document.getElementById(swiperCssId) == null) {
        let carouselCss = document.createElement('link');
        carouselCss.setAttribute('rel', 'stylesheet');
        carouselCss.setAttribute('href', 'https://cdn.jsdelivr.net/npm/swiper@10/swiper-bundle.min.css');
        carouselCss.setAttribute('id', swiperCssId);
        document.head.append(carouselCss);
    }

    swiperInitialized = true;
}

export function CreateSwiper(
    selector,
    autoPlayEnabled,
    loopEnabled,
    slidesPerView,
    paginationEnabled,
    navigationEnabled,
    hasBreakpoints,
    breakpointsSettings) {

    if (TryUpdateSwiper(selector)) {
        return;
    }

    InitSwiperIfNot();

    let swiperInstance = new Swiper(selector, {
        autoplay: autoPlayEnabled ? autoPlayDefaultSettings : undefined,
        speed: 400,
        direction: 'horizontal',
        loop: loopEnabled,
        slidesPerView: slidesPerView,
        preloadImages: false,
        lazy: {
            enabled: true,
            checkInView: true,
            loadOnTransitionStart: true,
        },
        pagination: paginationEnabled ? paginationDefaultSettings : {},
        navigation: navigationEnabled ? navigationDefaultSettings : {},
        breakpoints: hasBreakpoints ? breakpointsSettings : {}
    });

    instances.push(swiperInstance);
}

function TryDestroySwiper(selector) {
    let swiperInstance = document.querySelector(selector).swiper;

    if (swiperInstance != null) {
        swiperInstance.destroy(true, true);
    }
}

function TryUpdateSwiper(selector) {
    let swiperInstance = document.querySelector(selector).swiper;

    if (swiperInstance != null) {
        swiperInstance.update();

        return true;
    }

    return false;
}