using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Connect4
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string hashedPassword = Login.HashText(txtPassword.Text);  
            lblNull.Hide();
            lblPasswordError.Hide();
            lblError.Hide();
            if (txtUsername.Text == "" || txtPassword.Text == "" || txtConfirm.Text == "")
            {
                lblNull.Show();
                return;
            }
            else if (txtPassword.Text != txtConfirm.Text)
            {
                lblPasswordError.Show();
                return;
            }
            else
            {
                string connectionString = Login.connectionString;
                SqlConnection cnn = new SqlConnection(connectionString);
                cnn.Open();
                try
                {
                    string sql  = "INSERT INTO [Table] ([username], [password], [difficulty], [score]) VALUES (@username, @password, 1, 0);";
                    SqlCommand command = new SqlCommand(sql, cnn);
                    command.Parameters.Add("username", SqlDbType.NVarChar).Value = username;
                    command.Parameters.Add("password", SqlDbType.NVarChar).Value = hashedPassword;
                    command.ExecuteNonQuery();
                    command.Dispose();
                    lblSuccess.Show();
                    btnRegister.Hide();
                }
                catch
                {
                    lblError.Show();
                }
                cnn.Close();
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Form login = new Login();
            login.Show();
            Hide();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}
