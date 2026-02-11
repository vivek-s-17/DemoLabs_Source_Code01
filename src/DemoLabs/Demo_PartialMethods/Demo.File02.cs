namespace Demo_PartialMethods
{
    // Code in File02
    internal partial class Demo
    {
        private int x;

        // Definition of the Partial Method
        partial void DoSomething();
        partial void DoSomethingElse(int x);


        internal void n()
        {
            x = 50;

            Console.WriteLine("n() called from File02");
            DoSomething();
            DoSomethingElse(10);

            Console.WriteLine("x = {0}", x);

            // m();  // inside File01
        }

    }
}
