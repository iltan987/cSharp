using System.ComponentModel;

namespace YouthUp.Forms
{
    partial class MainForm
    {
        IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.placeholderTB1 = new PlaceholderTB.PlaceholderTB1();
            this.placeholderTB2 = new PlaceholderTB.PlaceholderTB1();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.downloadAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeAllMP4MP3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mP3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mP4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanQueueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enumerateFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.placeholderTB1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.placeholderTB2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(971, 547);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // placeholderTB1
            // 
            this.placeholderTB1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.placeholderTB1.Location = new System.Drawing.Point(3, 3);
            this.placeholderTB1.Name = "placeholderTB1";
            this.placeholderTB1.Size = new System.Drawing.Size(930, 26);
            this.placeholderTB1.TabIndex = 2;
            this.placeholderTB1.WaterMark = "Enter Youtube URL here...";
            this.placeholderTB1.WaterMarkActiveForeColor = System.Drawing.Color.Gray;
            this.placeholderTB1.WaterMarkFont = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.placeholderTB1.WaterMarkForeColor = System.Drawing.Color.LightGray;
            // 
            // placeholderTB2
            // 
            this.placeholderTB2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.placeholderTB2.Location = new System.Drawing.Point(3, 518);
            this.placeholderTB2.Name = "placeholderTB2";
            this.placeholderTB2.ReadOnly = true;
            this.placeholderTB2.Size = new System.Drawing.Size(930, 26);
            this.placeholderTB2.TabIndex = 0;
            this.placeholderTB2.WaterMark = "Select Folder to Save";
            this.placeholderTB2.WaterMarkActiveForeColor = System.Drawing.Color.Gray;
            this.placeholderTB2.WaterMarkFont = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.placeholderTB2.WaterMarkForeColor = System.Drawing.Color.LightGray;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::YouthUp.Properties.Resources.plus;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(939, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 26);
            this.button1.TabIndex = 3;
            this.button1.TabStop = false;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(939, 518);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 26);
            this.button2.TabIndex = 1;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(965, 477);
            this.panel1.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadAllToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(971, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // downloadAllToolStripMenuItem
            // 
            this.downloadAllToolStripMenuItem.Image = global::YouthUp.Properties.Resources.download;
            this.downloadAllToolStripMenuItem.Name = "downloadAllToolStripMenuItem";
            this.downloadAllToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.downloadAllToolStripMenuItem.Text = "Download";
            this.downloadAllToolStripMenuItem.Click += new System.EventHandler(this.downloadAllToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeAllMP4MP3ToolStripMenuItem,
            this.cleanQueueToolStripMenuItem,
            this.enumerateFilesToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // makeAllMP4MP3ToolStripMenuItem
            // 
            this.makeAllMP4MP3ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mP3ToolStripMenuItem,
            this.mP4ToolStripMenuItem});
            this.makeAllMP4MP3ToolStripMenuItem.Name = "makeAllMP4MP3ToolStripMenuItem";
            this.makeAllMP4MP3ToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.makeAllMP4MP3ToolStripMenuItem.Text = "Make All MP4/MP3";
            // 
            // mP3ToolStripMenuItem
            // 
            this.mP3ToolStripMenuItem.Name = "mP3ToolStripMenuItem";
            this.mP3ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.mP3ToolStripMenuItem.Text = "MP3";
            this.mP3ToolStripMenuItem.Click += new System.EventHandler(this.mP3ToolStripMenuItem_Click);
            // 
            // mP4ToolStripMenuItem
            // 
            this.mP4ToolStripMenuItem.Name = "mP4ToolStripMenuItem";
            this.mP4ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.mP4ToolStripMenuItem.Text = "MP4";
            this.mP4ToolStripMenuItem.Click += new System.EventHandler(this.mP4ToolStripMenuItem_Click);
            // 
            // cleanQueueToolStripMenuItem
            // 
            this.cleanQueueToolStripMenuItem.Name = "cleanQueueToolStripMenuItem";
            this.cleanQueueToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.cleanQueueToolStripMenuItem.Text = "Clean Queue";
            this.cleanQueueToolStripMenuItem.Click += new System.EventHandler(this.cleanQueueToolStripMenuItem_Click);
            // 
            // enumerateFilesToolStripMenuItem
            // 
            this.enumerateFilesToolStripMenuItem.Name = "enumerateFilesToolStripMenuItem";
            this.enumerateFilesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.enumerateFilesToolStripMenuItem.Text = "Enumerate Files";
            this.enumerateFilesToolStripMenuItem.Click += new System.EventHandler(this.enumerateFilesToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(971, 571);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::YouthUp.Properties.Resources.youtube;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(610, 377);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YouthUp";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private PlaceholderTB.PlaceholderTB1 placeholderTB1;
        private PlaceholderTB.PlaceholderTB1 placeholderTB2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeAllMP4MP3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mP3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mP4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanQueueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enumerateFilesToolStripMenuItem;
    }
}