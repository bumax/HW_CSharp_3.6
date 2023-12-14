/* Доработайте класс калькулятора способным работать как с целочисленными так и с дробными числами. (возможно стоит задействовать перегрузку операций).
//заменить Convert.ToDouble на собственный DoubleTryPars и обрабатываем ошибку
//проверить что введенное число небыло отрицательное (вывести ошибку Exeption , отловить Catch)
// сумма не может быть отрицательной (при делении и вычитании) */



namespace HW_CSharp_3._6
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var calc = new Calc();
            calc.MyEventHandler += Calc_MyEventHandler;
            calc.View();
        }

        private static void Calc_MyEventHandler(object? sender, EventArgs e)
        {
            if (sender is Calc)
                Console.WriteLine(((Calc)sender).Result);
        }
    }
}
