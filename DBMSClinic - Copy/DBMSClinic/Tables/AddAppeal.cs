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
    public partial class AddAppeal : Form
    {
        readonly DBConnect conn = new DBConnect();
        public AddAppeal()
        {
            InitializeComponent();
            comboBoxPatient.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void LoadComboBoxPatient()
        {
            conn.Connect();
            string sql = "SELECT id, CONCAT(lastname, ' ', firstname, ' ', middlename)AS Пациент FROM Patients";
            using (SqlCommand cmd = new SqlCommand(sql, conn.connection))
            {
                cmd.CommandType = CommandType.Text;
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                SqlDataReader reader = cmd.ExecuteReader();

                comboBoxPatient.DisplayMember = "Пациент";
                comboBoxPatient.ValueMember = "id";
                comboBoxPatient.DataSource = table;
            }
            conn.Disconnect();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            conn.Connect();

            try
            {
                SqlCommand command = new SqlCommand();

                command = new SqlCommand(
                    "insert into Appeals (patient_id, data) " +
                    "values (@patient_id, @data)", conn.connection);
                command.Parameters.Add("patient_id", SqlDbType.Int);
                command.Parameters["patient_id"].Value = int.Parse(comboBoxPatient.SelectedValue.ToString());
                command.Parameters.Add("data", SqlDbType.Date).Value = dateTimePicker1.Value.Date;
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddAppeal_Load(object sender, EventArgs e)
        {
            LoadComboBoxPatient();
        }
    }
}
