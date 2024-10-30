using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Connect4
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection(Login.connectionString);

        private void Options_Load(object sender, EventArgs e)
        {
            lblUsername.Text = Login.username;
            cnn.Open();
            string sql = "SELECT [difficulty] FROM [Table] WHERE [username] = @username;";
            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.Add("username", SqlDbType.NVarChar).Value = Login.username;
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                int difficulty = dataReader["difficulty"] == DBNull.Value ? 1 : (int)dataReader["difficulty"];
                tbSlider.Value = difficulty;
            }
            command.Dispose();
            dataReader.Close();
            cnn.Close();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Form main = new MainMenu();
            main.Show();
            Hide();
        }

        private void TbSlider_Scroll(object sender, EventArgs e)
        {
            int difficulty = tbSlider.Value;
            cnn.Open();
            string sql = "UPDATE [Table] SET [difficulty] = @difficulty WHERE [username] = @username;";
            SqlDataAdapter adapt = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.Add("username", SqlDbType.NVarChar).Value = Login.username;
            command.Parameters.Add("difficulty", SqlDbType.Int).Value = difficulty; 
            command.ExecuteNonQuery();
            command.Dispose();
            adapt.Dispose();
            cnn.Close();
        }
    }
}
