﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product." + nameof(AddVariantRequest))]
	public class AddVariantRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/variants";
		public override string Route => RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString());

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromBody]
		public ProductVariantDtoUpdate ProductVariant { get; set; }
	}
}