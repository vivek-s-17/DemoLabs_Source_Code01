namespace Demo_PartialMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TO RUN THIS DEMO:
            // "Exclude File01 from Project"

            // Partial Methods:
            // - has to have the CODE IMPLEMENTED only inside PARTIAL CLASS
            // - has to be DEFINED in the other files of the PARTIAL CLASS
            // - are always PRIVATE members
            // - cannot return any value


            Demo obj = new Demo();
            obj.n();
            obj.p();
        }
    }
}
