using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace TextFormatter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumericValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private bool IsTextNumeric(string str)
        {
            return int.TryParse(str, out _);
        }

        private TextRange GetSelectedTextRange()
        {
            if (int.TryParse(StartIndexTextBox.Text, out int startIndex) &&
                int.TryParse(EndIndexTextBox.Text, out int endIndex))
            {
                if (startIndex >= 0 && endIndex >= startIndex)
                {
                    TextPointer start = TextEditor.Document.ContentStart.GetPositionAtOffset(startIndex);
                    TextPointer end = TextEditor.Document.ContentStart.GetPositionAtOffset(endIndex + 1);
                    if (start != null && end != null)
                    {
                        return new TextRange(start, end);
                    }
                }
            }
            MessageBox.Show("Invalid range");
            return null;
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange selectedText = GetSelectedTextRange();
            if (selectedText != null)
            {
                selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange selectedText = GetSelectedTextRange();
            if (selectedText != null)
            {
                selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange selectedText = GetSelectedTextRange();
            if (selectedText != null)
            {
                selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange selectedText = GetSelectedTextRange();
            if (selectedText != null)
            {
                selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                selectedText.ApplyPropertyValue(TextElement.FontSizeProperty, 12.0);
                selectedText.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            }
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextRange selectedText = GetSelectedTextRange();
            if (selectedText != null && FontSizeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                selectedText.ApplyPropertyValue(TextElement.FontSizeProperty, Convert.ToDouble(selectedItem.Content));
            }
        }

        private void FontColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextRange selectedText = GetSelectedTextRange();
            if (selectedText != null && FontColorComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                Brush color = Brushes.Black;
                switch (selectedItem.Content.ToString())
                {
                    case "Red": color = Brushes.Red; break;
                    case "Blue": color = Brushes.Blue; break;
                    case "Green": color = Brushes.Green; break;
                }
                selectedText.ApplyPropertyValue(TextElement.ForegroundProperty, color);
            }
        }
    }
}