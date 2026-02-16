namespace Demo_ParameterTypes
{
    internal class Program
    {
        static void Main ( string[] args )
        {
            int a;

            a = 10;
            PassByValue( a );
            Console.WriteLine( "Value of A = {0}", a );
            Console.WriteLine();                                // 10

            a = 10;
            PassByReference( ref a );
            Console.WriteLine( "Value of A = {0}", a );
            Console.WriteLine();                                // 

            a = 10;
            PassByOutput( out a );
            Console.WriteLine( "Value of A = {0}", a );
            Console.WriteLine();                                // 

            Demo1.RunThis();
        }


        static void PassByValue(int i )
        {
            Console.WriteLine( "Received: {0}", i );
            i = 50;
            Console.WriteLine( "Changed: {0}", i );
        }


        static void PassByReference ( ref int i )
        {
            Console.WriteLine( "Received: {0}", i );
            i = 50;
            Console.WriteLine( "Changed: {0}", i );
        }

        static void PassByOutput ( out int i )
        {
            // Console.WriteLine( "Received: {0}", i );
            i = 50;
            Console.WriteLine( "Changed: {0}", i );
        }
    }
}
