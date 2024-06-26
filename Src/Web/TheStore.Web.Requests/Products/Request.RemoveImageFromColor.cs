﻿using System.ComponentModel;
using System.Web;
using TheStore.Web.Requests;

namespace TheStore.Web.Requests.Products
{
	[DisplayName("Product.Single." + nameof(RemoveImageFromColorRequest))]
	public class RemoveImageFromColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/colors/{ColorCode}/images/{ImagePath}";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ColorCode}", ColorCode.ToString())
			.Replace("{ImagePath}", HttpUtility.UrlEncode(ImagePath));

		public int ProductId { get; set; }
		public string ColorCode { get; set; }
		public string ImagePath { get; set; }

		public RemoveImageFromColorRequest()
		{

		}
		public RemoveImageFromColorRequest(int productId, string colorCode, string imagePath)
		{
			ProductId = productId;
			ColorCode = colorCode;
			ImagePath = imagePath;
		}
	}
}