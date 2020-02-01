using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba3
{
    class Parser
    {
        public Parser()
        {

        }
        public string str_error = "";
        private List<string> operators = new List<string>(new string[] { "(", ")", "+", "-", "*", "/", "^", "~",
            "max", "min", "inc", "dec","mmax", "mmin" });
        private List<string> standart_operators =
            new List<string>(new string[] { "(", ")", "+", "-", "*", "/", "^", "~" });

        public IEnumerable<string> Separate(string input)
        {
            bool flag_min = false;
            bool flag_max = false;
            int pos = 0;
            while (pos < input.Length - 1)
            {
                while (input[pos] == ' ') pos++;
                string s = string.Empty + input[pos];
                if (!standart_operators.Contains(input[pos].ToString()))
                {
                    if (Char.IsDigit(input[pos]))
                    {
                        for (int i = pos + 1; i < input.Length && (Char.IsDigit(input[i])); i++)
                            s += input[i];
                    }
                    else if (Char.IsLetter(input[pos]))
                    {
                        for (int i = pos + 1; i < input.Length &&
                            (Char.IsLetter(input[i]) && input[pos] != ','); i++)
                            s += input[i];
                        //MessageBox.Show(s);
                    }
                }
                if (s == "mmax")
                {
                    flag_max = true;
                    int col = 0;
                    yield return "mmax";
                    List<string> num = new List<string>();
                    pos += s.Length;
                    while (input[pos] == ' ') pos++;
                    s = string.Empty + input[pos];
                    yield return s;
                    pos++;
                    while (flag_max && pos < input.Length)
                    {
                        while (input[pos] == ' ') pos++;
                        if (Char.IsDigit(input[pos]))
                        {
                            s = string.Empty;
                            for (int i = pos; i < input.Length && (Char.IsDigit(input[i])); i++)
                                s += input[i];
                            num.Add(s);
                            //MessageBox.Show(s);
                            pos += s.Length;
                            col++;
                        }
                        else if (input[pos] == ',')
                        {
                            pos++;
                        }
                        else if (input[pos] == ')')
                        {

                            for (int i = 0; i < num.Count; i++)
                                yield return num[i];
                            yield return col.ToString();
                            yield return ")";
                            pos++;
                            flag_max = false;
                        }
                    }

                }
                else if (s == "mmin")
                {
                    flag_min = true;
                    int col = 0;
                    yield return "mmin";
                    List<string> num = new List<string>();
                    pos += s.Length;
                    while (input[pos] == ' ') pos++;
                    s = string.Empty + input[pos];
                    yield return s;
                    pos++;
                    while (flag_min && pos < input.Length)
                    {
                        while (input[pos] == ' ') pos++;
                        if (Char.IsDigit(input[pos]))
                        {
                            s = string.Empty;
                            for (int i = pos; i < input.Length && (Char.IsDigit(input[i])); i++)
                                s += input[i];
                            num.Add(s);
                            //MessageBox.Show(s);
                            pos += s.Length;
                            col++;
                        }
                        else if (input[pos] == ',')
                        {
                            pos++;
                        }
                        else if (input[pos] == ')')
                        {

                            for (int i = 0; i < num.Count; i++)
                                yield return num[i];
                            yield return col.ToString();
                            yield return ")";
                            pos++;
                            flag_min = false;
                        }
                    }
                }
                else if (s != ",")
                {
                    if (pos == 0)
                    {
                        if (s == "-")
                            yield return "~";
                        else yield return s;
                    }
                    else if (standart_operators.Contains(input[pos - 1].ToString()) && input[pos - 1].ToString() != ")" && s == "-")
                        yield return "~";
                    else yield return s;
                    pos += s.Length;
                }
                else pos++;
            }
        }
        private byte GetPriority(string s)
        {
            switch (s)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
                case "inc":
                case "dec":
                case "min":
                case "max":
                case "mmax":
                case "mmin":
                    return 4;
                case "~":
                    return 5;
                default:
                    return 6;
            }
        }
        public string[] ConvertToPostfixNotation(string input)
        {
            List<string> outputSeparated = new List<string>();
            Stack<string> stack = new Stack<string>();

            foreach (string c in Separate(input))
            {
                //MessageBox.Show(c);
                if (operators.Contains(c))
                {
                    if (stack.Count > 0 && !c.Equals("("))
                    {
                        if (c.Equals(")"))
                        {
                            string s = stack.Pop();
                            while (s != "(")
                            {
                                outputSeparated.Add(s);
                                s = stack.Pop();
                            }
                        }
                        else if (GetPriority(c) > GetPriority(stack.Peek()))
                            stack.Push(c);
                        else
                        {
                            while (stack.Count > 0 && GetPriority(c) <= GetPriority(stack.Peek()))
                                outputSeparated.Add(stack.Pop());
                            stack.Push(c);
                        }
                    }
                    else
                        stack.Push(c);
                }
                else
                {

                    for (int i = 0; i < c.Length; i++)
                    {
                        if (Char.IsLetter(c[i]))
                        {
                            throw new Exception();
                        }
                    }

                    outputSeparated.Add(c);
                }

            }
            if (stack.Count > 0)
                foreach (string c in stack)
                    outputSeparated.Add(c);

            return outputSeparated.ToArray();
        }
        public string result(string input)
        {
            Stack<string> stack = new Stack<string>();

            Queue<string> queue = new Queue<string>(ConvertToPostfixNotation(input));
            if (queue.Count() == 0)
                return 0.ToString();
            string str = queue.Dequeue();
            try
            {
                while (queue.Count >= 0)
                {

                    if (!operators.Contains(str))
                    {

                        stack.Push(str);

                        if (queue.Count > 0)
                            str = queue.Dequeue();
                        else break;
                    }
                    else
                    {
                        //MessageBox.Show(str);
                        long summ = 0;


                        switch (str)
                        {

                            case "+":
                                {
                                    long a = Convert.ToInt64(stack.Pop());
                                    long b = Convert.ToInt64(stack.Pop());
                                    summ = a + b;
                                    break;
                                }
                            case "~":
                                {
                                    long a = Convert.ToInt64(stack.Pop());
                                    summ = -a;
                                    break;
                                }
                            case "-":
                                {
                                    long a = Convert.ToInt64(stack.Pop());
                                    long b = Convert.ToInt64(stack.Pop());
                                    summ = b - a;
                                    break;
                                }
                            case "*":
                                {
                                    long a = Convert.ToInt64(stack.Pop());
                                    long b = Convert.ToInt64(stack.Pop());
                                    summ = b * a;
                                    break;
                                }
                            case "/":
                                {
                                    long a = Convert.ToInt64(stack.Pop());
                                    long b = Convert.ToInt64(stack.Pop());
                                    //if (a == 0)
                                    //  throw new DivideByZeroException();

                                    //else
                                    //{
                                    summ = b / a;
                                    break;
                                    //}

                                }
                            case "^":
                                {
                                    long a = Convert.ToInt64(stack.Pop());
                                    long b = Convert.ToInt64(stack.Pop());

                                    summ = Convert.ToInt64(Math.Pow(Convert.ToDouble(b), Convert.ToDouble(a)));
                                    // throw new StackOverflowException();

                                    break;
                                }
                            case "min":
                                {
                                    int a = Convert.ToInt32(stack.Pop());
                                    int b = Convert.ToInt32(stack.Pop());
                                    summ = Math.Min(a, b);
                                    break;
                                }
                            case "max":
                                {
                                    int a = Convert.ToInt32(stack.Pop());
                                    int b = Convert.ToInt32(stack.Pop());
                                    summ = Math.Max(a, b);
                                    break;
                                }
                            case "mmax":
                                {
                                    int k = Convert.ToInt32(stack.Pop());
                                    int b = Convert.ToInt32(stack.Pop());
                                    for (int i = 0; i < k - 1; i++)
                                    {
                                        int a = Convert.ToInt32(stack.Pop());
                                        b = Math.Max(b, a);
                                    }
                                    summ = b;
                                    //MessageBox.Show(summ.ToString());
                                    break;
                                }
                            case "mmin":
                                {
                                    int k = Convert.ToInt32(stack.Pop());
                                    int b = Convert.ToInt32(stack.Pop());
                                    //MessageBox.Show(k.ToString());
                                    for (int i = 0; i < k - 1; i++)
                                    {
                                        int a = Convert.ToInt32(stack.Pop());
                                        b = Math.Min(b, a);

                                    }
                                    summ = b;
                                    break;
                                }
                            case "inc":
                                {
                                    int a = Convert.ToInt32(stack.Pop());
                                    summ = a + 1;
                                    break;
                                }
                            case "dec":
                                {
                                    int a = Convert.ToInt32(stack.Pop());
                                    summ = a - 1;
                                    break;
                                }
                        }

                        /* catch (Exception ex)
                         {
                             MessageBox.Show(ex.Message);
                         }*/
                        stack.Push(summ.ToString());
                        if (queue.Count > 0)
                            str = queue.Dequeue();
                        else
                            break;
                    }

                }
                // MessageBox.Show(str);
                string ss = stack.Pop();

                return ss;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
