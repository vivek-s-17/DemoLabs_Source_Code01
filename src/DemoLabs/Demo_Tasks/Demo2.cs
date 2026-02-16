namespace Demo_Task;

/// <summary>
///     Using Task.Run introduced in .NET FW 4.5
/// </summary>
class Demo2
{

    public static void Run()
    {
        // Synchronous Version
        PrintMessage();
        Console.WriteLine();

        // Calling the method using a Task instantiated directly (defined within the parent)
        // Task t = new Task(Demo2.PrintMessage);
        // t.Start();
        // t.Wait();                               // this is to be done explicitly!
        // Console.WriteLine();

        // Asynchronous Version (using async/await)   (now defined inside a method)
        Task t1 = Demo2.PrintMessageAsyncWithAwait();

        // Asynchronous Version (using async)   (now defined inside a method)
        Task t2 = Demo2.PrintMessageOnlyAsync();

        Console.WriteLine( "--- completed!" );
        t1.Wait();                                    // t1 is awaited here NOW!!! 
    }


    #region demo of PrintMessage()

    // Asynchronous Version (using async/await)
    private async static Task PrintMessageAsyncWithAwait()
    {
        // --- version 2 (with AWAIT)
        await Task.Run(() => Demo2.PrintMessage());         // t1 is waited outside the method!
    }

    // Asynchronous Version (using async/await)
    private async static Task PrintMessageOnlyAsync()
    {
        // --- version 1
        // Task t1 = Task.Run(() => Demo2.PrintMessage() );
        // t1.Wait();                          // t1 is waiting here!
    }


    // Synchronous Version
    private static void PrintMessage()
    {
        Console.WriteLine( "-- PrintMessage() going to sleep on Thread: {0}",
            System.Threading.Thread.CurrentThread.ManagedThreadId );

        // long running work code goes here
        Thread.Sleep( 5000 );
        Console.WriteLine("Hello from Task library!");

        Console.WriteLine( "-- PrintMessage() woke up on Thread: {0}",
            System.Threading.Thread.CurrentThread.ManagedThreadId );
    }


    #endregion

}
