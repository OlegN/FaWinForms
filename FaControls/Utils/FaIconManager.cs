using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text;

namespace FaControls
{
	public enum IconFontFamilyEnum
	{
		None,
		FontAwesome,
		IonIcons,
		TypIcons
	}

	public static class FaIconManager
	{
		private static readonly PrivateFontCollection fontCollection = new PrivateFontCollection();

		static FaIconManager()
		{
			InitialiseFonts();
		}

		private static void InitialiseFonts()
		{
			InitialiseFont(Properties.Resources.fontawesome_webfont);
			InitialiseFont(Properties.Resources.ionicons);
			InitialiseFont(Properties.Resources.typicons);
		}

		private static void InitialiseFont(byte[] fontData)
		{
			IntPtr pFontData = IntPtr.Zero;
			try
			{
				pFontData = Marshal.AllocCoTaskMem(fontData.Length);
				Marshal.Copy(fontData, 0, pFontData, fontData.Length);
				fontCollection.AddMemoryFont((IntPtr)pFontData, fontData.Length);
			}
			finally
			{
				if (pFontData != IntPtr.Zero)
					Marshal.FreeCoTaskMem(pFontData);
			}
		}

		public static FontFamily GetIconFontFamily(IconFontFamilyEnum fontFamily)
		{
			FontFamily family = fontCollection.Families[0];
			foreach (var ff in fontCollection.Families)
			{
				if (String.Equals(ff.Name, fontFamily.ToString(), StringComparison.InvariantCultureIgnoreCase))
				{
					family = ff;
					break;
				}
			}
			return family;
		}

		public static Font GetIconFont(IconFontFamilyEnum fontFamily, float size)
		{
			FontFamily family = GetIconFontFamily(fontFamily);
			return new Font(family, size, GraphicsUnit.Point);
		}

		public static Bitmap RenderFontIcon(IconFontFamilyEnum fontFamily, string symbol, int iconSize, Color fontColor)
		{
			return RenderFontIcon(fontFamily, symbol, iconSize, fontColor, Color.Transparent, Point.Empty);
		}

		public static Bitmap RenderFontIcon(IconFontFamilyEnum fontFamily, string symbol, int iconSize, Color fontColor, Point margins)
		{
			return RenderFontIcon(fontFamily, symbol, iconSize, fontColor, Color.Transparent, margins);
		}

		public static Bitmap RenderFontIcon(IconFontFamilyEnum fontFamily, string symbol, int iconSize, Color fontColor, Color bgColor, Point margins)
		{
			return RenderFontIcon(fontFamily, symbol, iconSize, fontColor, Color.Transparent, margins, 0);
		}

		public static Bitmap RenderFontIcon(IconFontFamilyEnum fontFamily, string symbol, int iconSize, Color fontColor, Color bgColor, Point margins, int angle)
		{
			var bitmap = new Bitmap(iconSize, iconSize);
			var icoChar = NormalizeSymbol(symbol);

			using (var graphics = Graphics.FromImage(bitmap))
			using (var brush = new SolidBrush(fontColor))
			using (var path = new GraphicsPath())
			using (var translate = new Matrix())
			{
				var font = GetAdjustedFont(fontFamily, graphics, icoChar, iconSize, iconSize);

				graphics.Clear(bgColor);
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

//#if DEBUG
//				using (Pen p = new Pen(fontColor))
//					graphics.DrawRectangle(p, new Rectangle(0, 0, bitmap.Width - 1, bitmap.Height - 1));
//#endif

				var stringFormat = new StringFormat(StringFormat.GenericTypographic)
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center
				};

				path.AddString(icoChar, font.FontFamily, (int)font.Style, iconSize, new Point(0, 0), stringFormat);

				var area = Rectangle.Round(path.GetBounds());
				var bounds = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

				var offset = new Point(bounds.Left + (bounds.Width - area.Width) / 2 - area.Left,
					bounds.Top + (bounds.Height - area.Height) / 2 - area.Top);

				offset = Point.Add(offset, new Size(margins));

				translate.Translate(offset.X, offset.Y);
				translate.Rotate(angle);

				path.Transform(translate);

				graphics.FillPath(brush, path);
				font.Dispose();
			}

			return bitmap;
		}

		public static string NormalizeSymbol(string charSymbol)
		{
			string unicodeString = charSymbol;
			if (!String.IsNullOrEmpty(charSymbol) && charSymbol.Length > 1)
			{
				charSymbol = charSymbol.Replace("&#x", "");
				charSymbol = charSymbol.Replace("\\u", "");
				charSymbol = charSymbol.Replace(";", "");

				if (charSymbol.StartsWith("\\"))
					charSymbol = charSymbol.Remove(0, 1);

				unicodeString = charSymbol;
				int code = 0;

				if (int.TryParse(charSymbol, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out code))
					unicodeString = Char.ConvertFromUtf32(code);
			}
			return unicodeString;
		}

		private static Font GetAdjustedFont(IconFontFamilyEnum fontFamily, Graphics g, string symbol, int containerWidth, int maxFontSize)
		{
			float minFontSize = 8.25f;
			Font f1 = GetIconFont(fontFamily, (float)maxFontSize);
			//return f1;
			for (double adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize = adjustedSize - 1.5)
			{
				Font testFont = GetIconFont(fontFamily, (float)adjustedSize);
				SizeF adjustedSizeNew = g.MeasureString(symbol, testFont);
				if (containerWidth > Convert.ToInt32(adjustedSizeNew.Width) && containerWidth > Convert.ToInt32(adjustedSizeNew.Height))
				{
					return testFont;
				}
			}
			return GetIconFont(fontFamily, minFontSize);
		}
	}
}
