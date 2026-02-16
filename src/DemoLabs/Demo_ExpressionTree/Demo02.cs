namespace Demo_ExpressionTree;


internal static class Demo02
{

    delegate int AddHandler ( int x, int y, int z );

    internal static void RunThis ()
    {
        int a = 10, b = 20, c = 30, result;

        Console.WriteLine( "--- DELEGATE VERSION" );
        AddHandler objDelegate = new AddHandler( Demo02.Add );
        result = objDelegate( a, b, c );
        Console.WriteLine( $"Result: {a} + {b} + {c} = {result}" );
        Console.WriteLine();

        Console.WriteLine( "--- LAMBDA EXPRESSION VERSION" );
        AddHandler objD = new AddHandler( (x, y, z) => x + y + z );
        result = objD( 100, 200, 300 );
        Console.WriteLine( $"Result: {a} + {b} + {c} = {result}" );
        Console.WriteLine();


        AddHandler objD2 = (x, y, z) => x + y + z;
        result = objD2(1000, 2000, 3000);
    }


    static int Add(int x, int y, int z )
    {
        return x + y + z;
    }

}
