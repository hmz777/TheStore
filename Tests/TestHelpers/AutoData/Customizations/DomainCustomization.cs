using AutoFixture;

namespace TheStore.TestHelpers.AutoData.Customizations
{
	public class DomainCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new ImageCustomization());
			fixture.Customize(new ProductColorCustomization());
			fixture.Customize(new ProductReviewCustomization());
			fixture.Customize(new InventoryRecordCustomization());
			fixture.Customize(new CurrencyCustomization());
			fixture.Customize(new MoneyCustomization());
			fixture.Customize(new MultilanguageStringCustomization());
		}
	}
}