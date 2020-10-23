using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
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
        private bool isSaved = true;
        private bool isOpeningFile = false;
        private string textFileName = "Chưa có tên file";
        private const string textTitleName = "* - Notepad Plus v1.0";
        private const string textTitleNameNotStar = " - Notepad Plus v1.0";
        private int ln = 1;
        private int col = 1;
        PrintDocument printDocument = new PrintDocument();
        PrintDialog printDialog = new PrintDialog();

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
            lbLn.Location = new Point(sz1 - 150, 5); // location theo panel
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
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(txt.Text, new Font("Arial", 20, FontStyle.Regular), Brushes.Black, 20, 20);
        }

        #endregion

        #region event
        private void Txt_TextChanged(object sender, EventArgs e)
        {
            // is Save changed
            if (txt.Text == "")
            {
                if (isOpeningFile == true)
                {
                    isSaved = false;
                }
                this.Text = textFileName + textTitleNameNotStar;
            }
            else
            {
                isSaved = true;
                this.Text = textFileName + textTitleName;
            }

            // count how many line & column
            ln = txt.Lines.Length;
            if (ln == 0)
            {
                setLineColText(1, 1);
                return;
            }
            col = txt.Lines[ln - 1].Length + 1;
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
        private void setLineColText(int line, int column)
        {
            lbLn.Text = "Ln : " + line.ToString();
            lbCol.Text = "Col : " + column.ToString();
        }

        private string getNameFileFromUrl(string url)
        {
            var crt = @"\";
            var idx = url.LastIndexOf(crt);
            if (idx == -1)
            {
                return "";
            }
            else
            {
                return url.Substring(idx + 1, url.Length - idx - 1);
            }
        }
        #endregion

        #region Tùy chọn
        private void fileMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isSaved == false)
            {
                var btn = MessageBoxButtons.YesNoCancel;
                var img = MessageBoxIcon.Question;
                var title = "Thông báo";
                var msg = "Bạn muốn lưu lại file?";
                var res = MessageBox.Show(msg, title, btn, img);
                if (res == DialogResult.Yes) // save
                {
                    lưuToolStripMenuItem_Click(sender, e);
                    textFileName = "Chưa có tên file";
                    this.Text = textFileName + textTitleNameNotStar;
                    this.setLineColText(1, 1);
                    txt.Text = "";
                }
                else if (res == DialogResult.No) // don't save
                {
                    textFileName = "Chưa có tên file";
                    this.Text = textFileName + textTitleNameNotStar;
                    this.setLineColText(1, 1);
                    txt.Text = "";
                }
                else // Cancel
                {
                    // nothing
                }
            }
            else
            {
                textFileName = "Chưa có tên file";
                this.Text = textFileName + textTitleNameNotStar;
                this.setLineColText(1, 1);
                txt.Text = "";
            }
        }

        private void cửaSổMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mởToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Application.StartupPath + @"\";
            saveFileDialog.Title = "Lưu file";
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Text File (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, txt.Text);
                textFileName = getNameFileFromUrl(saveFileDialog.FileName);
                this.Text = textFileName + textTitleNameNotStar;
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // update status file saved
            isSaved = true;
        }

        private void lưuDướiDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void inToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isSaved == false)
            {
                var btn = MessageBoxButtons.YesNoCancel;
                var img = MessageBoxIcon.Question;
                var title = "Thông báo";
                var msg = "Bạn muốn lưu lại file?";
                var res = MessageBox.Show(msg, title, btn, img);
                if (res == DialogResult.Yes) // save
                {
                    lưuToolStripMenuItem_Click(sender, e);
                }
                isSaved = true;
            }
            this.Close();
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
