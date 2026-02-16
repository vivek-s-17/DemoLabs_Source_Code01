namespace Demo_Task;


class Demo4
{
    public static void Run()
    {
        int i = 10;
        string s = ("hello world " + i).ToUpper().Replace("HELLO", "DEMO");     // CHAINED CALLS


        Task task1 = Task.Factory.StartNew(Demo4.DoSomething);
        Task task2 = Task.Factory.StartNew(Demo4.DoSomething);
        Task task3 = Task.Factory.StartNew(Demo4.DoSomething);

        // Wait for tasks to finish
        Task.WaitAll(task1, task2, task3);
        Console.WriteLine();

        // example of Fluid Code invocations / Chained Calls.
        // Execute another task when current task is done
        Task t = Task.Factory
            .StartNew(Demo4.DoSomething)
            .ContinueWith((t) =>
            {
                Console.WriteLine("Hello Task library continued! - running on Thread: {0}!",
                   Thread.CurrentThread.ManagedThreadId);
            })
            .ContinueWith((t) =>
            {
                Console.WriteLine("Hello Task library continued again! - running on Thread: {0}!",
                    Thread.CurrentThread.ManagedThreadId);
            });

        //---- async code which will get executed while Factory is executing

        t.Wait();            // needed otherwise chained calls might be invoked after exiting MAIN!

        
    }


    private static void DoSomething()
    {
        Console.WriteLine("Hello Task library - running on Thread: {0}!", 
            Thread.CurrentThread.ManagedThreadId);
    }

}
