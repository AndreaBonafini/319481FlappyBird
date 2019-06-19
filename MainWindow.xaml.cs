/*Andrea Bonafini
June 16,2019
Flappy Bird
*/
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
using System.Windows.Threading;

namespace FlappyBird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Component cp;
        Window window;
        public MainWindow()
        {
            InitializeComponent();
            cp = new Component(this,canvas);
        }
        private void BtnResume_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRestart_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("");
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
