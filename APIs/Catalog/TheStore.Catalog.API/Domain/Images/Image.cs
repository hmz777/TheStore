using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Catalog.API.Domain.Images
{
	public class Image : BaseEntity
	{
		public string FileName { get; set; }
		public ImageType ImageType { get; set; }
		public string Alt { get; set; }

		public Image(string fileName, ImageType imageType, string alt)
		{
			Guard.Against.NullOrEmpty(fileName, nameof(fileName));
			Guard.Against.EnumOutOfRange(imageType, nameof(imageType));
			Guard.Against.NullOrEmpty(alt, nameof(alt));

			FileName = fileName;
			ImageType = imageType;
			Alt = alt;
		}
	}
}
