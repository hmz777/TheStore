﻿using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName(nameof(AssembledProductDtoRead))]
	public class AssembledProductDtoUpdate : DtoBase
	{
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public MultilanguageStringDto ShortDescription { get; set; }
		public MultilanguageStringDto Description { get; set; }
		public bool Published { get; set; }
	}
}
