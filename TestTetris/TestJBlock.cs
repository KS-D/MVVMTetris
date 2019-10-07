using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVVMTetris;
using MVVMTetris.Models.ShapeModel;

namespace TestTetris
{   
    [TestClass]
    public class TestJBlock
    {
        static int[,] grid = { {0,0,0,0,0},
                               {0,0,0,0,0},
                               {0,0,0,0,0},
                               {0,0,0,0,0},
                               {0,0,0,0,0} };

        ShapeModel blockShape = new JBlockModel(grid);
        
        private int[] PreviousXpos()
        {
            int[] xPos = new int[4];

            for(int i = 0; i < 4; i++)
            {
                xPos[i] = blockShape.ShapeBlocks[i].GridX;
            }

            return xPos;
        }

        private int[] PreviousYpos()
        {
            int[] yPos = new int[4];

            for (int i = 0; i < 4; i++)
            {
                yPos[i] = blockShape.ShapeBlocks[i].GridY;
            }

            return yPos;
        }


        [TestMethod]
        public void Test_Move_Right()
        {
            int[] prevX = PreviousXpos();
            blockShape.MoveRight();
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(prevX[i] + 1, blockShape.ShapeBlocks[i].GridX);
            }
        }

        [TestMethod]
        public void Test_Move_Left()
        {
            int[] prevX = PreviousXpos();
            blockShape.MoveLeft();
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(prevX[i], blockShape.ShapeBlocks[i].GridX);
            }
        }

        [TestMethod]
        public void Test_Move_Down()
        {
            int[] prevY = PreviousYpos();
            blockShape.MoveDown();
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(prevY[i] + 1, blockShape.ShapeBlocks[i].GridY); 
            }
        }

        [TestMethod]
        public void Test_Drop_Shape()
        {
            blockShape.DropShape();

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(3, blockShape.ShapeBlocks[i].GridY);
            }
            Assert.AreEqual(4, blockShape.ShapeBlocks[3].GridY);
        }

    }
}
