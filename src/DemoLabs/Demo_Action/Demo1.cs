namespace Demo_Action
{
    class Demo1
    {
        delegate void DoSomethingHandler(int i);

        public static void RunThis()
        {
            // Direct call
            DoSomething(10);

            // Calling through a delegate
            DoSomethingHandler objD = new DoSomethingHandler(Demo1.DoSomething);
            objD(20);

            // Calling through the delegate using an Anonymous Method
            DoSomethingHandler objD2 = 
                (i) => Console.WriteLine($"hello received: {i}");
            objD2(10);

            // NOTE: all of the above need the delegate to be defined!

            // Action defines the delegate and its implementation
            Action objD3 = 
                () => Console.WriteLine("method of the delegate with NO Arguments");
            objD3();            // SAME AS: objD3.Invoke();

            Action<int> objD4 = 
                (i) => Console.WriteLine("method of the delegate with INT Argument");
            objD4(10);

            // multi-cast delegate
            objD3 += () => Console.WriteLine("second method subscribed to the same NO Argument delegate");
            objD3.Invoke();
        }

        static void DoSomething(int i)
        {
            Console.WriteLine($"do something called with {i}");
        }
    }
}
