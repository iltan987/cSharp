using DatabaseManager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static DatabaseManager.DatabaseColumnProperty;

namespace Test.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Manager manager;

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                manager = new Manager("database.mdf");
                manager.CreateTable(tableName: "Table1", columns: new Dictionary<string, DatabaseColumnProperty>
                {
                    { "id", new DatabaseColumnProperty(dataType: DataTypes.INT, notNull: true) },
                    { "FirstName", new DatabaseColumnProperty(dataType: DataTypes.TEXT) },
                    { "LastName", new DatabaseColumnProperty(dataType: DataTypes.TEXT) },
                    { "Age", new DatabaseColumnProperty(dataType: DataTypes.INT) },
                }, primaryKeyColumn: "id", identityColumn: "id", keepConnectionOpen: true);
                UpdateData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateData()
        {
            dataGridView1.DataSource = manager.Select("Table1");
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["FirstName"].HeaderText = "First Name";
            dataGridView1.Columns["LastName"].HeaderText = "Last Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            manager.Insert(tableName: "Table1", columns: new List<string> { "FirstName", "LastName", "Age" }, parameterValues: new Dictionary<string, object>
            {
                { "@first", textBox1.Text },
                { "@last", textBox2.Text },
                { "@age", (int)numericUpDown1.Value }
            });
            UpdateData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                manager.Update(tableName: "Table1", columName_parameterName_value: new Dictionary<string, (string, object)>
                {
                    { "FirstName", ("@first", textBox1.Text) },
                    { "LastName", ("@last", textBox2.Text) },
                    { "Age", ("@age", (int)numericUpDown1.Value) }
                }, condition: "id=@id", conditionParameters: new Dictionary<string, object>
                {
                    { "@id", dataGridView1.SelectedRows[0].Cells["id"].Value }
                });
                UpdateData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                manager.Delete(tableName: "Table1", condition: "id=@id", conditionParameters: new Dictionary<string, object>
                {
                    { "@id", dataGridView1.SelectedRows[0].Cells["id"].Value }
                });
                UpdateData();
            }
        }
    }
}