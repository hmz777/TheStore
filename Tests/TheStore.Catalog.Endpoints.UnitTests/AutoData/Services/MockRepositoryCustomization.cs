using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.ApiCommon.Data.Repository;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Infrastructure.Data;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Services
{
	public class MockRepositoryCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			 
		}
	}
}
