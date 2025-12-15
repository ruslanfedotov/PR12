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
        }

        private void Model_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                string content = radioButton.Content.ToString();
                string model = content.Split('(')[0].Trim();
                _mainWindow.SelectedModel = model;
                _mainWindow.CalculateTotalPrice();
            }
        }

        private void Engine_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                string content = radioButton.Content.ToString();
                string engine = content.Split('(')[0].Trim();
                _mainWindow.SelectedEngine = engine;
                _mainWindow.CalculateTotalPrice();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToColorAndOptions();
        }
    }
}