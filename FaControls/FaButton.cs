using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace FaControls
{
	//[Designer(typeof(FaControls.Designers.FaButtonDesigner))] 
	public partial class FaButton : Button, IFaControl
	{
		#region Properties

		private IconFontFamilyEnum _fontType = IconFontFamilyEnum.FontAwesome;
		private string _iconSymbol = "f024";
		private int _iconFontSize = 18;
		private Color _iconColor = Color.Black;
		private Color _bgColor = Color.Transparent;
		private Point _iconOffset = Point.Empty;
		private int _rotationAngle = 0;

		private Bitmap _image = null;

		[Category("FaButton")]
		[DefaultValue(18)]
		public int IconFontSize
		{
			get { return _iconFontSize; }
			set
			{
				if (_iconFontSize != value)
				{
					_iconFontSize = value;
					OnIconChanged();
				}
			}
		}

		[Category("FaButton")]
		[DefaultValue(typeof(IconFontFamilyEnum), "FontAwesome")]
		public IconFontFamilyEnum IconFont
		{
			get
			{
				return _fontType;
			}
			set
			{
				if (_fontType != value)
				{
					_fontType = value;
					OnIconChanged();
				}
			}
		}

		[Category("FaButton")]
		[DefaultValue("f024")]
		public string IconSymbol
		{
			get
			{
				return _iconSymbol;
			}
			set
			{
				if (_iconSymbol != value)
				{
					_iconSymbol = value;
					OnIconChanged();
				}
			}
		}

		[Category("FaButton")]
		[DefaultValue(typeof(Color), "Black")]
		public virtual Color IconColor
		{
			get
			{
				return _iconColor;
			}
			set
			{
				if (_iconColor != value)
				{
					_iconColor = value;
					OnIconChanged();
				}
			}
		}

		[Category("FaButton")]
		[DefaultValue(typeof(Color), "Transparent")]
		public virtual Color IconBgColor
		{
			get
			{
				return _bgColor;
			}
			set
			{
				if (_bgColor != value)
				{
					_bgColor = value;
					OnIconChanged();
				}
			}
		}

		[Category("FaButton")]
		[DefaultValue(typeof(Point), "0,0")]
		public Point IconOffset
		{
			get
			{
				return _iconOffset;
			}
			set
			{
				if (_iconOffset != value)
				{
					_iconOffset = value;
					OnIconChanged();
				}
			}
		}

		[Category("FaButton")]
		[DefaultValue(0)]
		public int IconRotationAngle
		{
			get
			{
				return _rotationAngle;
			}
			set
			{
				if (_rotationAngle != value)
				{
					_rotationAngle = value;
					OnIconChanged();
				}
			}
		}

		[Category("FaButton")]
		[System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image Image
		{
			get { return base.Image; }
			set { base.Image = value; }
		}

		#endregion

		protected virtual Color IconRenderColor
		{
			get
			{
				return this.IconColor;
			}
		}

		public FaButton() :
			base()
		{
			this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var img = GetIconBitmap();
			if (img != null && (Image == null || Image != img))
			{
				this.Image = img;
				base.OnPaint(e);
			}
			else
			{
				base.OnPaint(e);
			}
			return;
		}

		protected void OnIconChanged()
		{
			if (_image != null)
			{
				if (Image == _image)
				{
					Image = null;
				}

				_image.Dispose();
				_image = null;
			}
		}

		protected Image GetImage()
		{
			Image bmp = GetIconBitmap();
			if (bmp == null)
				bmp = this.Image;
			return bmp;
		}

		protected Bitmap GetIconBitmap()
		{
			if (_image == null)
			{
				string symb = IconSymbol;
				if (!String.IsNullOrEmpty(IconSymbol) && IconFont != IconFontFamilyEnum.None)
				{
					_image = FaIconManager.RenderFontIcon(this.IconFont, this.IconSymbol, this.IconFontSize, this.IconRenderColor, this.IconBgColor, this.IconOffset, this.IconRotationAngle);
				}
			}
			return _image;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			OnIconChanged();
		}

		protected Rectangle GetImageBounds(int left = 3, int top = -1)
		{
			var rect = Rectangle.Empty;
			var icon = GetImage();

			if (icon != null)
			{
				var h = (int)Math.Max((this.Height - icon.Height) / 2, 0);

				if (top < 0)
					top = h;

				rect = new Rectangle(left, top, icon.Width, icon.Height);
			}

			return rect;
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (_image != null)
					_image.Dispose();
			}
		}

	}
}
