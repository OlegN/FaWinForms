using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FaControls
{
	public partial class FaLinkButton : LinkLabel
	{
		private System.ComponentModel.IContainer components = null;

		private Bitmap _image = null;
		private Color? _iconColor = null;
		private IconFontFamilyEnum _fontType = IconFontFamilyEnum.None;
		private int _iconFontSize = 16;
		private string _iconSymbol = null;
		private Point _iconOffset = Point.Empty;
		private int _rotationAngle = 0;


		[Category("FaButton")]
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
		[DefaultValue(IconFontFamilyEnum.None)]
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
		public virtual Color? IconColor
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
		[System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image Image
		{
			get { return base.Image; }
			set { base.Image = value; }
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

		public FaLinkButton()
			: base()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var img = GetIconBitmap();
			if (img != null && (Image == null || Image != img))
			{
				//this.Image = img;

				var imgWidth = img.Width;
				var rect = new Rectangle(e.ClipRectangle.X + img.Width, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height);
				PaintEventArgs args = new PaintEventArgs(e.Graphics, rect);
				base.OnPaint(args);

				e.Graphics.DrawImage(img, GetImageBounds());
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

		protected Bitmap GetIconBitmap()
		{
			if (_image == null)
			{
				string symb = IconSymbol;
				if (!String.IsNullOrEmpty(IconSymbol) && IconFont != IconFontFamilyEnum.None)
				{
					var color = this.IconColor.GetValueOrDefault(this.LinkColor);
					if (!this.Enabled)
						color = this.DisabledLinkColor;

					_image = FaIconManager.RenderFontIcon(IconFont, IconSymbol, IconFontSize, color, Color.Transparent, IconOffset, IconRotationAngle);
				}
			}
			return _image;
		}

		protected Rectangle GetImageBounds(int left = 3, int top = -1)
		{
			var rect = Rectangle.Empty;
			var icon = GetIconBitmap();

			if (icon != null)
			{
				var h = (int)Math.Max((this.Height - icon.Height) / 2, 0);

				if (top < 0)
					top = h;

				rect = new Rectangle(left, top, icon.Width, icon.Height);
			}

			return rect;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			OnIconChanged();
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();

				if (_image != null)
					_image.Dispose();
			}
			base.Dispose(disposing);
		}

		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
		}
	}
}
