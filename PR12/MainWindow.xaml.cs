using PR12.Pages;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace PR12
{
    public partial class MainWindow : Window
    {
        // Данные
        public string SelectedModel { get; set; } = "Седан Comfort";
        public string SelectedEngine { get; set; } = "1.6L Бензин";
        public string SelectedColor { get; set; } = "Белый";
        public ObservableCollection<string> SelectedOptions { get; } = new ObservableCollection<string>();

        // Цены
        private decimal _modelPrice = 1200000m;
        private decimal _enginePrice = 0m;
        private decimal _colorPrice = 0m;
        private decimal _optionsPrice = 0m;
        private decimal _totalPrice = 1200000m;

        // Кредит
        private decimal _downPaymentPercent = 20m;
        private int _loanTerm = 36;
        private decimal _monthlyPayment = 0m;
        private decimal _downPaymentAmount = 0m;
        private decimal _loanAmount = 0m;
        private decimal _totalInterest = 0m;
        private decimal _totalWithInterest = 0m;

        public MainWindow()
        {
            InitializeComponent();
            CalculateCredit(); // Сразу считаем кредит при старте
            NavigateToModelAndEngine();
        }

        // Навигация
        public void NavigateToModelAndEngine()
        {
            MainFrame.Navigate(new ModelAndEngine(this));
        }

        public void NavigateToColorAndOptions()
        {
            MainFrame.Navigate(new ColorAndOptions(this));
        }

        public void NavigateToPrice()
        {
            MainFrame.Navigate(new Price(this));
        }

        public void NavigateToCredit()
        {
            MainFrame.Navigate(new Credit(this));
        }

        // Расчет общей цены
        public void CalculateTotalPrice()
        {
            // Модель
            if (SelectedModel == "Седан Comfort") _modelPrice = 1200000m;
            else if (SelectedModel == "Кроссовер Sport") _modelPrice = 1800000m;
            else if (SelectedModel == "Внедорожник Premium") _modelPrice = 2500000m;
            else if (SelectedModel == "Хэтчбек Urban") _modelPrice = 950000m;
            else _modelPrice = 1200000m;

            // Двигатель
            if (SelectedEngine == "1.6L Бензин") _enginePrice = 0m;
            else if (SelectedEngine == "2.0L Бензин") _enginePrice = 150000m;
            else if (SelectedEngine == "2.0L Дизель") _enginePrice = 200000m;
            else if (SelectedEngine == "Гибрид 1.8L") _enginePrice = 300000m;
            else if (SelectedEngine == "Электрический") _enginePrice = 500000m;
            else _enginePrice = 0m;

            // Цвет
            if (SelectedColor == "Белый") _colorPrice = 0m;
            else if (SelectedColor == "Черный") _colorPrice = 15000m;
            else if (SelectedColor == "Серебристый") _colorPrice = 20000m;
            else if (SelectedColor == "Синий металлик") _colorPrice = 25000m;
            else if (SelectedColor == "Красный перламутр") _colorPrice = 40000m;
            else _colorPrice = 0m;

            // Опции
            _optionsPrice = 0m;
            foreach (var option in SelectedOptions)
            {
                if (option == "Кожаный салон") _optionsPrice += 150000m;
                else if (option == "Панорамная крыша") _optionsPrice += 120000m;
                else if (option == "Премиум-аудиосистема") _optionsPrice += 80000m;
                else if (option == "Система помощи при парковке") _optionsPrice += 60000m;
                else if (option == "Адаптивный круиз-контроль") _optionsPrice += 90000m;
                else if (option == "Вентиляция сидений") _optionsPrice += 70000m;
                else if (option == "Подогрев руля") _optionsPrice += 25000m;
                else if (option == "Беспроводная зарядка") _optionsPrice += 15000m;
            }

            _totalPrice = _modelPrice + _enginePrice + _colorPrice + _optionsPrice;
            CalculateCredit(); // Пересчитываем кредит
        }

        // Расчет кредита ПО ФОРМУЛЕ из ТЗ
        public void CalculateCredit()
        {
            try
            {
                // 1. Сумма первоначального взноса
                _downPaymentAmount = _totalPrice * _downPaymentPercent / 100;

                // 2. Сумма кредита
                _loanAmount = _totalPrice - _downPaymentAmount;

                if (_loanAmount <= 0 || _loanTerm <= 0)
                {
                    _monthlyPayment = 0;
                    _totalInterest = 0;
                    _totalWithInterest = _downPaymentAmount;
                    return;
                }

                // 3. Месячная процентная ставка
                double annualRate = 0.12; // 12% годовых
                double monthlyRate = annualRate / 12;

                // 4. Расчет ежемесячного платежа по формуле
                double numerator = monthlyRate * Math.Pow(1 + monthlyRate, _loanTerm);
                double denominator = Math.Pow(1 + monthlyRate, _loanTerm) - 1;

                double monthlyPayment = (double)_loanAmount * (numerator / denominator);

                _monthlyPayment = (decimal)Math.Round(monthlyPayment, 2);

                // 5. Общая сумма с процентами
                _totalWithInterest = _downPaymentAmount + (_monthlyPayment * _loanTerm);

                // 6. Переплата по кредиту
                _totalInterest = _totalWithInterest - _totalPrice;
            }
            catch
            {
                _monthlyPayment = 0;
                _downPaymentAmount = 0;
                _loanAmount = 0;
                _totalInterest = 0;
                _totalWithInterest = 0;
            }
        }

        // Геттеры для цен
        public decimal GetModelPrice() => _modelPrice;
        public decimal GetEnginePrice() => _enginePrice;
        public decimal GetColorPrice() => _colorPrice;
        public decimal GetOptionsPrice() => _optionsPrice;
        public decimal GetTotalPrice() => _totalPrice;

        // Геттеры для кредита
        public decimal GetDownPaymentPercent() => _downPaymentPercent;
        public int GetLoanTerm() => _loanTerm;
        public decimal GetMonthlyPayment() => _monthlyPayment;
        public decimal GetDownPaymentAmount() => _downPaymentAmount;
        public decimal GetLoanAmount() => _loanAmount;
        public decimal GetTotalInterest() => _totalInterest;
        public decimal GetTotalWithInterest() => _totalWithInterest;

        // Установка параметров кредита
        public bool SetDownPaymentPercent(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            if (!decimal.TryParse(input, out decimal value)) return false;
            if (value < 10 || value > 90) return false;

            _downPaymentPercent = value;
            CalculateCredit();
            return true;
        }

        public bool SetLoanTerm(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            if (!int.TryParse(input, out int value)) return false;
            if (value < 12 || value > 96) return false;

            _loanTerm = value;
            CalculateCredit();
            return true;
        }

        // Форматированные строки
        public string GetFormattedTotalPrice() => $"{_totalPrice:N0} ₽";
        public string GetFormattedMonthlyPayment() => $"{_monthlyPayment:N0} ₽/мес";
        public string GetFormattedDownPaymentAmount() => $"{_downPaymentAmount:N0} ₽";
        public string GetFormattedLoanAmount() => $"{_loanAmount:N0} ₽";
        public string GetFormattedTotalInterest() => $"{_totalInterest:N0} ₽";
        public string GetFormattedTotalWithInterest() => $"{_totalWithInterest:N0} ₽";
    }
}