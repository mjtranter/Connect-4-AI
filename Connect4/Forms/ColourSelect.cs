using System;
using System.Windows.Forms;

namespace Connect4
{
    public partial class ColourSelect : Form
    {
        public static int playerColour = 0;
        public ColourSelect()
        {
            InitializeComponent();
        }

        private void BtnYellow_Click(object sender, EventArgs e)
        {
            playerColour = 1;
            Form game = new GameScreen();
            game.Show();
            Hide();
        }

        private void BtnRed_Click(object sender, EventArgs e)
        {
            playerColour = 2;
            Form game = new GameScreen();
            game.Show();
            Hide();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Form menu = new MainMenu();
            menu.Show();
            Hide();
        }
    }
}
