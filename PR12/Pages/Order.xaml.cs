using System.Windows.Controls;

namespace PR12.Pages
{
    public partial class Order : Page
    {
        private MainWindow _mainWindow;

        public Order(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            // Загружаем сохраненные данные
            NameTextBox.Text = _mainWindow.CustomerName;
            PhoneTextBox.Text = _mainWindow.CustomerPhone;
            EmailTextBox.Text = _mainWindow.CustomerEmail;

            // Сохраняем изменения
            NameTextBox.TextChanged += (s, e) => _mainWindow.CustomerName = NameTextBox.Text;
            PhoneTextBox.TextChanged += (s, e) => _mainWindow.CustomerPhone = PhoneTextBox.Text;
            EmailTextBox.TextChanged += (s, e) => _mainWindow.CustomerEmail = EmailTextBox.Text;
        }
    }
}