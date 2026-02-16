namespace Demo_AsyncStreams;


class Demo02
{

    public static async Task RunAsync()
    {
        IAsyncEnumerator<int> enumerator = GetElementsAsync().GetAsyncEnumerator();
        try
        {
            while(await enumerator.MoveNextAsync())
            {
                int item = enumerator.Current;
                Console.Write($"{item} ");
            }
        }
        finally
        {
            await enumerator.DisposeAsync();
        }
    }


    static async IAsyncEnumerable<int> GetElementsAsync()
    {
        var numbers = Enumerable.Range(1, 20).ToArray();
        foreach(int i in numbers)
        {
            if (i % 5 == 0)
            {
                await Task.Delay(2000);
            }
            yield return i;
        }
    }

}
