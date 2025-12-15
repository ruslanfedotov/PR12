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

            try
            {
                UpdateCalculation();
            }
            catch
            {
                // Просто игнорируем ошибки при инициализации
            }
        }

        private void DownPaymentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInputs();
        }

        private void LoanTermTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInputs();
        }

        private void CheckInputs()
        {
            bool isDownPaymentValid = true;
            bool isLoanTermValid = true;

            // Проверяем первый ввод (первоначальный взнос)
            string dpText = DownPaymentTextBox.Text;
            if (string.IsNullOrWhiteSpace(dpText))
            {
                DownPaymentError.Text = "Введите число";
                isDownPaymentValid = false;
            }
            else if (!decimal.TryParse(dpText, out decimal dpValue))
            {
                DownPaymentError.Text = "Только числа";
                isDownPaymentValid = false;
            }
            else if (dpValue < 10 || dpValue > 90)
            {
                DownPaymentError.Text = "От 10 до 90";
                isDownPaymentValid = false;
            }
            else
            {
                DownPaymentError.Text = "";
                _mainWindow.SetDownPaymentPercent(dpText);
            }

            // Проверяем второй ввод (срок кредита)
            string ltText = LoanTermTextBox.Text;
            if (string.IsNullOrWhiteSpace(ltText))
            {
                LoanTermError.Text = "Введите число";
                isLoanTermValid = false;
            }
            else if (!int.TryParse(ltText, out int ltValue))
            {
                LoanTermError.Text = "Только целые числа";
                isLoanTermValid = false;
            }
            else if (ltValue < 12 || ltValue > 96)
            {
                LoanTermError.Text = "От 12 до 96";
                isLoanTermValid = false;
            }
            else
            {
                LoanTermError.Text = "";
                _mainWindow.SetLoanTerm(ltText);
            }

            // Если оба ввода корректны, обновляем расчет
            if (isDownPaymentValid && isLoanTermValid)
            {
                FinishButton.IsEnabled = true;
                UpdateCalculation();
            }
            else
            {
                FinishButton.IsEnabled = false;
            }
        }

        private void UpdateCalculation()
        {
            try
            {
                CarPriceText.Text = $"{_mainWindow.GetTotalPrice():N0} ₽";
                DownPaymentAmountText.Text = $"Первоначальный взнос: {_mainWindow.GetDownPaymentAmount():N0} ₽";
                LoanAmountText.Text = $"Сумма кредита: {_mainWindow.GetLoanAmount():N0} ₽";
                MonthlyPaymentText.Text = $"Ежемесячный платеж: {_mainWindow.GetMonthlyPayment():N0} ₽";
            }
            catch
            {
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToPrice();
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Заявка оформлена!", "Успешно", MessageBoxButton.OK);
            Application.Current.Shutdown();
        }
    }
}