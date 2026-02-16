namespace Demo_AsyncStreams;

class Demo01
{
    public static async Task RunAsync()
    {
        await foreach (var item in GetElementsAsync())
        {
            Console.Write($"{item} ");
        }
    }


    static async IAsyncEnumerable<int> GetElementsAsync()
    {
        for(int i = 0; i < 20; i++)
        {
            if (i % 5 == 0)
            {
                await Task.Delay(2000);
            }
            yield return i;
        }
    }

}
