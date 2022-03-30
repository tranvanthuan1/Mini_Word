using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace MiniWord_TranVanThuan
{
    public partial class MiniWord : Form
    {
        public MiniWord()
        {
            InitializeComponent();
            Load_Emoji();

        }
        private FileInfo currentFile = null;
        private FileInfo CurrentFile
        {
            get { return currentFile; }
            set
            {
                currentFile = value;
                FileChanged.Invoke(this, currentFile.FullName);
            }
        }
        public EventHandler<string> FileChanged;
        private List<string> fontList;
        private List<int> fontSizeList;


        private void MiniWord_Load(object sender, EventArgs e)
        {
            MiniWords font = new MiniWords();
            fontList = font.getFontFamilies();
            foreach (string item in fontList)
            {
                cmbFont.Items.Add(item);
            }

            fontSizeList = font.getFontSize();
            foreach (int item in fontSizeList)
            {
                cmbSize.Items.Add(item.ToString());
            }

            string fontName = rtxDocumento.SelectionFont.Name;
            float fontSize = rtxDocumento.SelectionFont.Size;
            setComboBoxSelectedIndex(fontName, fontSize);

        }
        private class MiniWords
        {
            private List<string> fontFamilies = new List<string>();
            private List<int> fontsizeList = new List<int>();

            public List<string> getFontFamilies()
            {
                fontFamilies = new List<string>();
                foreach (FontFamily family in FontFamily.Families)
                {
                    fontFamilies.Add(family.Name);
                }
                return fontFamilies;
            }

            public List<int> getFontSize()
            {
                fontsizeList = new List<int>();
                for (int i = 0; i < 100; i++)
                {
                    fontsizeList.Add(i + 1);
                }
                return fontsizeList;
            }
        }
        public void setComboBoxSelectedIndex(string fontName, float fontSize)
        {
            int fontNameIndex = cmbFont.Items.IndexOf(fontName);
            cmbFont.SelectedIndex = fontNameIndex;

            int fontSizeIndex = cmbSize.Items.IndexOf(fontSize.ToString());
            cmbSize.SelectedIndex = fontSizeIndex;
        }
        private void Save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|RTF Files (*.rtf)|*.rtf";
            saveFileDialog.Title = "Save";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {


                path = saveFileDialog.FileName;
                if (path != "")
                {
                    rtxDocumento.SaveFile(path);
                    
                }
            }
        }




        private void btnFontUp_Click(object sender, EventArgs e)
        {
            try
            {
                cmbSize.SelectedIndex = cmbSize.SelectedIndex + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmbSize.SelectedIndex = 1;
            }
        }

        private void btnFontDown_Click(object sender, EventArgs e)
        {
            try
            {
                cmbSize.SelectedIndex = cmbSize.SelectedIndex - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmbSize.SelectedIndex = 0;
            }
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            FontStyle stileFont;
            Font currentFont = rtxDocumento.SelectionFont;
            if (rtxDocumento.SelectionFont.Bold == true)
            {
                stileFont = FontStyle.Regular;
            }
            else
            {
                stileFont = FontStyle.Bold;
            }
            rtxDocumento.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, stileFont);
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            FontStyle stileFont;
            Font currentFont = rtxDocumento.SelectionFont;
            if (rtxDocumento.SelectionFont.Italic == true)
            {
                stileFont = FontStyle.Regular;
            }
            else
            {
                stileFont = FontStyle.Italic;
            }
            rtxDocumento.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, stileFont);
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            FontStyle stileFont;
            Font currentFont = rtxDocumento.SelectionFont;
            if (rtxDocumento.SelectionFont.Underline == true)
            {
                stileFont = FontStyle.Regular;
            }
            else
            {
                stileFont = FontStyle.Underline;
            }
            rtxDocumento.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, stileFont);
        }

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            ColorDialog myColor = new ColorDialog();
            myColor.Color = rtxDocumento.ForeColor;
            if (myColor.ShowDialog() == DialogResult.OK)
            {
                rtxDocumento.SelectionColor = myColor.Color;
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            rtxDocumento.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void btnCenter_Click(object sender, EventArgs e)
        {
            rtxDocumento.SelectionAlignment = HorizontalAlignment.Center;

        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            rtxDocumento.SelectionAlignment = HorizontalAlignment.Right;

        }

        private void btnBulletList_Click(object sender, EventArgs e)
        {
            if (rtxDocumento.SelectionBullet == true)
            {
                rtxDocumento.SelectionBullet = false;
            }
            else
            {
                rtxDocumento.SelectionBullet = true;
            }
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                rtxDocumento.SelectionFont = new Font(cmbFont.Text, Convert.ToInt32(cmbSize.Text));
            }
            catch (Exception)
            {
            }
        }
        private void cmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                rtxDocumento.SelectionFont = new Font(cmbFont.Text, Convert.ToInt32(cmbSize.Text));
            }
            catch (Exception)
            {
            }

        }
        private void BtnJustify_Click(object sender, EventArgs e)
        {
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileImage = new OpenFileDialog();
            openFileImage.Filter = "Images |*.bmp;*.jpg;*.png;*.gif;*.ico";
            openFileImage.Multiselect = false;
            openFileImage.FileName = "";
            DialogResult result = openFileImage.ShowDialog();
            if (result == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileImage.FileName);
                Clipboard.SetImage(img);
                rtxDocumento.Paste();
                rtxDocumento.Focus();
            }
            else
            {
                rtxDocumento.Focus();
            }
        }

        string path = "";
        private void BtnSaveDoc_Click(object sender, EventArgs e)
        {

            if (rtxDocumento.Text != "")
            {
                if (MessageBox.Show("Bạn có muốn lưu File không ?", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Save();

                }
                else
                {

                }
            }

        }

        private void BtnOpenDoc_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            path = openFileDialog.FileName;
            if (path == "")
            {
                return;
            }
            rtxDocumento.LoadFile(path);
            // opens existing text



        }

        private void BtnNewDoc_Click(object sender, EventArgs e)
        {
            rtxDocumento.Visible = true;
            rtxDocumento.Text = String.Empty;
        }

        private void btnLeftIndent_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem btn = (ToolStripMenuItem)sender;
            switch (btn.Name)
            {
                case "L10":
                    if (rtxDocumento.SelectionIndent == 10)
                    {
                        rtxDocumento.SelectionIndent = 0;
                    }
                    else
                    {
                        rtxDocumento.SelectionIndent = 10;
                    }
                    break;
                case "L15":
                    if (rtxDocumento.SelectionIndent == 15)
                    {
                        rtxDocumento.SelectionIndent = 0;
                    }
                    else
                    {
                        rtxDocumento.SelectionIndent = 15;
                    }
                    break;
                case "L25":
                    if (rtxDocumento.SelectionIndent == 25)
                    {
                        rtxDocumento.SelectionIndent = 0;
                    }
                    else
                    {
                        rtxDocumento.SelectionIndent = 25;
                    }
                    break;
                case "L50":
                    if (rtxDocumento.SelectionIndent == 50)
                    {
                        rtxDocumento.SelectionIndent = 0;
                    }
                    else
                    {
                        rtxDocumento.SelectionIndent = 50;
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnRigthIndent_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem btn = (ToolStripMenuItem)sender;
            switch (btn.Name)
            {
                case "R10":
                    if (rtxDocumento.SelectionRightIndent == 10)
                    {
                        rtxDocumento.SelectionRightIndent = 0;
                    }
                    else
                    {
                        rtxDocumento.SelectionRightIndent = 10;
                    }
                    break;
                case "R15":
                    if (rtxDocumento.SelectionRightIndent == 15)
                    {
                        rtxDocumento.SelectionRightIndent = 0;
                    }
                    else
                    {
                        rtxDocumento.SelectionRightIndent = 15;
                    }
                    break;
                case "R25":
                    if (rtxDocumento.SelectionRightIndent == 25)
                    {
                        rtxDocumento.SelectionRightIndent = 0;
                    }
                    else
                    {
                        rtxDocumento.SelectionRightIndent = 25;
                    }
                    break;
                case "R50":
                    if (rtxDocumento.SelectionRightIndent == 50)
                    {
                        rtxDocumento.SelectionRightIndent = 0;
                    }
                    else
                    {
                        rtxDocumento.SelectionRightIndent = 50;
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            if (rtxDocumento.ZoomFactor < 64.5)
            {
                rtxDocumento.ZoomFactor = rtxDocumento.ZoomFactor + (float)0.5;
            }
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            if (rtxDocumento.ZoomFactor > 0.515625)
            {
                rtxDocumento.ZoomFactor = rtxDocumento.ZoomFactor + (float)-0.5;
            }
        }

        private void rtxDocumento_TextChanged(object sender, EventArgs e)
        {
            if(rtxDocumento.Text != null)
            {
                this.Text = "MiniWord";
            }
            else
            {
                this.Text = "MiniWord*";
            }


        }

        private void BtnHightLight_Click(object sender, EventArgs e)
        {
            ColorDialog colorDiag = new ColorDialog();
            if (colorDiag.ShowDialog() == DialogResult.OK)
            {
                rtxDocumento.SelectionBackColor = colorDiag.Color;
            }
        }
        private void Load_Emoji()
        {
            string duongDan = Environment.CurrentDirectory.ToString();
            var url = Directory.GetParent(Directory.GetParent(duongDan).ToString());
            string path = url + @"\icons";
            string[] files = Directory.GetFiles(path);
            foreach (String f in files)
            {
                Image img = Image.FromFile(f);
                imageList1.Images.Add(img);
            }

            this.listView1.LargeImageList = this.imageList1;

            for (int i = 0; i < this.imageList1.Images.Count; i++)
            {
                this.listView1.Items.Add(" ", i);
            }

        }
        private void btnEmoji_Click(object sender, EventArgs e)
        {
            if (listView1.Visible == false)
            {
                listView1.Visible = true;
            }
            else
            {
                listView1.Visible = false;
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            rtxDocumento.Paste();
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            rtxDocumento.Copy();
        }

        private void BtnCut_Click(object sender, EventArgs e)
        {
            rtxDocumento.Cut();
        }

        private void BtnSaveAs_Click(object sender, EventArgs e)
        {
            // Save documents Document data as.
            if (rtxDocumento.Text != "")
            {
                if (MessageBox.Show("Bạn có muốn lưu File không ?", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Save();

                }
                else
                {

                }
            }
        }
        int id = 0;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedIndices.Count <= 0) return;
                if (listView1.FocusedItem == null) return;
                id = listView1.SelectedIndices[0];
                if (id < 0) return;
                Clipboard.SetImage(imageList1.Images[id]);
                rtxDocumento.Paste();
                listView1.Visible = false;
            }
            catch
            {

            }

        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            rtxDocumento.Undo();
        }

        private void BtnRedo_Click(object sender, EventArgs e)
        {
            rtxDocumento.Redo();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
         //   pnlChinh.Enabled = true;
            rtxDocumento.Visible = true;
            rtxDocumento.Text = String.Empty;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            path = openFileDialog.FileName;
            if (path == "")
            {
                return;
            }
            rtxDocumento.LoadFile(path);
            // opens existing text

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtxDocumento.Text != "")
            {
                if (MessageBox.Show("Bạn có muốn lưu File không ?", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Save();
                    
                }
                else
                {
                    
                }
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Bạn có chắc muốn thoát không?", "Error", MessageBoxButtons.OKCancel);
            if (exit == DialogResult.OK)
                Application.Exit();

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxDocumento.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxDocumento.Paste();

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxDocumento.Cut();
        }

        private void infoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Trường Đại học Kiến Trúc Đà Nẵng\r\nKhoa Công Nghệ Thông Tin\r\nTrần Văn Thuần\r\nLớp 18CT4", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) => rtxDocumento.SelectAll();

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindReplace frm = new FindReplace(rtxDocumento, this);
            frm.Show();
        }

        private void listView1_MouseLeave(object sender, EventArgs e)
        {
            listView1.Visible = false;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Visible = false;
       
            rtxDocumento.Visible = false;
      
           
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Bạn có muốn lưu không?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.ShowDialog();
                path = saveFileDialog.FileName;
                if (path == "")
                {
                    return;
                }
                rtxDocumento.SaveFile(path);

            }

        }
    }
}
