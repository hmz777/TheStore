﻿using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product." + nameof(GetByIdRequest))]
	public class GetByIdRequest : RequestBase
	{
		public const string RouteName = "Products.Id";
		public const string RouteTemplate = "products/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }

		public GetByIdRequest(int productId)
		{
			ProductId = productId;
		}
	}
}