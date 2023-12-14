using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMSClinic
{
    public class DBConnect
    {
        public SqlConnection connection;
        public void Connect()
        {
            try
            {
                connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\focsit\Desktop\Course work\DBMSClinic - Copy\DBMSClinic\db.mdf"";Integrated Security=True");
                connection.Open();
            }
            catch (SqlException exception)
            {
                MessageBox.Show("Ошибка подключения к базе данных", exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public SqlConnection getConnection()
        {
            {
                return connection;
            }
        }
        public void Disconnect()
        {
            connection.Close();
        }
    }
}
