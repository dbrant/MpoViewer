using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using MpoViewer.Properties;

namespace MpoViewer
{
    public partial class Form1 : Form
    {
        private List<Image> images = new List<Image>();
        private string FileName = "";

        public Form1(string[] args)
        {
            InitializeComponent();
            this.Text = Application.ProductName;
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
            openDlg.Title = Resources.openDlgTitle;
            openDlg.Filter = Resources.openDlgFilter;
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

        private void OpenMPO(string fileName)
        {
            FileName = fileName;
            images.Clear();
            cbImage.Items.Clear();

            try
            {
                Cursor = Cursors.WaitCursor;
                images = MpoImage.GetMpoImages(fileName);
                for (int i = 0; i < images.Count; i++)
                {
                    cbImage.Items.Add((i + 1).ToString());
                }

                if (images.Count == 0)
                {
                    pictureBox.Image = null;
                    MessageBox.Show(this, Resources.errorInvalidMpo, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                Cursor = Cursors.Default;
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
                saveDlg.Title = Resources.saveDlgTitle;
                saveDlg.Filter = Resources.saveDlgFilter;
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
            showImage(cbImage.SelectedIndex);
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
            {
                timerCycle.Interval -= 25;
            }
        }

        private void btnCycle_Click(object sender, EventArgs e)
        {
            timerCycle.Enabled = !timerCycle.Enabled;
        }
        
        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, Resources.aboutText, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblImage.Enabled = cbImage.Enabled = btnCycle.Enabled = btnFaster.Enabled = btnSlower.Enabled = (cbMode.SelectedIndex == 0);
            
            if (cbMode.SelectedIndex == 0)
            {
                showImage(cbImage.SelectedIndex);
            }
            else if (cbMode.SelectedIndex == 1)
            {
                pictureBox.Image = buildStereoImage(false);
            }
            else if (cbMode.SelectedIndex == 2)
            {
                pictureBox.Image = buildStereoImage(true);
            }
        }

        private void showImage(int index)
        {
            try
            {
                pictureBox.Image = images[index];
            }
            catch
            {
                // doesn't matter if this fails...
            }
        }

        private Image buildStereoImage(bool order)
        {
            if (images.Count == 0) return null;
            int totalWidth = 0, totalHeight = 0;

            foreach (var img in images)
            {
                totalWidth += img.Width;
                if (img.Height > totalHeight)
                    totalHeight = img.Height;
            }

            Image stereoImage = new Bitmap(totalWidth, totalHeight);
            var g = Graphics.FromImage(stereoImage);

            int curX = 0;
            int imgIndex = order ? 0 : images.Count - 1;
            for (int i = 0; i < images.Count; i++)
            {
                g.DrawImage(images[imgIndex], new Rectangle(curX, 0, images[imgIndex].Width, totalHeight), new Rectangle(0, 0, images[imgIndex].Width, images[imgIndex].Height), GraphicsUnit.Pixel);
                curX += images[imgIndex].Width;
                imgIndex += (order ? 1 : -1);
            }
            return stereoImage;
        }

    }
}
