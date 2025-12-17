using System;
using System.Windows;
using System.Windows.Controls;

namespace PR12.Pages
{
    public partial class ModelAndEngine : Page
    {
        private MainWindow _mainWindow;

        public ModelAndEngine(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Простая установка выбранного значения
                foreach (ComboBoxItem item in ModelComboBox.Items)
                {
                    string content = item.Content.ToString();
                    if (content.Contains(_mainWindow.SelectedModel))
                    {
                        ModelComboBox.SelectedItem = item;
                        break;
                    }
                }

                foreach (ComboBoxItem item in EngineComboBox.Items)
                {
                    string content = item.Content.ToString();
                    if (content.Contains(_mainWindow.SelectedEngine))
                    {
                        EngineComboBox.SelectedItem = item;
                        break;
                    }
                }

                // Подписываемся на изменения
                ModelComboBox.SelectionChanged += (s, e) =>
                {
                    if (ModelComboBox.SelectedItem is ComboBoxItem selected)
                    {
                        string content = selected.Content.ToString();
                        // Простой способ получить модель
                        if (content.Contains("Седан")) _mainWindow.SelectedModel = "Седан Comfort";
                        else if (content.Contains("Кроссовер")) _mainWindow.SelectedModel = "Кроссовер Sport";
                        else if (content.Contains("Внедорожник")) _mainWindow.SelectedModel = "Внедорожник Premium";
                        else if (content.Contains("Хэтчбек")) _mainWindow.SelectedModel = "Хэтчбек Urban";
                    }
                };

                EngineComboBox.SelectionChanged += (s, e) =>
                {
                    if (EngineComboBox.SelectedItem is ComboBoxItem selected)
                    {
                        string content = selected.Content.ToString();
                        // Простой способ получить двигатель
                        if (content.Contains("1.6L")) _mainWindow.SelectedEngine = "1.6L Бензин";
                        else if (content.Contains("2.0L Бензин")) _mainWindow.SelectedEngine = "2.0L Бензин";
                        else if (content.Contains("2.0L Дизель")) _mainWindow.SelectedEngine = "2.0L Дизель";
                        else if (content.Contains("Гибрид")) _mainWindow.SelectedEngine = "Гибрид 1.8L";
                        else if (content.Contains("Электрический")) _mainWindow.SelectedEngine = "Электрический";
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки шага 1: {ex.Message}");
            }
        }
    }
}