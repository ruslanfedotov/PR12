using System.Windows;
using System.Windows.Controls;

namespace PR12.Pages
{
    public partial class ColorAndOptions : Page
    {
        private MainWindow _mainWindow;

        public ColorAndOptions(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToModelAndEngine();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToPrice();
        }
    }
}