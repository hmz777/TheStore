using AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Tests.Domain.AutoData.Specimens
{
	public class HexColorSpecimen : ISpecimenBuilder
	{
		public object Create(object request, ISpecimenContext context)
		{
			var random = new Random();

			return request switch
			{
				ParameterInfo p when p.ParameterType == typeof(string) && p.Name.Contains("Color", StringComparison.InvariantCultureIgnoreCase) => $"#{random.Next(0x1000000):X6}",
				_ => new NoSpecimen()
			};
		}
	}
}