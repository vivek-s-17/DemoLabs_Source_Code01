
using System.Xml.Linq;

namespace Demo_Linq2Xml
{
    internal class Demo01
    {
        internal static void RunThis()
        {
            SaveXml();
            ReadXml();
        }

        private static void SaveXml ()
        {
            XElement xml = new XElement( "Customers",
                        new XElement( "Customer",
                            new XAttribute( "CustomerID", "1" ),
                            new XElement( "FirstName", "Manoj" ),
                            new XElement( "LastName", "Sharma" )
                        ),
                        new XElement( "Customer",
                            new XAttribute( "CustomerID", "2" ),
                            new XElement( "FirstName", "Kumar" ),
                            new XElement( "LastName", "Sachdev" )
                        )
                    );

            Console.WriteLine( "-- The xml content" );
            Console.WriteLine( xml );
            Console.WriteLine();

            // xml.Save( "C:\\Temp\\Linq2Xml.xml" );
            xml.Save(@"C:\Temp\Linq2Xml.xml");
            Console.WriteLine( "Saved the XML file..." );
            Console.WriteLine();
        }

        private static void ReadXml ()
        {
            // extracts only the names of the winners from the file
            var result = from e in XElement.Load( @"C:\Temp\Linq2Xml.xml" ).Elements( "Customer" )
                         orderby (string) e.Element( "FirstName" )
                         select new
                         {
                             ID = (int) e.Attribute( "CustomerID" ),
                             FName = (string) e.Element( "FirstName" ),
                             LName = (string) e.Element( "LastName" )
                         };

            // creates a sequence of distinct names
            //var result2 = Enumerable.Distinct(result);

            Console.WriteLine( "-- The data" );
            foreach ( var cust in result )
            {
                Console.WriteLine( "{0,3} {1,-10} {2,-10}",
                    cust.ID, cust.FName, cust.LName );
            }
        }

    }
}
