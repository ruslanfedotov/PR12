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
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            ModelText.Text = "Модель: " + _mainWindow.SelectedModel;
            EngineText.Text = "Двигатель: " + _mainWindow.SelectedEngine;
            ColorText.Text = "Цвет: " + _mainWindow.SelectedColor;

            if (_mainWindow.SelectedOptions.Count > 0)
            {
                OptionsText.Text = "Опции:\n" + string.Join("\n", _mainWindow.SelectedOptions);
            }
            else
            {
                OptionsText.Text = "Опции: нет";
            }

            BasePriceText.Text = $"{_mainWindow.GetModelPrice():N0} ₽";
            EnginePriceText.Text = $"+ {_mainWindow.GetEnginePrice():N0} ₽";
            ColorPriceText.Text = $"+ {_mainWindow.GetColorPrice():N0} ₽";
            OptionsPriceText.Text = $"+ {_mainWindow.GetOptionsPrice():N0} ₽";
            TotalPriceText.Text = _mainWindow.GetFormattedTotalPrice();
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