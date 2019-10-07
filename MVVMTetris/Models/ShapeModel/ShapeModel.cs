using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTetris.Models.ShapeModel
{
    /// <summary>
    /// Class <c>ShapeModel</c>
    /// Abstract class that represents a shape consisting of 4 blocks.
    /// </summary>
    abstract class ShapeModel
    {
        private BlockModel[] _shapeBlocks;
        protected int[,] _grid;
        protected int _rotationState = 0;
        protected int startXpos;
        abstract protected int[,] RotationOffset0 { get; }
        abstract protected int[,] RotationOffset1 { get; }


        public ShapeModel(int[,] grid)
        {
            _grid = grid;
            _shapeBlocks = new BlockModel[4];
            BuildShape();

        }
        
        /// <summary>
        /// Method <c>BuildShape</c>
        /// Abstract method that builds the instance of an actual shape.
        /// </summary>
        public abstract void BuildShape();

        public void CenterShape()
        {
            int count = 0;
            while (count != 3)
            {
                if (!this.ShapeCollideWallR())
                {
                    this.MoveRight();
                }
                count++;
            }
        }

        public BlockModel[] ShapeBlocks
        {
            get { return _shapeBlocks; }
            set
            {
                if (value.Length == 4)
                    _shapeBlocks = value;
            } 
        }

        public void MoveDown()
        {
            if (ValidMoveDown())
            {
                foreach (BlockModel b in ShapeBlocks)
                {
                    b.MoveDown();
                }
            }
        }

        public void MarkPosition()
        {
            foreach (BlockModel b in ShapeBlocks)
            {
                b.MarkPosition();
            }
        }

        public void DropShape()
        {
            while (ValidMoveDown())
            {
                this.MoveDown();
            }
        }

        public bool ValidMoveDown()
        {
            int count = 0;

            foreach (BlockModel b in ShapeBlocks)
            {
                if (!b.BottomCollision())
                {
                    count++;
                }
            }

            return count == 4;
        }

        private bool ShapeCollideWallL()
        {
            foreach (BlockModel b in ShapeBlocks)
            {
                if (b.GridX <= 0 
                    || b.Grid[b.GridY, b.GridX - 1] == 1)
                {
                    return true;
                }
            }
            return false;
        }

        private bool ShapeCollideWallR()
        {
            foreach (BlockModel b in ShapeBlocks)
            {
                if (b.GridX >= _grid.GetLength(1) - 1 
                    || b.Grid[b.GridY, b.GridX + 1] == 1)
                {
                    return true;
                }
            }
            return false;
        }

        public void MoveLeft()
        {
            if (!ShapeCollideWallL())
            {
                foreach (BlockModel b in ShapeBlocks)
                {
                    b.MoveLeft();
                }                   
            }
        }

        public void MoveRight()
        {
            if (!ShapeCollideWallR())
            {
                foreach (BlockModel b in ShapeBlocks)
                {
                    b.MoveRight();
                }
            }
        }

        public void RowDeleteUpdate(int row)
        {
            foreach (BlockModel b in ShapeBlocks)
            {
                if (b.GridY < row)
                {
                    _grid[b.GridY, b.GridX] = 0;
                    b.MoveDown();
                }
            }
        }

        public virtual void Rotate(bool clockWise)
        {
            int pivotX = _shapeBlocks[1].GridX;
            int pivotY = _shapeBlocks[1].GridY;

            if (ValidRotation(clockWise))
            {


                if ((_rotationState == 0 && clockWise) || (_rotationState == 2 && !clockWise))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        ShapeBlocks[i].MoveBlock(pivotY + RotationOffset0[1, i], pivotX + RotationOffset0[0, i]);
                    }

                    _rotationState = 1;
                }
                else if ((_rotationState == 1 && clockWise) || (_rotationState == 3 && !clockWise))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        ShapeBlocks[i].MoveBlock(pivotY + RotationOffset1[1, i], pivotX + RotationOffset1[0, i]);
                    }

                    _rotationState = 2;
                }
                else if ((_rotationState == 2 && clockWise) || (_rotationState == 0 && !clockWise))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        ShapeBlocks[i].MoveBlock(pivotY - RotationOffset0[1, i], pivotX - RotationOffset0[0, i]);
                    }

                    _rotationState = 3;
                }
                else if ((_rotationState == 3 && clockWise) || (_rotationState == 1 && !clockWise))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        ShapeBlocks[i].MoveBlock(pivotY - RotationOffset1[1, i], pivotX - RotationOffset1[0, i]);
                    }

                    _rotationState = 0;
                }
            }
        }

        private bool ValidRotation(bool clockWise)
        {

            int pivotX = _shapeBlocks[1].GridX;
            int pivotY = _shapeBlocks[1].GridY;

            if ((_rotationState == 0 && clockWise) || (_rotationState == 2 && !clockWise))
            {
                for (int i = 0; i < 4; i++)
                {
                    if (pivotX + RotationOffset0[0, i] >= _grid.GetLength(1) ||
                        pivotY + RotationOffset0[1, i] >= _grid.GetLength(0) ||
                        pivotX + RotationOffset0[0, i] < 0 ||
                        pivotY + RotationOffset0[1, i] < 0 ||
                        _grid[pivotY + RotationOffset0[1, i], pivotX + RotationOffset0[0, i]] == 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            else if ((_rotationState == 1 && clockWise) || (_rotationState == 3 && !clockWise))
            {
                for (int i = 0; i < 4; i++)
                {
                    if (pivotX + RotationOffset1[0, i] < 0 ||
                        pivotX + RotationOffset1[0, i] >= _grid.GetLength(1) - 1||
                        pivotY + RotationOffset1[1, i] >= _grid.GetLength(0)  ||
                        pivotY + RotationOffset1[1, i] < 0 ||
                        _grid[pivotY + RotationOffset1[1, i], pivotX + RotationOffset1[0, i]] == 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            else if ((_rotationState == 2 && clockWise) || (_rotationState == 0 && !clockWise))
            {
                for (int i = 0; i < 4; i++)
                {
                    if (pivotX - RotationOffset0[0, i] < 0 ||
                        pivotY - RotationOffset0[1, i] < 0 ||
                        pivotX - RotationOffset0[0, i] >= _grid.GetLength(1) ||
                        pivotY - RotationOffset0[1, i] >= _grid.GetLength(0) ||
                        _grid[pivotY - RotationOffset0[1, i], pivotX - RotationOffset0[0, i]] == 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            else if ((_rotationState == 3 && clockWise) || (_rotationState == 1 && !clockWise))
            {
                for (int i = 0; i < 4; i++)
                {
                    if (pivotX - RotationOffset0[1, i] >= _grid.GetLength(1) - 1 ||
                        pivotY - RotationOffset0[0, i] >= _grid.GetLength(0) ||
                        pivotX - RotationOffset0[1, i] < 0 ||
                        pivotY - RotationOffset0[0, i] < 0 ||
                        _grid[pivotY - RotationOffset0[1, i], pivotX - RotationOffset0[0, i]] == 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }


    }
}
