﻿using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.AspNetCore.Http;

namespace TheStore.TestHelpers.AutoData.Customizations
{
    public class FormFileCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new TypeRelay(typeof(IFormFile), typeof(FormFile)));
            fixture.Register(() =>
            {
                // This needs to be disposed along with the outer form data object
                var file = File.OpenRead(@"TestResources/TestImages/TestImage.jpg");
                var formFile = new FormFile(file, 0, file.Length, "TestImage", "TestImage.jpeg");

                return formFile;
            });
        }
    }
}
