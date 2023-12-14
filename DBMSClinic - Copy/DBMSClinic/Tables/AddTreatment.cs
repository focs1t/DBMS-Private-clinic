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

namespace DBMSClinic.Tables
{
    public partial class AddTreatment : Form
    {
        readonly DBConnect conn = new DBConnect();
        public AddTreatment()
        {
            InitializeComponent();
            comboBoxDiagnosis.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxNumAppeal.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void LoadComboBoxDoctor()
        {
            conn.Connect();
            string sql = "SELECT id, CONCAT(lastname, ' ', firstname, ' ', middlename) AS Врач FROM Doctors";
            using (SqlCommand cmd = new SqlCommand(sql, conn.connection))
            {
                cmd.CommandType = CommandType.Text;
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                SqlDataReader reader = cmd.ExecuteReader();

                comboBoxDoctor.DisplayMember = "Врач";
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
            conn.Connect();

            if (comboBoxNumAppeal.Text == "" || comboBoxDoctor.Text == "" || comboBoxDiagnosis.Text == "" ||
                textBoxPrice.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    SqlCommand command = new SqlCommand();

                    command = new SqlCommand(
                        "insert into Treatments (appeal_num, doctor_id, diagnoses_id, price) " +
                        "values (@appeal_num, @doctor_id, @diagnoses_id, @price)", conn.connection);
                    command.Parameters.Add("appeal_num", SqlDbType.Int);
                    command.Parameters["appeal_num"].Value = comboBoxNumAppeal.SelectedValue;
                    command.Parameters.Add("doctor_id", SqlDbType.Int);
                    command.Parameters["doctor_id"].Value = int.Parse(comboBoxDoctor.SelectedValue.ToString());
                    command.Parameters.Add("diagnoses_id", SqlDbType.Int);
                    command.Parameters["diagnoses_id"].Value = int.Parse(comboBoxDiagnosis.SelectedValue.ToString());
                    //command.Parameters.Add("price", SqlDbType.Int).Value = textBoxPrice.Text;
                    command.Parameters.Add("price", SqlDbType.Decimal).Value = Convert.ToDecimal(textBoxPrice.Text);
                    command.Parameters["price"].Precision = 10;
                    command.Parameters["price"].Scale = 2;
                    command.ExecuteNonQuery();

                    MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK);

                    this.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Disconnect();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddTreatment_Load(object sender, EventArgs e)
        {
            LoadComboBoxDoctor();
            LoadComboBoxNumAppeal();
            LoadComboBoxDiagnosis();
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
