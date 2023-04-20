using AutoFixture.Kernel;
using System.Reflection;

namespace TheStore.Domain.UnitTests.AutoData.Specimens
{
	public class FileUriSpecimen : ISpecimenBuilder
	{
		public object Create(object request, ISpecimenContext context)
		{
			return request switch
			{
				ParameterInfo p when p.ParameterType == typeof(string) &&
				p.Name!.Contains("File", StringComparison.InvariantCultureIgnoreCase) ||
				p.Name.Contains("Uri", StringComparison.InvariantCultureIgnoreCase) => "http://example.com/file.txt",
				_ => new NoSpecimen()
			};
		}
	}
}