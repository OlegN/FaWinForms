using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FaControls;

namespace FaTestForm
{
	public partial class FaForm : Form
	{
		public FaForm()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.tbMic.Selected = true;
		}

		private void faTabButton2_SelectionChanged(object sender, EventArgs e)
		{
			foreach (var c in panel1.Controls)
			{
				if (c is FaTabButton && c != sender)
					((FaTabButton)c).Selected = false;
			}
		}

		private void faSelectButton1_SelectionChanged(object sender, EventArgs e)
		{
			var c = ((FaSelectButton)sender);
			c.IconSymbol = c.Selected ? "f131" : "f130";
			c.Text = c.Selected ? "Stop" : "Record";
		}

		private void faButton5_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
