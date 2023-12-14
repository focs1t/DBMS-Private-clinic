using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBMSClinic.Tables
{
    public partial class EditTreatment : Form
    {
        readonly DBConnect conn = new DBConnect();
        public string selectedDoctor { get; set; }
        public string selectedDiagnosis { get; set; }
        public int selectedNumAppeal { get; set; }
        public decimal price { get; set; }
        public string doctor { get; set; }
        public string diagnosis { get; set; }
        public int num_appeal { get; set; }
        public EditTreatment(string s1, string s2, decimal i1, int i2, string selectedDoctor, string selectedDiagnosis, int selectedNumAppeal)
        {
            InitializeComponent();
            LoadComboBoxDoctor();
            LoadComboBoxNumAppeal();
            LoadComboBoxDiagnosis();
            textBoxPrice.Text = i1.ToString();
            comboBoxNumAppeal.Text = i2.ToString();
            comboBoxDoctor.Text = s1;
            comboBoxDiagnosis.Text = s2;
            comboBoxDiagnosis.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxNumAppeal.DropDownStyle = ComboBoxStyle.DropDownList;
            this.selectedDoctor = comboBoxDoctor.Text;
            this.selectedDoctor = selectedDoctor;
            this.selectedDiagnosis = comboBoxDiagnosis.Text;
            this.selectedDiagnosis = selectedDiagnosis;
            this.selectedNumAppeal = Convert.ToInt32(comboBoxNumAppeal.Text);
            this.selectedNumAppeal = selectedNumAppeal;

        }
        private void LoadComboBoxDoctor()
        {
            conn.Connect();
            string sql = "SELECT CONCAT(lastname, ' ', firstname, ' ', middlename)AS FULLNAME FROM Doctors";
            using (SqlCommand cmd = new SqlCommand(sql, conn.connection))
            {
                cmd.CommandType = CommandType.Text;
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                SqlDataReader reader = cmd.ExecuteReader();

                comboBoxDoctor.DisplayMember = "FULLNAME";
                comboBoxDoctor.ValueMember = "id";
                comboBoxDoctor.DataSource = table;
            }
            conn.Disconnect();
        }

        private void LoadComboBoxNumAppeal()
        {
            conn.Connect();
            string sql = "SELECT * FROM Appeals";
            using (SqlCommand cmd = new SqlCommand(sql, conn.connection))
            {
                cmd.CommandType = CommandType.Text;
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                comboBoxNumAppeal.DisplayMember = "id";
                comboBoxNumAppeal.ValueMember = "id";
                comboBoxNumAppeal.DataSource = table;
            }
            conn.Disconnect();
        }

        private void LoadComboBoxDiagnosis()
        {
            conn.Connect();
            string sql = "SELECT * FROM Diagnoses";
            using (SqlCommand cmd = new SqlCommand(sql, conn.connection))
            {
                cmd.CommandType = CommandType.Text;
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                comboBoxDiagnosis.DisplayMember = "name";
                comboBoxDiagnosis.ValueMember = "id";
                comboBoxDiagnosis.DataSource = table;
            }
            conn.Disconnect();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.price = Convert.ToInt32(textBoxPrice.Text);
            this.doctor = comboBoxDoctor.Text;
            this.diagnosis = comboBoxDiagnosis.Text;
            this.num_appeal = Convert.ToInt32(comboBoxNumAppeal.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')

            {

                e.Handled = true;

            }
        }
    }
}
