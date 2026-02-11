namespace Demo_AnonymousMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int result;
            Calculator objCalculator = new Calculator();

            Console.WriteLine("--- implicitly instantiated the delegate object");
            result = objCalculator.Compute(Program.Multiply, 10, 20);
            Console.WriteLine("Result: {0}", result);
            Console.WriteLine();

            Console.WriteLine("--- explicitly instantiated the delegate object");
            ComputeHandler objD = new ComputeHandler(Program.Multiply);
            result = objCalculator.Compute(objD, 10, 20);
            Console.WriteLine("Result: {0}", result);
            Console.WriteLine();

            // NOTE: Anonymous method is ALWAYS MAPPED TO A DELEGATE
            //       Reason: The delegate DEFINES the signature of the ANONYMOUS METHOD
            Console.WriteLine("--- anonymous method example");
            ComputeHandler objD2 
                = delegate(int x, int y)
                {
                    return x * y;
                };
            result = objCalculator.Compute(objD2, 10, 20);
            Console.WriteLine("Result: {0}", result);
            Console.WriteLine();

            Console.WriteLine("--- anonymous method example (VERSION 2)");
            ComputeHandler objD3
                = (x, y) =>          // "GOES TO" operator
                  {
                        return x * y;
                  };
            result = objCalculator.Compute(objD3, 10, 20);
            Console.WriteLine("Result: {0}", result);
            Console.WriteLine();

            // Expression tree is built.  For example, the following expression is processed using BODMAS principle
            //  int result = 3 + 4 / 2 - 1 * 6;
            // This is achieved using the services of:
            //      System.Linq.Expressions.Expression
            //      System.Action
            //      System.Func

            Console.WriteLine("--- anonymous method example (LAMBDA VERSION)");
            ComputeHandler objD4
                = (x, y) => x * y;
            result = objCalculator.Compute(objD4, 10, 20);
            Console.WriteLine("Result: {0}", result);
            Console.WriteLine();

            // inline invocation to the anonymous method
            result = objCalculator.Compute( (x,y) => x * y, 10, 20 );

            // "z" is a local variable to the MAIN() method
            int z = 300;                                // <- SHARED variable
            ComputeHandler objD5 = (x, y) =>
            {
                z = 500;                               // <-- HOOK to the local shared variable
                return x * y + z;
            };
            objCalculator.Compute(objD5, 10, 20);

            // with the shared variable example
            result = objCalculator.Compute((x, y) => x * y + z, 10, 20);
        }

        private static int Multiply(int x, int y)
        {
            return x * y;
        }
    }
}
