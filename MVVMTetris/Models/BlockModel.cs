using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MVVMTetris
{   
    /// <summary>
    /// Class <c>BlockModel</c>
    /// Represents a single block or square shape.
    /// It contains all logic that would define a square, as well
    /// as a reference to (x,y) coordinates.
    /// </summary>
    class BlockModel : INotifyPropertyChanged
    {

        private double _left;
        private double _top;
        private int[,] _grid;
        private int _gridX;
        private int _gridY;
        private Brush _blockColor;
        

        public int[,] Grid { get { return _grid; } set { _grid = value; } } 
        public int GridX { get { return _gridX; } set { _gridX = value; } }
        public int GridY { get { return _gridY; } set { _gridY = value; } }
        public double BlockSize { get { return 34; } }
        public double Top { get { return _top; } set { _top = value; NotifyPropertyChanged(); } }
        public double Left { get { return _left; } set { _left = value; NotifyPropertyChanged(); } }
        public double Width { get { return BlockSize; } }
        public double Height { get { return BlockSize; } }
        public Brush BlockColor { get { return _blockColor; } set { _blockColor = value; NotifyPropertyChanged(); } }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Method <c>MoveDown()</c>
        /// the logic to move a block down.
        /// </summary>
        public void MoveDown()
        {
            Top += BlockSize;
            GridY++;               
            
        }

        /// <summary>
        /// Method <c>MoveRight()</c>
        /// Moves the block right 1 unit
        /// </summary>
        public void MoveRight()
        {
            if (!RightSideCollision())
            {
                Left += BlockSize;
                GridX++;
            }
        }

        /// <summary>
        /// Method <c>MoveLeft()</c>
        /// Moves the block left 1 unit
        /// </summary>
        public void MoveLeft()
        {
            if (!LeftSideCollision())
            {
                Left -= BlockSize;
                GridX--;
            }
        }

        /// <summary>
        /// Method <c>BottomCollision()</c>
        /// </summary>
        /// <returns>true if the block will collide with another block one unit below it</returns>
        public bool BottomCollision()
        {
            if (GridY + 1 > Grid.GetLength(0) - 1 
                || Grid[GridY + 1, GridX] == 1)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Method <c>RightCollision()</c>
        /// </summary>
        /// <returns>true if the block will collide with something 1 unit right of it on the grid</returns>
        public bool RightSideCollision()
        {
            if (GridX + 1 >= Grid.GetLength(1))
            {
                return true;
            }

            if (Grid[GridY, GridX + 1] == 1)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Method <c>LeftCollision()</c>
        /// </summary>
        /// <returns>true if the block will collide with something 1 unit left of it on the grid</returns>
        public bool LeftSideCollision()
        {
            if (GridX - 1 < 0)
            {
                return true;
            }

            if (Grid[GridY, GridX - 1] == 1)
            {
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Method <c>MarkPosition()</c>
        /// marks the position of the block on the grid that it is contained upon.
        /// </summary>
        public void MarkPosition()
        {
            Grid[GridY, GridX] = 1;
        }
        
        /// <summary>
        /// Method <c>MoveBlock(int row, int col)</c>
        /// </summary>
        /// Moves the block to the coordinates passed into the function.
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void MoveBlock(int row, int col)
        {
            GridX = col;
            GridY = row;
            Top = row * BlockSize;
            Left = col * BlockSize;
        }
    }
}
