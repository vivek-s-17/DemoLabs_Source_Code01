namespace Demo_Task;


// For More information:
// https://msdn.microsoft.com/en-us/library/dd537609(v=vs.110).aspx

class Demo5_DemoOfParallel
{
    public static void Run()
    {
        // DemoParallelForEach();

        // DemoParallelFor();

        Console.WriteLine("Demo running on {0}", Thread.CurrentThread.ManagedThreadId);
        Console.WriteLine();

        int[] arr = { 10, 20, 30 };
        Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Factory Thread running on: {0}", Thread.CurrentThread.ManagedThreadId);
            Parallel.ForEach<int>(arr, ndx => { 
                DoSomething(ndx); 
            });
        }).Wait();

        Console.WriteLine();
    }

    private static void DemoParallelFor()
    {
        int[] arr = { 10, 20, 30 };

        for (int ndx = 0; ndx < arr.Length; ndx++)
        {
            DoSomething(arr[ndx]);
        }
        Console.WriteLine();

        Parallel.For(fromInclusive: 0, toExclusive: arr.Length, ndx =>
        {
            DoSomething(arr[ndx]);
        });
        Console.WriteLine();
    }

    private static void DoSomething(int item)
    {
        Console.WriteLine("{0} on Thread: {1}", item, Thread.CurrentThread.ManagedThreadId );
    }


    private static void DemoParallelForEach()
    {
        int[] arr = { 10, 20, 30 };

        // Example 1
        foreach (int i in arr)
        {
            DoSomething(i);
        }
        Console.WriteLine();

        // Example 2
        //Console.WriteLine("example of foreach with Task.Factory");
        //foreach (var item in arr)
        //{
        //    Task.Factory.StartNew(() => DoSomething(item));
        //}
        //Console.WriteLine();

        // Example 3 (A)
        //Console.WriteLine("example of Parallel.ForEach");
        //System.Threading.Tasks.Parallel.ForEach(arr, ndx => DoSomething(ndx));
        //Console.WriteLine();        // line is executed only after all Parallel threads are awaited!


        //// Example 3 (B)
        // Parallel.ForEach( arr, DoSomething );
        // Console.WriteLine();
    }

}
