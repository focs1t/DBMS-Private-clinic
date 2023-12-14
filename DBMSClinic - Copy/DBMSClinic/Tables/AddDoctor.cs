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
    public partial class AddDoctor : Form
    {
        readonly DBConnect conn = new DBConnect();
        public AddDoctor()
        {
            InitializeComponent();
            comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSpecialty.DropDownStyle = ComboBoxStyle.DropDownList;
            textBoxSurname.MaxLength = 50;
            textBoxMiddlename.MaxLength = 50;
            textBoxFirstname.MaxLength = 50;
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
                comboBoxSpecialty.Items.Clear();
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
                comboBoxCategory.Items.Clear();
                adapter.Fill(table);
                comboBoxCategory.DisplayMember = "name";
                comboBoxCategory.ValueMember = "id";
                comboBoxCategory.DataSource = table;
            }
            conn.Disconnect();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            conn.Connect();
            MemoryStream ms = new MemoryStream();

            if (textBoxFirstname.Text == "" || textBoxSurname.Text == "" || textBoxMiddlename.Text == ""
                || comboBoxSpecialty.Text == "" || comboBoxCategory.Text == "" || picturePhotoDoctor.Image == null)
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                picturePhotoDoctor.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                var photo = ms.ToArray();
                try
                {
                    SqlCommand command = new SqlCommand();

                    command = new SqlCommand(
                        "insert into Doctors (lastname, firstname, middlename, specialty_id, category_id, image) " +
                        "values (@lastname, @firstname, @middlename, @specialty_id, @category_id, @image)", conn.connection);
                    command.Parameters.Add("lastname", SqlDbType.NVarChar).Value = textBoxSurname.Text;
                    command.Parameters.Add("firstname", SqlDbType.NVarChar).Value = textBoxFirstname.Text;
                    command.Parameters.Add("middlename", SqlDbType.NVarChar).Value = textBoxMiddlename.Text;
                    command.Parameters.Add("specialty_id", SqlDbType.Int);
                    command.Parameters["specialty_id"].Value = int.Parse(comboBoxSpecialty.SelectedValue.ToString());
                    command.Parameters.Add("category_id", SqlDbType.Int);
                    command.Parameters["category_id"].Value = int.Parse(comboBoxCategory.SelectedValue.ToString());
                    command.Parameters.Add("image", SqlDbType.Image).Value = photo;
                    command.ExecuteNonQuery();

                    MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK);

                    this.Close();
                    //dataDoctors.Colums[5].Width = 200;
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

        private void AddDoctor_Load(object sender, EventArgs e)
        {
            LoadComboBoxSpecialty();
            LoadComboBoxCategory();
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
