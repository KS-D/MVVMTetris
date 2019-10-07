using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MVVMTetris.Models.ShapeModel
{
    class TBlockModel : ShapeModel
    {
        private readonly int[,] _rotationOffset0 = { {0,0,1,0 }, 
                                                     {-1,0,0,1 } };
        private readonly int[,] _rotationOffset1 = { {1,0,0,-1 },
                                                     {0,0,1,0 } };

        protected override int[,] RotationOffset0 => _rotationOffset0;

        protected override int[,] RotationOffset1 => _rotationOffset1;

        public TBlockModel(int[,] grid) : base(grid)
        {
        }

        public override void BuildShape()
        {
            ShapeBlocks[0] = new BlockModel
            {
                Grid = this._grid,
                GridX = 0,
                GridY = 1,
                BlockColor = Brushes.Purple,
                Left = 0 * 34,
                Top = 1 * 34
            };
            ShapeBlocks[1] = new BlockModel
            {
                Grid = this._grid,
                GridX = 1,
                GridY = 1,
                BlockColor = Brushes.Purple,
                Left = 1 * 34,
                Top = 1 * 34
            };
            ShapeBlocks[2] = new BlockModel
            {
                Grid = this._grid,
                GridX = 1,
                GridY = 0,
                BlockColor = Brushes.Purple,
                Left = 1 * 34,
                Top = 0 * 34
            };
            ShapeBlocks[3] = new BlockModel
            {
                Grid = this._grid,
                GridX = 2,
                GridY = 1,
                BlockColor = Brushes.Purple,
                Left = 2 * 34,
                Top = 1 * 34
            };
        }
    }
}
