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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBMSClinic.Tables
{
    public partial class EditDoctor : Form
    {
        readonly DBConnect conn = new DBConnect();
        public string surname { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string specialty { get; set; }
        public string category { get; set; }
        public string selectedSpecialty { get; set; }
        public string selectedCategory { get; set; }
        public Image dimage { get; set; }
        public EditDoctor(string s1, string s2, string s3, string s4, string s5, Image image, string selectedSpecialty, string selectedCategory)
        {
            InitializeComponent();
            LoadComboBoxSpecialty();
            LoadComboBoxCategory();
            textBoxSurname.Text = s1;
            textBoxFirstname.Text = s2;
            textBoxMiddlename.Text = s3;
            comboBoxSpecialty.Text = s4;
            comboBoxCategory.Text = s5;
            comboBoxSpecialty.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            picturePhotoDoctor.Image = image;
            this.selectedCategory = comboBoxCategory.Text;
            this.selectedSpecialty = comboBoxSpecialty.Text;
        }
        private void LoadComboBoxSpecialty()
        {
            conn.Connect();
            string sql = "SELECT * FROM Specialties";
            using (SqlCommand cmd = new SqlCommand(sql, conn.connection))
            {
                cmd.CommandType = CommandType.Text;
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                comboBoxSpecialty.DisplayMember = "name";
                comboBoxSpecialty.ValueMember = "id";
                comboBoxSpecialty.DataSource = table;
            }
            conn.Disconnect();
        }

        private void LoadComboBoxCategory()
        {
            conn.Connect();
            string sql = "SELECT * FROM Category";
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

        private void EditDoctor_Load(object sender, EventArgs e)
        {
            //LoadComboBoxSpecialty();
            //LoadComboBoxCategory();
        }

        private void buttonAdd_Click_1(object sender, EventArgs e)
        {
            this.surname = textBoxSurname.Text;
            this.firstname = textBoxFirstname.Text;
            this.middlename = textBoxMiddlename.Text;
            this.specialty = comboBoxSpecialty.Text;
            this.category = comboBoxCategory.Text;
            this.dimage = picturePhotoDoctor.Image;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonUploadPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK && Path.GetExtension(ofd.FileName) == ".png" || Path.GetExtension(ofd.FileName) == ".jpeg" || Path.GetExtension(ofd.FileName) == ".jpg")
            {
                picturePhotoDoctor.SizeMode = PictureBoxSizeMode.StretchImage;
                picturePhotoDoctor.Image = Image.FromFile(ofd.FileName);
            }
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
    }
}
