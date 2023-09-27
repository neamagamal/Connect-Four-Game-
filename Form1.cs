using System.ComponentModel;
using System.Drawing.Text;
using System.Net.PeerToPeer.Collaboration;

namespace connectFour
{
    public partial class Form1 : Form
    {
        //decalring the rectangle
        private readonly Rectangle[] boardColumns;
        // board
        private int[,] board;
        //variable for turn role
        private int turn;
        public Form1()
        {
            InitializeComponent();
           this.boardColumns = new Rectangle[7];
            this.board = new int[6, 7];
            this.turn = 2;
            label1.Text = "blue turn";
            
            
        }

        //painting the board game on screen 
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            
            e.Graphics.FillRectangle(Brushes.Purple, 36, 36, 430, 360);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i == 0)
                    {
                        this.boardColumns[j] = new Rectangle(48 + 60 * j, 36, 44, 300);
                    }
                    e.Graphics.FillEllipse(Brushes.White, 48 + 60 * j, 48 + 60 * i, 44, 44);
                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int columnIndex = this.CoulmnNumber(e.Location);
            //filling the empty
            //check if the colum is empty
            if (columnIndex != -1)
            {
                int rowIndex = this.EmptyRow(columnIndex);
                //row of this colum is empty
                if (rowIndex != -1)
                {
                    //first player playing
                    this.board[rowIndex, columnIndex] = this.turn;
                    if (this.turn==1)
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillEllipse(Brushes.Red,48 + 60 * columnIndex,48+60*rowIndex,44,44);
                    }
                    //second player playing
                    else if(this.turn==2)
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillEllipse(Brushes.Blue, 48 + 60 * columnIndex, 48 + 60 * rowIndex, 44, 44);

                    }
                    //winner 
                    int winner = this.winnerPlayer(this.turn);
                    if (winner != -1)
                    {
                        string player = (winner == 1) ? "red" : "green";
                        MessageBox.Show($"congratulation {player} wins");
                        Application.Restart();
                    }
                   
                    // draw method calling 
                  var checkDraw =  Draw();
                    if (checkDraw!=0)
                    {
                        MessageBox.Show("its draw");
                    }
                    
                    
                    //change the turn and label name and color to identify the player easily
                    if (this.turn==1)
                    {
                        this.turn = 2;
                        label1.Text = "Blues turn";
                        label1.ForeColor= Color.Blue;
                    }
                    else
                    {
                        this.turn = 1;
                        label1.Text = "Reds turn";
                        label1.ForeColor = Color.Red;

                    }
                    //end of change turn
                }
            }
            
        }
        //checking the winner all states
        private int winnerPlayer(int playerToCheck)
        {
            //vertical check win
            for (int row = 0; row < this.board.GetLength(0)-3 ; row++)
            {
                for (int col = 0; col < this.board.GetLength(1); col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row+1,col], this.board[row + 2, col], this.board[row + 3, col]))
                    {
                        return playerToCheck;
                    }
                }
            }
            //horizintal check win
            for (int row = 0; row < this.board.GetLength(0); row++)
            {
                for (int col = 0; col < this.board.GetLength(1) -3; col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row, col + 1], this.board[row, col + 2], this.board[row, col + 3]))
                    { 
                        return playerToCheck;
                    }
                }
            }
            //top left digonal win check
            for (int row = 0; row < this.board.GetLength(0)-3; row++)
            {
                for (int col = 0; col < this.board.GetLength(1)-3; col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row +1, col + 1], this.board[row +2, col + 2], this.board[row + 3, col + 3]))
                    {
                        return playerToCheck;
                    }
                }
            }
            //right digonal win check
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 3; col < this.board.GetLength(1); col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row + 1, col - 1], this.board[row + 2, col - 2], this.board[row + 3, col - 3]))
                    {
                        return playerToCheck;
                    }
                }
            }
            
            return -1;
        }
        //checking four equals numbers
        private bool AllNumbersEqual (int tocheck , params int[] numbers)
        {
            foreach (var item in numbers)
            {
                if (item != tocheck)
                {
                    return false;
                }
            }
            return true;

        }
        //empty coulmn or not
        private int CoulmnNumber (Point mouse)
        {
            for (int i = 0; i < boardColumns.Length; i++)
            {
                if ((mouse.X >= this.boardColumns[i].X)&&(mouse.Y >= this.boardColumns[i].Y))
                {
                    if ((mouse.X <= this.boardColumns[i].X + this.boardColumns[i].Width)&& (mouse.Y <= this.boardColumns[i].Y + this.boardColumns[i].Height))
                    {
                        return i;
                    }
                } 
            }
            return -1;
        }
        //empty row
        private int EmptyRow(int col)
        {
            for (int i = 5; i >= 0; i--)
            {
                if (this.board[i,col]==0)
                {
                    return i;
                }
            }
            return -1;
        }
        //the draw loop method
        private int Draw() {
            ;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (board[i,j]==0)
                    {
                        Console.WriteLine(i);
                        Console.WriteLine(j);
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            return -1;
                            

                }
                    
        

       
    }
}