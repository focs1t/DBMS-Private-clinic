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
using static System.Net.Mime.MediaTypeNames;

namespace DBMSClinic.Tables
{
    public partial class EditPatient : Form
    {
        readonly DBConnect conn = new DBConnect();
        public string surname { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public int p_age { get; set; }
        public string category { get; set; }
        public string address { get; set; }
        public string selectedCategory { get; set; }
        public EditPatient(string s1, string s2, string s3, int i1, string s4, string s5, string selectedCategory)
        {
            InitializeComponent();
            LoadComboBoxCategoryP();
            textBoxSurname.Text = s1;
            textBoxFirstname.Text = s2;
            textBoxMiddlename.Text = s3;
            textBoxAge.Text = i1.ToString();
            comboBoxCategory.Text = s5;
            comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            textBoxAddress.Text = s4;
            this.selectedCategory = comboBoxCategory.Text;
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
                comboBoxCategory.DisplayMember = "name";
                comboBoxCategory.ValueMember = "id";
                comboBoxCategory.DataSource = table;
            }
            conn.Disconnect();
        }

        private void EditPatient_Load(object sender, EventArgs e)
        {
            //LoadComboBoxCategory();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBoxAge.Text) <= 65 && int.Parse(textBoxAge.Text) >= 18)
            {
                if (comboBoxCategory.Text == "Pensioner" || comboBoxCategory.Text == "Rebenok")
                {
                    MessageBox.Show($"С возрастом {textBoxAge.Text} нельзя ставить категорию {comboBoxCategory.Text}!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    this.surname = textBoxSurname.Text;
                    this.firstname = textBoxFirstname.Text;
                    this.middlename = textBoxMiddlename.Text;
                    this.p_age = Convert.ToInt32(textBoxAge.Text);
                    this.address = textBoxAddress.Text;
                    this.category = comboBoxCategory.Text;
                    this.DialogResult = DialogResult.OK;
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
                    this.surname = textBoxSurname.Text;
                    this.firstname = textBoxFirstname.Text;
                    this.middlename = textBoxMiddlename.Text;
                    this.p_age = Convert.ToInt32(textBoxAge.Text);
                    this.address = textBoxAddress.Text;
                    this.category = comboBoxCategory.Text;
                    this.DialogResult = DialogResult.OK;
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
                    this.surname = textBoxSurname.Text;
                    this.firstname = textBoxFirstname.Text;
                    this.middlename = textBoxMiddlename.Text;
                    this.p_age = Convert.ToInt32(textBoxAge.Text);
                    this.address = textBoxAddress.Text;
                    this.category = comboBoxCategory.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
