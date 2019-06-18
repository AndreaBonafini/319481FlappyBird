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
using System.Windows.Shapes;

namespace FlappyBird
{
    /// <summary>
    /// Interaction logic for GamePause.xaml
    /// </summary>
    public partial class GamePause : Window
    {
        Canvas canvas;
                    Component cp;
        public GamePause()
        {
            InitializeComponent();
        }

        private void BtnResume_Click(object sender, RoutedEventArgs e)
        {
            cp.timer.Start();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void BtnRestart_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }
    }
}
