using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVVMTetris;

namespace TestTetris
{   
    [TestClass]
    public class TestGameBoard
    {

        GameBoardVM SetUpGameBoard()
        {
            GameBoardVM board = new GameBoardVM();
            return board;
        }

        GameBoardVM FillGameGrid()
        {
            GameBoardVM board = SetUpGameBoard();

            for (int i = 0; i < board.Grid.GetLength(0); i++)
            {

                for (int j = 0; j < board.Grid.GetLength(1); j++)
                {
                    board.Grid[i, j] = 1;
                }
            }
            return board;
        }

        [TestMethod]
        public void Test_ClearGrid()
        {
            GameBoardVM board = FillGameGrid();

            board.ClearGrid();

            for (int i = 0; i < board.Grid.GetLength(0); i++)
            {

                for (int j = 0; j < board.Grid.GetLength(1); j++)
                {
                    Assert.AreEqual(board.Grid[i,j], 0);
                }
            }
        }

        [TestMethod]
        public void Test_ClearRow()
        {
            GameBoardVM board = FillGameGrid();

            board.ClearRow(0);

            for (int i = 0; i < board.Grid.GetLength(1); i++)
            {
                Assert.AreEqual(board.Grid[0,i], 0);
            }
        }

    }
}
