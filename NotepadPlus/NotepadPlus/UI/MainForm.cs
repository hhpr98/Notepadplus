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
        private Panel pn;
        private Label lbLn;
        private Label lbCol;
        private bool isSaved = false;
        private int ln = 1;
        private int col = 1;

        public MainForm()
        {
            InitializeComponent();

            // add textbox to components
            txt = new TextBox();
            txt.AutoSize = false;
            int sz1 = this.Size.Width;
            int sz2 = this.Size.Height;
            txt.Size = new Size(sz1, sz2 - 24 - 65); // 65 là phần info (footer) dưới cùng
            txt.Location = new Point(0, 24);
            txt.Multiline = true;
            txt.Font = new Font("Time New Roman", 11F, FontStyle.Regular);
            txt.BorderStyle = BorderStyle.None;
            this.Controls.Add(txt);

            // add a panel to footer
            pn = new Panel();
            pn.Size = new Size(sz1, 30);
            pn.Location = new Point(0, sz2 - 65);
            pn.BackColor = Color.Khaki;
            this.Controls.Add(pn);

            lbLn = new Label();
            lbLn.Text = "Ln : " + ln.ToString();
            lbLn.Font = new Font("Arial", 11F, FontStyle.Regular);
            lbLn.Location = new Point(sz1-200,5); // location theo panel
            lbLn.Size = new Size(100, 25);
            pn.Controls.Add(lbLn);

            lbCol = new Label();
            lbCol.Text = "Col : " + col.ToString();
            lbCol.Font = new Font("Arial", 11F, FontStyle.Regular);
            lbCol.Location = new Point(sz1 - 100, 5); // location theo panel
            lbCol.Size = new Size(100, 25);
            pn.Controls.Add(lbCol);



            // add event
            this.SizeChanged += MainForm_SizeChanged;
            txt.TextChanged += Txt_TextChanged;
            // ???? // event scroll bar
        }

        #region event
        private void Txt_TextChanged(object sender, EventArgs e)
        {
            // count how many line & column
            ln = txt.Lines.Length;
            if (ln == 0)
            {
                lbLn.Text = "Ln : 1";
                lbCol.Text = "Col : 1";
                return;
            }
            lbLn.Text = "Ln : " + ln.ToString();
            col = txt.Lines[ln-1].Length + 1;
            lbCol.Text = "Col : " + col.ToString();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            int sz1 = this.Size.Width;
            int sz2 = this.Size.Height;
            txt.Size = new Size(sz1, sz2 - 24 - 65);
            pn.Size = new Size(sz1, 30);
            pn.Location = new Point(0, sz2 - 65);
            lbLn.Location = new Point(sz1 - 200, 5);
            lbCol.Location = new Point(sz1 - 100, 5);

        }
        #endregion
    }
}
