using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MVVMTetris.Models.ShapeModel
{
    class IBlockModel : ShapeModel
    {
        private readonly int[,] _rotationOffset0 = { { 0, 0, 0, 0 },
                                            {-1, 0, 1, 2 }};
        private readonly int[,] _rotationOffset1 = { { 1, 0, -1, -2},
                                            { 0, 0, 0, 0 } };        
        protected override int[,] RotationOffset0 => _rotationOffset0; 

        protected override int[,] RotationOffset1 => _rotationOffset1; 
        
        public IBlockModel(int[,] grid) : base(grid)
        {
        }

        public override void BuildShape()
        {
            for (int i = 0; i < 4; i++)
            {
                ShapeBlocks[i] = new BlockModel
                {
                    Grid = this._grid,
                    GridX = i,
                    GridY = 0,
                    BlockColor = Brushes.Aqua,
                    Left = i * 34,
                    Top = 0
                };
            }
        }

    }
}
