using AutoFixture.Kernel;
using System.Reflection;

namespace TheStore.Domain.UnitTests.AutoData.Specimens
{
	public class HexColorSpecimen : ISpecimenBuilder
	{
		public object Create(object request, ISpecimenContext context)
		{
			var random = new Random();

			return request switch
			{
				ParameterInfo p
				when p.ParameterType == typeof(string) &&
				p.Name.Contains("Color", StringComparison.InvariantCultureIgnoreCase) =>
				$"#{random.Next(0x1000000):X6}",

				PropertyInfo p
				when p.PropertyType == typeof(string) &&
				p.Name.Contains("Color", StringComparison.InvariantCultureIgnoreCase) =>
				$"#{random.Next(0x1000000):X6}",

				_ => new NoSpecimen()
			};
		}
	}
}