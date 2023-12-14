using DBMSClinic.Reference_books;
using DBMSClinic.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBMSClinic
{
    public partial class Main : Form
    {
        readonly DBConnect conn = new DBConnect();
        List<Panel> listPanel = new List<Panel>();
        public Main()
        {
            InitializeComponent();
            textBoxPassword.UseSystemPasswordChar = true;
            label6.Text = DateTime.Now.ToString("yyyy.MM.dd");
            this.dataDoctors.DefaultCellStyle.Font = new Font("MS Reference Sans Serif", 11);
        }
        private void Main_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            conn.Connect();
            listPanel.Add(panel1);
            listPanel.Add(panel2);
            listPanel.Add(panel3);
            listPanel.Add(panel4);
            btnLogout.Visible = false;
            comboBoxPatientId.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDoctorId.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAppealId.DropDownStyle = ComboBoxStyle.DropDownList;
            textBoxCategory.MaxLength = 15;
            textBoxSpecialty.MaxLength = 20;
            textBoxDiagnoses.MaxLength = 20;
        }

        #region [ Authorization ]
        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (pictureBox2.Visible == true)
            {
                textBoxPassword.UseSystemPasswordChar = true;
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = false;
            }

        }

        private void buttonAuth_Click(object sender, EventArgs e)
        {
            conn.Connect();
            if (linkLabel1.Visible == true)
            {
                var loginUser = textBoxLogin.Text;
                var passUser = textBoxPassword.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                string querystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}'and password_user ='{passUser}'";
                SqlCommand command = new SqlCommand(querystring, conn.getConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count == 1)
                {
                    MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (loginUser == "admin" && passUser == "admin")
                    {
                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = true;
                    }
                    btnLogout.Visible = true;
                    panel2.Visible = true;
                    button1.BackColor = Color.PeachPuff;
                    RefreshTableDoctor();
                }
                else
                    MessageBox.Show("Такого аккаунта не существует!", "Аккаунта не существует!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                conn.Disconnect();
            }
            else
            {
                var login = textBoxLogin.Text;
                var password = textBoxPassword.Text;
                string querystring = $"insert into register(login_user, password_user) values('{login}', '{password}')";
                SqlCommand command = new SqlCommand(querystring, conn.getConnection());
                conn.Connect();
                if (checkUser())
                {
                    return;
                }
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                    linkLabel1.Visible = true;
                    buttonAuth.Text = "Войти";
                }
                else
                    MessageBox.Show("Аккаунт не создан!");

                conn.Disconnect();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            string text = textBoxPassword.Text;
            textBoxPassword.Text += "1";
            textBoxPassword.Text = text;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            string text = textBoxPassword.Text;
            textBoxPassword.Text += "1";
            textBoxPassword.Text = text;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            buttonAuth.Text = "Зарегестрироваться";
            linkLabel1.Visible = false;
        }

        public Boolean checkUser()
        {
            var loginUser = textBoxLogin.Text;
            var passUser = textBoxPassword.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}' and password_user = '{passUser}'";
            SqlCommand command = new SqlCommand(querystring, conn.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else
                return false;
        }

        #endregion

        private void btnLogout_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = false;
            panel4.Visible = false;
            btnLogout.Visible = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button1.BackColor = Color.White;
        }
        public void RefreshTable(DataGridView dgw, string tableName)
        {
            conn.Connect();

            dgw.Rows.Clear();

            int counter = 0;
            SqlCommand command = new SqlCommand($"select * from {tableName} ORDER BY id", conn.connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if(tableName == "Specialties")
                {
                    ReadSingleRowSpecialties(dgw, reader);//для таблицы специальности
                }
                else if(tableName == "Category")
                {
                    ReadSingleRowCategory(dgw, reader);//для таблицы категории врачей
                }
                else if (tableName == "Diagnoses")
                {
                    ReadSingleRowDiagnoses(dgw, reader);//для таблицы диагонзы
                }
                else if (tableName == "Categories_of_patients")
                {
                    ReadSingleRowPatientCategory(dgw, reader);//для таблицы категории пациентов
                }

                counter++;
            }
            reader.Close();
            conn.Disconnect();
        }

        #region[ Tables ]
        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = false;
            panel2.Visible = true;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button1.BackColor = Color.PeachPuff;
        }

        private void tabTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage selectedTab = tabTables.SelectedTab;

            if (selectedTab == tabPageDoctors) // проверяем, что выбрана нужная вкладка
            {
                RefreshTableDoctor();
            }
            else if (selectedTab == tabPagePatients) 
            {
                RefreshTablePatient();
            }
            else if (selectedTab == tabPageAppeals) 
            {
                RefreshTableAppeal();
            }
            else if(selectedTab == tabPageTreatments)
            {
                RefreshTableTreatment();
            }
        }

        #region [ Doctor Tab ]

        public void RefreshTableDoctor()
        {
            conn.Connect();

            dataDoctors.DataSource = null;
            dataDoctors.Rows.Clear();

            SqlCommand command = new SqlCommand("select Doctors.id as 'ID', Doctors.lastname as 'Фамилия'," +
                "Doctors.firstname as 'Имя', Doctors.middlename as 'Отчество', Specialties.name as 'Специальность', " +
                "Category.name as 'Категория', Doctors.image as 'Фото' " +
                "from Doctors, Category, Specialties WHERE Category.id = Doctors.category_id AND " +
                "Specialties.id = Doctors.specialty_id", conn.connection);
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            dataDoctors.DataSource = table;
            conn.Disconnect();
        }

        private void btnAddDoctor_Click(object sender, EventArgs e)
        {
            AddDoctor addDoctor = new AddDoctor();
            addDoctor.ShowDialog();
            RefreshTableDoctor();
            //dataDoctors.Colums[5].Width = 200;
            //dataDoctors.RowTemplate.Height = 200;
        }

        private void btnDelDoctor_Click(object sender, EventArgs e)
        {
            int rowIndex = dataDoctors.CurrentCell.RowIndex;
            DataGridViewCell cell = dataDoctors.Rows[rowIndex].Cells[0];
            string value = cell.Value.ToString();
            dataDoctors.Rows.RemoveAt(rowIndex);
            conn.Connect();
            SqlCommand delete = new SqlCommand("delete from Doctors where id=" + value, conn.connection);
            delete.ExecuteNonQuery();
            MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK);
            conn.Disconnect();
        }
        private void dataDoctors_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            conn.Connect();
            int index = dataDoctors.CurrentCell.RowIndex;
            var id = dataDoctors.Rows[index].Cells[0].Value.ToString();
            var surname = dataDoctors.Rows[index].Cells[1].Value.ToString();
            var firstname = dataDoctors.Rows[index].Cells[2].Value.ToString();
            var middlename = dataDoctors.Rows[index].Cells[3].Value.ToString();
            var specialty = dataDoctors.Rows[index].Cells[4].Value.ToString();
            var category = dataDoctors.Rows[index].Cells[5].Value.ToString();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            byte[] img = (Byte[])dataDoctors.CurrentRow.Cells[6].Value;
            MemoryStream ms = new MemoryStream(img);
            Image image = Image.FromStream(ms);

            var selectedCategory = dataDoctors.Rows[index].Cells[5].Value.ToString();
            var selectedSpecialty = dataDoctors.Rows[index].Cells[4].Value.ToString();
            EditDoctor editDoctor = new EditDoctor(surname, firstname, middlename, specialty, category, image, selectedCategory, selectedSpecialty);
            var result = editDoctor.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataDoctors.Rows[index].Cells[1].Value = editDoctor.surname;
                dataDoctors.Rows[index].Cells[2].Value = editDoctor.firstname;
                dataDoctors.Rows[index].Cells[3].Value = editDoctor.middlename;
                dataDoctors.Rows[index].Cells[4].Value = editDoctor.specialty;
                dataDoctors.Rows[index].Cells[5].Value = editDoctor.category;
                dataDoctors.Rows[index].Cells[6].Value = editDoctor.dimage;
                byte[] sph;
                using (MemoryStream mss = new MemoryStream())
                {
                    editDoctor.dimage.Save(mss, ImageFormat.Jpeg);
                    sph = mss.ToArray();
                }
                var changeQuery = $"update Doctors set lastname = '{editDoctor.surname}', firstname = '{editDoctor.firstname}', " +
                    $"middlename = '{editDoctor.middlename}', specialty_id = (SELECT id FROM Specialties WHERE name = '{editDoctor.specialty}'), category_id = (SELECT id FROM Category WHERE name = '{editDoctor.category}'), " +
                    $"image = @image WHERE id = '{id}'";
                var command = new SqlCommand(changeQuery, conn.getConnection());
                conn.getConnection();
                adapter.SelectCommand = command;
                command.Parameters.AddWithValue("@image", sph);
                command.ExecuteNonQuery();
                adapter.Fill(table);
                MessageBox.Show("Запись обновлена!", "", MessageBoxButtons.OK);
                conn.Disconnect();
                RefreshTableDoctor();
            }
        }

        private void btnEditDoctor_Click(object sender, EventArgs e)
        {
            conn.Connect();
            int index = dataDoctors.CurrentCell.RowIndex;
            var id = dataDoctors.Rows[index].Cells[0].Value.ToString();
            var surname = dataDoctors.Rows[index].Cells[1].Value.ToString();
            var firstname = dataDoctors.Rows[index].Cells[2].Value.ToString();
            var middlename = dataDoctors.Rows[index].Cells[3].Value.ToString();
            var specialty = dataDoctors.Rows[index].Cells[4].Value.ToString();
            var category = dataDoctors.Rows[index].Cells[5].Value.ToString();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            byte[] img = (Byte[])dataDoctors.CurrentRow.Cells[6].Value;
            MemoryStream ms = new MemoryStream(img);
            Image image = Image.FromStream(ms);

            var selectedCategory = dataDoctors.Rows[index].Cells[5].Value.ToString();
            var selectedSpecialty = dataDoctors.Rows[index].Cells[4].Value.ToString();
            EditDoctor editDoctor = new EditDoctor(surname, firstname, middlename, specialty, category, image, selectedCategory, selectedSpecialty);
            var result = editDoctor.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataDoctors.Rows[index].Cells[1].Value = editDoctor.surname;
                dataDoctors.Rows[index].Cells[2].Value = editDoctor.firstname;
                dataDoctors.Rows[index].Cells[3].Value = editDoctor.middlename;
                dataDoctors.Rows[index].Cells[4].Value = editDoctor.specialty;
                dataDoctors.Rows[index].Cells[5].Value = editDoctor.category;
                dataDoctors.Rows[index].Cells[6].Value = editDoctor.dimage;
                byte[] sph;
                using(MemoryStream mss = new MemoryStream())
                {
                    editDoctor.dimage.Save(mss, ImageFormat.Jpeg);
                    sph = mss.ToArray();
                }
                var changeQuery = $"update Doctors set lastname = '{editDoctor.surname}', firstname = '{editDoctor.firstname}', " +
                    $"middlename = '{editDoctor.middlename}', specialty_id = (SELECT id FROM Specialties WHERE name = '{editDoctor.specialty}'), category_id = (SELECT id FROM Category WHERE name = '{editDoctor.category}'), " +
                    $"image = @image WHERE id = '{id}'";
                var command = new SqlCommand(changeQuery, conn.getConnection());
                conn.getConnection();
                adapter.SelectCommand = command;
                command.Parameters.AddWithValue("@image", sph);
                command.ExecuteNonQuery();
                adapter.Fill(table);
                MessageBox.Show("Запись обновлена!", "", MessageBoxButtons.OK);
                conn.Disconnect();
                RefreshTableDoctor();
            }
        }

        #endregion

        #region [ Patient Tab ]

        public void RefreshTablePatient()
        {
            conn.Connect();

            dataPatients.DataSource = null;
            dataPatients.Rows.Clear();

            SqlCommand command = new SqlCommand("select Patients.id as 'ID', Patients.lastname as 'Фамилия'," +
                "Patients.firstname as 'Имя', Patients.middlename as 'Отчество', Patients.age as 'Возраст', Patients.[address] as 'Адрес', " +
                "Categories_of_patients.name as 'Категория' " +
                "from Patients, Categories_of_patients WHERE Categories_of_patients.id = Patients.category_id", conn.connection);
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            dataPatients.DataSource = table;
            conn.Disconnect();
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            AddPatient addPatient = new AddPatient();
            addPatient.ShowDialog();
            RefreshTablePatient();
        }
        private void dataPatients_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            conn.Connect();
            int index = dataPatients.CurrentCell.RowIndex;
            var id = dataPatients.Rows[index].Cells[0].Value.ToString();
            var surname = dataPatients.Rows[index].Cells[1].Value.ToString();
            var firstname = dataPatients.Rows[index].Cells[2].Value.ToString();
            var middlename = dataPatients.Rows[index].Cells[3].Value.ToString();
            int age = (int)dataPatients.Rows[index].Cells[4].Value;
            var address = dataPatients.Rows[index].Cells[5].Value.ToString();
            var category = dataPatients.Rows[index].Cells[6].Value.ToString();

            string selectedCategory = dataDoctors.Rows[index].Cells[6].Value.ToString();
            EditPatient editPatient = new EditPatient(surname, firstname, middlename, age, address, category, selectedCategory);
            var result = editPatient.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataPatients.Rows[index].Cells[1].Value = editPatient.surname;
                dataPatients.Rows[index].Cells[2].Value = editPatient.firstname;
                dataPatients.Rows[index].Cells[3].Value = editPatient.middlename;
                dataPatients.Rows[index].Cells[4].Value = editPatient.p_age;
                dataPatients.Rows[index].Cells[5].Value = editPatient.address;
                dataPatients.Rows[index].Cells[6].Value = editPatient.category;
                var changeQuery = $"update Patients set lastname = '{editPatient.surname}', firstname = '{editPatient.firstname}', " +
                    $"middlename = '{editPatient.middlename}', age = '{editPatient.p_age}', address = '{editPatient.address}', " +
                    $"category_id = (SELECT id FROM Categories_of_patients WHERE name = '{editPatient.category}') where id = '{id}'";
                var command = new SqlCommand(changeQuery, conn.getConnection());
                conn.getConnection();
                command.ExecuteNonQuery();
                MessageBox.Show("Запись обновлена!", "", MessageBoxButtons.OK);
                conn.Disconnect();
                RefreshTablePatient();
            }
        }
        private void btnEditPatient_Click(object sender, EventArgs e)
        {
            conn.Connect();
            int index = dataPatients.CurrentCell.RowIndex;
            var id = dataPatients.Rows[index].Cells[0].Value.ToString();
            var surname = dataPatients.Rows[index].Cells[1].Value.ToString();
            var firstname = dataPatients.Rows[index].Cells[2].Value.ToString();
            var middlename = dataPatients.Rows[index].Cells[3].Value.ToString();
            int age = (int)dataPatients.Rows[index].Cells[4].Value;
            var address = dataPatients.Rows[index].Cells[5].Value.ToString();
            var category = dataPatients.Rows[index].Cells[6].Value.ToString();

            string selectedCategory = dataDoctors.Rows[index].Cells[6].Value.ToString();
            EditPatient editPatient = new EditPatient(surname, firstname, middlename, age, address, category, selectedCategory);
            var result = editPatient.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataPatients.Rows[index].Cells[1].Value = editPatient.surname;
                dataPatients.Rows[index].Cells[2].Value = editPatient.firstname;
                dataPatients.Rows[index].Cells[3].Value = editPatient.middlename;
                dataPatients.Rows[index].Cells[4].Value = editPatient.p_age;
                dataPatients.Rows[index].Cells[5].Value = editPatient.address;
                dataPatients.Rows[index].Cells[6].Value = editPatient.category;
                var changeQuery = $"update Patients set lastname = '{editPatient.surname}', firstname = '{editPatient.firstname}', " +
                    $"middlename = '{editPatient.middlename}', age = '{editPatient.p_age}', address = '{editPatient.address}', " +
                    $"category_id = (SELECT id FROM Categories_of_patients WHERE name = '{editPatient.category}') where id = '{id}'";
                var command = new SqlCommand(changeQuery, conn.getConnection());
                conn.getConnection();
                command.ExecuteNonQuery();
                MessageBox.Show("Запись обновлена!", "", MessageBoxButtons.OK);
                conn.Disconnect();
                RefreshTablePatient();
            }
        }

        private void btnDelPatient_Click(object sender, EventArgs e)
        {
            int rowIndex = dataPatients.CurrentCell.RowIndex;
            DataGridViewCell cell = dataPatients.Rows[rowIndex].Cells[0];
            string value = cell.Value.ToString();
            dataPatients.Rows.RemoveAt(rowIndex);
            conn.Connect();
            SqlCommand delete = new SqlCommand("delete from Patients where id=" + value, conn.connection);
            delete.ExecuteNonQuery();
            MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK);
            conn.Disconnect();
        }

        #endregion

        #region [ Appeal Tab ]

        public void RefreshTableAppeal()
        {
            conn.Connect();

            dataAppeals.DataSource = null;
            dataAppeals.Rows.Clear();

            SqlCommand command = new SqlCommand("select Appeals.id as 'ID', " +
                "CONCAT(Patients.lastname, ' ', Patients.firstname, ' ', Patients.middlename) as 'Пациент', Appeals.data as 'Дата' " +
                "from Appeals, Patients WHERE Patients.id = Appeals.patient_id", conn.connection);
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            dataAppeals.DataSource = table;
            conn.Disconnect();
        }

        private void btnAddAppeal_Click(object sender, EventArgs e)
        {
            AddAppeal addAppeal = new AddAppeal();
            addAppeal.ShowDialog();
            RefreshTableAppeal();
        }

        private void btnDelAppeal_Click(object sender, EventArgs e)
        {
            int rowIndex = dataAppeals.CurrentCell.RowIndex;
            DataGridViewCell cell = dataAppeals.Rows[rowIndex].Cells[0];
            string value = cell.Value.ToString();
            dataAppeals.Rows.RemoveAt(rowIndex);
            conn.Connect();
            SqlCommand delete = new SqlCommand("delete from Appeals where id=" + value, conn.connection);
            delete.ExecuteNonQuery();
            MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK);
            conn.Disconnect();
        }
        #endregion

        #region [ Treatment Tab ]

        public void RefreshTableTreatment()
        {
            conn.Connect();

            dataTreatments.DataSource = null;
            dataTreatments.Rows.Clear();

            SqlCommand command = new SqlCommand("select Treatments.id as 'ID', Appeals.id as '№ обращения', " +
                "CONCAT(Doctors.lastname, ' ', Doctors.firstname, ' ', Doctors.middlename) as 'Врач', Diagnoses.name as 'Диагноз', " +
                "Treatments.price as 'Стоимость лечения' FROM Treatments, Appeals, Doctors, Diagnoses WHERE (Appeals.id = Treatments.appeal_num " +
                "AND Doctors.id = Treatments.doctor_id AND Diagnoses.id = Treatments.diagnoses_id)", conn.connection);
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            dataTreatments.DataSource = table;
            conn.Disconnect();
        }

        private void btnAddTreatment_Click(object sender, EventArgs e)
        {
            AddTreatment addTreatment = new AddTreatment();
            addTreatment.ShowDialog();
            RefreshTableTreatment();
        }
        
        private void btnEditTreatment_Click(object sender, EventArgs e)
        {
            conn.Connect();
            int index = dataTreatments.CurrentCell.RowIndex;
            var id = dataTreatments.Rows[index].Cells[0].Value.ToString();
            int num_appeal = (int)dataTreatments.Rows[index].Cells[1].Value;
            var doctor = dataTreatments.Rows[index].Cells[2].Value.ToString();
            var diagnosis = dataTreatments.Rows[index].Cells[3].Value.ToString();
            decimal price = (decimal)dataTreatments.Rows[index].Cells[4].Value;

            int selectedNumAppeal = (int)dataTreatments.Rows[index].Cells[1].Value;
            string selectedDoctor = dataTreatments.Rows[index].Cells[2].Value.ToString();
            string selectedDiagnosis = dataTreatments.Rows[index].Cells[3].Value.ToString();
            EditTreatment editTreatment = new EditTreatment(doctor, diagnosis, price, num_appeal, selectedDiagnosis, selectedDoctor, selectedNumAppeal);
            var result = editTreatment.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataTreatments.Rows[index].Cells[1].Value = editTreatment.num_appeal;
                dataTreatments.Rows[index].Cells[2].Value = editTreatment.doctor;
                dataTreatments.Rows[index].Cells[3].Value = editTreatment.diagnosis;
                dataTreatments.Rows[index].Cells[4].Value = editTreatment.price;
                var changeQuery = $"update Treatments set num_appeal = '{editTreatment.num_appeal}', doctor_id = (SELECT id FROM Doctors " +
                    $"WHERE CONCAT(lastname, ' ', firstname, ' ', middlename) = '{editTreatment.doctor}'), " +
                    $"diagnosis_id = (SELECT id FROM Diagnoses WHERE name = '{editTreatment.diagnosis}'), price = '{editTreatment.price}' " +
                    $"where id = '{id}'";
                var command = new SqlCommand(changeQuery, conn.getConnection());
                conn.getConnection();
                command.ExecuteNonQuery();
                MessageBox.Show("Запись обновлена!", "", MessageBoxButtons.OK);
                conn.Disconnect();
                RefreshTableTreatment();
            }
        }

        private void btnDelTreatment_Click(object sender, EventArgs e)
        {
            int rowIndex = dataTreatments.CurrentCell.RowIndex;
            DataGridViewCell cell = dataTreatments.Rows[rowIndex].Cells[0];
            string value = cell.Value.ToString();
            dataTreatments.Rows.RemoveAt(rowIndex);
            conn.Connect();
            SqlCommand delete = new SqlCommand("delete from Treatmnets where id=" + value, conn.connection);
            delete.ExecuteNonQuery();
            MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK);
            conn.Disconnect();
        }

        private void dataTreatments_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            conn.Connect();
            int index = dataTreatments.CurrentCell.RowIndex;
            var id = dataTreatments.Rows[index].Cells[0].Value.ToString();
            int num_appeal = (int)dataTreatments.Rows[index].Cells[1].Value;
            var doctor = dataTreatments.Rows[index].Cells[2].Value.ToString();
            var diagnosis = dataTreatments.Rows[index].Cells[3].Value.ToString();
            decimal price = (decimal)dataTreatments.Rows[index].Cells[4].Value;

            int selectedNumAppeal = (int)dataTreatments.Rows[index].Cells[1].Value;
            string selectedDoctor = dataTreatments.Rows[index].Cells[2].Value.ToString();
            string selectedDiagnosis = dataTreatments.Rows[index].Cells[3].Value.ToString();
            EditTreatment editTreatment = new EditTreatment(doctor, diagnosis, price, num_appeal, selectedDiagnosis, selectedDoctor, selectedNumAppeal);
            var result = editTreatment.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataTreatments.Rows[index].Cells[1].Value = editTreatment.num_appeal;
                dataTreatments.Rows[index].Cells[2].Value = editTreatment.doctor;
                dataTreatments.Rows[index].Cells[3].Value = editTreatment.diagnosis;
                dataTreatments.Rows[index].Cells[4].Value = editTreatment.price;
                var changeQuery = $"update Treatments set num_appeal = '{editTreatment.num_appeal}', doctor_id = (SELECT id FROM Doctors " +
                    $"WHERE CONCAT(lastname, ' ', firstname, ' ', middlename) = '{editTreatment.doctor}'), " +
                    $"diagnosis_id = (SELECT id FROM Diagnoses WHERE name = '{editTreatment.diagnosis}'), price = '{editTreatment.price}' " +
                    $"where id = '{id}'";
                var command = new SqlCommand(changeQuery, conn.getConnection());
                conn.getConnection();
                command.ExecuteNonQuery();
                MessageBox.Show("Запись обновлена!", "", MessageBoxButtons.OK);
                conn.Disconnect();
                RefreshTableTreatment();
            }
        }

        #endregion

        #endregion

        #region [ Reference books ]
        private void button2_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = true;
            button3.BackColor = Color.White;
            button1.BackColor = Color.White;
            button2.BackColor = Color.PeachPuff;
            RefreshTable(dataSpecialties, "Specialties");
        }
        private void tabReferenceBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage selectedTab = tabReferenceBooks.SelectedTab;

            if (selectedTab == tabPageSpecialties) // проверяем, что выбрана нужная вкладка
            {
                RefreshTable(dataSpecialties, "Specialties");
            }
            else if (selectedTab == tabPageCategories) // аналогично для других вкладок
            {
                RefreshTable(dataCategory, "Category");
            }
            else if (selectedTab == tabPageDiagnosis) // аналогично для других вкладок
            {
                RefreshTable(dataDiagnosis, "Diagnoses");
            }
            else if (selectedTab == tabPagePatientCategory) // аналогично для других вкладок
            {
                RefreshTable(dataPatientCategory, "Categories_of_patients");
            }
        }


        #region [ Specialty Tab ]
        private void ReadSingleRowSpecialties(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1));
        }

        private void buttonAddSpecialty_Click(object sender, EventArgs e)
        {
            RefreshTable(dataSpecialties, "Specialties");

            if (textBoxSpecialty.Text == null)
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    conn.Connect();
                    SqlCommand command = new SqlCommand();

                    command = new SqlCommand(
                        "insert into Specialties (name) " +
                        "values (@name)", conn.connection);
                    command.Parameters.Add("name", SqlDbType.NVarChar).Value = textBoxSpecialty.Text;
                    command.ExecuteNonQuery();

                    MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK);
                    textBoxSpecialty.Clear();

                    RefreshTable(dataSpecialties, "Specialties");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Disconnect();
            }
        }

        private void buttonDelSpecialty_Click(object sender, EventArgs e)
        {
            int rowIndex = dataSpecialties.CurrentCell.RowIndex;
            DataGridViewCell cell = dataSpecialties.Rows[rowIndex].Cells[0];
            string value = cell.Value.ToString();
            dataSpecialties.Rows.RemoveAt(rowIndex);
            conn.Connect();
            SqlCommand delete = new SqlCommand("delete from Specialties where id=" + value, conn.connection);
            delete.ExecuteNonQuery();
            MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK);
            conn.Disconnect();
        }
        #endregion

        #region [ Category Tab ]
        private void ReadSingleRowCategory(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1));
        }
        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            RefreshTable(dataCategory, "Category");

            if (textBoxSpecialty.Text == null)
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    conn.Connect();
                    SqlCommand command = new SqlCommand();

                    command = new SqlCommand(
                        "insert into Category (name) " +
                        "values (@name)", conn.connection);
                    command.Parameters.Add("name", SqlDbType.NVarChar).Value = textBoxCategory.Text;
                    command.ExecuteNonQuery();

                    MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK);
                    textBoxCategory.Clear();

                    RefreshTable(dataCategory, "Category");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Disconnect();
            }
        }

        private void buttonDelCategory_Click(object sender, EventArgs e)
        {
            int rowIndex = dataCategory.CurrentCell.RowIndex;
            DataGridViewCell cell = dataCategory.Rows[rowIndex].Cells[0];
            string value = cell.Value.ToString();
            dataCategory.Rows.RemoveAt(rowIndex);
            conn.Connect();
            SqlCommand delete = new SqlCommand("delete from Category where id=" + value, conn.connection);
            delete.ExecuteNonQuery();
            MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK);
            conn.Disconnect();
        }
        #endregion

        #region [ Diagnoses Tab ]
        private void ReadSingleRowDiagnoses(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1));
        }
        private void buttonAddDiagnoses_Click(object sender, EventArgs e)
        {
            RefreshTable(dataDiagnosis, "Diagnoses");

            if (textBoxSpecialty.Text == null)
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    conn.Connect();
                    SqlCommand command = new SqlCommand();

                    command = new SqlCommand(
                        "insert into Diagnoses (name) " +
                        "values (@name)", conn.connection);
                    command.Parameters.Add("name", SqlDbType.NVarChar).Value = textBoxDiagnoses.Text;
                    command.ExecuteNonQuery();

                    MessageBox.Show("Запись добавлена!", "", MessageBoxButtons.OK);
                    textBoxDiagnoses.Clear();

                    RefreshTable(dataDiagnosis, "Diagnoses");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Disconnect();
            }
        }

        private void buttonDelDiagnoses_Click(object sender, EventArgs e)
        {
            int rowIndex = dataDiagnosis.CurrentCell.RowIndex;
            DataGridViewCell cell = dataDiagnosis.Rows[rowIndex].Cells[0];
            string value = cell.Value.ToString();
            dataDiagnosis.Rows.RemoveAt(rowIndex);
            conn.Connect();
            SqlCommand delete = new SqlCommand("delete from Diagnoses where id=" + value, conn.connection);
            delete.ExecuteNonQuery();
            MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK);
            conn.Disconnect();
        }
        #endregion

        #region [ Categories of patients Tab ]
        private void ReadSingleRowPatientCategory(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetDecimal(2));
        }
        private void buttonAddPatientCategory_Click(object sender, EventArgs e)
        {
            AddPatientCategory addPatientCategory = new AddPatientCategory();
            addPatientCategory.ShowDialog();
            RefreshTable(dataPatientCategory, "Categories_of_patients");
        }

        private void buttonDelPatientCategory_Click(object sender, EventArgs e)
        {
            int rowIndex = dataPatientCategory.CurrentCell.RowIndex;
            DataGridViewCell cell = dataPatientCategory.Rows[rowIndex].Cells[0];
            string value = cell.Value.ToString();
            dataPatientCategory.Rows.RemoveAt(rowIndex);
            conn.Connect();
            SqlCommand delete = new SqlCommand("delete from Categories_of_patients where id=" + value, conn.connection);
            delete.ExecuteNonQuery();
            MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK);
            conn.Disconnect();
        }
        private void dataPatientCategory_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            conn.Connect();
            int index = dataPatientCategory.CurrentCell.RowIndex;
            var id = dataPatientCategory.Rows[index].Cells[0].Value.ToString();
            var name = dataPatientCategory.Rows[index].Cells[1].Value.ToString();
            var discount = dataPatientCategory.Rows[index].Cells[2].Value.ToString();
            EditPatientCategory editPatientCategory = new EditPatientCategory(name, discount);
            var result = editPatientCategory.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataPatientCategory.Rows[index].Cells[1].Value = editPatientCategory.name;
                dataPatientCategory.Rows[index].Cells[2].Value = editPatientCategory.discount;
                var changeQuery = $"update Categories_of_patients set name = '{editPatientCategory.name}', discount = '{editPatientCategory.discount}' where id = '{id}'";
                var command = new SqlCommand(changeQuery, conn.getConnection());
                conn.getConnection();
                command.ExecuteNonQuery();
                conn.Disconnect();
                RefreshTable(dataPatientCategory, "Categories_of_patients");
            }
        }

        private void buttonEditPatientCategory_Click(object sender, EventArgs e)
        {
            conn.Connect();
            int index = dataPatientCategory.CurrentCell.RowIndex;
            var id = dataPatientCategory.Rows[index].Cells[0].Value.ToString();
            var name = dataPatientCategory.Rows[index].Cells[1].Value.ToString();
            var discount = dataPatientCategory.Rows[index].Cells[2].Value.ToString();
            EditPatientCategory editPatientCategory = new EditPatientCategory(name, discount);
            var result = editPatientCategory.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataPatientCategory.Rows[index].Cells[1].Value = editPatientCategory.name;
                dataPatientCategory.Rows[index].Cells[2].Value = editPatientCategory.discount;
                var changeQuery = $"update Categories_of_patients set name = '{editPatientCategory.name}', discount = '{editPatientCategory.discount}' where id = '{id}'";
                var command = new SqlCommand(changeQuery, conn.getConnection());
                conn.getConnection();
                command.ExecuteNonQuery();
                conn.Disconnect();
                RefreshTable(dataPatientCategory, "Categories_of_patients");
            }
        }

        #endregion

        #endregion

        #region [ Results ]

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = false;
            panel4.Visible = true;
            button2.BackColor = Color.White;
            button1.BackColor = Color.White;
            button3.BackColor = Color.PeachPuff;
            LoadComboBoxNumAppeal();
            LoadComboBoxPatient();
            LoadComboBoxDoctor();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region [ Search by Appeal ID ]
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (comboBoxAppealId.Text == "")
            {
                MessageBox.Show("Вы не ввели номер!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    label17.Visible = false;
                    LoadResByAppealM();
                    LoadResByAppealP();
                    int count = 0;
                    for (int i = 0; i < dataByTreatment.Rows.Count; ++i)
                    {
                        count++;
                    }
                    if (count == 0)
                    {
                        label19.Text = " ";
                        label18.Text = " ";
                        MessageBox.Show("Записей не найдено!", "", MessageBoxButtons.OK);
                    }
                    else if(count != 0)
                    {
                        label17.Visible = true;
                        double sum = 0;
                        for (int i = 0; i < dataByTreatment.Rows.Count; ++i)
                        {
                            sum += Convert.ToInt32(dataByTreatment.Rows[i].Cells[2].Value);
                        }
                        sum = sum * ((100 - Convert.ToDouble(textBoxDiscount.Text)) / 100);
                        label19.Text = sum.ToString() + "p";
                        if (count == 1)
                        {
                            label18.Text = $"Найдена {count} запись";
                        }
                        else if (count > 1 && count <= 4)
                        {
                            label18.Text = $"Найдено {count} записи";
                        }
                        else
                        {
                            label18.Text = $"Найдено {count} записей";
                        }

                        MessageBox.Show("Поиск завершен!", "", MessageBoxButtons.OK);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadResByAppealP()
        {
            textBoxPFullname.Clear();
            textBoxDate.Clear();
            textBoxAge.Clear();
            textBoxDiscount.Clear();
            textBoxPatientCategory.Clear();
            int id = Convert.ToInt32(comboBoxAppealId.Text);
            SqlCommand command = new SqlCommand();
            conn.Connect();
            string query = $"SELECT CONCAT(Patients.lastname, ' ', Patients.firstname, ' ', Patients.middlename) AS 'Пациент', " +
                        $"Appeals.data AS 'Дата', Patients.age AS 'Возраст', Categories_of_patients.name AS 'Категория', " +
                        $"Categories_of_patients.discount AS 'Скидка' FROM Patients, Appeals, Categories_of_patients " +
                        $"WHERE (Appeals.id = '{id}' AND Appeals.patient_id = Patients.id AND Patients.category_id = Categories_of_patients.id)";

            command = new SqlCommand(query, conn.connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                textBoxPFullname.Text = reader["Пациент"].ToString();
                textBoxDate.Text = Convert.ToDateTime(reader["Дата"]).ToString("dd/MM/yyyy");
                textBoxAge.Text = reader["Возраст"].ToString();
                textBoxPatientCategory.Text = reader["Категория"].ToString();
                textBoxDiscount.Text = reader["Скидка"].ToString();
            }

            reader.Close();
            conn.Disconnect();
        }
        private void LoadResByAppealM()
        {
            int id = Convert.ToInt32(comboBoxAppealId.Text);
            SqlCommand command = new SqlCommand();
            conn.Connect();
            DataTable table = new DataTable();
            string query = $"SELECT CONCAT(Doctors.lastname, ' ', Doctors.firstname, ' ', Doctors.middlename) AS 'Врач', Diagnoses.name AS 'Диагноз', " +
            $"Treatments.price AS 'Стоимость' FROM Doctors, Diagnoses, Treatments WHERE (Treatments.appeal_num = {id} AND " +
            $"Treatments.doctor_id = Doctors.id AND Treatments.diagnoses_id = Diagnoses.id)";
            command = new SqlCommand(query, conn.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            SqlDataReader reader = command.ExecuteReader();
            dataByTreatment.DataSource = table;
            conn.Disconnect();
        }
        #endregion

        #region [ Search by Doctor ]

        private void btnSearchDoctorID_Click(object sender, EventArgs e)
        {
            int id = int.Parse(comboBoxDoctorId.SelectedValue.ToString());
            if (comboBoxDoctorId.Text == null)
            {
                MessageBox.Show("Вы не ввели номер!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    label21.Visible = false;
                    LoadResByDoctorM();
                    LoadResByDoctorP();
                    int count = 0;
                    for (int i = 0; i < dataByDoctor.Rows.Count; ++i)
                    {
                        count++;
                    }
                    if (count == 0)
                    {
                        label20.Visible = false;
                        //textBox1.Text = " ";
                        //textBox3.Text = " ";
                        //pictureBox3.Image = null;
                        label11.Text = " ";
                        MessageBox.Show("Записей не найдено!", "", MessageBoxButtons.OK);
                    }
                    else if (count != 0)
                    {
                        label20.Visible = true;
                        label21.Visible = true;
                        double sum = 0;
                        for (int i = 0; i < dataByDoctor.Rows.Count; ++i)
                        {
                            sum += Convert.ToInt32(dataByDoctor.Rows[i].Cells[3].Value);
                        }
                        sum = sum * ((100 - Convert.ToDouble(dataByDoctor.Rows[count-1].Cells[4].Value)) / 100);
                        label11.Text = sum.ToString() + "p";
                        if (count == 1)
                        {
                            label20.Text = $"Найдена {count} запись";
                        }
                        else if (count > 1 && count <= 4)
                        {
                            label20.Text = $"Найдено {count} записи";
                        }
                        else
                        {
                            label20.Text = $"Найдено {count} записей";
                        }

                        MessageBox.Show("Поиск завершен!", "", MessageBoxButtons.OK);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadResByDoctorP()
        {
            textBox1.Clear();
            textBox3.Clear();
            pictureBox3.Image = null;
            int id = int.Parse(comboBoxDoctorId.SelectedValue.ToString());
            SqlCommand command = new SqlCommand();
            conn.Connect();
            string query = $"SELECT " +
                        $"Specialties.name AS 'Специальность', Category.name AS 'Категория', Doctors.image AS 'Фото' " +
                        $"FROM Doctors, Specialties, Category " +
                        $"WHERE (Doctors.id = '{id}' AND Doctors.specialty_id = Specialties.id AND Doctors.category_id = Category.id)";

            command = new SqlCommand(query, conn.connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                textBox1.Text = reader["Специальность"].ToString();
                textBox3.Text = reader["Категория"].ToString();
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                byte[] picture = (byte[])reader["Фото"];
                MemoryStream ms = new MemoryStream(picture);
                pictureBox3.Image = Image.FromStream(ms);
            }

            reader.Close();
            conn.Disconnect();
        }
        private void LoadResByDoctorM()
        {
            int id = int.Parse(comboBoxDoctorId.SelectedValue.ToString());
            SqlCommand command = new SqlCommand();
            conn.Connect();
            DataTable table = new DataTable();
            string query = $"SELECT  Appeals.id '№ обращения', CONCAT(Patients.lastname, ' ', Patients.firstname, ' ', Patients.middlename) AS 'Пациент', Diagnoses.name AS 'Диагноз', " +
            $"Treatments.price AS 'Стоимость', Categories_of_patients.discount AS 'Скидка' FROM Appeals, Patients, Diagnoses, Treatments, Categories_of_patients WHERE (Treatments.doctor_id = {id} AND " +
            $"Treatments.appeal_num = Appeals.id AND Appeals.patient_id = Patients.id AND Treatments.diagnoses_id = Diagnoses.id AND Patients.category_id = Categories_of_patients.id)";
            command = new SqlCommand(query, conn.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            SqlDataReader reader = command.ExecuteReader();
            dataByDoctor.DataSource = table;
            conn.Disconnect();
        }


        #endregion

        #region [ Search by Patient ]

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBoxPatientId.Text == "")
            {
                MessageBox.Show("Вы не ввели номер!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    label28.Visible = false;
                    LoadResByPatientM();
                    LoadResByPatientP();
                    int count = 0;
                    for (int i = 0; i < dataByPatient.Rows.Count; ++i)
                    {
                        count++;
                    }
                    if (count == 0)
                    {
                        textBox8.Text = " ";
                        textBox2.Text = " ";
                        textBox4.Text = " ";
                        label25.Text = " ";
                        label23.Text = " ";
                        MessageBox.Show("Записей не найдено!", "", MessageBoxButtons.OK);
                    }
                    else if (count != 0)
                    {
                        label28.Visible = true;
                        double sum = 0;
                        for (int i = 0; i < dataByPatient.Rows.Count; ++i)
                        {
                            sum += Convert.ToInt32(dataByPatient.Rows[i].Cells[3].Value);
                        }
                        sum = sum * ((100 - Convert.ToDouble(textBox5.Text)) / 100);
                        label23.Text = sum.ToString() + "p";
                        if (count == 1)
                        {
                            label25.Text = $"Найдена {count} запись";
                        }
                        else if (count > 1 && count <= 4)
                        {
                            label25.Text = $"Найдено {count} записи";
                        }
                        else
                        {
                            label25.Text = $"Найдено {count} записей";
                        }

                        MessageBox.Show("Поиск завершен!", "", MessageBoxButtons.OK);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadResByPatientP()
        {
            textBox8.Clear();
            textBox2.Clear();
            textBox5.Clear();
            textBox4.Clear();
            int id = int.Parse(comboBoxPatientId.SelectedValue.ToString());
            SqlCommand command = new SqlCommand();
            conn.Connect();
            string query = $"SELECT Patients.age AS 'Возраст', Patients.address AS 'Адрес', " +
                        $"Categories_of_patients.name AS 'Категория', Categories_of_patients.discount AS 'Скидка' " +
                        $"FROM Patients, Categories_of_patients " +
                        $"WHERE (Patients.id = '{id}' AND Patients.category_id = Categories_of_patients.id)";

            command = new SqlCommand(query, conn.connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                textBox8.Text = reader["Возраст"].ToString();
                textBox5.Text = reader["Скидка"].ToString();
                textBox2.Text = reader["Адрес"].ToString();
                textBox4.Text = reader["Категория"].ToString();
            }

            reader.Close();
            conn.Disconnect();
        }
        private void LoadResByPatientM()
        {
            int id = int.Parse(comboBoxPatientId.SelectedValue.ToString());
            SqlCommand command = new SqlCommand();
            conn.Connect();
            DataTable table = new DataTable();
            string query = $"SELECT Appeals.data AS 'Дата', CONCAT(Doctors.lastname, ' ', Doctors.firstname, ' ', Doctors.middlename) AS 'Врач', Diagnoses.name AS 'Диагноз', " +
            $"Treatments.price AS 'Стоимость' FROM Appeals, Doctors, Diagnoses, Treatments " +
            $"WHERE (Appeals.patient_id = {id} AND " +
            $"Treatments.appeal_num = Appeals.id AND Treatments.doctor_id = Doctors.id AND Treatments.diagnoses_id = Diagnoses.id)";
            command = new SqlCommand(query, conn.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            SqlDataReader reader = command.ExecuteReader();
            dataByPatient.DataSource = table;
            conn.Disconnect();
        }

        #endregion

        #region [ Search by Date ]

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                label35.Visible = false;
                LoadResByDate();
                int count = 0;
                for (int i = 0; i < dataByDate.Rows.Count; ++i)
                {
                    count++;
                }
                if (count == 0)
                {
                    label34.Text = " ";
                    label31.Text = " ";
                    MessageBox.Show("Записей не найдено!", "", MessageBoxButtons.OK);
                }
                else if (count != 0)
                {
                    label35.Visible = true;
                    double sum = 0;
                    double sum2 = 0;
                    double sum3 = 0;
                    for (int i = 0; i < dataByDate.Rows.Count; ++i)
                    {
                        sum = Convert.ToInt32(dataByDate.Rows[i].Cells[3].Value);
                        sum2 = sum * ((100 - Convert.ToDouble(dataByDate.Rows[i].Cells[4].Value)) / 100);
                        sum3 += sum2;
                    }

                    //sum = sum * ((100 - Convert.ToDouble(dataByDate.Rows[count - 1].Cells[4].Value)) / 100);
                    label31.Text = sum3.ToString() + "p";
                    if (count == 1)
                    {
                        label34.Text = $"Найдена {count} запись";
                    }
                    else if (count > 1 && count <= 4)
                    {
                        label34.Text = $"Найдено {count} записи";
                    }
                    else
                    {
                        label34.Text = $"Найдено {count} записей";
                    }

                    MessageBox.Show("Поиск завершен!", "", MessageBoxButtons.OK);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadResByDate()
        {
            SqlCommand command = new SqlCommand();
            conn.Connect();
            DataTable table = new DataTable();
            DateTime first = dateTimePicker1.Value;
            DateTime second = dateTimePicker2.Value;
            string query = $"SELECT CONCAT(Patients.lastname, ' ', Patients.firstname, ' ', Patients.middlename) AS 'Пациент'," +
                $"CONCAT(Doctors.lastname, ' ', Doctors.firstname, ' ', Doctors.middlename) AS 'Врач', Diagnoses.name AS 'Диагноз', " +
            $"Treatments.price AS 'Стоимость', Categories_of_patients.discount AS 'Скидка' FROM Appeals, Doctors, Diagnoses, Treatments, Patients, Categories_of_patients " +
            $"WHERE (data < '{second}' AND data > '{first}' AND Appeals.patient_id = Patients.id AND Patients.category_id = Categories_of_patients.id AND " +
            $"Treatments.appeal_num = Appeals.id AND Treatments.doctor_id = Doctors.id AND Treatments.diagnoses_id = Diagnoses.id)";
            command = new SqlCommand(query, conn.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            SqlDataReader reader = command.ExecuteReader();
            dataByDate.DataSource = table;
            conn.Disconnect();
        }

        #endregion

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
                comboBoxAppealId.DisplayMember = "id";
                comboBoxAppealId.ValueMember = "id";
                comboBoxAppealId.DataSource = table;
            }
            conn.Disconnect();
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
                comboBoxPatientId.DisplayMember = "Пациент";
                comboBoxPatientId.ValueMember = "id";
                comboBoxPatientId.DataSource = table;
            }
            conn.Disconnect();
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

                comboBoxDoctorId.DisplayMember = "Врач";
                comboBoxDoctorId.ValueMember = "id";
                comboBoxDoctorId.DataSource = table;
            }
            conn.Disconnect();
        }

        #endregion

        private void textBoxSpecialty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void textBoxCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void textBoxDiagnoses_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        
    }
}
