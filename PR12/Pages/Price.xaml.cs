using System.Windows;
using System.Windows.Controls;

namespace PR12.Pages
{
    public partial class Price : Page
    {
        private MainWindow _mainWindow;

        public Price(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToColorAndOptions();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToCredit();
        }
    }
}