using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadPlus
{
    public partial class MainForm : Form
    {
        private TextBox txt;

        public MainForm()
        {
            InitializeComponent();

            // add textbox to components
            txt = new TextBox();
            txt.AutoSize = false;
            int sz1 = this.Size.Width;
            int sz2 = this.Size.Height;
            txt.Size = new Size(sz1, sz2-24);
            txt.Location = new Point(0, 24);
            txt.Multiline = true;
            txt.Font = new Font("Time New Roman", 11F, FontStyle.Regular);
            this.Controls.Add(txt);

            // add event
            this.SizeChanged += MainForm_SizeChanged;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            int sz1 = this.Size.Width;
            int sz2 = this.Size.Height;
            txt.Size = new Size(sz1, sz2 - 24);
        }
    }
}
