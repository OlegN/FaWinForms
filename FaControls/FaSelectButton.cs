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
	public partial class FaSelectButton : FaButton
	{
		#region Properties
		private Color _foreColor = SystemColors.ControlText;
		private Color _selectedColor = Color.RoyalBlue;
		private bool _selected = false;

		[Category("FaButton")]
		[DefaultValue(typeof(Color), "RoyalBlue")]
		public Color ActiveColor
		{
			get { return _selectedColor; }
			set { _selectedColor = value; }
		}

		[Category("FaButton")]
		[DefaultValue(false)]
		public bool Selected
		{
			get
			{
				return _selected;
			}
			set
			{
				if (value != _selected)
				{
					_selected = value;
					OnCheckChanged(value);
				}
			}
		}

		[Category("Action")]
		public event EventHandler SelectionChanged;

		[Category("Action")]
		public event CancelEventHandler SelectionChanging;

		public new Color ForeColor
		{
			get
			{
				return _foreColor;
			}
			set
			{
				_foreColor = value;
				Invalidate();
			}
		}

		protected override Color IconRenderColor
		{
			get
			{
				return Selected ? ActiveColor : IconColor;
			}
		}

		protected virtual Color TextRenderColor
		{
			get
			{
				return Selected ? ActiveColor : this.ForeColor;
			}
		}

		#endregion

		public FaSelectButton()
			: base()
		{

		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			this.SuspendLayout();
			base.ForeColor = TextRenderColor;
			this.ResumeLayout();

			base.OnPaint(pe);

		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			var cea = new CancelEventArgs(false);
			
			if (SelectionChanging != null)
				SelectionChanging(this, cea);
			
			if (cea.Cancel)
				return;

			this.Selected = !Selected;
			if (SelectionChanged != null)
				SelectionChanged(this, e);
		}

		protected virtual void OnCheckChanged(bool selected)
		{
			OnIconChanged();
			Invalidate();
		}
	}
}
