using AutoFixture.Kernel;
using System.Reflection;

namespace TheStore.Tests.Domain.AutoData.Specimens
{
    public class FileUriSpecimen : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            return request switch
            {
				ParameterInfo p when p.ParameterType == typeof(Uri) &&
                p.Name!.Contains("File", StringComparison.InvariantCultureIgnoreCase) &&
                p.Name.Contains("Uri", StringComparison.InvariantCultureIgnoreCase) => new Uri("http://example.com/file.txt"),
                _ => new NoSpecimen()
            };
        }
    }
}