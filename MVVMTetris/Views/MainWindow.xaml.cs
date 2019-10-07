using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVMTetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        private Tetris mainWindowVM = new Tetris();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainWindowVM;
            this.KeyDown += mainWindowVM.KeyDown_Event;
        }

        private void Controls_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Left Arrow Key: move left. {Environment.NewLine}" +
                            $"Right Arrow Key: move right. {Environment.NewLine}" +
                            $"Down Arrow Key: move down. {Environment.NewLine}" +
                            $"Up Arrow Key: rotate clockwise. {Environment.NewLine}" +
                            $"P: Pause or unpause the game.{Environment.NewLine}" +
                            $"R: Restart the game. {Environment.NewLine}");
        }
    }
}
