using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTetris
{
    /// <summary>
    /// Class <c>GameBoardVM</c>
    /// Represents the grid that the tetris game uses to keep track of the position of 
    /// all the blocks.
    /// </summary>
    class GameBoardVM
    {
        private int[,] _grid = { {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 },
                                 {0,0,0,0,0,0,0,0,0,0 }
        };
        
        public int[,] Grid { get { return _grid; } }
        public double Height { get { return 612; } }
        public double Width { get { return 340; } }

        public void ShiftDown(int row)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (Grid[i,j] == 1)
                    {
                        Grid[i, j] = 0;
                        Grid[i + 1, j] = 1;
                    }
                }

            }
        }

        public void ClearRow(int row)
        {
            for (int i = 0; i < 10; i++)
            {
                Grid[row, i] = 0;
            }
        }

        public void ClearGrid()
        {
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                ClearRow(i);
            }
        }

        
    }
}
