using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FaControls
{
	public interface IFaControl
	{
		IconFontFamilyEnum IconFont { get; set; }
		int IconSize { get; set; }
		string IconSymbol { get; set; }
		Color IconColor { get; set; }
		Color IconBgColor { get; set; }
		Point IconOffset { get; set; }
		int IconRotationAngle { get; set; }
	}
}
