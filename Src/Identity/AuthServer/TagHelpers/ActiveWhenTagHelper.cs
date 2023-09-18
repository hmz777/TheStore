using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace AuthServer.TagHelpers
{
	[HtmlTargetElement("li")]
	public class ActiveWhenTagHelper : TagHelper
	{
		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }

		public string ActiveWhen { get; set; }
		public string ActiveClass { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{		
			var request = ViewContext.HttpContext.Request;

			if (ActiveWhen is not null && request.Path.ToString().Contains(ActiveWhen, StringComparison.InvariantCultureIgnoreCase))
			{
				output.AddClass(ActiveClass, HtmlEncoder.Default);
			}
		}
	}
}
