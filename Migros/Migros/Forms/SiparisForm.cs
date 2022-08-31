using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Migros.Forms
{
    public partial class SiparisForm : Form
    {
        public Cari Cari { get; }
        ulong maxSipNo;

        public SiparisForm(Cari cari)
        {
            InitializeComponent();
            Cari = cari;
            Text = cari.CariNo.ToString(Globals.settings.cariNoFormat) + " - " + cari.Isim;
        }

        private void SiparisForm_Load(object sender, EventArgs e)
        {
            searchTimer = new System.Threading.Timer(SearchTimerCallBack);
            cSipNo.DefaultCellStyle.Format = mtbSipNo.Mask = Globals.settings.siparisNoFormat;
            cTL.DefaultCellStyle.Format = cKullanilan.DefaultCellStyle.Format = Globals.settings.tlFormat;
            cIslemTarihi.DefaultCellStyle.Format = dtpIslemTarihi.CustomFormat = Globals.settings.tarihFormat;

            Cari.GetSiparisler();

            foreach (var item in Cari.Siparisler)
                dgvSiparisler.Rows.Add(item.Id, item.SipNo, item.Puan, item.TL, item.Kullanilan, item.IslemTarihi);

            maxSipNo = Cari.Siparisler.Select(f => f.SipNo).DefaultIfEmpty(0ul).Max();
            UpdateToplamlar();
            dgvSiparisler.Sort(cIslemTarihi, ListSortDirection.Ascending);
        }

        private void SiparisForm_Shown(object sender, EventArgs e)
        {
            dgvSiparisler.ClearSelection();
            dgvSiparisler.SelectionChanged += dgvSiparisler_SelectionChanged;
        }

        private void dgvSiparisler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSiparisler.SelectedRows.Count == 1)
            {
                var cells = dgvSiparisler.SelectedRows[0].Cells;

                mtbSipNo.Text = ((ulong)cells[nameof(cSipNo)].Value).ToString(mtbSipNo.Mask);
                nudPuan.Value = (decimal)cells[nameof(cPuan)].Value;
                nudKullanilan.Value = (decimal)cells[nameof(cKullanilan)].Value;
                cbIslemTarihi.Checked = false;
                dtpIslemTarihi.Value = (DateTime)cells[nameof(cIslemTarihi)].Value;

                mtbSipNo.Enabled = nudPuan.Enabled = nudKullanilan.Enabled = cbIslemTarihi.Enabled = true;
                btnSave.Enabled = btnDelete.Enabled = true;
            }
            else
            {
                mtbSipNo.Enabled = nudPuan.Enabled = nudKullanilan.Enabled = cbIslemTarihi.Enabled = false;
                cbIslemTarihi.Checked = true;
                btnSave.Enabled = btnDelete.Enabled = false;
                mtbSipNo.Clear();
                nudPuan.Value = nudKullanilan.Value = 0;
            }
        }

        private void UpdateToplamlar()
        {
            var toplamlar = Cari.GetToplamlar();
            label5.Text = "T. Puan: " + toplamlar.tPuan;
            label6.Text = "T. TL: " + toplamlar.tTL.ToString(Globals.settings.tlFormat);
            label7.Text = "T. Kullanılan: " + toplamlar.tKullanilan.ToString(Globals.settings.tlFormat);
            label8.Text = "Kalan: " + toplamlar.kalan.ToString(Globals.settings.tlFormat);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dgvSiparisler.Enabled = false;
            mtbSipNo.Enabled = nudKullanilan.Enabled = nudPuan.Enabled = cbIslemTarihi.Checked = cbIslemTarihi.Enabled = true;
            btnNew.Enabled = btnDelete.Enabled = menuStrip1.Enabled = false;
            btnSave.Enabled = btnVazgec.Enabled = true;
            nudPuan.Select();
            mtbSipNo.Text = (maxSipNo + 1).ToString(mtbSipNo.Mask);
            nudPuan.Value = nudKullanilan.Value = 0;
            dgvSiparisler.SelectionChanged -= dgvSiparisler_SelectionChanged;
            int a = dgvSiparisler.Rows.Add(Globals.settings.lastSiparisId + 1, maxSipNo + 1, null, null, null, null);
            dgvSiparisler.ClearSelection();
            dgvSiparisler.Rows[a].Selected = true;
            dgvSiparisler.SelectionChanged += dgvSiparisler_SelectionChanged;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var rowCells = dgvSiparisler.SelectedRows[0].Cells;
            ulong id = (ulong)rowCells[nameof(cId)].Value;

            Siparis siparis;
            if ((siparis = Cari.Siparisler.Find(f => f.Id == id)) == null)
            {
                siparis = new Siparis(id, default, default, default, default);
                Cari.Siparisler.Add(siparis);
            }

            if (mtbSipNo.MaskCompleted)
            {
                ulong sipNo = ulong.Parse(mtbSipNo.Text);
                siparis.SipNo = sipNo;
                rowCells[nameof(cSipNo)].Value = sipNo;
                if (sipNo > maxSipNo)
                    maxSipNo = sipNo;
            }
            else
                siparis.SipNo = ++maxSipNo;

            if (cbIslemTarihi.Checked)
            {
                var now = DateTime.Today;
                siparis.IslemTarihi = now;
                rowCells[nameof(cIslemTarihi)].Value = now;
            }
            else
            {
                rowCells[nameof(cIslemTarihi)].Value = dtpIslemTarihi.Value;
                siparis.IslemTarihi = dtpIslemTarihi.Value;
            }

            decimal puan = nudPuan.Value, kullanilan = nudKullanilan.Value;
            siparis.Puan = puan;
            siparis.Kullanilan = kullanilan;
            Cari.SaveSiparis(siparis);

            rowCells[nameof(cPuan)].Value = puan;
            rowCells[nameof(cTL)].Value = puan * Globals.settings.puanCarpani;
            rowCells[nameof(cKullanilan)].Value = kullanilan;

            btnNew.Enabled = btnSave.Enabled = btnDelete.Enabled = menuStrip1.Enabled = true;
            btnVazgec.Enabled = false;
            btnNew.Select();
            dgvSiparisler.Sort(dgvSiparisler.SortedColumn ?? cIslemTarihi, dgvSiparisler.SortOrder == SortOrder.Ascending || dgvSiparisler.SortOrder == SortOrder.None ? ListSortDirection.Ascending : ListSortDirection.Descending);
            dgvSiparisler.Enabled = true;
            UpdateToplamlar();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var row = dgvSiparisler.SelectedRows[0];
            if (MessageBox.Show(((ulong)row.Cells[nameof(cSipNo)].Value).ToString(cSipNo.DefaultCellStyle.Format) + " Sipariş No'lu siparişi silmek istediğinize emin misiniz?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ulong id = (ulong)row.Cells[nameof(cId)].Value;
                Cari.DeleteSiparis(Cari.Siparisler.Find(f => f.Id == id));
                dgvSiparisler.Rows.Remove(row); // Let DataGridView SelectionChanged take care of the rest
                maxSipNo = Cari.Siparisler.Select(f => f.SipNo).DefaultIfEmpty(0ul).Max();
                btnNew.Select();
                UpdateToplamlar();
                MessageBox.Show("Silindi");
            }
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            cbIslemTarihi.Checked = true;
            mtbSipNo.Enabled = nudKullanilan.Enabled = nudPuan.Enabled = cbIslemTarihi.Enabled = false;
            btnSave.Enabled = btnVazgec.Enabled = false;
            btnNew.Enabled = menuStrip1.Enabled = true;
            dgvSiparisler.SelectionChanged -= dgvSiparisler_SelectionChanged;
            dgvSiparisler.Rows.Remove(dgvSiparisler.SelectedRows[0]);
            dgvSiparisler.SelectionChanged += dgvSiparisler_SelectionChanged;
            mtbSipNo.Clear();
            nudKullanilan.Value = nudPuan.Value = 0;
            dgvSiparisler.Enabled = true;
        }

        private void cbIslemTarihi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIslemTarihi.Checked)
            {
                cbIslemTarihi.Text = "Otomatik İşlem Tarihi";
                label4.Visible = dtpIslemTarihi.Visible = false;
            }
            else
            {
                cbIslemTarihi.Text = "Manuel İşlem Tarihi";
                label4.Visible = dtpIslemTarihi.Visible = true;
                dtpIslemTarihi.Value = DateTime.Today;
            }
        }

        System.Threading.Timer searchTimer;
        private void toolStripTextBox1_TextChanged(object sender, EventArgs e) => searchTimer.Change(1000, Timeout.Infinite);

        private void SearchTimerCallBack(object state)
        {
            foreach (DataGridViewRow row in dgvSiparisler.Rows)
            {
                string search = toolStripTextBox1.Text.ToLower();
                if (row.Cells[nameof(cSipNo)].Value.ToString().ToLower().Contains(search) ||
                    ((ulong)row.Cells[nameof(cSipNo)].Value).ToString(cSipNo.DefaultCellStyle.Format).ToLower().Contains(search) ||
                    row.Cells[nameof(cPuan)].Value.ToString().ToLower().Contains(search) ||
                    row.Cells[nameof(cTL)].Value.ToString().ToLower().Contains(search) ||
                    ((decimal)row.Cells[nameof(cTL)].Value).ToString(cTL.DefaultCellStyle.Format).ToLower().Contains(search) ||
                    row.Cells[nameof(cKullanilan)].Value.ToString().ToLower().Contains(search) ||
                    ((decimal)row.Cells[nameof(cKullanilan)].Value).ToString(cKullanilan.DefaultCellStyle.Format).ToLower().Contains(search) ||
                    ((DateTime)row.Cells[nameof(cIslemTarihi)].Value).ToString(cIslemTarihi.DefaultCellStyle.Format).ToLower().Contains(search))
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        dgvSiparisler.SelectionChanged -= dgvSiparisler_SelectionChanged;
                        dgvSiparisler.ClearSelection();
                        dgvSiparisler.SelectionChanged += dgvSiparisler_SelectionChanged;
                        row.Selected = true;
                    }));
                    break;
                }
            }
        }

        private void yazdırToolStripMenuItem_Click(object sender, EventArgs e) => Printer.Print(Cari, Cari.Siparisler.OrderBy(f => f.IslemTarihi).ToList());

        bool selectByMouse = false;
        private void nuds_Enter(object sender, EventArgs e)
        {
            NumericUpDown nud = sender as NumericUpDown;
            nud.Select();
            nud.Select(0, nud.Text.Length);
            if (MouseButtons == MouseButtons.Left)
                selectByMouse = true;
        }

        private void nuds_MouseDown(object sender, MouseEventArgs e)
        {
            NumericUpDown nud = sender as NumericUpDown;
            if (selectByMouse)
            {
                nud.Select(0, nud.Text.Length);
                selectByMouse = false;
            }
        }
    }
}