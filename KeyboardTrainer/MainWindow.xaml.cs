using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainer
{
    public partial class MainWindow : Window
    {
        private string generatedString = "";
        private int correctChars = 0;
        private int errors = 0;
        private DateTime startTime;

        public MainWindow()
        {
            InitializeComponent();
            LengthSlider.Value = 10; // Установим значение по умолчанию для длины строки
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Генерация строки для тренировки
            GenerateString();
            UserInputTextBox.IsReadOnly = false;
            UserInputTextBox.Focus();

            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;

            correctChars = 0;
            errors = 0;
            startTime = DateTime.Now;

            ErrorsTextBlock.Text = "0";
            SpeedTextBlock.Text = "0";
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            // Остановка тренировки
            StopTraining();
        }

        private void GenerateString()
        {
            // Генерация строки на основе настроек
            int length = (int)LengthSlider.Value;
            bool caseSensitive = CaseSensitivityCheckBox.IsChecked ?? false;
            generatedString = GetRandomString(length, caseSensitive);
            GeneratedStringTextBlock.Text = generatedString;
            UserInputTextBox.Text = "";
        }

        private string GetRandomString(int length, bool caseSensitive)
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            if (caseSensitive)
            {
                chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            Random random = new Random();
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return new string(result);
        }

        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (generatedString == "")
                return;

            // Проверка правильности ввода символов
            string userInput = UserInputTextBox.Text;

            if (userInput.Length < generatedString.Length)
            {
                if (e.Key.ToString().ToUpper() == generatedString[userInput.Length].ToString().ToUpper())
                {
                    correctChars++;
                }
                else
                {
                    errors++;
                    ErrorsTextBlock.Text = errors.ToString();
                }

                // Подсветка клавиш
                HighlightKey(e.Key);
            }

            // Обновление скорости
            UpdateSpeed();
        }

        private void HighlightKey(Key key)
        {
            // Пример: подсветка кнопки Q
            if (key == Key.Q)
            {
                QKey.Background = Brushes.LightGreen;
            }
            else
            {
                QKey.Background = Brushes.Red;
            }

            // Сброс подсветки через некоторое время
            Dispatcher.InvokeAsync(async () =>
            {
                await Task.Delay(200);
                QKey.Background = Brushes.White;
            });
        }

        private void UpdateSpeed()
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            double speed = correctChars / elapsed.TotalMinutes;
            SpeedTextBlock.Text = ((int)speed).ToString();
        }

        private void StopTraining()
        {
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            UserInputTextBox.IsReadOnly = true;
        }

        private void LengthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Изменение длины генерируемой строки
            if (StartButton.IsEnabled == false)
            {
                GenerateString();
            }
        }
    }
}