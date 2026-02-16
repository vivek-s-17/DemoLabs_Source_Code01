using System;
using System.Globalization;

namespace Demo_StringFormatting
{
    class Program
    {
        static void Main(string[] args)
        {
            bool b = true;
            string name = "Manoj Kumar Sharma";
            int i = 10;
            double cost = 50.85;
            decimal salary = 5000.75M;          // same as = (decimal) 5000.75;

            Console.WriteLine("bool : {0}", b);
            Console.WriteLine( "salary: {1}, name: {0}", name, salary );    // implicitly invoke string.Format();
            Console.WriteLine("name: {0}, salary: {1}", name, salary);      // implicitly invoke string.Format();

            string output = string.Format( "name: {0}, salary {1}", name, salary );
            Console.WriteLine(output);


            string message;
            message = "name: " + name + ", salary: " + salary;          // .ToString();
            Console.WriteLine("after CONCATENATION: {0}", message);
            message = string.Format("name: {0}, salary: {1}", name, salary);
            Console.WriteLine("after String.Format(): {0}", message);

            message = string.Format("name: {0,30}, salary: {1:C}", name, salary);
            Console.WriteLine("after String.Format(): {0}", message);
            message = string.Format( "name: {0,-30}, salary: {1:C}", name, salary );
            Console.WriteLine( "after String.Format(): {0}", message );

            Console.WriteLine();

            System.DateTime dt = System.DateTime.Now;
            Console.WriteLine("LongDateString: {0}", dt.ToLongDateString());
            Console.WriteLine("ShortDateString: {0}", dt.ToShortDateString());
            Console.WriteLine("{0:dd-MMM-yyyy}", dt);
            message = string.Format("{0:dd-MMM-yyyy}", dt);
            Console.WriteLine(message);
            Console.WriteLine(string.Format("{0:dd-MMM-yyyy}", dt));

            // C# > 10
            // - String Interpolation
            string x = "hello world";

            // 10 hello world     5,430
            // 20 another world      30
            Console.WriteLine( "{0} {1, -20} $ {2, 10:#,##0.00}", 10, "Hello world", 5430 );
            Console.WriteLine( "{0} {1, -20} $ {2, 10:F}", 20, "another world", 30 );
            Console.WriteLine( $"{10} {"Hello world".ToUpper(),-20} $ {5430,10:#,##0.00}" );
            Console.WriteLine();

            // $"{.30:0.00}"                        =>  "0.30"
            // $"{1.30:000.00}"                     => "001.30"
            // $"{1.30:##0.00}"                     => "  1.30"
            // $"{1.30:##0.00}.Trim()"              => "  1.30.Trim()"
            // $"{1.30:##0.00}".Trim()              => "1.30"
            // $"{ ( $"{1.30:##0.00}" ).Trim() }"   => "1.30"


            int y = 50;
            Console.WriteLine("x = {0}", x);
            Console.WriteLine($"x = {x}");      // "x = " + x.ToString() // string.Format("x = {x}")
            Console.WriteLine($"x = {x}, y = {y}");
            Console.WriteLine($"x = {x, -30}, y = {y:C}");
            Console.WriteLine($"{dt:dd-MMM-yyyy hh:mm:ss tt}");
            Console.WriteLine();


			// String Interpolation
            Console.WriteLine( "name: {0}, salary: {1}", name, salary );   // implicitly invoke string.Format();
            Console.WriteLine( $"name: {name}, salary: {salary}");         // string interpolation
            Console.WriteLine( $"name: {name.ToUpper()}, salary: {salary:C}" );


			// Create a CultureInfo object for the UK (en-GB)
        	System.Globalization.CultureInfo ukCulture = new CultureInfo("en-GB");

	        // Format the number as currency with the UK culture
    	    string formattedAmount = salary.ToString("C", ukCulture);
			Console.WriteLine($"Formatted amount: {formattedAmount}");
            Console.WriteLine();

            DateTime dt2 = DateTime.Now;
            Console.WriteLine("culture version: {0}",
                dt2.ToString("dd-MMMM-yyyy", new CultureInfo("hi-IN") ) );
            Console.WriteLine();

			// Using CultureInfo in String Interpolation
			Console.WriteLine( $"name: {name.ToUpper()}, salary: {salary.ToString("C", ukCulture)}" );
            Console.WriteLine();


			// Using FormattableString
			formattedAmount
                = FormattableString.Invariant( $"{salary.ToString("C", ukCulture)}" );
			Console.WriteLine( $"name: {name.ToUpper()}, salary: {formattedAmount}" );
            Console.WriteLine();

        }
    }
}

