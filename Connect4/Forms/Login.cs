using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Connect4
{
    public partial class Login : Form
    {
        public static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\mjtra\OneDrive\Documents\School stuff\My Work\!Connect 4\Connect4\Connect4\Database2.mdf';Integrated Security=True";
        public static string username = "";

        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string hashedPassword = HashText(txtPassword.Text);

            username = txtUsername.Text;
            lblIncorrect.Hide();
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();            
            int difficulty = 0;
            string sql = "SELECT [difficulty] FROM [Table] WHERE [username] = @username AND [password] = @password;";
            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.Add("username", SqlDbType.NVarChar).Value = username;
            command.Parameters.Add("password", SqlDbType.NVarChar).Value = hashedPassword;
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                difficulty = dataReader["difficulty"] == DBNull.Value ? 0 : (int)dataReader["difficulty"];
            }
            if (difficulty == 0)
            {
                lblIncorrect.Show();
            }
            else
            {
                command.Dispose();
                cnn.Close();
                Form main = new MainMenu();
                main.Show();
                Hide();                
            }            
        }

        public static string HashText(string text)
        {
            // source : www.c-sharpcorner.com/article/compute-sha256-hash-in-c-sharp/
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] bytes = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString());
                }
                return builder.ToString();
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            Form register = new Register();
            register.Show();
            Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
