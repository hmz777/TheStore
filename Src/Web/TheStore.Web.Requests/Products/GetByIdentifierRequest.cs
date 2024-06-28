﻿using System.ComponentModel;

namespace TheStore.Web.Requests.Products
{
	[DisplayName("Product." + nameof(GetByIdentifierRequest))]
	public class GetByIdentifierRequest : RequestBase
	{
		public const string RouteName = "Products.Identifier";
		public const string RouteTemplate = "products/{Identifier}";
		public override string Route => RouteTemplate.Replace("{Identifier}", Identifier);

		public string Identifier { get; set; }
	}
}