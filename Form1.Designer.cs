namespace MpoViewer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cbMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lblImage = new System.Windows.Forms.ToolStripLabel();
            this.cbImage = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCycle = new System.Windows.Forms.ToolStripButton();
            this.btnSlower = new System.Windows.Forms.ToolStripButton();
            this.btnFaster = new System.Windows.Forms.ToolStripButton();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.timerCycle = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSave});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(144, 26);
            // 
            // mnuSave
            // 
            this.mnuSave.Image = ((System.Drawing.Image)(resources.GetObject("mnuSave.Image")));
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(143, 22);
            this.mnuSave.Text = "Save image...";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBox.Location = new System.Drawing.Point(12, 34);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(664, 383);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 7;
            this.pictureBox.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.cbMode,
            this.toolStripSeparator3,
            this.lblImage,
            this.cbImage,
            this.toolStripSeparator2,
            this.btnCycle,
            this.btnSlower,
            this.btnFaster,
            this.btnAbout});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(688, 30);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnOpen
            // 
            this.btnOpen.AutoToolTip = false;
            this.btnOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(66, 27);
            this.btnOpen.Text = "Open...";
            this.btnOpen.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(41, 27);
            this.toolStripLabel2.Text = "Mode:";
            // 
            // cbMode
            // 
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbMode.Items.AddRange(new object[] {
            "Single",
            "Stereo (cross-eye)",
            "Stereo (relaxed-eye)"});
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(130, 30);
            this.cbMode.SelectedIndexChanged += new System.EventHandler(this.cbMode_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 30);
            // 
            // lblImage
            // 
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(43, 27);
            this.lblImage.Text = "Image:";
            // 
            // cbImage
            // 
            this.cbImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cbImage.Name = "cbImage";
            this.cbImage.Size = new System.Drawing.Size(75, 30);
            this.cbImage.SelectedIndexChanged += new System.EventHandler(this.cbImage_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // btnCycle
            // 
            this.btnCycle.AutoToolTip = false;
            this.btnCycle.CheckOnClick = true;
            this.btnCycle.Image = ((System.Drawing.Image)(resources.GetObject("btnCycle.Image")));
            this.btnCycle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCycle.Name = "btnCycle";
            this.btnCycle.Size = new System.Drawing.Size(56, 27);
            this.btnCycle.Text = "Cycle";
            this.btnCycle.Click += new System.EventHandler(this.btnCycle_Click);
            // 
            // btnSlower
            // 
            this.btnSlower.AutoToolTip = false;
            this.btnSlower.Image = ((System.Drawing.Image)(resources.GetObject("btnSlower.Image")));
            this.btnSlower.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSlower.Name = "btnSlower";
            this.btnSlower.Size = new System.Drawing.Size(62, 27);
            this.btnSlower.Text = "Slower";
            this.btnSlower.Click += new System.EventHandler(this.btnSlower_Click);
            // 
            // btnFaster
            // 
            this.btnFaster.AutoToolTip = false;
            this.btnFaster.Image = ((System.Drawing.Image)(resources.GetObject("btnFaster.Image")));
            this.btnFaster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFaster.Name = "btnFaster";
            this.btnFaster.Size = new System.Drawing.Size(58, 27);
            this.btnFaster.Text = "Faster";
            this.btnFaster.Click += new System.EventHandler(this.btnFaster_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnAbout.AutoToolTip = false;
            this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
            this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(69, 27);
            this.btnAbout.Text = "About...";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // timerCycle
            // 
            this.timerCycle.Interval = 250;
            this.timerCycle.Tick += new System.EventHandler(this.timerCycle_Tick);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 429);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblImage;
        private System.Windows.Forms.ToolStripComboBox cbImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnCycle;
        private System.Windows.Forms.ToolStripButton btnSlower;
        private System.Windows.Forms.ToolStripButton btnFaster;
        private System.Windows.Forms.Timer timerCycle;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cbMode;
    }
}

