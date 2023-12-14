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

namespace DBMSClinic.Reference_books
{
    public partial class AddPatientCategory : Form
    {
        readonly DBConnect conn = new DBConnect();
        public AddPatientCategory()
        {
            InitializeComponent();
            textBoxName.MaxLength = 15;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            conn.Connect();

            if (textBoxName.Text == "" || textBoxDiscount.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    SqlCommand command = new SqlCommand();

                    command = new SqlCommand(
                        "insert into Categories_of_patients (name, discount) " +
                        "values (@name, @discount)", conn.connection);
                    command.Parameters.Add("name", SqlDbType.NVarChar).Value = textBoxName.Text;
                    command.Parameters.Add("discount", SqlDbType.Decimal).Value = Convert.ToDecimal(textBoxDiscount.Text);
                    command.Parameters["discount"].Precision = 4;
                    command.Parameters["discount"].Scale = 2;
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

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void textBoxDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')

            {

                e.Handled = true;

            }
        }
    }
}
