using System;

namespace cs8_con_Patterns
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 10;
            Console.WriteLine(Demo01(i));        // Unknown Point
            Console.WriteLine();

            Point p;
            p = new Point(0, 0);
            Console.WriteLine(Demo01(p));          // Point of Origin
            p = new Point(5, 2);
            Console.WriteLine(Demo01(p));          // Point (5, 2)
            Console.WriteLine();

            Console.WriteLine(Demo02(p));          // Point (5, 2)
            Console.WriteLine();

            Console.WriteLine(Demo03(p.X, p.Y));   // Point (5, 2)
            Console.WriteLine();

            p = new Point(0, 0);
            Console.WriteLine(Demo04(p));           // Point of Origin
            Console.WriteLine();

            Console.WriteLine(Demo05(p));           // Point of Origin
            p = new Point(5, 5);
            Console.WriteLine(Demo05(p));           // Perfect Point
            Console.WriteLine();

        }

        // Demo of SWITCH...CASE...WHEN - C# 7.0 feature 
        static string Demo01(object o)
        {
            switch (o)
            {
                case Point p when p.X == 0 && p.Y == 0:
                    return "Point of Origin";
                case Point p:
                    return $"Point ( {p.X}, {p.Y} )";
                default:
                    return "Unknown Point";
            }
        }

        // Demo of Switch Expression - C# 8.0 feature
        static string Demo02(object o)
        {
            return o switch
            {
                Point p when p.X == 0 && p.Y == 0       => "Point of Origin",
                Point p                                 => $"Point ( {p.X}, {p.Y} )",
                _                                       => "unknown"
            };
        }

        // Demo of Switch Expression using Tuple - C# 8.0 feature
        static string Demo03(int x, int y)
        {
            return (x, y) switch
            {
                (0, 0)  => "Point of Origin",
                _       => $"Point ( {x}, {y} )"        // Discard Pattern - C# 8.0 feature
            };
        }

        // Demo of Switch Expression using Pattern Matching - C# 8.0 feature
        static string Demo04(Point p)
        {
            return p switch
            {
                { X:0, Y:0 } => $"Point of Origin",
                _            => $"Point ( {p.X}, {p.Y} )"        // Discard Pattern - C# 8.0 feature
            };
        }


        // Demo of Switch Expression using Type and Pattern Matching - C# 8.0 feature
        static string Demo05(object o)
        {
            // Earlier:
            var obj = o as Point;
            if(obj != null)
            {
                Console.WriteLine($"Receipt Point ( {obj.X}, {obj.Y} )");
            }

            // Pattern Matching Feature -- C# 7.0 feature
            if (o is Point f)
            {
                Console.WriteLine($"Receipt Point ( {f.X}, {f.Y} )");
            }

            return o switch
            {
                Point p when p.X == 0 && p.Y == 0   => "Point of Origin",
                Point { X: 5, Y: 5 }                => "Perfect Point",
                Point p                             => $"Point ( {p.X}, {p.Y} )",
                _                                   => "unknown"                    // Discard Pattern
            };
        }
    }
}
