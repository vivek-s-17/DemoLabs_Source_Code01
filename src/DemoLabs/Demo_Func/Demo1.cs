namespace Demo_Func
{
    // what you can do with DELEGATES but cannot do with ACTION or FUNC
    delegate void MyHandler1(int a, int b);            // mapped to Action<int, int>           - POSSIBLE
    delegate string MyHandler2(int a, int b);          // mapped to Fun<int, int, string>     - POSSIBLE

    delegate void MyHandler3(int a, out int b);        // mapped to Action<int a, out int b>      - NOT POSSIBLE
    delegate string MyHandler4(int a, out int b);      // mapped to Fun<int, out int, string>   - NOT POSSIBLE 

    class Demo1
    {
        // delegate int RandomHandler();
        // Func<int> getRandomNumber;                // same as above

        // delegate int DoSomething(string message);
        // Func<string, int> DoSomething;

        public static void RunThis()
        {
            // Func<int> getRandomNumber = delegate () { ... return int }
            Func<int> getRandomNumber = () =>
            {
                System.Random randomizer = new Random();
                System.Threading.Thread.Sleep(500);                 // 1000 millisecond = 1 second
                return randomizer.Next(minValue: 1, maxValue: 10);
            };

            Console.WriteLine("Random Number between 1 and 10: {0} ", getRandomNumber());
            Console.WriteLine("Random Number between 1 and 10: {0} ", getRandomNumber());
            Console.WriteLine("Random Number between 1 and 10: {0} ", getRandomNumber());
            Console.WriteLine("Random Number between 1 and 10: {0} ", getRandomNumber());
            Console.WriteLine("Random Number between 1 and 10: {0} ", getRandomNumber());
            Console.WriteLine("Random Number between 1 and 10: {0} ", getRandomNumber());
            Console.WriteLine("Random Number between 1 and 10: {0} ", getRandomNumber());
            Console.WriteLine("Random Number between 1 and 10: {0} ", getRandomNumber());

        }

    }
}
