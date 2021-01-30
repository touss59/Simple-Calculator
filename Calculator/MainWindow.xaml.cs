using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calculator.Helper;

namespace Calculator
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }

        public void SelectButton(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string numberSelect = button.Content.ToString();
            string label = resultLabel.Content.ToString();
            if (label == "0" || label == CalculatorHelper.errorSyntax)
            {
                resultLabel.Content = numberSelect;
                return;
            }
            else
            {
                if (label.Contains('|'))
                {
                    int index = label.IndexOf('|');
                    label = label.Insert(index+1, numberSelect);
                }
                else
                {
                    label += numberSelect;
                }
            }
            resultLabel.Content = label;
        }

        public void DeleteLastChar(object sender, RoutedEventArgs e)
        {
            string label = resultLabel.Content.ToString();
            if (label.Length > 0)
            {
                if (label.Contains('|'))
                {
                    int index = label.IndexOf('|');
                    if (index > 0)
                    {
                        resultLabel.Content = label.Remove(index - 1, 1);
                    }
                }
                else
                {
                    resultLabel.Content = label.Remove(label.Length - 1, 1);
                }
            }
        }

        private void ResetCalculator(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
        }

        private void CursorAction(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            bool toRight =Convert.ToBoolean(button.Tag.ToString());
            List<char> inputList = new List<char> { };
            string input = resultLabel.Content.ToString();
            foreach (var c in input) { inputList.Add(c); }

            if (!inputList.Contains('|'))
            {
                if(!toRight)
                    inputList.Insert(inputList.Count() - 1, '|');
            }
            else
            {
                inputList.MoveCursor(toRight);
            }
            resultLabel.Content = inputList.GetLabel();
        }

        private void GiveResult(object sender, RoutedEventArgs e)
        {
            try
            {
                resultLabel.Content = CalculatorHelper.CalCulateGlobalResult(resultLabel.Content.ToString());
            }
            catch
            {
                resultLabel.Content = CalculatorHelper.errorSyntax;
            }
        }

    }
}
