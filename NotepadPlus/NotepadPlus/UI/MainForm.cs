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
        #region Load
        private TextBox txt;
        private Panel pn;
        private Label lbLn, lbCol, lblFont;
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
            txt.Size = new Size(sz1 - 15, sz2 - 24 - 65); // 65 là phần info (footer) dưới cùng
            txt.Location = new Point(0, 24);
            txt.Multiline = true;
            txt.Font = new Font("Time New Roman", 11F, FontStyle.Regular);
            txt.BorderStyle = BorderStyle.None;
            txt.ScrollBars = ScrollBars.Vertical;
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
            lbLn.Location = new Point(sz1-150,5); // location theo panel
            lbLn.Size = new Size(50, 25);
            pn.Controls.Add(lbLn);

            lbCol = new Label();
            lbCol.Text = "Col : " + col.ToString();
            lbCol.Font = new Font("Arial", 11F, FontStyle.Regular);
            lbCol.Location = new Point(sz1 - 100, 5); // location theo panel
            lbCol.Size = new Size(50, 25);
            pn.Controls.Add(lbCol);

            lblFont = new Label();
            lblFont.Text = "100% Windows(CRFL)  UTF-8";
            lblFont.Font = new Font("Arial", 11F, FontStyle.Regular);
            lblFont.Location = new Point(sz1 - 400, 5); // location theo panel
            lblFont.Size = new Size(250, 25);
            pn.Controls.Add(lblFont);

            // add event
            this.SizeChanged += MainForm_SizeChanged;
            txt.TextChanged += Txt_TextChanged;
        }

        #endregion

        #region event
        private void Txt_TextChanged(object sender, EventArgs e)
        {
            // count how many line & column
            ln = txt.Lines.Length;
            if (ln == 0)
            {
                setLineColText(1, 1);
                return;
            }
            col = txt.Lines[ln-1].Length + 1;
            setLineColText(ln, col);
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            int sz1 = this.Size.Width;
            int sz2 = this.Size.Height;
            txt.Size = new Size(sz1 - 15, sz2 - 24 - 65);
            pn.Size = new Size(sz1, 30);
            pn.Location = new Point(0, sz2 - 65);
            lbLn.Location = new Point(sz1 - 150, 5);
            lbCol.Location = new Point(sz1 - 100, 5);
            lblFont.Location = new Point(sz1 - 400, 5);
        }
        #endregion

        #region function
        private void setLineColText(int line,int column)
        {
            lbLn.Text = "Ln : " + line.ToString();
            lbCol.Text = "Col : " + column.ToString();
        }
        #endregion

        #region Tùy chọn
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Chỉnh sửa
        #endregion

        #region Định dạng
        #endregion

        #region View
        #endregion

        #region Trợ giúp
        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://github.com/hhpr98/Notepadplus");
        }

        private void gửiPhảnHồiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://github.com/hhpr98/Notepadplus/issues");
        }

        private void vềNotepadPlusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var title = "Thông báo";
            var message = "NotepadPlus v1.0 by Nguyễn Hữu Hòa.\nKhoa CNTT, trường Đại học KHTN HCM\nRelease : DD/MM/YYYY";
            var button = MessageBoxButtons.OK;
            var icon = MessageBoxIcon.Information;
            MessageBox.Show(message, title, button, icon);
        }
        #endregion
    }
}
