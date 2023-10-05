import * as swiperModule from "/modules/SwiperModule.js";
export function CreateBranchesCarousel(selector) {

    var breakpointsSettings = {
        320: {
            slidesPerView: 1,
            spaceBetween: 20
        },
        768: {
            slidesPerView: 2,
            spaceBetween: 40
        },
    };

    swiperModule.CreateSwiper(selector, true, true, 2, true, true, true, breakpointsSettings);
}