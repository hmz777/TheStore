﻿using System.ComponentModel;
using TheStore.Web.Requests;

namespace TheStore.Web.Requests.Products
{
	[DisplayName("Product.Assembled." + nameof(RemovePartRequest))]
	public class RemovePartRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts/{ProductId:int}/parts/{PartId:int}/{Sku}";
		public override string Route => RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{PartId:int}", PartId.ToString());

		public int ProductId { get; set; }

		public int PartId { get; set; }

		public string Sku { get; set; }
	}
}