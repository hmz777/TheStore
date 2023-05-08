﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(UpdateImageDto))]
	public class UpdateImageDto : DtoBase
	{
		public int ImageId { get; set; }
		public IFormFile File { get; set; }
		public string Alt { get; set; }

		[JsonIgnore]
		public string StringFileUri { get; set; }
	}
}