import * as swiperModule from "/modules/SwiperModule.js";

export function CreateProductImagesCarousel(selector) {
    swiperModule.CreateSwiper(selector, false, false, 1, true, false, false, null);
}