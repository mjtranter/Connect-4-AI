using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Connect4
{
    public partial class Leaderboard : Form
    {
        public Leaderboard()
        {
            InitializeComponent();
        }
                
        private void BtnBack_Click(object sender, EventArgs e)
        {
            Form main = new MainMenu();
            main.Show();
            Hide();
        }

        private void Leaderboard_Load(object sender, EventArgs e)
        {
            string username = Login.username;
            string connectionString = Login.connectionString;
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            string sql = "SELECT [username],[score] FROM [Table] ORDER BY [score] DESC";
            SqlCommand command = new SqlCommand(sql, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                string users = dataReader["username"] == DBNull.Value ? "" : (string)dataReader["username"];
                int currentScore = dataReader["score"] == DBNull.Value ? 0 : (int)dataReader["score"];
                dgvData.Rows.Add(users, currentScore.ToString());
            }
            command.Dispose();
            cnn.Close();
            dgvData.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvData.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvData.DefaultCellStyle.SelectionForeColor = dgvData.ForeColor;
            DataGridViewCellStyle style = new DataGridViewCellStyle
            {
                BackColor = Color.CornflowerBlue
            };
            DataGridViewCellStyle lightStyle = new DataGridViewCellStyle
            {
                BackColor = Color.LightGray
            };
            DataGridViewCellStyle darkStyle = new DataGridViewCellStyle
            {
                BackColor = Color.Gray
            };
            int i = 0;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                
                if (i % 2 == 0)
                {
                    row.Cells[0].Style = lightStyle;
                    row.Cells[1].Style = lightStyle;
                }
                else
                {
                    row.Cells[0].Style = darkStyle;
                    row.Cells[1].Style = darkStyle;
                }
                if (row.Cells[0].Value.ToString() == username)
                {
                    row.Cells[0].Style = style;
                    row.Cells[1].Style = style;
                }
                i++;  
            }
        }

        private void DgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.CurrentCell.Style.BackColor == Color.CornflowerBlue)
            {
                dgvData.CurrentCell.Style.SelectionBackColor = Color.CornflowerBlue;
            }
            else if (dgvData.CurrentCell.RowIndex % 2 == 0)
            {
                dgvData.CurrentCell.Style.SelectionBackColor = Color.LightGray;
            }
            else
            {
                dgvData.CurrentCell.Style.SelectionBackColor = Color.Gray;
            }
        }
    }
}
