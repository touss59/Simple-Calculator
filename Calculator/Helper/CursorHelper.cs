using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Helper
{
    public static class CursorHelper
    {
        public static string GetLabel(this List<char> inputList)
        {
            string input = "";
            foreach (var c in inputList)
            {
                input += c;
            }
            return input;
        }

        public static void MoveCursor(this List<char> inputList, bool toRight)
        {
            int direction = toRight ? 1 : -1;
            int i = inputList.IndexOf('|');
            if (i + direction >= 0 && i + direction < inputList.Count)
            {
                if (i + direction == inputList.Count - 1)
                {
                    inputList.RemoveAt(i);
                    return;
                }
                char temp = inputList[i];
                inputList[i] = inputList[i + direction];
                inputList[i + direction] = temp;
            }
        }
    }
}
