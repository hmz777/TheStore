using System.ComponentModel.DataAnnotations;

namespace TheStore.Blazor.Models.Contact
{
	public class ContactRequest : RequestBase
	{
		public const string RouteTemplate = "contact";
		public override string Route => RouteTemplate;

		[Required]
        public string Subject { get; set; }

		[Required]
		public string Message { get; set; }
    }
}