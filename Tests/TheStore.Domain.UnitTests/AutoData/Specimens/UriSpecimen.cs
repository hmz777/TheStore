using AutoFixture.Kernel;
using System.Reflection;

namespace TheStore.Catalog.Domain.UnitTests.AutoData.Specimens
{
	public class UriSpecimen : ISpecimenBuilder
	{
		public object Create(object request, ISpecimenContext context)
		{
			return request switch
			{
				PropertyInfo p when p.PropertyType == typeof(Uri) &&
				p.Name.Contains("Uri", StringComparison.InvariantCultureIgnoreCase) => new Uri("http://example.com"),

				ParameterInfo p when p.ParameterType == typeof(Uri) &&
				p.Name.Contains("Uri", StringComparison.InvariantCultureIgnoreCase) => new Uri("http://example.com"),

				_ => new NoSpecimen()
			};
		}
	}
}