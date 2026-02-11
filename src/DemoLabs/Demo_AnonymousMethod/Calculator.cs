namespace Demo_AnonymousMethod
{
    public delegate int ComputeHandler(int x, int y);

    public class Calculator
    {
        public int Compute(ComputeHandler objD, int a, int b)
        {
            int result = -1;

            Console.WriteLine("1. Authentication");
            Console.WriteLine("2. Authorization");
            Console.WriteLine("3. Validation");

            Console.WriteLine("4. The Process / The Activity");
            result = objD(a, b);

            Console.WriteLine("5. Audit Logging");
            return result;
        }
    }
}
