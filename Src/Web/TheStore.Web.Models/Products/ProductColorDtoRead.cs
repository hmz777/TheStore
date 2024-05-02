﻿using System.ComponentModel;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Products
{
    [DisplayName(nameof(ProductColorDtoRead))]
    public class ProductColorDtoRead : DtoBase
    {
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public List<ImageDto> Images { get; set; }
        public bool IsMainColor { get; set; }

        public ImageDto GetMainImage() =>
            Images.Where(image => image.IsMainImage).FirstOrDefault() ?? Images.First();
    }
}