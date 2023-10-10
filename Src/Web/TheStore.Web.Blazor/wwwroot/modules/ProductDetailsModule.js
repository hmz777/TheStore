import * as swiperModule from "/modules/SwiperModule.js";

export function CreateImageGalleryCarousel(selector) {
    swiperModule.CreateSwiper(selector, false, false, 6, true, false, false, null);
}