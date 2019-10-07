using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MVVMTetris.Models.ShapeModel
{
    class LBlockModel : ShapeModel
    {
        private readonly int[,] _rotationOffset0 = { {0, 0, 0,-1 },
                                            {-1, 0, 1, -1 } };
        private readonly int[,] _rotationOffset1 = { {1, 0, -1, 1 },
                                            {0, 0, 0, -1 } };
        protected override int[,] RotationOffset0 => _rotationOffset0;

        protected override int[,] RotationOffset1 => _rotationOffset1; 


        public LBlockModel(int[,] grid) : base(grid)
        {
        }

         public override void BuildShape()
        {


            for (int i = 0; i < 3; i++)
            {
                ShapeBlocks[i] = new BlockModel
                {
                    Grid = this._grid,
                    GridX = i,
                    GridY = 0,
                    BlockColor = Brushes.Orange,
                    Left = i * 34,
                    Top = 0 
                };
            }
                ShapeBlocks[3] = new BlockModel
                {
                    Grid = this._grid,
                    GridX = 0,
                    GridY = 1,
                    BlockColor = Brushes.Orange,
                    Left = 0,
                    Top = 34 
                };

        }
    }
}
