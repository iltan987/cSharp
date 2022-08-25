namespace Migros.Forms
{
    partial class SiparisForm
    {
        System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbIslemTarihi = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnVazgec = new System.Windows.Forms.Button();
            this.mtbSipNo = new System.Windows.Forms.MaskedTextBox();
            this.nudPuan = new System.Windows.Forms.NumericUpDown();
            this.nudKullanilan = new System.Windows.Forms.NumericUpDown();
            this.dtpIslemTarihi = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvSiparisler = new System.Windows.Forms.DataGridView();
            this.cId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSipNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPuan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cKullanilan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cIslemTarihi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.yazdırToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelMenuItem = new Migros.LabelMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPuan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKullanilan)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiparisler)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(767, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(300, 527);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cbIslemTarihi, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnNew, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.btnDelete, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.btnVazgec, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.mtbSipNo, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.nudPuan, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.nudKullanilan, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.dtpIslemTarihi, 1, 4);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 165);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 9;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(300, 282);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sipariş No:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Puan:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Kullanılan:";
            // 
            // cbIslemTarihi
            // 
            this.cbIslemTarihi.AutoSize = true;
            this.cbIslemTarihi.Checked = true;
            this.cbIslemTarihi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel2.SetColumnSpan(this.cbIslemTarihi, 2);
            this.cbIslemTarihi.Enabled = false;
            this.cbIslemTarihi.Location = new System.Drawing.Point(3, 87);
            this.cbIslemTarihi.Name = "cbIslemTarihi";
            this.cbIslemTarihi.Size = new System.Drawing.Size(151, 20);
            this.cbIslemTarihi.TabIndex = 1;
            this.cbIslemTarihi.Text = "Otomatik İşlem Tarihi";
            this.cbIslemTarihi.UseVisualStyleBackColor = true;
            this.cbIslemTarihi.CheckedChanged += new System.EventHandler(this.cbIslemTarihi_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "İşlem Tarihi:";
            this.label4.Visible = false;
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.btnNew, 2);
            this.btnNew.Location = new System.Drawing.Point(0, 141);
            this.btnNew.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(307, 30);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "Yeni Sipariş";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.btnSave, 2);
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(0, 177);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(307, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.btnDelete, 2);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(0, 213);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(307, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Siparişi Sil";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnVazgec
            // 
            this.btnVazgec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.btnVazgec, 2);
            this.btnVazgec.Enabled = false;
            this.btnVazgec.Location = new System.Drawing.Point(0, 249);
            this.btnVazgec.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnVazgec.Name = "btnVazgec";
            this.btnVazgec.Size = new System.Drawing.Size(307, 30);
            this.btnVazgec.TabIndex = 2;
            this.btnVazgec.Text = "Vazgeç";
            this.btnVazgec.UseVisualStyleBackColor = true;
            this.btnVazgec.Click += new System.EventHandler(this.btnVazgec_Click);
            // 
            // mtbSipNo
            // 
            this.mtbSipNo.Enabled = false;
            this.mtbSipNo.Location = new System.Drawing.Point(85, 3);
            this.mtbSipNo.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.mtbSipNo.Mask = "\\S000000";
            this.mtbSipNo.Name = "mtbSipNo";
            this.mtbSipNo.Size = new System.Drawing.Size(200, 22);
            this.mtbSipNo.TabIndex = 3;
            this.mtbSipNo.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // nudPuan
            // 
            this.nudPuan.Enabled = false;
            this.nudPuan.Location = new System.Drawing.Point(85, 31);
            this.nudPuan.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.nudPuan.Name = "nudPuan";
            this.nudPuan.Size = new System.Drawing.Size(200, 22);
            this.nudPuan.TabIndex = 4;
            this.nudPuan.Enter += new System.EventHandler(this.nuds_Enter);
            this.nudPuan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.nuds_MouseDown);
            // 
            // nudKullanilan
            // 
            this.nudKullanilan.Enabled = false;
            this.nudKullanilan.Location = new System.Drawing.Point(85, 59);
            this.nudKullanilan.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.nudKullanilan.Name = "nudKullanilan";
            this.nudKullanilan.Size = new System.Drawing.Size(200, 22);
            this.nudKullanilan.TabIndex = 4;
            this.nudKullanilan.Enter += new System.EventHandler(this.nuds_Enter);
            this.nudKullanilan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.nuds_MouseDown);
            // 
            // dtpIslemTarihi
            // 
            this.dtpIslemTarihi.CustomFormat = "dd.MM.yyyy";
            this.dtpIslemTarihi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpIslemTarihi.Location = new System.Drawing.Point(85, 113);
            this.dtpIslemTarihi.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.dtpIslemTarihi.Name = "dtpIslemTarihi";
            this.dtpIslemTarihi.Size = new System.Drawing.Size(200, 22);
            this.dtpIslemTarihi.TabIndex = 5;
            this.dtpIslemTarihi.Visible = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 3);
            this.tableLayoutPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(300, 85);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(116, 3);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "T. Puan: ";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(124, 25);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "T. TL: ";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 47);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "T. Kullanılan: ";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(123, 69);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Kalan: ";
            // 
            // dgvSiparisler
            // 
            this.dgvSiparisler.AllowUserToAddRows = false;
            this.dgvSiparisler.AllowUserToDeleteRows = false;
            this.dgvSiparisler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSiparisler.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cId,
            this.cSipNo,
            this.cPuan,
            this.cTL,
            this.cKullanilan,
            this.cIslemTarihi});
            this.dgvSiparisler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSiparisler.Location = new System.Drawing.Point(0, 27);
            this.dgvSiparisler.Name = "dgvSiparisler";
            this.dgvSiparisler.ReadOnly = true;
            this.dgvSiparisler.RowHeadersVisible = false;
            this.dgvSiparisler.RowHeadersWidth = 51;
            this.dgvSiparisler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSiparisler.Size = new System.Drawing.Size(767, 527);
            this.dgvSiparisler.TabIndex = 1;
            // 
            // cId
            // 
            this.cId.HeaderText = "Id";
            this.cId.MinimumWidth = 6;
            this.cId.Name = "cId";
            this.cId.ReadOnly = true;
            this.cId.Visible = false;
            this.cId.Width = 125;
            // 
            // cSipNo
            // 
            this.cSipNo.HeaderText = "Sipariş No";
            this.cSipNo.MinimumWidth = 6;
            this.cSipNo.Name = "cSipNo";
            this.cSipNo.ReadOnly = true;
            this.cSipNo.Width = 125;
            // 
            // cPuan
            // 
            this.cPuan.HeaderText = "Puan";
            this.cPuan.MinimumWidth = 6;
            this.cPuan.Name = "cPuan";
            this.cPuan.ReadOnly = true;
            this.cPuan.Width = 125;
            // 
            // cTL
            // 
            this.cTL.HeaderText = "TL";
            this.cTL.MinimumWidth = 6;
            this.cTL.Name = "cTL";
            this.cTL.ReadOnly = true;
            this.cTL.Width = 125;
            // 
            // cKullanilan
            // 
            this.cKullanilan.HeaderText = "Kullanılan";
            this.cKullanilan.MinimumWidth = 6;
            this.cKullanilan.Name = "cKullanilan";
            this.cKullanilan.ReadOnly = true;
            this.cKullanilan.Width = 125;
            // 
            // cIslemTarihi
            // 
            this.cIslemTarihi.HeaderText = "İşlem Tarihi";
            this.cIslemTarihi.MinimumWidth = 6;
            this.cIslemTarihi.Name = "cIslemTarihi";
            this.cIslemTarihi.ReadOnly = true;
            this.cIslemTarihi.Width = 125;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yazdırToolStripMenuItem,
            this.labelMenuItem,
            this.toolStripTextBox1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1067, 27);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // yazdırToolStripMenuItem
            // 
            this.yazdırToolStripMenuItem.Name = "yazdırToolStripMenuItem";
            this.yazdırToolStripMenuItem.Size = new System.Drawing.Size(50, 23);
            this.yazdırToolStripMenuItem.Text = "Yazdır";
            this.yazdırToolStripMenuItem.Click += new System.EventHandler(this.yazdırToolStripMenuItem_Click);
            // 
            // labelMenuItem
            // 
            this.labelMenuItem.Name = "labelMenuItem";
            this.labelMenuItem.Size = new System.Drawing.Size(25, 20);
            this.labelMenuItem.Text = "Ara";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // SiparisForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.dgvSiparisler);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::Migros.Properties.Resources.icon;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SiparisForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Siparişler";
            this.Load += new System.EventHandler(this.SiparisForm_Load);
            this.Shown += new System.EventHandler(this.SiparisForm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPuan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKullanilan)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiparisler)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvSiparisler;
        private System.Windows.Forms.DataGridViewTextBoxColumn cId;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSipNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTL;
        private System.Windows.Forms.DataGridViewTextBoxColumn cKullanilan;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIslemTarihi;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbIslemTarihi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnVazgec;
        private System.Windows.Forms.MaskedTextBox mtbSipNo;
        private System.Windows.Forms.NumericUpDown nudPuan;
        private System.Windows.Forms.NumericUpDown nudKullanilan;
        private System.Windows.Forms.DateTimePicker dtpIslemTarihi;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem yazdırToolStripMenuItem;
        private LabelMenuItem labelMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
    }
}