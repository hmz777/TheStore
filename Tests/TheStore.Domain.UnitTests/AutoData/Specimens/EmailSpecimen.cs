using AutoFixture;
using AutoFixture.Kernel;
using System.Reflection;

namespace TheStore.Domain.UnitTests.AutoData.Specimens
{
	public class EmailSpecimen : ISpecimenBuilder
	{
		public object Create(object request, ISpecimenContext context)
		{
			return request switch
			{
				PropertyInfo p when p.PropertyType == typeof(string) && p.Name.Contains("Email", StringComparison.InvariantCultureIgnoreCase) => $"{context.Create<string>()}@fobar.com",
				_ => new NoSpecimen()
			};
		}
	}
}