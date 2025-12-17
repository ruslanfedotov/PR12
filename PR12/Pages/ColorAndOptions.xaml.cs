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
            LoadData();
        }

        private void LoadData()
        {
            // Устанавливаем цвет
            foreach (ComboBoxItem item in ColorComboBox.Items)
            {
                if (item.Content.ToString().StartsWith(_mainWindow.SelectedColor))
                {
                    ColorComboBox.SelectedItem = item;
                    break;
                }
            }

            // Устанавливаем опции
            Option1.IsChecked = _mainWindow.SelectedOptions.Contains("Кожаный салон");
            Option2.IsChecked = _mainWindow.SelectedOptions.Contains("Панорамная крыша");
            Option3.IsChecked = _mainWindow.SelectedOptions.Contains("Премиум-аудиосистема");
            Option4.IsChecked = _mainWindow.SelectedOptions.Contains("Система помощи при парковке");
            Option5.IsChecked = _mainWindow.SelectedOptions.Contains("Адаптивный круиз-контроль");
            Option6.IsChecked = _mainWindow.SelectedOptions.Contains("Вентиляция сидений");
            Option7.IsChecked = _mainWindow.SelectedOptions.Contains("Подогрев руля");
            Option8.IsChecked = _mainWindow.SelectedOptions.Contains("Беспроводная зарядка");

            // Подписываемся на изменения
            ColorComboBox.SelectionChanged += (s, e) =>
            {
                if (ColorComboBox.SelectedItem is ComboBoxItem selected)
                {
                    string content = selected.Content.ToString();
                    string color = content.Split('+')[0].Trim();
                    _mainWindow.SelectedColor = color;
                }
            };

            // Обработчики для чекбоксов
            Option1.Checked += (s, e) => { if (!_mainWindow.SelectedOptions.Contains("Кожаный салон")) _mainWindow.SelectedOptions.Add("Кожаный салон"); };
            Option1.Unchecked += (s, e) => _mainWindow.SelectedOptions.Remove("Кожаный салон");

            Option2.Checked += (s, e) => { if (!_mainWindow.SelectedOptions.Contains("Панорамная крыша")) _mainWindow.SelectedOptions.Add("Панорамная крыша"); };
            Option2.Unchecked += (s, e) => _mainWindow.SelectedOptions.Remove("Панорамная крыша");

            Option3.Checked += (s, e) => { if (!_mainWindow.SelectedOptions.Contains("Премиум-аудиосистема")) _mainWindow.SelectedOptions.Add("Премиум-аудиосистема"); };
            Option3.Unchecked += (s, e) => _mainWindow.SelectedOptions.Remove("Премиум-аудиосистема");

            Option4.Checked += (s, e) => { if (!_mainWindow.SelectedOptions.Contains("Система помощи при парковке")) _mainWindow.SelectedOptions.Add("Система помощи при парковке"); };
            Option4.Unchecked += (s, e) => _mainWindow.SelectedOptions.Remove("Система помощи при парковке");

            Option5.Checked += (s, e) => { if (!_mainWindow.SelectedOptions.Contains("Адаптивный круиз-контроль")) _mainWindow.SelectedOptions.Add("Адаптивный круиз-контроль"); };
            Option5.Unchecked += (s, e) => _mainWindow.SelectedOptions.Remove("Адаптивный круиз-контроль");

            Option6.Checked += (s, e) => { if (!_mainWindow.SelectedOptions.Contains("Вентиляция сидений")) _mainWindow.SelectedOptions.Add("Вентиляция сидений"); };
            Option6.Unchecked += (s, e) => _mainWindow.SelectedOptions.Remove("Вентиляция сидений");

            Option7.Checked += (s, e) => { if (!_mainWindow.SelectedOptions.Contains("Подогрев руля")) _mainWindow.SelectedOptions.Add("Подогрев руля"); };
            Option7.Unchecked += (s, e) => _mainWindow.SelectedOptions.Remove("Подогрев руля");

            Option8.Checked += (s, e) => { if (!_mainWindow.SelectedOptions.Contains("Беспроводная зарядка")) _mainWindow.SelectedOptions.Add("Беспроводная зарядка"); };
            Option8.Unchecked += (s, e) => _mainWindow.SelectedOptions.Remove("Беспроводная зарядка");

            // Подписываемся на изменения
            ColorComboBox.SelectionChanged += (s, e) =>
            {
                if (ColorComboBox.SelectedItem is ComboBoxItem selected)
                {
                    string content = selected.Content.ToString();
                    // Берем часть до скобки (например "Белый")
                    string color = content.Split('(')[0].Trim();
                    _mainWindow.SelectedColor = color;
                }
            };
        }
    }
}