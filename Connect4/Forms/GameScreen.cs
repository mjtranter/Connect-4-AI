using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Connect4
{
    public partial class GameScreen : Form
    {
        static GameScreen game;
        public static int depth = 0;
        int score = 0;
        int colour = 0;
        public static int aiColour = 0;
        public bool gameDone = false;

        public GameScreen()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            gameDone = false;

            for (int column = 0; column < GameBoard.width; column++)
            {
                for (int row = 0; row < GameBoard.height; row++)
                {
                    GameBoard.boardValues[row, column] = 0;
                }
            }

            SqlConnection cnn = new SqlConnection(Login.connectionString);
            cnn.Open();
            string sql = "SELECT [difficulty] FROM [Table] WHERE [username] = @username;";
            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.Add("username", SqlDbType.NVarChar).Value = Login.username;
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            
            depth = dataReader["difficulty"] == DBNull.Value ? 1 : (int)dataReader["difficulty"];
            
            game = this;
            gridTemplate.Hide();
            PickTeam();
            for (int row = 0; row < GameBoard.height; row++)
            {
                for (int column = 0; column < GameBoard.width; column++)
                {
                    PictureBox p = new PictureBox
                    {
                        Width = gridTemplate.Width - 5,
                        Height = gridTemplate.Height - 5,
                        Top = gridTemplate.Top + gridTemplate.Height + (row * gridTemplate.Height),
                        Left = gridTemplate.Left + (column * gridTemplate.Width),
                        BackColor = Color.White,
                        Name = "img" + row + column
                    };
                    game.Controls.Add(p);

                    if (row == 0)
                    {
                        Button b = new Button
                        {
                            Width = gridTemplate.Width - 5,
                            Height = gridTemplate.Height - 5,
                            Top = gridTemplate.Top,
                            Left = gridTemplate.Left + (column * gridTemplate.Width),
                            Tag = column,
                            TabStop = false,
                            BackColor = Color.LightGray
                        };
                        b.Click += BtnDropCounter_Click;
                        game.Controls.Add(b);
                    }
                }
            }
            if (MainMenu.AINeeded)
            {
                if (aiColour == colour)
                {
                    MessageBox.Show("AI will place first.");
                    AI AI = new AI();
                    AI.RunAI(GameBoard.boardValues);
                }
                else
                {
                    MessageBox.Show("You will place first.");
                }
            }
        }

        private void BtnDropCounter_Click(object sender, EventArgs e)
        {
            if (!gameDone)
            {
                int column = (int)((Button)sender).Tag;
                for (int row = 0; row < GameBoard.height; row++)
                {
                    if (GameBoard.boardValues[GameBoard.height - row - 1, column] == 0)
                    {
                        GameBoard.boardValues[GameBoard.height - row - 1, column] = colour;
                        PaintColours();
                        break;
                    }
                }
            }
        }

        private void PaintColours()
        {
            for (int col = 0; col < GameBoard.width; col++)
            {
                for (int row = 0; row < GameBoard.height; row++)
                {
                    Controls.Find("img" + row + col, true)[0].BackColor = GameBoard.GetColour(row, col);
                }
            }
            CheckTerminalNode(GameBoard.boardValues, colour, false);
            if (!gameDone)
            {
                SwapColour();
                if (MainMenu.AINeeded)
                {
                    AI AI = new AI();
                    AI.RunAI(GameBoard.boardValues);
                }
            }
        }

        private static void SwapColour()
        {
            if (game.picRed.Visible)
            {
                game.picRed.Hide();
                game.picYellow.Show();
                game.colour = 1;
            }
            else
            {
                game.picRed.Show();
                game.picYellow.Hide();
                game.colour = 2;
            }
        }

        public void GameOver()
        {
            if (!gameDone)
            {
                if (MainMenu.AINeeded)
                {
                    switch (colour)
                    {
                        case 1:
                            if (aiColour == 1)
                            {
                                MessageBox.Show("AI wins!");
                            }
                            else
                            {
                                MessageBox.Show("You win!");
                            }
                            break;
                        case 2:
                            if (aiColour == 2)
                            {
                                MessageBox.Show("AI wins!");
                            }
                            else
                            {
                                MessageBox.Show("You win!");
                            }
                            break;
                    }
                    SqlConnection cnn = new SqlConnection(Login.connectionString);
                    SelectScore(cnn);
                    score += 100 * depth;
                    string sql = "UPDATE [Table] SET [score] = @score WHERE [username] = @username;";
                    SqlCommand command = new SqlCommand(sql, cnn);
                    command.Parameters.Add("username", SqlDbType.NVarChar).Value = Login.username;
                    command.Parameters.Add("score", SqlDbType.Int).Value = score;
                    command.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                }

                else
                {
                    switch (colour)
                    {
                        case 1:
                            MessageBox.Show("Yellow wins!");
                            break;
                        case 2:
                            MessageBox.Show("Red wins!");
                            break;
                    }
                }
            }
            gameDone = true;
            lblPlayerColour.Text = "Winner:";
            btnExit.Show();
        }

        private void SelectScore(SqlConnection cnn)
        {
            cnn.Open();
            string sql = "SELECT [score] FROM [Table] WHERE [username] = @username;";
            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.Add("username", SqlDbType.NVarChar).Value = Login.username;
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                score = dataReader["score"] == DBNull.Value ? 0 : (int)dataReader["score"];
            }
            command.Dispose();
            dataReader.Close();
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            if (MainMenu.AINeeded)
            {
                if (!gameDone)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to restart? You will lose " + (depth * 100) + " points", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SqlConnection cnn = new SqlConnection(Login.connectionString);
                        SelectScore(cnn);
                        for (int i = 0; i < depth; i++)
                        {
                            if (score - 100 < 0)
                            {
                                break;
                            }
                            score -= 100;
                        }
                        string sql = "UPDATE [Table] SET [score] = @score WHERE [username] = @username;";
                        SqlCommand command = new SqlCommand(sql, cnn);
                        command.Parameters.Add("username", SqlDbType.NVarChar).Value = Login.username;
                        command.Parameters.Add("score", SqlDbType.Int).Value = score;
                        command.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        Close();
                        Form colour = new ColourSelect();
                        colour.Show();
                    }
                }
                else
                {
                    Close();
                    Form colour = new ColourSelect();
                    colour.Show();
                }
            }
            else
            {
                Close();
                Form game = new GameScreen();
                game.Show();
            }
        }

        private void PickTeam()
        {
            Random rand = new Random();
            int spinner = rand.Next(1, 3);//randomly chooses 1 or 2 as lower bound is inclusive and upper bound is exclusive
            switch (spinner)
            {
                case 1:
                    picYellow.Show();
                    picRed.Hide();
                    break;
                case 2:
                    picRed.Show();
                    picYellow.Hide();
                    break;
            }
            colour = spinner;

            if (MainMenu.AINeeded)
            {
                aiColour = ColourSelect.playerColour == 1 ? 2 : 1;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Form main = new MainMenu();
            main.Show();
            Hide();
        }

        public static bool CheckTerminalNode(int[,] board, int testColour, bool runningAI)
        {
            //horizontal
            for (int testCol = 0; testCol < GameBoard.width - 3; testCol++)
            {
                for (int testRow = 0; testRow < GameBoard.height; testRow++)
                {
                    if (board[testRow, testCol] == testColour && board[testRow, testCol + 1] == testColour && board[testRow, testCol + 2] == testColour && board[testRow, testCol + 3] == testColour)
                    {
                        if (!runningAI)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                game.Controls.Find("img" + testRow + (testCol + k), true)[0].BackColor = Color.LimeGreen;
                            }
                            game.GameOver();
                        }
                        else
                            return true;
                    }
                }
            }
            //vertical
            for (int testCol = 0; testCol < GameBoard.width; testCol++)
            {
                for (int testRow = 0; testRow < GameBoard.height - 3; testRow++)
                {
                    if (board[testRow, testCol] == testColour && board[testRow + 1, testCol] == testColour && board[testRow + 2, testCol] == testColour && board[testRow + 3, testCol] == testColour)
                    {
                        if (!runningAI)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                game.Controls.Find("img" + (testRow + k) + testCol, true)[0].BackColor = Color.LimeGreen;
                            }
                            game.GameOver();
                        }
                        else
                            return true;
                    }
                }
            }

            //diagonal upward sloping
            for (int testCol = 0; testCol < GameBoard.width - 3; testCol++)
            {
                for (int testRow = 0; testRow < GameBoard.height - 3; testRow++)
                {
                    if (board[testRow, testCol] == testColour && board[testRow + 1, testCol + 1] == testColour && board[testRow + 2, testCol + 2] == testColour && board[testRow + 3, testCol + 3] == testColour)
                    {
                        if (!runningAI)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                game.Controls.Find("img" + (testRow + k) + (testCol + k), true)[0].BackColor = Color.LimeGreen;
                            }
                            game.GameOver();
                        }
                        else
                            return true;
                    }
                }
            }
            //diagonal downward sloping
            for (int testCol = 0; testCol < GameBoard.width - 3; testCol++)
            {
                for (int testRow = 3; testRow < GameBoard.height; testRow++)
                {
                    if (board[testRow, testCol] == testColour && board[testRow - 1, testCol + 1] == testColour && board[testRow - 2, testCol + 2] == testColour && board[testRow - 3, testCol + 3] == testColour)
                    {
                        if (!runningAI)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                game.Controls.Find("img" + (testRow - k) + (testCol + k), true)[0].BackColor = Color.LimeGreen;
                            }
                            game.GameOver();
                        }
                        else
                            return true;
                    }
                }
            }

            if (!runningAI)
            {
                for (int col = 0; col < GameBoard.width; col++)
                {
                    if (board[0, col] == 0)//if the top square of the column is empty
                    {
                        break;//break out of the loop
                    }
                    if (col == GameBoard.width - 1)//if it hasn't broken out of the loop yet, the whole top row is full so the whole board is full
                    {
                        game.gameDone = true;
                        game.lblPlayerColour.Text = "Draw!";
                        game.picYellow.Hide();
                        game.picRed.Hide();
                        game.btnExit.Show();
                    }
                }
            }

            return false;
        }

        public static void CheckAI()
        {
            for (int col = 0; col < GameBoard.width; col++)
            {
                for (int row = 0; row < GameBoard.height; row++)
                {
                    game.Controls.Find("img" + row + col, true)[0].BackColor = GameBoard.GetColour(row, col);
                }
            }
            CheckTerminalNode(GameBoard.boardValues, game.colour, false);
            if (!game.gameDone)
            {
                SwapColour();
            }
        }
    }
}