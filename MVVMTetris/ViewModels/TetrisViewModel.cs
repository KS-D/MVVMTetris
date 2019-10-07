using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MVVMTetris.Extensions;
using MVVMTetris.Models;
using MVVMTetris.Models.ShapeModel;
using MVVMTetris.Views;

namespace MVVMTetris
{   
    /// <summary>
    /// Class <c> Tetris</c> represents the logic to play the game tetris. 
    /// has a reference to the level, rows cleared, score, timeInterval,
    /// a message bound to when the game is paused and 2 bools that 
    /// represent game over and puased.
    /// It maintains a reference to the current shape, next shape, and held shape.
    /// </summary>
    class Tetris : INotifyPropertyChanged
    {
        private ShapeModel _shape;
        private ShapeModel _nextShape;
        private ShapeModel _heldShape;

        private ObservableCollection<BlockModel> _blocks;

        private int _level;
        private int _rowsCleared;
        private int _totalRowsCleared;
        private int _score;
        private int _timeInterval = 500;
        private string _pauseMessage = "";
        private bool _gameOver = false;
        private bool _paused = false;
        public ObservableCollection<BlockModel> NextBlock { get; set; }

        public ObservableCollection<BlockModel> Blocks
        {
            get{ return _blocks; }
            set{_blocks = value; }
        }


        public string PauseMessage {
            get { return _pauseMessage; }
            private set { _pauseMessage = value; NotifyPropertyChanged(); }
        }

        public ObservableCollection<BlockModel> HeldBlock { get; set; }
        
        public GameBoardVM GameBoard { get; private set; } = new GameBoardVM();

        public int Score { get { return _score; } set { _score = value; NotifyPropertyChanged(); } }
        public int Level { get { return _level; } set { _level = value; NotifyPropertyChanged(); } }
        public int RowsCleared { get {return _totalRowsCleared; } set { _totalRowsCleared = value; NotifyPropertyChanged(); } }        

        private DispatcherTimer _timer;
        private ShapeFactory _shapeFactory;

        /// <summary>
        /// Constructor <c> Tetris</c> instantiates tetris with all the default conditions
        /// </summary>
        public Tetris()
        {
            _shapeFactory = new ShapeFactory(GameBoard.Grid);
            Blocks = new ObservableCollection<BlockModel>();
            NextBlock = new ObservableCollection<BlockModel>();
            HeldBlock = new ObservableCollection<BlockModel>();
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, _timeInterval);
            _timer.Tick += GameTimer_Tick;
            _shape = _shapeFactory.BuildRandomShape();
            _shape.CenterShape();
            _nextShape = _shapeFactory.BuildRandomShape();
            NextBlock.AddRange(_nextShape.ShapeBlocks);
            Blocks.AddRange(_shape.ShapeBlocks);
            Score = 0;
            Level = 1;
            _timer.Start();
        }
        
        /// <summary>
        /// Method <c> RestartGame</c> Restarts the game, setting all state to default,
        /// and clears all items that would be bound to the gui.
        /// </summary>

