using System.Linq;
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

        private void Color_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                string content = radioButton.Content.ToString();
                string color = content.Split('(')[0].Trim();
                _mainWindow.SelectedColor = color;
                _mainWindow.CalculateTotalPrice();
            }
        }

        private void Option_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                string content = checkBox.Content.ToString();
                string option = content.Split('(')[0].Trim();

                if (!_mainWindow.SelectedOptions.Contains(option))
                {
                    _mainWindow.SelectedOptions.Add(option);
                    _mainWindow.CalculateTotalPrice();
                }
            }
        }

        private void Option_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                string content = checkBox.Content.ToString();
                string option = content.Split('(')[0].Trim();

                if (_mainWindow.SelectedOptions.Contains(option))
                {
                    _mainWindow.SelectedOptions.Remove(option);
                    _mainWindow.CalculateTotalPrice();
                }
            }
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