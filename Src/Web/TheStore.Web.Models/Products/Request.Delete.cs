﻿using System.ComponentModel;
using TheStore.Web.Models;

namespace TheStore.Web.Models.Products
{
	[DisplayName("Product.Single." + nameof(DeleteRequest))]
	public class DeleteRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }

		public DeleteRequest()
		{

		}

		public DeleteRequest(int productId)
		{
			ProductId = productId;
		}
	}
}
