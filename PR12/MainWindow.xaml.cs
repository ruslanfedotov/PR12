using PR12.Pages;
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

namespace PR12
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigateToModelAndEngine();
        }

        public void NavigateToModelAndEngine()
        {
            MainFrame.Navigate(new ModelAndEngine(this));
        }

        public void NavigateToColorAndOptions()
        {
            MainFrame.Navigate(new ColorAndOptions(this));
        }

        public void NavigateToPrice()
        {
            MainFrame.Navigate(new Price(this));
        }

        public void NavigateToCredit()
        {
            MainFrame.Navigate(new Credit(this));
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
