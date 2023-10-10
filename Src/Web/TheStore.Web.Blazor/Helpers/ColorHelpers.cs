using System.Drawing;

namespace TheStore.Web.Blazor.Helpers
{
	public class ColorHelpers
	{
		public static string RgbFromHex(string hexColor, decimal backgroundOpacity)
		{
			Color color = ColorTranslator.FromHtml(hexColor);
			int r = Convert.ToInt16(color.R);
			int g = Convert.ToInt16(color.G);
			int b = Convert.ToInt16(color.B);
			return string.Format("rgba({0}, {1}, {2}, {3});", r, g, b, backgroundOpacity);
		}

		/// <summary>
		/// Creates color with corrected brightness.
		/// <br/>
		/// <a href="https://stackoverflow.com/q/801406/6843077">C#: Create a lighter/darker color based on a system color</a>
		/// </summary>
		/// <param name="color">Color to correct.</param>
		/// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1.
		/// Negative values produce darker colors.</param>
		/// <returns>
		/// Corrected <see cref="Color"/> structure.
		/// </returns>
		public static string ChangeColorBrightness(string hexColor, float correctionFactor)
		{
			Color color = ColorTranslator.FromHtml(hexColor);

			float red = (float)color.R;
			float green = (float)color.G;
			float blue = (float)color.B;

			if (correctionFactor < 0)
			{
				correctionFactor = 1 + correctionFactor;
				red *= correctionFactor;
				green *= correctionFactor;
				blue *= correctionFactor;
			}
			else
			{
				red = (255 - red) * correctionFactor + red;
				green = (255 - green) * correctionFactor + green;
				blue = (255 - blue) * correctionFactor + blue;
			}

			return string.Format("rgb({0}, {1}, {2});", (int)red, (int)green, (int)blue);
		}
	}
}
