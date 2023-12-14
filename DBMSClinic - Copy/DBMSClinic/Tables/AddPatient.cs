using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMSClinic.Tables
{
    public partial class AddPatient : Form
    {
        readonly DBConnect conn = new DBConnect();
        public AddPatient()
        {
            InitializeComponent();
            comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            textBoxAddress.MaxLength = 150;
            textBoxFirstname.MaxLength = 50;
            textBoxMiddlename.MaxLength = 50;
            textBoxSurname.MaxLength = 50;
        }

        private void LoadComboBoxCategoryP()
        {
            conn.Connect();
            string sql = "SELECT * FROM Categories_of_patients";
            using (SqlCommand cmd = new SqlCommand(sql, conn.connection))
            {
                cmd.CommandType = CommandType.Text;
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                comboBoxCategory.Items.Clear();
                comboBoxCategory.DisplayMember = "name";
                comboBoxCategory.ValueMember = "id";
                comboBoxCategory.DataSource = table;
            }
            conn.Disconnect();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            conn.Connect();

            if (textBoxFirstname.Text == "" || textBoxSurname.Text == "" || textBoxMiddlename.Text == ""
                || textBoxAge.Text == "" || comboBoxCategory.Text == "" || textBoxAddress.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (int.Parse(textBoxAge.Text) <= 65 && int.Parse(textBoxAge.Text) >= 18)
                    {
                        if(comboBoxCategory.Text == "Pensioner" || comboBoxCategory.Text == "Rebenok")
                        {
                            MessageBox.Show($"С возрастом {textBoxAge.Text} нельзя ставить категорию {comboBoxCategory.Text}!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            SqlCommand command = new SqlCommand();

                            command = new SqlCommand(
                                "insert into Patients (lastname, firstname, middlename, age, address, category_id) " +
                                "values (@lastname, @firstname, @middlename, @age, @address, @category_id)", conn.connection);
                            command.Parameters.Add("lastname", SqlDbType.NVarChar).Value = textBoxSurname.Text;
                            command.Parameters.Add("firstname", SqlDbType.NVarChar).Value = textBoxFirstname.Text;
                            command.Parameters.Add("middlename", SqlDbType.NVarChar).Value = textBoxMiddlename.Text;
                            command.Parameters.Add("age", SqlDbType.Int).Value = textBoxAge.Text;
                            command.Parameters.Add("address", SqlDbType.NVarChar).Value = textBoxAddress.Text;
                            command.Parameters.Add("category_id", SqlDbType.Int);
                            command.Parameters["category_id"].Value = int.Parse(comboBoxCategory.SelectedValue.ToString());
                            command.ExecuteNonQuery();

                            MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK);

                            this.Close();
                        }
                    }
                    else if (int.Parse(textBoxAge.Text) <= 65)
                    {
                        if (comboBoxCategory.Text == "Pensioner")
                        {
                            MessageBox.Show($"С возрастом {textBoxAge.Text} нельзя ставить категорию {comboBoxCategory.Text}!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            SqlCommand command = new SqlCommand();

                            command = new SqlCommand(
                                "insert into Patients (lastname, firstname, middlename, age, address, category_id) " +
                                "values (@lastname, @firstname, @middlename, @age, @address, @category_id)", conn.connection);
                            command.Parameters.Add("lastname", SqlDbType.NVarChar).Value = textBoxSurname.Text;
                            command.Parameters.Add("firstname", SqlDbType.NVarChar).Value = textBoxFirstname.Text;
                            command.Parameters.Add("middlename", SqlDbType.NVarChar).Value = textBoxMiddlename.Text;
                            command.Parameters.Add("age", SqlDbType.Int).Value = textBoxAge.Text;
                            command.Parameters.Add("address", SqlDbType.NVarChar).Value = textBoxAddress.Text;
                            command.Parameters.Add("category_id", SqlDbType.Int);
                            command.Parameters["category_id"].Value = int.Parse(comboBoxCategory.SelectedValue.ToString());
                            command.ExecuteNonQuery();

                            MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK);

                            this.Close();
                        }
                    }
                    else if (int.Parse(textBoxAge.Text) >= 18)
                    {
                        if (comboBoxCategory.Text == "Rebenok")
                        {
                            MessageBox.Show($"С возрастом {textBoxAge.Text} нельзя ставить категорию {comboBoxCategory.Text}!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            SqlCommand command = new SqlCommand();

                            command = new SqlCommand(
                                "insert into Patients (lastname, firstname, middlename, age, address, category_id) " +
                                "values (@lastname, @firstname, @middlename, @age, @address, @category_id)", conn.connection);
                            command.Parameters.Add("lastname", SqlDbType.NVarChar).Value = textBoxSurname.Text;
                            command.Parameters.Add("firstname", SqlDbType.NVarChar).Value = textBoxFirstname.Text;
                            command.Parameters.Add("middlename", SqlDbType.NVarChar).Value = textBoxMiddlename.Text;
                            command.Parameters.Add("age", SqlDbType.Int).Value = textBoxAge.Text;
                            command.Parameters.Add("address", SqlDbType.NVarChar).Value = textBoxAddress.Text;
                            command.Parameters.Add("category_id", SqlDbType.Int);
                            command.Parameters["category_id"].Value = int.Parse(comboBoxCategory.SelectedValue.ToString());
                            command.ExecuteNonQuery();

                            MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK);

                            this.Close();
                        }
                    }
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

        private void AddPatient_Load(object sender, EventArgs e)
        {
            LoadComboBoxCategoryP();
        }

        private void textBoxSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void textBoxFirstname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void textBoxMiddlename_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void textBoxAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))

            {

                e.Handled = true;

            }
        }

        private void textBoxAddress_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
