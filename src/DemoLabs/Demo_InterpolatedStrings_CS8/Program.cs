using System;

namespace cs8_con_InterpolatedStrings_CS8
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "Manoj";

            // string message1 = $"C:\{name}";         // gives Error

            // -- C# 6.0 using string interpolation
            string message2 = $"C:\\{name}";        // old way 
            string message3 = $@"C:\{name}";        // old way

            // -- C# 8.0
            string message4 = @$"C:\{name}";       // now ALSO available in C# 8.0
        }
    }
}