        public void RestartGame()
        {
            // reset game variables
            _timeInterval = 500;
            _gameOver = false;
            _paused = false;
            PauseMessage = "";
            Score = 0;
            Level = 1;

            // clear collections
            Blocks.Clear();
            NextBlock.Clear();
            HeldBlock.Clear();
            GameBoard.ClearGrid();

            _timer.Interval = new TimeSpan(0, 0, 0, 0, _timeInterval);
            
            // create shapes
            _shape = _shapeFactory.BuildRandomShape();
            _nextShape = _shapeFactory.BuildRandomShape();
            NextBlock.AddRange(_nextShape.ShapeBlocks);
            Blocks.AddRange(_shape.ShapeBlocks);

            _timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Method <c>GameTimer_Tick()</c>
        /// Represents the that should happen every timer tick. 
        /// Ex: the _shape should move down if it can, if not a new shape 
        /// is generated at the top.
        /// Checks for game over condition and calls the method that clears full 
        /// rows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GameTimer_Tick(object sender, EventArgs e)
        {
            _shape.MoveDown();
           
            if (!_shape.ValidMoveDown() && !_gameOver)
            {
                _shape.MarkPosition();
                _shape = _nextShape;
                _shape.CenterShape();
                NextBlock.Clear();
                _nextShape = _shapeFactory.BuildRandomShape();

                NextBlock.AddRange(_nextShape.ShapeBlocks);
                
                Blocks.AddRange(_shape.ShapeBlocks);
            if (GameOverCondition())
            {
                _gameOver = true;
                _timer.Stop();
                GameOver gameOverWindow = new GameOver();
                gameOverWindow.ShowDialog();

            }
                CheckForPoints();

            }
                                   
        }
        /// <summary>
        /// Method <c>GameOverContion()</c>
        /// returns a bool representing whether a block can be placed
        /// at the top.
        /// </summary>
        /// <returns>bool</returns>

        private bool GameOverCondition()
        {
            foreach (BlockModel b in _shape.ShapeBlocks)
            {
                if (GameBoard.Grid[b.GridY, b.GridX] == 1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method <c>CheckForPoints()</c>
        /// clears full rows, and updates the points if a row was cleared.
        /// </summary>
        private void CheckForPoints()
        {
            int rowClearCount = 0;
            for (int i = 17; i >= 0; i--) // starts at the bottom of the grid
            {
                int countOfBlock = 0;
                for (int j = 0; j < 10; j++)
                {
                    if (GameBoard.Grid[i,j] > 0)
                    {
                        countOfBlock++;
                    }
                }
    
                if (countOfBlock == 10)
                {
                    rowClearCount++;

                    Blocks.Remove(item => (item.GridY == i));

                    GameBoard.ClearRow(i);
                    RowDeleteUpdate(i);
                    MarkOnGrid();
                    i++;
                }
            }

            RowsCleared += rowClearCount;
            _rowsCleared += rowClearCount;

            IncreaseScore(rowClearCount);

            if (rowClearCount > 0 && _rowsCleared >10)
            {
                IncreaseLevel();
                _rowsCleared = 0;
            }

        }

        /// <summary>
        /// Method <c>HoldBlock()</c>
        /// sets the heldBlock to the current block, and then sets the current block to 
        /// the next block.
        /// </summary>
        public void HoldBlock()
        {
            if (HeldBlock.Count == 0)
            {

                _heldShape = _shapeFactory.BuildShape(_shape);
    
                for (int i = 0; i < 4; i++)
                {

                Blocks.Remove(x => x.GridX == _shape.ShapeBlocks[i].GridX 
                &&  x.GridY == _shape.ShapeBlocks[i].GridY );
                }

                _shape = _nextShape;
                _shape.CenterShape();
                
                Blocks.AddRange(_shape.ShapeBlocks);
                _nextShape = _shapeFactory.BuildRandomShape();
                NextBlock.Clear();
                NextBlock.AddRange(_nextShape.ShapeBlocks);
                HeldBlock.AddRange(_heldShape.ShapeBlocks);
            }
            else
            {
                HeldBlock.Clear();
                var tempShape = _heldShape;
                _heldShape = _shapeFactory.BuildShape(_shape);

                for (int i = 0; i < 4; i++)
                {

                    Blocks.Remove(x => x.GridX == _shape.ShapeBlocks[i].GridX 
                    &&  x.GridY == _shape.ShapeBlocks[i].GridY );
                }

                _shape = tempShape;
                _shape.CenterShape();
                Blocks.AddRange(_shape.ShapeBlocks);
                HeldBlock.AddRange(_heldShape.ShapeBlocks);
                

            }
        }

        /// <summary>
        /// Method <c>IncreaseLevel()</c>
        /// Increments the level and speeds up the interval of the timer.
        /// </summary>
        
        private void IncreaseLevel()
        {
            Level++;
            if (_timeInterval > 0)
            {
                _timeInterval = (int)(_timeInterval - (.25 * _timeInterval));
                _timer.Interval = new TimeSpan(0, 0, 0, 0, _timeInterval);
            }
        }
        
        /// <summary>
        /// Increases the score by the number or rows multiplied by the level multiplied 
        /// by 100 points and any row > 1 recieves 50 bonus points multiplied by level and row number.
        /// </summary>
        /// <param name="rows"> the number of rows cleared</param>
        private void IncreaseScore(int rows)
        {
            if (rows > 0)
            {
                Score += (100 * Level * rows) + (50 * Level * (rows - 1));
            }
        }

        /// <summary>
        /// Method <c>Pause()</c>
        /// Pauses the timer and stops all game movement
        /// </summary>
        private void Pause()
        {
            _paused = !_paused;

            if (_timer.IsEnabled)
            {
                _timer.Stop();
                PauseMessage = "Paused!!!";
            }  
            else
            {
                _timer.Start();
                PauseMessage = "";
            }
        }

        /// <summary>
        /// Method <c>KeyDown_Event(object sender, KeyEventArgs e)</c>
        /// A method that represents the controls for tetris.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KeyDown_Event(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                _shape.MoveDown();
            }
            if (e.Key == Key.Left)
            {
                _shape.MoveLeft();
            }
            if (e.Key == Key.Right)
            {
                _shape.MoveRight();
            }
            if (e.Key == Key.Up)
            {
                _shape.Rotate(true);
            }
            if (e.Key == Key.Space)
            {
                _shape.DropShape(); 
            }
             if (e.Key == Key.I)
                {
                    string gridString = "";
                    for (int i = 0; i < GameBoard.Grid.GetLength(0); i++)
                    {
                        for (int j = 0; j < GameBoard.Grid.GetLength(1); j++)
                        {
                            gridString += GameBoard.Grid[i, j] + " "; 
                        }
                        gridString += Environment.NewLine;
                    }

                    MessageBox.Show(gridString.ToString());
                }
            if (e.Key == Key.R)
            {
                RestartGame();
            }
            if (e.Key == Key.P)
            {
                Pause();
            }
            if (e.Key == Key.H)
            {
                HoldBlock();
            }

        }

        /// <summary>
        /// Method <c>RowDeleteUpdate(int row)</c>
        /// Moves all the blocks above the rows that are cleared down. 
        /// </summary>
        /// <param name="row"></param>

        private void RowDeleteUpdate(int row)
        {
            foreach (BlockModel b in Blocks)
            {
                if (b.GridY <= row && !CheckInShape(row))
                {
                    GameBoard.Grid[b.GridY, b.GridX] = 0;
                    b.MoveDown();
                }
            }
        }

        /// <summary>
        /// Method <c>MarkOnGrid()</c>
        /// Marks the position of all blocks not in the current shape onto the 
        /// game grid.
        /// </summary>

        private void MarkOnGrid()
        {
            foreach (BlockModel b in Blocks)
            {
                if (!CheckInShape(b))
                {
                    b.MarkPosition();
                }
            }
        }

        /// <summary>
        /// Method <c>CheckInShape(int row)</c>
        /// returns a bool representing if a block is present within a shape.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>

        private bool CheckInShape(int row)
        {
            foreach (BlockModel b in _shape.ShapeBlocks)
            {
                if (b.GridY == row)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// returns a bool representing if a block is present within a shape.
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        private bool CheckInShape(BlockModel block)
        {
            foreach (BlockModel b in _shape.ShapeBlocks)
            {
                if (block.GridX == b.GridX && block.GridY == b.GridY)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
