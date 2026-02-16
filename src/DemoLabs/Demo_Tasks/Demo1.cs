namespace Demo_Task;

class Demo1
{

    private static void PrintMessage ()
    {
        System.Threading.Thread.Sleep( 2000 );
        Console.WriteLine( "Hello from Task library! - on Thread {0}",
                System.Threading.Thread.CurrentThread.ManagedThreadId );
    }


    public static void Run()
    {
        Console.WriteLine( "---Synchronous Version" );
        PrintMessage();

        Console.WriteLine( "---Using ThreadStart Delegate being instantiated explicitly." );
        ThreadStart objD1 = new ThreadStart( Demo1.PrintMessage );
        Thread thread1 = new Thread( objD1 );
        thread1.Start();
        thread1.Join();
        Console.WriteLine();

        Console.WriteLine( "---Using ThreadStart Delegate instantiated in-line." );
        Thread thread2 = new Thread( new ThreadStart(Demo1.PrintMessage) );
        thread2.Start();
        thread2.Join();
        Console.WriteLine();

        Console.WriteLine( "---Using ThreadStart Delegate instantiated implicitly." );
        Thread thread3 = new Thread( Demo1.PrintMessage );
        thread3.Start();
        thread3.Join();
        Console.WriteLine();

        // ----------------------------------

        Console.WriteLine( "---Using TASK with Delegate (ANONYMOUS METHOD VERSION)." );
        Task task1 = new Task( delegate { Demo1.PrintMessage(); } );
        task1.Start();

        Console.WriteLine( "---using Task with Action explicitly instantiated" );
        Action objD2 = new Action(Demo1.PrintMessage);
        Task task2 = new Task(objD2);
        task2.Start();
        task2.Wait();
        Console.WriteLine();

        Console.WriteLine( "---Using Task with Action explicitly instantiated in-line" );
        Task task3 = new Task(new Action(Demo1.PrintMessage));
        task3.Start();
        Console.WriteLine();

        Console.WriteLine( "---Using task with Action implicitly instantiated" );
        Task task4 = new Task( Demo1.PrintMessage );
        task4.Start();

        Console.WriteLine( "--- waiting for tasks to complete" );
        //task3.Wait();
        //task4.Wait();
        Task.WaitAll(task3, task4);
        Console.WriteLine();

        Console.WriteLine( "---Using Task Factory" );
        Task task5 = Task.Factory.StartNew( () => Demo1.PrintMessage() );
        Console.WriteLine();

        Console.WriteLine( "--- Using Task.Run" );
        Task task6 = Task.Run( () => Demo1.PrintMessage() );
        Console.WriteLine();

        // wait for all the sub-tasks to complete
        // task5.Wait();
        // task6.Wait();
        Task.WaitAll( task5, task6 );   
        
    }

}
