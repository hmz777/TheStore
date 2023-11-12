﻿using AutoFixture;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Domain.UnitTests.AutoData.Customizations
{
	public class MultilanguageStringCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new LocalizedStringCustomization());
			fixture.Register(() =>
			{
				var multilanguageString = new MultilanguageString(fixture.Create<LocalizedString>());

				return multilanguageString;
			});
		}
	}
}
