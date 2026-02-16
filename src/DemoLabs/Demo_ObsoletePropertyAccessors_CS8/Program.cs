using System;

namespace cs8_con_ObsoletePropertyAccessors
{
    class Program
    {
        static void Main(string[] args)
        {
            // Demo();
        }

        [Obsolete]
        static void Demo()
        {
            Class1 obj = new Class1() { ID = 5 };
        }
    }
}
