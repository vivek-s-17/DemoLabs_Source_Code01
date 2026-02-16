namespace Demo_Linq
{
    internal class Demo07_LinqParallel
    {
        public static void RunThis()
        {
            int[] arr = { 10, 20, 30, 40, 50 };

            Console.WriteLine("---- using ForEach");
            foreach (int i in arr)
            {
                Console.WriteLine($"element {i} on Thread {Thread.CurrentThread.ManagedThreadId}");
            }
            Console.WriteLine();

            Console.WriteLine("--- using Parallel.ForEach");
            Parallel.ForEach(arr, i =>
            {
                Console.WriteLine( $"element {i} on Thread {Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine();


            Console.WriteLine("--- using LINQ");
            var query = from i in arr
                        select i;
            foreach( var item in query)
            {
                Console.WriteLine($"element {item} on Thread {Thread.CurrentThread.ManagedThreadId}");
            }
            Console.WriteLine();

            // TPL = Task Parallel Library
            Console.WriteLine("--- using LINQ with TPL - EXTRACT elements in Parallel");
            var query2 = from i in arr.AsParallel()
                        select i;
            foreach (var item in query2)            // invoking .GetEnumerator()
            {
                Console.WriteLine($"element {item} on Thread {Thread.CurrentThread.ManagedThreadId}");
            }
            Console.WriteLine();

            Console.WriteLine("--- using LINQ with TPL - EXTRACT elements in Parallel & PROCESS Parallely");
            var query3 = from i in arr.AsParallel()
                         select i;
            Parallel.ForEach(query3, item =>
            {
                Console.WriteLine($"element {item} on Thread {Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine();
        }

    }
}
