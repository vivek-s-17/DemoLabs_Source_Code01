namespace Demo_Task;

/// <summary>
///     Using Task.Run introduced in .NET FW 4.5
/// </summary>
class Demo3
{

    public static void Run()
    {
        // Asynchronous Version (using async/await)
        Task<bool>? result = Demo3.AddAsync(10, 20);

        Console.WriteLine("this line will not wait for the result to be received!");

        bool isOk = result.Result;                  // the task would AWAITed here!

        // --- all the below lines are running SYNCHRONOUSLY

        if (isOk)
        {
            Console.WriteLine("Both numbers are positive!");
        }

        Console.WriteLine( "--- completed!" );
    }


    private async static Task<bool> AddAsync(int a, int b)
    {
        return await Task.Run(() => Add(a, b));
    }


    private static bool Add ( int a, int b )
    {
        System.Threading.Thread.Sleep(2000);
        return (a > 0 && b > 0);
    }

}
