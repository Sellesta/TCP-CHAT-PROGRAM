using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Windows_Forms_Chat
{
    public enum TileType
    {
        blank, cross, naught
    }
    public enum GameState
    {
        playing, draw, crossWins, naughtWins
    }

    public class TicTacToe
    {
        public bool myTurn = true;
        public TileType playerTileType = TileType.blank;
        public List<Button> buttons = new List<Button>();
        public TileType[] grid = new TileType[9];

        public string GridToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TileType tile in grid)
            {
                sb.Append(TileTypeToString(tile));
            }
            return sb.ToString();
        }

        public void StringToGrid(string s)
        {
            if (s.Length != 9)
                throw new ArgumentException("Invalid string length");
            
            for (int i = 0; i < 9; i++)
            {
                grid[i] = StringToTileType(s[i]);
                if (buttons.Count >= 9)
                    buttons[i].Text = TileTypeToString(grid[i]);
            }
        }

        public bool SetTile(int index, TileType tileType)
        {
            if (grid[index] == TileType.blank)
            {
                grid[index] = tileType;
                if (buttons.Count >= 9)
                    buttons[index].Text = TileTypeToString(tileType);
                return true;
            }
            return false;
        }

        public GameState GetGameState()
        {
            GameState state = GameState.playing;
            if (CheckForWin(TileType.cross))
                state = GameState.crossWins;
            else if (CheckForWin(TileType.naught))
                state = GameState.naughtWins;
            else if (CheckForDraw())
                state = GameState.draw;
            return state;
        }

        public bool CheckForWin(TileType t)
        {
            // Rows
            if ((grid[0] == t && grid[1] == t && grid[2] == t) ||
                (grid[3] == t && grid[4] == t && grid[5] == t) ||
                (grid[6] == t && grid[7] == t && grid[8] == t))
                return true;
            // Columns
            if ((grid[0] == t && grid[3] == t && grid[6] == t) ||
                (grid[1] == t && grid[4] == t && grid[7] == t) ||
                (grid[2] == t && grid[5] == t && grid[8] == t))
                return true;
            // Diagonals
            if ((grid[0] == t && grid[4] == t && grid[8] == t) ||
                (grid[2] == t && grid[4] == t && grid[6] == t))
                return true;
            return false;
        }

        public bool CheckForDraw()
        {
            foreach (TileType tile in grid)
            {
                if (tile == TileType.blank)
                    return false;
            }
            return true;
        }

        public void ResetBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                grid[i] = TileType.blank;
                if (buttons.Count >= 9)
                    buttons[i].Text = TileTypeToString(TileType.blank);
            }
        }

        public static string TileTypeToString(TileType t)
        {
            if (t == TileType.cross)
                return "X";
            else if (t == TileType.naught)
                return "O";
            else
                return "";
        }

        public static TileType StringToTileType(char c)
        {
            if (c == 'X')
                return TileType.cross;
            else if (c == 'O')
                return TileType.naught;
            else
                return TileType.blank;
        }
    }
}
