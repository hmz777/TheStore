﻿using System.ComponentModel;
using TheStore.Web.Models;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Branches
{
	[DisplayName("Branch." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "branches";
		public override string Route => RouteTemplate;

		public string Name { get; set; }
		public string Description { get; set; }
		public AddressDto Address { get; set; }
	}
}