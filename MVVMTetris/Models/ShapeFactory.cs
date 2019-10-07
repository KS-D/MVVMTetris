using System;
using MVVMTetris.Models.ShapeModel;

namespace MVVMTetris.Models
{
    /// <summary>
    /// Class <c>ShapeFactory</c>
    /// This class is reposible for creating the various types of block shapes used
    /// in the game of tetris.
    /// </summary>
    class ShapeFactory
    {
        private Random _rand = new Random();
        private int[,] _grid;

        public ShapeFactory(int[,] grid)
        {
            _grid = grid;
        }

        public ShapeModel.ShapeModel BuildRandomShape()
        {
            int shapeNum = _rand.Next(7);

            if (shapeNum == 0)
            {
                return new IBlockModel(_grid);
            }
            else if (shapeNum == 1)
            {
                return new JBlockModel(_grid);
            }
            else if (shapeNum == 2)
            {
                return new LBlockModel(_grid);
            }
            else if (shapeNum == 3)
            {
                return new ZBlockModel(_grid);
            }
            else if (shapeNum == 4)
            {
                return new SBlockModel(_grid);
            }
            else if (shapeNum == 5)
            {
                return new TBlockModel(_grid);
            }
            else if (shapeNum == 6)
            {
                return new OBlockModel(_grid);
            }
            else // default condition is IBlock
            {
                return new IBlockModel(_grid);
            }
        } 

        public ShapeModel.ShapeModel BuildShape(ShapeModel.ShapeModel shape)
        {
            int startXpos = _rand.Next(10);

            if (shape is IBlockModel)
            {
                return new IBlockModel(_grid);
            }
            else if (shape is JBlockModel)
            {
                return new JBlockModel(_grid);
            }
            else if (shape is LBlockModel)
            {
                return new LBlockModel(_grid);
            }
            else if (shape is ZBlockModel)
            {
                return new ZBlockModel(_grid);
            }
            else if (shape is SBlockModel)
            {
                return new SBlockModel(_grid);
            }
            else if (shape is TBlockModel)
            {
                return new TBlockModel(_grid);
            }
            else if (shape is OBlockModel)
            {
                return new OBlockModel(_grid);
            }
            else
            {
                return new IBlockModel(_grid);
            }
        }


    }
}
