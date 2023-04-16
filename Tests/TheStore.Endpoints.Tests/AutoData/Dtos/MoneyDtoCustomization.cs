﻿using AutoFixture;
using AutoMapper;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Domain.Tests.AutoData.Customizations;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Endpoints.Tests.AutoData.Dtos
{
	public class MoneyDtoCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new MoneyCustomization());
			fixture.Register(() =>
			{
				var mapper = fixture.Create<IMapper>();
				var money = fixture.Create<Money>();

				return mapper.Map<MoneyDto>(money);
			});
		}
	}
}
