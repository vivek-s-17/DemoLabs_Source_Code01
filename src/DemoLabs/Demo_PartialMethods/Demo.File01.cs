namespace Demo_PartialMethods
{
    // Code in File01
    internal partial class Demo
    {

        // Implementation of the Partial Method
        partial void DoSomething()
        {
            x++;
            Console.WriteLine("x is incremented");

            Console.WriteLine("DoSomething() called");
        }

        internal void m()
        {
            Console.WriteLine("m() called from File01");
            DoSomething();
        }
    }
}
