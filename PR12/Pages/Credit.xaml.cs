using System.Windows;
using System.Windows.Controls;

namespace PR12.Pages
{
    public partial class Credit : Page
    {
        private MainWindow _mainWindow;

        public Credit(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            // Подписываемся на изменения слайдеров
            DownPaymentSlider.ValueChanged += Slider_ValueChanged;
            LoanTermSlider.ValueChanged += Slider_ValueChanged;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Здесь можно добавить пересчет платежей при изменении слайдеров
            // Для простоты оставим статические значения
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToPrice();
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Заявка оформлена успешно!\nС вами свяжется менеджер.",
                          "Заявка отправлена",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);

            // Закрываем приложение
            Application.Current.Shutdown();
        }
    }
}