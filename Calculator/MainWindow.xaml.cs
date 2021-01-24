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

namespace Calculator
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }

        public void SelectButton(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = button.Content.ToString();
            }
            else
            {
                resultLabel.Content += button.Content.ToString();
            }
        }

        public void DeleteLastChar(object sender, RoutedEventArgs e)
        {
            string label = resultLabel.Content.ToString();
            if (label.Length > 0)
                resultLabel.Content = label.Remove(label.Length - 1, 1);
        }

        private void ResetCalculator(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
        }

        private void GiveResult(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = CalculatorHelper.CalCulateGlobalResult(resultLabel.Content.ToString());
        }

    }

    public class Parenthese
    {
        public Parenthese Ancestor { get; }

        public List<string> NumbersOperators { get; set; } = new List<string> { };

        private Parenthese(Parenthese ancestor)
        {
            Ancestor = ancestor;
        }

        public Parenthese OpenParenthese()
        {
            return new Parenthese(this);
        }

        public (Parenthese ancestor, string resultThis) CloseParenthese()
        {
            return (Ancestor, NumbersOperators.ResultOperationsOfThisBlock());
        }

        public Parenthese()
        {
            this.Ancestor = null;
        }
    }

    public static class CalculatorHelper
    {
        private static readonly List<char> operators = new List<char> { '*', '/', '-', '+' };
        public static string CalCulateGlobalResult(string operations)
        {
            string numberNow = "";
            Parenthese parenthese = null;
            List<string> numbersOperationsBase = new List<string> { };

            foreach (var numberOperator in operations)
            {
                if (numberOperator == '(')
                {
                    if (parenthese == null)
                    {
                        parenthese = new Parenthese();
                    }
                    else
                    {
                        parenthese = parenthese.OpenParenthese();
                    }
                }
                else if (numberOperator == ')')
                {
                    parenthese.NumbersOperators.Add(numberNow);
                    numberNow = "";
                    (Parenthese parentheseAncestor, string resultParenthese) = parenthese.CloseParenthese();
                    if (parentheseAncestor == null)
                    {
                        parenthese = null;
                        numbersOperationsBase.Add(resultParenthese);
                    }
                    else
                    {
                        parenthese = parentheseAncestor;
                        parenthese.NumbersOperators.Add(resultParenthese);
                    }
                }
                else
                {
                    if (operators.Contains(numberOperator))
                    {
                        if (parenthese == null)
                        {
                            numbersOperationsBase.Add(numberNow);
                            numbersOperationsBase.Add(numberOperator.ToString());
                            numberNow = "";
                        }
                        else
                        {
                            parenthese.NumbersOperators.Add(numberNow);
                            parenthese.NumbersOperators.Add(numberOperator.ToString());
                            numberNow = "";
                        }
                    }
                    else
                    {
                        numberNow += numberOperator;
                    }
                }
            }

            numbersOperationsBase.Add(numberNow);

            return numbersOperationsBase.ResultOperationsOfThisBlock();
        }

        public static string ResultOperationsOfThisBlock(this List<string> numbersOperators)
        {
            numbersOperators= numbersOperators.CleanUpTheInput();
            numbersOperators.TakeCareOfPriorityOperationsFirst();
            return numbersOperators.GetResultFromListOperators();
        }



        private static void TakeCareOfPriorityOperationsFirst(this List<string> numbersOperators)
        {
            for (var i = 0; i < numbersOperators.Count; i++)
            {
                if (numbersOperators.ContainsDivisorOrMultiplication(i))
                {
                    if (numbersOperators[i + 1] == "/")
                    {
                        numbersOperators[i] = (double.Parse(numbersOperators[i]) / double.Parse(numbersOperators[i + 2])).ToString();
                    }
                    else
                    {
                        numbersOperators[i] = (double.Parse(numbersOperators[i]) * double.Parse(numbersOperators[i + 2])).ToString();
                    }
                    numbersOperators.RemoveRange(i + 1, 2);
                    i--;
                }
            }
        }

        private static bool ContainsDivisorOrMultiplication(this List<string> numbersOperators, int index)
        {
            if (index + 2 < numbersOperators.Count && (numbersOperators[index + 1] == "/" || numbersOperators[index + 1] == "*"))
            {
                return true;
            }
            return false;
        }

        private static string GetResultFromListOperators(this List<string> numbersOperators)
        {
            double result = 0;
            while (numbersOperators.Count > 0)
            {
                if (numbersOperators[0] == "-")
                {
                    result -= double.Parse(numbersOperators[1]);
                    numbersOperators.RemoveRange(0, 2);
                }
                else if (numbersOperators[0] == "+")
                {
                    result += double.Parse(numbersOperators[1]);
                    numbersOperators.RemoveRange(0, 2);
                }
                else
                {
                    result += double.Parse(numbersOperators[0]);
                    numbersOperators.RemoveAt(0);
                }
            }
            return result.ToString();
        }

        private static List<string> CleanUpTheInput(this List<string> numbersOperators)
        {
            numbersOperators = numbersOperators.RemoveNullValues();
            numbersOperators= numbersOperators.TakeCareOfMultipleOperators();

            return numbersOperators;
        }

        private static List<string> RemoveNullValues(this List<string> numbersOperators)
        {
            return numbersOperators.Where(x => x != "").ToList();
        }

        private static List<string> TakeCareOfMultipleOperators(this List<string> numbersOperators)
        {
            for(var i = 0; i < numbersOperators.Count; i++)
            {
                if (i+1 < numbersOperators.Count && ((numbersOperators[i]=="-" && numbersOperators[i+1] == "-")|| (numbersOperators[i] == "+" && numbersOperators[i + 1] == "+")))
                {
                    numbersOperators[i] = "+";
                    numbersOperators.RemoveAt(i + 1);
                    i--;
                    continue;
                }

                if (i + 1 < numbersOperators.Count &&((numbersOperators[i] == "-" && numbersOperators[i+1] == "+") ||(numbersOperators[i] == "+" && numbersOperators[i + 1] == "-")))
                {
                    numbersOperators[i] = "-";
                    numbersOperators.RemoveAt(i + 1);
                    i--;
                    continue;
                }
            }
            return numbersOperators;
        }
    }
}
