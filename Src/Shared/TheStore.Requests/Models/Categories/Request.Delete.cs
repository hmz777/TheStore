﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.Requests;

namespace TheStore.Requests.Models.Categories
{
	[DisplayName("Category." + nameof(DeleteRequest))]
	public class DeleteRequest : RequestBase
	{
		public const string RouteTemplate = "categories/{CategoryId:int}";
		public override string Route => RouteTemplate.Replace("{CategoryId:int}", CategoryId.ToString());

		public int CategoryId { get; set; }
	}
}
