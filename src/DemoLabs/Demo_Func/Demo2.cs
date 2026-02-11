namespace Demo_Func
{
    class Demo2
    {
        public static void RunThis()
        {
            // delegate int AddHandler ( int a, int b );
            // AddHandler objD = new AddHandler( Demo2.Add );
            // AddHandler objD = Demo2.Add;
            Func<int, int, int> addTwoNumbers = Demo2.Add;

            Console.WriteLine("Sum of {0} and {1} = {2}", 10, 20, addTwoNumbers(10, 20));

            Func<int, int, int, int> addThreeNumbers
                = (a, b, c)
                => 
                {
                    return a + b + c;
                };
            Console.WriteLine("Result :" + addThreeNumbers(10, 20, 30));
        }

        private static int Add(int a, int b)
        {
            return a + b;
        }
    }
}
