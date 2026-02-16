namespace Demo_ParameterTypes
{
    internal class Demo1
    {
        public static void RunThis()
        {
            Console.WriteLine( "Hello, World!" );

            int result;

            // inline comment
            // inline comment line 2

            /*
             * multi-line block comment
             */

            result = Add( 10, 20 );                                     // DEFAULT: positional parameters
            Console.WriteLine( "1. Result : {0}", result );

            result = Add( b: 10, a: 20, c: 30 );                        // named parameters
            Console.WriteLine( "2. Result : {0}", result );

            result = Add();
            Console.WriteLine( "3. Result : {0}", result );

            result = Add( 10 );

            result = Add( 10, 20, 30, 40, 50 );
            Console.WriteLine( "4. Result : {0}", result );


            //int[] arrExample = new int[5] { 10, 20, 30, 40, 50 };
            // int[] arrExample = { 10, 20, 30, 40, 50 };
            // var arrExample = new[] { 10, 20, 30, 40, 50 };  // C# 3: Type Inference
            int[] arrExample = [10, 20, 30, 40, 50];        // C# 12: Collection Initialization
            result = Add( arrExample );
            Console.WriteLine( "5. Result: {0}", result );
        }


        /// <summary>
        ///     Add two numbers
        /// </summary>
        /// <param name="a">first number</param>
        /// <param name="b">second number</param>
        /// <returns>result of add</returns>
        static int Add ( int a, int b )
        {
            return a + b;
        }

        /// <summary>
        ///     Add three numbers
        /// </summary>
        /// <param name="a">first number</param>
        /// <param name="b">second number</param>
        /// <param name="c">second number</param>
        /// <returns>result of add</returns>
        static int Add ( int a, int b, int c )
        {

            return a + b + c;
        }


        /// <summary>
        ///     Optional Parameter version.
        /// </summary>
        /// <param name="a">with a default value</param>
        /// <returns></returns>
        static int Add ( int a = 10 )
        {
            Console.WriteLine( "Optional Parameter version was called" );
            return a;
        }

        /// <summary>
        ///     Adds any number of numbers
        /// </summary>
        /// <remarks>
        ///      NOTE:
        ///      - Received as a Parameter Collection of Integers
        ///      - Will not be NULL
        ///      - May have 0 or more elements
        /// </remarks>
        /// <param name="arr"></param>
        /// <returns>Sum of all the numbers</returns>
        static int Add ( params int[] arr )
        {
            int result = 0;
            for ( int i = 0 ; i < arr.Length ; i++ )
            {
                result += arr[i];
                arr[i] = 0;
            }

            return result;
        }
    }
}
