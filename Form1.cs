using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MpoViewer
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();
            Functions.FixDialogFont(this);
            this.Text = Application.ProductName;

            images = new List<Image>();

            cbMode.SelectedIndex = 0;

            if (args.Length > 0)
            {
                OpenMPO(args[0]);
                timerCycle.Enabled = true;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var openDlg = new OpenFileDialog();
            openDlg.DefaultExt = ".mpo";
            openDlg.CheckFileExists = true;
            openDlg.Title = "Open MPO file...";
            openDlg.Filter = "MPO Files (*.mpo)|*.mpo|All Files (*.*)|*.*";
            openDlg.FilterIndex = 1;
            if (openDlg.ShowDialog() == DialogResult.Cancel) return;
            OpenMPO(openDlg.FileName);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                e.Effect = DragDropEffects.All;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0) return;
            OpenMPO(files[0]);
        }


        private List<Image> images;
        private Image stereoImage = null;
        private string FileName = "";


        private void OpenMPO(string fileName)
        {
            FileName = fileName;
            images.Clear();
            cbImage.Items.Clear();

            try
            {
                this.Cursor = Cursors.WaitCursor;
                byte[] tempBytes = new byte[100];
                using (var f = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    tempBytes = new byte[f.Length];
                    f.Read(tempBytes, 0, (int)f.Length);
                }

                List<int> imageOffsets = new List<int>();
                int offset = 0, tempOffset = 0;
                byte[] keyBytes = { 0xFF, 0xD8, 0xFF, 0xE1 };
                byte[] keyBytes2 = { 0xFF, 0xD8, 0xFF, 0xE0 };

                while (true)
                {
                    tempOffset = Functions.SearchBytes(tempBytes, keyBytes, offset, tempBytes.Length);
                    if (tempOffset == -1)
                        tempOffset = Functions.SearchBytes(tempBytes, keyBytes2, offset, tempBytes.Length);
                    if (tempOffset == -1) break;
                    offset = tempOffset;
                    imageOffsets.Add(offset);
                    offset += 4;
                }

                for (int i = 0; i < imageOffsets.Count; i++)
                {
                    int length;
                    if (i < (imageOffsets.Count - 1))
                        length = imageOffsets[i + 1] - imageOffsets[i];
                    else
                        length = tempBytes.Length - imageOffsets[i];

                    MemoryStream stream = new MemoryStream(tempBytes, imageOffsets[i], length);
                    images.Add(new Bitmap(stream));
                    cbImage.Items.Add((i + 1).ToString());
                }

                if (images.Count == 0)
                {
                    pictureBox.Image = null;
                    MessageBox.Show(this, "This does not appear to be a valid MPO file.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    cbImage.SelectedIndex = 0;
                    cbMode_SelectedIndexChanged(null, null);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            if (images.Count == 0) return;
            try
            {
                var saveDlg = new SaveFileDialog();
                saveDlg.DefaultExt = ".jpg";
                saveDlg.OverwritePrompt = true;
                saveDlg.Title = "Save file...";
                saveDlg.Filter = "JPG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";
                saveDlg.FilterIndex = 1;
                saveDlg.InitialDirectory = Path.GetDirectoryName(FileName);
                saveDlg.FileName = Path.GetFileNameWithoutExtension(FileName) + "_" + (cbImage.SelectedIndex + 1).ToString() + ".jpg";
                if (saveDlg.ShowDialog() == DialogResult.Cancel) return;

                images[cbImage.SelectedIndex].Save(saveDlg.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox.Image = images[cbImage.SelectedIndex];
            }
            catch { }
        }

        private void timerCycle_Tick(object sender, EventArgs e)
        {
            if (cbImage.Items.Count == 0) return;
            if (cbMode.SelectedIndex == 1) return;

            int i = cbImage.SelectedIndex + 1;
            if (i >= cbImage.Items.Count) i = 0;
            cbImage.SelectedIndex = i;
        }

        private void btnSlower_Click(object sender, EventArgs e)
        {
            timerCycle.Interval += 25;
        }

        private void btnFaster_Click(object sender, EventArgs e)
        {
            if (timerCycle.Interval > 25)
                timerCycle.Interval -= 25;
        }

        private void btnCycle_Click(object sender, EventArgs e)
        {
            timerCycle.Enabled = !timerCycle.Enabled;
        }


        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, Application.ProductName + "\nCopyright © 2011-2015 by Dmitry Brant\n\nhttp://dmitrybrant.com", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblImage.Enabled = cbImage.Enabled = btnCycle.Enabled = btnFaster.Enabled = btnSlower.Enabled = (cbMode.SelectedIndex == 0);


            if (cbMode.SelectedIndex == 0)
            {
                cbImage_SelectedIndexChanged(null, null);
            }
            else if (cbMode.SelectedIndex == 1)
            {
                if (images.Count == 0) return;

                //rebuild the stereo image...
                int totalWidth = 0, totalHeight = 0;

                foreach (var img in images)
                {
                    totalWidth += img.Width;
                    if (img.Height > totalHeight)
                        totalHeight = img.Height;
                }

                stereoImage = new Bitmap(totalWidth, totalHeight);
                var g = Graphics.FromImage(stereoImage);

                int curX = 0;
                for (int i = images.Count - 1; i >= 0; i--)
                {
                    g.DrawImage(images[i], new Rectangle(curX, 0, images[i].Width, totalHeight), new Rectangle(0, 0, images[i].Width, images[i].Height), GraphicsUnit.Pixel);
                    curX += images[i].Width;
                }

                pictureBox.Image = stereoImage;
            }
            else if (cbMode.SelectedIndex == 2)
            {
                if (images.Count == 0) return;

                //rebuild the stereo image...
                int totalWidth = 0, totalHeight = 0;

                foreach (var img in images)
                {
                    totalWidth += img.Width;
                    if (img.Height > totalHeight)
                        totalHeight = img.Height;
                }

                stereoImage = new Bitmap(totalWidth, totalHeight);
                var g = Graphics.FromImage(stereoImage);

                int curX = 0;
                for (int i = 0; i < images.Count; i++)
                {
                    g.DrawImage(images[i], new Rectangle(curX, 0, images[i].Width, totalHeight), new Rectangle(0, 0, images[i].Width, images[i].Height), GraphicsUnit.Pixel);
                    curX += images[i].Width;
                }

                pictureBox.Image = stereoImage;
            }
        }

    }



    public class Functions
    {

        /// <summary>
        /// Sets the font of a given control, and all child controls, to
        /// the current system font, while preserving font styles.
        /// </summary>
        /// <param name="c0">Control whose font will be set.</param>
        public static void FixDialogFont(Control c0)
        {
            Font old = c0.Font;
            c0.Font = new Font(SystemFonts.MessageBoxFont.FontFamily.Name, old.Size, old.Style);
            if (c0.Controls.Count > 0)
                foreach (Control c in c0.Controls)
                    FixDialogFont(c);
        }


        /// <summary>
        /// Search an array of bytes for a byte pattern specified in another array.
        /// </summary>
        /// <param name="bytesToSearch">Array of bytes to search</param>
        /// <param name="matchBytes">Byte pattern to search for</param>
        /// <param name="startIndex">Starting index within the first array to start searching</param>
        /// <param name="count">Number of bytes in the first array to search</param>
        /// <returns>Zero-based index of the beginning of the byte pattern found in 
        /// the byte array, or -1 if not found.</returns>
        public static int SearchBytes(byte[] bytesToSearch, byte[] matchBytes, int startIndex, int count)
        {
            int ret = -1, max = count - matchBytes.Length + 1;
            bool found;
            for (int i = startIndex; i < max; i++)
            {
                found = true;
                for (int j = 0; j < matchBytes.Length; j++)
                {
                    if (bytesToSearch[i + j] != matchBytes[j]) { found = false; break; }
                }
                if (found) { ret = i; break; }
            }
            return ret;
        }
        public static int SearchBytes(byte[] bytesToSearch, string matchStr, int startIndex, int count)
        {
            byte[] matchBytes = Encoding.ASCII.GetBytes(matchStr);
            return SearchBytes(bytesToSearch, matchBytes, startIndex, count);
        }

        /// <summary>
        /// Compare two arrays of bytes.
        /// </summary>
        /// <param name="array1">First array to compare.</param>
        /// <param name="start1">Starting index in the first array to begin comparing.</param>
        /// <param name="array2">Second array to compare.</param>
        /// <param name="start2">Starting index in the second array to begin comparing.</param>
        /// <param name="count">Number of bytes to compare.</param>
        /// <returns>True if the bytes are identical, false otherwise.</returns>
        public static bool MemCmp(byte[] array1, int start1, byte[] array2, int start2, int count)
        {
            bool ret = true;
            for (int i = 0; i < count; i++)
            {
                if (array1[start1] != array2[start2]) { ret = false; break; }
                start1++; start2++;
            }
            return ret;
        }


    }

}
