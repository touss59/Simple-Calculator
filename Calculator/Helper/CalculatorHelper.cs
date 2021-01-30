using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Helper
{
    public static class CalculatorHelper
    {
        public static string errorSyntax = "Error Syntax";
        private static readonly List<char> operators = new List<char> { '*', '/', '-', '+' };
        public static string CalCulateGlobalResult(string operations)
        {
            operations = operations.Replace("|", "");
            string numberNow = "";
            Parenthese parenthese = null;
            List<string> numbersOperationsBase = new List<string> { };
            TakeCareNoneOperatorBeforeParenthese(ref operations);

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
            CleanUpTheInput(ref numbersOperators);
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

        private static void CleanUpTheInput(ref List<string> numbersOperators)
        {
            RemoveNullValues(ref numbersOperators);
            numbersOperators.TakeCareOfSuccessiveOperators();

        }

        private static void RemoveNullValues(ref List<string> numbersOperators)
        {
            numbersOperators = numbersOperators.Where(x => x != "").ToList();
        }

        private static void TakeCareNoneOperatorBeforeParenthese(ref string numbersOperators)
        {
            for (var i = 0; i < numbersOperators.Length; i++)
            {
                if (i - 1 > 0 && numbersOperators[i] == '(' && !operators.Contains(numbersOperators[i - 1]))
                {
                    numbersOperators = numbersOperators.Insert(i, "*");
                }
            }
        }

        private static void TakeCareOfSuccessiveOperators(this List<string> numbersOperators)
        {
            for (var i = 0; i < numbersOperators.Count; i++)
            {
                if (numbersOperators.CanConvertTwoSuccessiveOperatorsInOnePositive(i))
                {
                    numbersOperators[i] = "+";
                    numbersOperators.RemoveAt(i + 1);
                    i--;
                    continue;
                }

                if (numbersOperators.CanConvertTwoSuccessiveOperatorsInOneNegative(i))
                {
                    numbersOperators[i] = "-";
                    numbersOperators.RemoveAt(i + 1);
                    i--;
                    continue;
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


        private static bool CanConvertTwoSuccessiveOperatorsInOnePositive(this List<string> numbersOperators, int i)
        {
            if (i + 1 < numbersOperators.Count && ((numbersOperators[i] == "-" && numbersOperators[i + 1] == "-") || (numbersOperators[i] == "+" && numbersOperators[i + 1] == "+")))
            {
                return true;
            }
            return false;
        }

        private static bool CanConvertTwoSuccessiveOperatorsInOneNegative(this List<string> numbersOperators, int i)
        {
            if (i + 1 < numbersOperators.Count && ((numbersOperators[i] == "-" && numbersOperators[i + 1] == "+") || (numbersOperators[i] == "+" && numbersOperators[i + 1] == "-")))
            {
                return true;
            }
            return false;
        }
    }
}
