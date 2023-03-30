using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Catalog.Core.ValueObjects;

namespace TheStore.Domain.Tests.AutoData.Customizations
{
    public class InventoryRecordCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() =>
            {
                var rand = new Random();
                return new InventoryRecord(rand.Next(1, 500), 1, 501, 0, false);
            });
        }
    }
}
