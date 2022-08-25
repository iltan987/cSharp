using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Migros.Forms
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        List<Cari> cariler;

        ulong maxCariNo;

        private void MainForm_Load(object sender, EventArgs e)
        {
            Settings.Read();

            searchTimer = new System.Threading.Timer(SearchTimerCallBack);

            cCariNo.DefaultCellStyle.Format = mtbCariNo.Mask = Globals.settings.cariNoFormat;

            cariler = Globals.database.GetCariler();
            foreach (var item in cariler)
                dgvCariler.Rows.Add(item.Id, item.CariNo, item.Isim, item.KartNo);

            maxCariNo = cariler.Select(f => f.CariNo).DefaultIfEmpty(0ul).Max();

            dgvCariler.Sort(cIsim, ListSortDirection.Ascending);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            dgvCariler.ClearSelection();
            dgvCariler.SelectionChanged += dgvCariler_SelectionChanged;
            dgvCariler.CellDoubleClick += dgvCariler_CellDoubleClick;
        }

        private void dgvCariler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCariler.SelectedRows.Count == 1)
            {
                var cells = dgvCariler.SelectedRows[0].Cells;

                mtbCariNo.Text = ((ulong)cells[nameof(cCariNo)].Value).ToString(mtbCariNo.Mask);
                tbIsim.Text = (string)cells[nameof(cIsim)].Value;
                tbKartNo.Text = (string)cells[nameof(cKartNo)].Value;

                mtbCariNo.Enabled = tbIsim.Enabled = tbKartNo.Enabled = true;
                btnSave.Enabled = btnDelete.Enabled = true;


                UpdateToplamlar((ulong)cells[nameof(cId)].Value);
                tableLayoutPanel3.Visible = true;
            }
            else
            {
                mtbCariNo.Enabled = tbIsim.Enabled = tbKartNo.Enabled = false;
                btnSave.Enabled = btnDelete.Enabled = false;
                mtbCariNo.Clear();
                tbIsim.Clear();
                tbKartNo.Clear();
                tableLayoutPanel3.Visible = false;
            }
        }

        private void UpdateToplamlar(ulong Id)
        {
            Cari cari = cariler.Find(f => f.Id == Id);
            cari.GetSiparisler();
            var toplamlar = cari.GetToplamlar();
            label4.Text = "T. Puan: " + toplamlar.tPuan;
            label5.Text = "T. TL: " + toplamlar.tTL.ToString(Globals.settings.tlFormat);
            label6.Text = "T. Kullanılan: " + toplamlar.tKullanilan.ToString(Globals.settings.tlFormat);
            label7.Text = "Kalan: " + toplamlar.kalan.ToString(Globals.settings.tlFormat);
        }

        private void dgvCariler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            Hide();
            ulong Id = (ulong)dgvCariler.SelectedRows[0].Cells[nameof(cId)].Value;
            new SiparisForm(cariler.Find(f => f.Id == Id)).ShowDialog();
            UpdateToplamlar(Id);
            Show();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dgvCariler.Enabled = false;
            mtbCariNo.Enabled = tbIsim.Enabled = tbKartNo.Enabled = true;
            btnNew.Enabled = btnDelete.Enabled = menuStrip1.Enabled = false;
            btnSave.Enabled = btnVazgec.Enabled = true;
            tbIsim.Select();
            mtbCariNo.Text = (maxCariNo + 1).ToString(mtbCariNo.Mask);
            tbIsim.Clear();
            tbKartNo.Clear();
            dgvCariler.SelectionChanged -= dgvCariler_SelectionChanged;
            int a = dgvCariler.Rows.Add(Globals.settings.lastCariId + 1, maxCariNo + 1, "", "");
            dgvCariler.ClearSelection();
            dgvCariler.Rows[a].Selected = true;
            dgvCariler.SelectionChanged += dgvCariler_SelectionChanged;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var rowCells = dgvCariler.SelectedRows[0].Cells;
            ulong id = (ulong)rowCells[nameof(cId)].Value;
            Cari cari;
            if ((cari = cariler.Find(f => f.Id == id)) == null)
            {
                cari = new Cari(id, default, default, default);
                cariler.Add(cari);
            }

            if (mtbCariNo.MaskCompleted)
            {
                ulong cariNo = ulong.Parse(mtbCariNo.Text);
                cari.CariNo = cariNo;
                rowCells[nameof(cCariNo)].Value = cariNo;
                if (cariNo > maxCariNo)
                    maxCariNo = cariNo;
            }
            else
                cari.CariNo = ++maxCariNo;
            cari.Isim = tbIsim.Text;
            cari.KartNo = tbKartNo.Text;
            cari.Save();
            rowCells[nameof(cIsim)].Value = tbIsim.Text;
            rowCells[nameof(cKartNo)].Value = tbKartNo.Text;

            btnNew.Enabled = btnSave.Enabled = btnDelete.Enabled = menuStrip1.Enabled = true;
            btnVazgec.Enabled = false;
            btnNew.Select();
            dgvCariler.Sort(dgvCariler.SortedColumn ?? cIsim, dgvCariler.SortOrder == SortOrder.Ascending || dgvCariler.SortOrder == SortOrder.None ? ListSortDirection.Ascending : ListSortDirection.Descending);
            dgvCariler.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var row = dgvCariler.SelectedRows[0];
            if (MessageBox.Show(((ulong)row.Cells[nameof(cCariNo)].Value).ToString(mtbCariNo.Mask) + " Cari No'lu müşteriyi silmek istediğinize emin misiniz?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ulong id = (ulong)row.Cells[nameof(cId)].Value;
                Cari cari = cariler.Find(f => f.Id == id);
                cari.Delete();
                cariler.Remove(cari);
                dgvCariler.Rows.Remove(row); // Let DataGridView SelectionChanged take care of the rest
                maxCariNo = cariler.Select(f => f.CariNo).DefaultIfEmpty(0ul).Max();
                btnNew.Select();
                MessageBox.Show("Silindi");
            }
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            mtbCariNo.Enabled = tbIsim.Enabled = tbKartNo.Enabled = false;
            btnSave.Enabled = btnVazgec.Enabled = false;
            btnNew.Enabled = menuStrip1.Enabled = true;
            dgvCariler.SelectionChanged -= dgvCariler_SelectionChanged;
            dgvCariler.Rows.Remove(dgvCariler.SelectedRows[0]);
            dgvCariler.SelectionChanged += dgvCariler_SelectionChanged;
            mtbCariNo.Clear();
            tbIsim.Clear();
            tbKartNo.Clear();
            dgvCariler.Enabled = true;
        }

        private void ayarlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            new SettingsForm().ShowDialog();
            cCariNo.DefaultCellStyle.Format = Globals.settings.cariNoFormat;
            mtbCariNo.Mask = Globals.settings.cariNoFormat;
            Show();
        }

        System.Threading.Timer searchTimer;
        private void toolStripTextBox1_TextChanged(object sender, EventArgs e) => searchTimer.Change(1000, Timeout.Infinite);

        private void SearchTimerCallBack(object state)
        {
            foreach (DataGridViewRow row in dgvCariler.Rows)
            {
                if (row.Cells[nameof(cCariNo)].Value.ToString().ToLower().Contains(toolStripTextBox1.Text.ToLower()) ||
                    ((ulong)row.Cells[nameof(cCariNo)].Value).ToString(cCariNo.DefaultCellStyle.Format).ToLower().Contains(toolStripTextBox1.Text.ToLower()) ||
                    row.Cells[nameof(cIsim)].Value.ToString().ToLower().Contains(toolStripTextBox1.Text.ToLower()) ||
                    row.Cells[nameof(cKartNo)].Value.ToString().ToLower().Contains(toolStripTextBox1.Text.ToLower()))
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        dgvCariler.SelectionChanged -= dgvCariler_SelectionChanged;
                        dgvCariler.ClearSelection();
                        dgvCariler.SelectionChanged += dgvCariler_SelectionChanged;
                        row.Selected = true;
                    }));
                    break;
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => Globals.database.Backup();
    }
}