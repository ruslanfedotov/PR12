using System;
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

            // Загружаем данные после инициализации компонентов
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Проверяем, что MainWindow существует
                if (_mainWindow == null)
                {
                    MessageBox.Show("MainWindow не инициализирован");
                    return;
                }

                // Проверяем, что элементы управления созданы
                if (CarPriceText == null || DownPaymentTextBox == null ||
                    LoanTermTextBox == null || DownPaymentResultText == null ||
                    LoanAmountText == null || MonthlyPaymentText == null ||
                    TotalWithInterestText == null)
                {
                    MessageBox.Show("Элементы управления не инициализированы");
                    return;
                }

                // Загружаем данные
                CarPriceText.Text = $"{_mainWindow.GetTotalPrice():N0} ₽";
                DownPaymentTextBox.Text = _mainWindow.DownPaymentPercent.ToString();
                LoanTermTextBox.Text = _mainWindow.LoanTerm.ToString();

                // Выполняем расчет
                UpdateCreditDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных шага 4: {ex.Message}");
            }
        }

        private void CalculateCredit(object sender, TextChangedEventArgs e)
        {
            try
            {
                // Проверяем, что MainWindow существует
                if (_mainWindow == null) return;

                // Проверяем ввод
                if (!decimal.TryParse(DownPaymentTextBox.Text, out decimal downPercent) ||
                    downPercent < 10 || downPercent > 90)
                {
                    DownPaymentResultText.Text = "10-90%";
                    return;
                }

                if (!int.TryParse(LoanTermTextBox.Text, out int term) ||
                    term < 12 || term > 96)
                {
                    MonthlyPaymentText.Text = "12-96 месяцев";
                    return;
                }

                // Сохраняем значения
                _mainWindow.DownPaymentPercent = downPercent;
                _mainWindow.LoanTerm = term;

                // Пересчитываем кредит
                _mainWindow.CalculateCredit();

                // Обновляем отображение
                UpdateCreditDisplay();
            }
            catch (Exception ex)
            {
                // Устанавливаем значения по умолчанию при ошибке
                DownPaymentResultText.Text = "Ошибка";
                LoanAmountText.Text = "Ошибка";
                MonthlyPaymentText.Text = "Ошибка";
                TotalWithInterestText.Text = "Ошибка";
            }
        }

        private void UpdateCreditDisplay()
        {
            try
            {
                if (_mainWindow == null) return;

                decimal carPrice = _mainWindow.GetTotalPrice();
                decimal downPayment = _mainWindow.GetDownPaymentAmount();
                decimal loanAmount = _mainWindow.GetLoanAmount();
                decimal monthlyPayment = _mainWindow.GetMonthlyPayment();
                decimal totalWithInterest = downPayment + (monthlyPayment * _mainWindow.LoanTerm);

                DownPaymentResultText.Text = $"{downPayment:N0} ₽";
                LoanAmountText.Text = $"{loanAmount:N0} ₽";
                MonthlyPaymentText.Text = $"{monthlyPayment:N0} ₽/мес";
                TotalWithInterestText.Text = $"{totalWithInterest:N0} ₽";
            }
            catch (Exception)
            {
                // Игнорируем ошибки отображения
            }
        }
    }
}