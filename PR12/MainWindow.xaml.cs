using PR12.Pages;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PR12
{
    public partial class MainWindow : Window
    {
        public string SelectedModel { get; set; } = "Седан Comfort";
        public string SelectedEngine { get; set; } = "1.6L Бензин";
        public string SelectedColor { get; set; } = "Белый";
        public ObservableCollection<string> SelectedOptions { get; } = new ObservableCollection<string>();

        // Контактные данные
        public string CustomerName { get; set; } = "";
        public string CustomerPhone { get; set; } = "";
        public string CustomerEmail { get; set; } = "";

        // Параметры кредита
        public decimal DownPaymentPercent { get; set; } = 20m; // %
        public int LoanTerm { get; set; } = 36; // месяцев

        // Цены
        private decimal _totalPrice = 1200000m;
        private decimal _monthlyPayment = 0m;
        private decimal _downPaymentAmount = 0m;
        private decimal _loanAmount = 0m;

        private int _currentStep = 1;

        public MainWindow()
        {
            InitializeComponent();
            NavigateToStep(1);
        }

        private void NavigateToStep(int step)
        {
            try
            {
                _currentStep = step;
                UpdateProgressBar();

                switch (step)
                {
                    case 1:
                        MainFrame.Navigate(new ModelAndEngine(this));
                        BackButton.IsEnabled = false;
                        NextButton.Content = "Далее";
                        break;
                    case 2:
                        MainFrame.Navigate(new ColorAndOptions(this));
                        BackButton.IsEnabled = true;
                        NextButton.Content = "Далее";
                        break;
                    case 3:
                        CalculatePrice();
                        MainFrame.Navigate(new Price(this));
                        BackButton.IsEnabled = true;
                        NextButton.Content = "Далее";
                        break;
                    case 4:
                        CalculateCredit();
                        MainFrame.Navigate(new Credit(this));
                        BackButton.IsEnabled = true;
                        NextButton.Content = "Далее";
                        break;
                    case 5:
                        MainFrame.Navigate(new Order(this));
                        BackButton.IsEnabled = true;
                        NextButton.Content = "Отправить";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при переходе на шаг {step}: {ex.Message}");
            }
        }

        private void UpdateProgressBar()
        {
            try
            {
                // Убедимся, что элементы существуют
                if (Step1Text == null || Step2Text == null || Step3Text == null ||
                    Step4Text == null || Step5Text == null)
                {
                    return;
                }

                // Сбрасываем все
                Step1Text.Background = Brushes.LightGray;
                Step2Text.Background = Brushes.LightGray;
                Step3Text.Background = Brushes.LightGray;
                Step4Text.Background = Brushes.LightGray;
                Step5Text.Background = Brushes.LightGray;

                // Активируем текущий шаг и все предыдущие
                if (_currentStep >= 1) Step1Text.Background = Brushes.DodgerBlue;
                if (_currentStep >= 2) Step2Text.Background = Brushes.DodgerBlue;
                if (_currentStep >= 3) Step3Text.Background = Brushes.DodgerBlue;
                if (_currentStep >= 4) Step4Text.Background = Brushes.DodgerBlue;
                if (_currentStep >= 5) Step5Text.Background = Brushes.DodgerBlue;
            }
            catch (Exception ex)
            {
                // Игнорируем ошибки в прогресс-баре
            }
        }

        private void CalculatePrice()
        {
            try
            {
                decimal price = 0;

                // Базовая цена модели
                if (SelectedModel == "Седан Comfort") price = 1200000m;
                else if (SelectedModel == "Кроссовер Sport") price = 1800000m;
                else if (SelectedModel == "Внедорожник Premium") price = 2500000m;
                else if (SelectedModel == "Хэтчбек Urban") price = 950000m;

                // Двигатель
                if (SelectedEngine == "1.6L Бензин") price += 0m;
                else if (SelectedEngine == "2.0L Бензин") price += 150000m;
                else if (SelectedEngine == "2.0L Дизель") price += 200000m;
                else if (SelectedEngine == "Гибрид 1.8L") price += 300000m;
                else if (SelectedEngine == "Электрический") price += 500000m;

                // Цвет
                if (SelectedColor == "Белый") price += 0m;
                else if (SelectedColor == "Черный") price += 15000m;
                else if (SelectedColor == "Серебристый") price += 20000m;
                else if (SelectedColor == "Синий металлик") price += 25000m;
                else if (SelectedColor == "Красный перламутр") price += 40000m;

                // Опции
                foreach (var option in SelectedOptions)
                {
                    if (option == "Кожаный салон") price += 150000m;
                    else if (option == "Панорамная крыша") price += 120000m;
                    else if (option == "Премиум-аудиосистема") price += 80000m;
                    else if (option == "Система помощи при парковке") price += 60000m;
                    else if (option == "Адаптивный круиз-контроль") price += 90000m;
                    else if (option == "Вентиляция сидений") price += 70000m;
                    else if (option == "Подогрев руля") price += 25000m;
                    else if (option == "Беспроводная зарядка") price += 15000m;
                }

                _totalPrice = price;
            }
            catch (Exception ex)
            {
                _totalPrice = 1200000m;
            }
        }

        public void CalculateCredit()
        {
            try
            {
                // Формула из ТЗ
                double C = (double)_totalPrice;
                double P = (double)(_totalPrice * DownPaymentPercent / 100);
                double S = C - P; // Сумма кредита

                _downPaymentAmount = (decimal)P;
                _loanAmount = (decimal)S;

                if (S <= 0 || LoanTerm <= 0)
                {
                    _monthlyPayment = 0;
                    return;
                }

                double r = 12.0; // 12% годовых
                double i = r / 100 / 12; // Месячная ставка
                double n = LoanTerm; // Срок в месяцах

                // A = S * (i * (1+i)^n) / ((1+i)^n - 1)
                double numerator = i * Math.Pow(1 + i, n);
                double denominator = Math.Pow(1 + i, n) - 1;

                double A = S * (numerator / denominator);

                _monthlyPayment = (decimal)Math.Round(A, 2);
            }
            catch (Exception)
            {
                // Если ошибка, используем простой расчет
                double S = (double)_totalPrice * 0.8; // 20% первоначальный взнос
                _monthlyPayment = (decimal)(S / LoanTerm);
                _downPaymentAmount = _totalPrice * DownPaymentPercent / 100;
                _loanAmount = _totalPrice - _downPaymentAmount;
            }
        }

        public decimal GetTotalPrice() => _totalPrice;
        public decimal GetMonthlyPayment() => _monthlyPayment;
        public decimal GetDownPaymentAmount() => _downPaymentAmount;
        public decimal GetLoanAmount() => _loanAmount;

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentStep > 1)
            {
                NavigateToStep(_currentStep - 1);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentStep < 5)
            {
                NavigateToStep(_currentStep + 1);
            }
            else
            {
                // Отправка заявки
                if (!string.IsNullOrEmpty(CustomerName) &&
                    !string.IsNullOrEmpty(CustomerPhone) &&
                    !string.IsNullOrEmpty(CustomerEmail))
                {
                    MessageBox.Show($"Заявка отправлена!\n\nИмя: {CustomerName}\nТелефон: {CustomerPhone}\nEmail: {CustomerEmail}\n\nСумма: {_totalPrice:N0} ₽", "Успех");
                    Application.Current.Shutdown();
                }
                else
                {
                    MessageBox.Show("Заполните все поля", "Ошибка");
                }
            }
        }
    }
}