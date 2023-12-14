using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMSClinic.Reference_books
{
    public partial class EditPatientCategory : Form
    {
        readonly DBConnect conn = new DBConnect();
        public string name { get; set; }
        public string discount { get; set; }
        public EditPatientCategory(string s1, string s2)
        {
            InitializeComponent();
            textBoxName.Text = s1;
            textBoxDiscount.Text = s2;
            textBoxName.MaxLength = 15;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.name = textBoxName.Text;
            this.discount = textBoxDiscount.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
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
