﻿using System.ComponentModel;

namespace TheStore.Web.Models.ValueObjectsDtos
{
	[DisplayName(nameof(ImageDto))]
	public class ImageDto : DtoBase
	{
		public string StringFileUri { get; set; }
		public MultilanguageStringDto Alt { get; set; }
		public bool IsMainImage { get; set; }
	}
}