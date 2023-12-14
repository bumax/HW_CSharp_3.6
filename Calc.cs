using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CSharp_3._6
{
    public interface ICalc
    {
        double Result { get; set; }
        void Sum(double x);
        void Sub(double x);
        void Multy(double x);
        void Divide(double x);
        void CancelLast();
        event EventHandler<EventArgs> MyEventHandler;
    }
    internal class Calc : ICalc
    {
        private delegate double ParseText(string input);
        public double Result { get; set; } = 0D;
        private Stack<double> LastResult { get; set; } = new Stack<double>();

        public event EventHandler<EventArgs> MyEventHandler;

        private void PrintResult()
        {
            MyEventHandler?.Invoke(this, new EventArgs());
        }


        public void Divide(double x)
        {
            try
            {
                if(x==0) { 
                    throw new DivideByZeroException();
                }
                Result /= x;
                PrintResult();
                LastResult.Push(Result);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Делить на ноль нельзя! Операция отклонена");
            }
        }

        public void Multy(double x)
        {
            Result *= x;
            PrintResult();
            LastResult.Push(Result);
        }

        public void Sub(double x)
        {
            Result -= x;
            PrintResult();
            LastResult.Push(Result);
        }

        public void Sum(double x)
        {
            Result += x;
            PrintResult();
            LastResult.Push(Result);
        }

        public void CancelLast()
        {
            if (LastResult.TryPop(out double res))
            {
                Result = res;
                Console.WriteLine("Последнее действие отменено. Результат равен:");
                PrintResult();
            }
            else
            {
                Console.WriteLine("Невозможно отменить послдеднее действие!");
            }
        }
        /// <summary>
        /// реализация GUI калькулятора
        /// </summary>
        /// <param name="type">тип данных: 0 - double, 1 - int</param>
        public void View(int type = 0)
        {
            ParseText pt;
            if (type == 0)
                pt = DoubleTryParse;
            else
                pt = IntTryParse;
            bool done = true;
            while (done)
            {
                Console.Write("Введите команду (+,-,*,/, undo, cancel):");
                var cmd = Console.ReadLine().ToLower();
                switch (cmd)
                {
                    case "+":
                        Console.Write("Введите слагаемое:");
                        Sum(pt(Console.ReadLine()));
                        break;
                    case "-":
                        Console.Write("Введите вычитаемое:");
                        Sub(pt(Console.ReadLine()));
                        break;
                    case "*":
                        Console.Write("Введите множитель:");
                        Multy(pt(Console.ReadLine()));
                        break;
                    case "/":
                        Console.Write("Введите делитель:");
                        Divide(pt(Console.ReadLine()));
                        break;
                    case "undo":
                        CancelLast();
                        break;
                    case "":
                    case "cancel":
                        done = false;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда!");
                        break;
                }
            }
        }

        private double DoubleTryParse(string text)
        {
            double res = 0;
            try
            {
                res = Convert.ToDouble(text.Replace('.', ','));
                if (res < 0)
                    throw new Exception("Число должно быть положительным!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Не удалось распарсить ввод!");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return res;
        }
        private double IntTryParse(string text)
        {
            double res = 0;
            try
            {
                res = Convert.ToInt32(text);
                if (res < 0)
                    throw new Exception("Число должно быть положительным!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Не удалось распарсить ввод!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return res;
        }
    }
}
