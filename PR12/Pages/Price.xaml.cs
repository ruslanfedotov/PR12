using System.Windows.Controls;

namespace PR12.Pages
{
    public partial class Price : Page
    {
        public Price(MainWindow mainWindow)
        {
            InitializeComponent();

            ModelText.Text = $"Модель: {mainWindow.SelectedModel}";
            EngineText.Text = $"Двигатель: {mainWindow.SelectedEngine}";
            ColorText.Text = $"Цвет: {mainWindow.SelectedColor}";

            if (mainWindow.SelectedOptions.Count > 0)
                OptionsText.Text = $"Опции: {string.Join(", ", mainWindow.SelectedOptions)}";
            else
                OptionsText.Text = "Опции: нет";

            TotalPriceText.Text = $"{mainWindow.GetTotalPrice():N0} ₽";
        }
    }
}