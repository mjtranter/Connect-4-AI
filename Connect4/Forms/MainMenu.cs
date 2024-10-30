using System;
using System.Windows.Forms;

namespace Connect4
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        public static bool AINeeded = false;
        private void MainMenu_Load(object sender, EventArgs e)
        {
            txtUsername.Text = Login.username;
        }
        public void Btn1Player_Click(object sender, EventArgs e)
        {
            AINeeded = true;
            Form colour = new ColourSelect();
            colour.Show();
            Hide();
        }

        public void Btn2Player_Click(object sender, EventArgs e)
        {
            AINeeded = false;
            Form game = new GameScreen();
            game.Show();
            Hide();
        }

        private void BtnLeaderboard_Click(object sender, EventArgs e)
        {
            Form leaderboard = new Leaderboard();
            leaderboard.Show();
            Hide();
        }

        private void BtnOptions_Click(object sender, EventArgs e)
        {
            Form options = new Options();
            options.Show();
            Hide();
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to quit?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {                
                Application.Exit();
            }
        }
    }
}
