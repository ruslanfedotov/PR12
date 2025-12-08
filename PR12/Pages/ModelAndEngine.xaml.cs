using System.Windows;
using System.Windows.Controls;

namespace PR12.Pages
{
    public partial class ModelAndEngine : Page
    {
        private MainWindow _mainWindow;

        public ModelAndEngine(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            // Выбираем значения по умолчанию
            ModelComboBox.SelectedIndex = 0;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToColorAndOptions();
        }
    }
}