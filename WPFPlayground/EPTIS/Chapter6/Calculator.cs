namespace EPTIS.Chapter6
{
    public class Calculator
    {
        public string Add(string arg1, string arg2)
        {
            double x = 0;
            double y = 0;
            double z = 0;
            if (double.TryParse(arg1, out x) && double.TryParse(arg2, out y))
            {
                return (x + y).ToString();
            }

            return "Input Error";
        }
    }
}
