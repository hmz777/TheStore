﻿namespace TheStore.SharedModels.Models.Products
{
	public class RemovePartRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts/{ProductId:int}/parts/{PartId:int}";
		internal override string Route => RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{PartId:int}", PartId.ToString());

		public int ProductId { get; set; }
		public int PartId { get; set; }
	}
}