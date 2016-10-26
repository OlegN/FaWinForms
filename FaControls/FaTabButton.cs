using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace FaControls
{
	public partial class FaTabButton : FaSelectButton
	{
		private int _tabBorderWidth = 3;
		private int _borderWidth = 1;
		private TabAlignment _tabBorderDirection = TabAlignment.Bottom;

		[Category("FaButton")]
		public int TabBorderWidth
		{
			get
			{
				return _borderWidth;
			}
			set
			{
				_borderWidth = value;
			}
		}

		[Category("FaButton")]
		[DefaultValue(TabAlignment.Bottom)]
		public TabAlignment TabBorderDirection
		{
			get
			{
				return _tabBorderDirection;
			}
			set
			{
				if (_tabBorderDirection != value)
				{
					_tabBorderDirection = value;
					Invalidate();
				}
			}
		}

		[Category("FaButton")]
		public int BorderWidth
		{
			get
			{
				return _tabBorderWidth;
			}
			set
			{
				_tabBorderWidth = value;
			}
		}

		public FaTabButton() :
			base()
		{
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			var clipRect = pe.ClipRectangle;

			float cornerRadius = 10.0f;
			float noCorner = 0.0001f;

			float halfBWidth = (float)BorderWidth / 2;

			using (var mainBrush = new SolidBrush(this.TextRenderColor))
			using(var borderPen = new Pen(mainBrush, BorderWidth))
			using(var px = new GraphicsPath())
			{
				Rectangle tabRect;
				GraphicsPath gfxPath = new GraphicsPath();

				switch (TabBorderDirection)
				{
					case TabAlignment.Left:
						tabRect = new Rectangle(clipRect.X, clipRect.Y, this.TabBorderWidth, clipRect.Y + clipRect.Height);

						gfxPath.AddArc(clipRect.X + halfBWidth, clipRect.Y + halfBWidth, noCorner, noCorner, 180, 90);
						gfxPath.AddArc(clipRect.X + clipRect.Width - cornerRadius - halfBWidth, clipRect.Y + halfBWidth, cornerRadius, cornerRadius, 270, 90);
						gfxPath.AddArc(clipRect.X + clipRect.Width - cornerRadius - halfBWidth, clipRect.Y + clipRect.Height - cornerRadius - halfBWidth, cornerRadius, cornerRadius, 0, 90);
						gfxPath.AddArc(clipRect.X + halfBWidth, clipRect.Y + clipRect.Height - noCorner - halfBWidth, noCorner, noCorner, 90, 90);

						break;
					case TabAlignment.Right:
						tabRect = new Rectangle(clipRect.X + clipRect.Width - TabBorderWidth, 0, this.TabBorderWidth, clipRect.Y + clipRect.Height);

						gfxPath.AddArc(clipRect.X + halfBWidth, clipRect.Y + halfBWidth, cornerRadius, cornerRadius, 180, 90);
						gfxPath.AddArc(clipRect.X + clipRect.Width - noCorner - halfBWidth, clipRect.Y + halfBWidth, noCorner, noCorner, 270, 90);
						gfxPath.AddArc(clipRect.X + clipRect.Width - noCorner - halfBWidth, clipRect.Y + clipRect.Height - noCorner - halfBWidth * 2, noCorner, noCorner, 0, 90);
						gfxPath.AddArc(clipRect.X + halfBWidth, clipRect.Y + clipRect.Height - cornerRadius - halfBWidth * 2, cornerRadius, cornerRadius, 90, 90);
						break;
					case TabAlignment.Top:
						tabRect = new Rectangle(clipRect.X, clipRect.Y, clipRect.X + clipRect.Width, TabBorderWidth);

						gfxPath.AddArc(clipRect.X + halfBWidth, clipRect.Y + halfBWidth, noCorner, noCorner, 180, 90);
						gfxPath.AddArc(clipRect.X + clipRect.Width - noCorner - halfBWidth, clipRect.Y + halfBWidth, noCorner, noCorner, 270, 90);
						gfxPath.AddArc(clipRect.X + clipRect.Width - cornerRadius - halfBWidth, clipRect.Y + clipRect.Height - cornerRadius - halfBWidth, cornerRadius, cornerRadius, 0, 90);
						gfxPath.AddArc(clipRect.X + halfBWidth, clipRect.Y + clipRect.Height - cornerRadius - halfBWidth, cornerRadius, cornerRadius, 90, 90);
						break;
					case TabAlignment.Bottom:
					default:
						tabRect = new Rectangle(clipRect.X, clipRect.Y + clipRect.Height - TabBorderWidth, clipRect.X + clipRect.Width, TabBorderWidth);

						gfxPath.AddArc(clipRect.X + halfBWidth, clipRect.Y + halfBWidth, cornerRadius, cornerRadius, 180, 90);
						gfxPath.AddArc(clipRect.X + clipRect.Width - cornerRadius - halfBWidth * 2, clipRect.Y + halfBWidth, cornerRadius, cornerRadius, 270, 90);
						gfxPath.AddArc(clipRect.X + clipRect.Width - noCorner - halfBWidth * 2, clipRect.Y + clipRect.Height - noCorner - halfBWidth, noCorner, noCorner, 0, 90);
						gfxPath.AddArc(clipRect.X + halfBWidth, clipRect.Y + clipRect.Height - noCorner - halfBWidth, noCorner, noCorner, 90, 90);
						break;
				}

				gfxPath.CloseAllFigures();
				pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

				pe.Graphics.DrawPath(borderPen, gfxPath);
				pe.Graphics.FillRectangle(mainBrush, tabRect);
				px.AddRectangle(clipRect);
				px.AddPath(gfxPath, false);

				using (var b1 = new SolidBrush(Color.Transparent))
				{
					pe.Graphics.FillPath(b1, px);
				}
			}
			//var anim = new Animation
		}

		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(0, 3, 0, 3);
			}
		}

		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.NoControl;
			}
		}
	}
}
