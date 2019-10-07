using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVVMTetris;

namespace TestTetris
{
    [TestClass]
    public class UnitTestBlockModel
    {
        public int[,] grid = { {0,0,0,0,0 },
                               {0,0,0,0,0 },
                               {0,0,0,0,0 },
                               {0,0,0,0,0 }, 
                               {0,0,0,0,0 }}; 

        // build a simple block that starts at index [0,0]
        BlockModel SetUpBlockLeftBound()
        {
            BlockModel block = new BlockModel {
                Grid = grid,
                GridX = 0,
                GridY = 0,
                Left = 0,
                Top = 0
            };
            return block;
        }

        BlockModel SetUpBlockRightBound()
        {
            BlockModel b = new BlockModel
            {
                Grid = grid,
                GridX = 4,
                GridY = 0,
                Left = 34 * 4,
                Top = 0
            };

            return b;
        }

        [TestMethod]
        public void Test_BlockModel_Down_Once()
        {
            BlockModel b = SetUpBlockLeftBound();
            b.MoveDown();
            Assert.AreEqual(b.GridY, 1);
            Assert.AreEqual(b.GridX, 0);
            Assert.AreEqual(b.Top, 34);
        }

        [TestMethod]
        public void Test_BlockModel_Down_3Times()
        {
            BlockModel b = SetUpBlockLeftBound();

            b.MoveDown();
            b.MoveDown();
            b.MoveDown();

            Assert.AreEqual(b.GridY, 3);
            Assert.AreEqual(b.GridX, 0);
            Assert.AreEqual(b.Top, 34 * 3);
        }
        [TestMethod]
        public void Test_BlockModel_Right_1Time()
        {
            BlockModel b = SetUpBlockLeftBound();

            b.MoveRight();

            Assert.AreEqual(b.GridX, 1);
            Assert.AreEqual(b.Left, 34);
        }

        [TestMethod]
        public void Test_Left_Bound_Movement()
        {
            BlockModel b = SetUpBlockLeftBound();

            b.MoveLeft();

            Assert.AreEqual(b.GridX, 0);
        }

        [TestMethod]
        public void Test_Right_Bound_Movement()
        {
            BlockModel b = SetUpBlockRightBound();

            b.MoveRight();

            Assert.AreEqual(b.GridX, 4);
        }

        [TestMethod]
        public void Test_One_Left_Movement()
        {
            BlockModel b = SetUpBlockRightBound();

            b.MoveLeft();

            Assert.AreEqual(b.GridX, 3);
            Assert.AreEqual(b.Left, 3 * 34);
        }
        
        [TestMethod]
        public void Test_MarkPosition_Left_Bound()
        {
            BlockModel b = SetUpBlockLeftBound();

            b.MarkPosition();

            Assert.AreEqual(grid[0, 0], 1);
        }
        
        [TestMethod]
        public void Test_MarkPosition_Right_Bound()
        {
            BlockModel b = SetUpBlockRightBound();

            b.MarkPosition();

            Assert.AreEqual(grid[0, 4], 1);
        }

        [TestMethod]
        public void Test_MarkPosition_Moved()
        {
            BlockModel b = SetUpBlockLeftBound();

            b.MoveDown();
            b.MoveDown();

            b.MoveRight();
            b.MoveRight();
            b.MoveRight();

            b.MarkPosition();

            Assert.AreEqual(grid[2,3], 1);
        }

        [TestMethod]
        public void Test_MoveBlock()
        {
            BlockModel b = SetUpBlockLeftBound();

            b.MoveBlock(2, 4);

            Assert.AreEqual(b.GridX, 4);
            Assert.AreEqual(b.GridY, 2);
            Assert.AreEqual(b.Top, 2 * 34);
            Assert.AreEqual(b.Left, 4 * 34);

        }


    }
}
