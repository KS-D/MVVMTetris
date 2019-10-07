using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MVVMTetris.Models.ShapeModel
{
    class OBlockModel : ShapeModel
    {
        private int[,] _noRotation = { {0,0,0,0 },
                                      {0,0,0,0 } };
        protected override int[,] RotationOffset0 => _noRotation;

        protected override int[,] RotationOffset1 => _noRotation;

        public OBlockModel(int[,] grid) : base(grid)
        {
        }



        public override void BuildShape()
        {
                ShapeBlocks[0] = new BlockModel
                {
                    Grid = this._grid,
                    GridX = 0,
                    GridY = 0,
                    BlockColor = Brushes.Gold,
                    Left = 0 * 34,
                    Top = 0 * 34
                };
                ShapeBlocks[1] = new BlockModel
                {
                    Grid = this._grid,
                    GridX = 1,
                    GridY = 0,
                    BlockColor = Brushes.Gold,
                    Left = 1 * 34,
                    Top = 0 * 34
                };
                ShapeBlocks[2] = new BlockModel
                {
                    Grid = this._grid,
                    GridX = 0,
                    GridY = 1,
                    BlockColor = Brushes.Gold,
                    Left = 0 * 34,
                    Top = 1 * 34
                };
                ShapeBlocks[3] = new BlockModel
                {
                    Grid = this._grid,
                    GridX = 1,
                    GridY = 1,
                    BlockColor = Brushes.Gold,
                    Left = 1 * 34,
                    Top = 1 * 34
                };
        }

        public override void Rotate(bool clockWise)
        {
            //Do nothing becuase this block is the same for all rotations.
        }
    }
}
