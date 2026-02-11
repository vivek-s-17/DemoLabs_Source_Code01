namespace Demo_PartialMethods
{
    // Code in File03
    internal partial class Demo
    {
        // NOTE: Prototype is declared in File02
        //       So, not needed to be redeclared
        // partial void DoSomething();
        
        public void p()
        {
            Console.WriteLine("p() called from FILE03");
            DoSomething();
        }
    }
}
