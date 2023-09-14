using System.Globalization;
using System.Text.RegularExpressions;

namespace AuthServer.Routing
{
	public partial class CultureRouteConstraint : IRouteConstraint
	{
		[GeneratedRegex("^[a-z]{2}-[A-Z]{2}$", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 150)]
		private static partial Regex LanguageCodeRegex();

		public bool Match(
			HttpContext httpContext,
			IRouter route,
			string routeKey,
			RouteValueDictionary values,
			RouteDirection routeDirection)
		{
			// TODO: check if the provided value is a supported culture and
			// not just in a valid format

			if (!values.TryGetValue(routeKey, out var routeValue))
				return false;

			var routeValueString = Convert.ToString(routeValue, CultureInfo.InvariantCulture);

			if (routeValueString is null)
				return false;

			return LanguageCodeRegex().IsMatch(routeValueString);
		}
	}
}