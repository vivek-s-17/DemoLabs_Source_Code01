using System.Xml.Linq;

namespace Demo_Linq2Xml
{
    class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public int Age { get; set; }
    }


    internal class Demo02
    {
        internal static void RunThis ()
        {
            List<Employee> employees = new List<Employee>()
            { 
                new Employee { EmployeeID=10, EmployeeName="First Employee", Age=45 },
                new Employee { EmployeeID=20, EmployeeName="Second Employee", Age=18 },
                new Employee { EmployeeID=30, EmployeeName="Third Employee", Age=24 } 
            };

            // One way of generating the XML
            XElement root = new XElement( "Employees" );
            foreach ( Employee emp in employees )
            {
                root.Add( new XElement( "Employee",
                                new XAttribute( "ID", emp.EmployeeID ),
                                new XElement( "Name", emp.EmployeeName ),
                                new XElement( "Age", emp.Age ) ) );
            }

            root.Save( @"c:\temp\linq2employees.xml" );

            Console.WriteLine( root.ToString() );
            Console.WriteLine();


            // The easier LINQ way of generating XML
            XElement root2 = new XElement( "Employees",
                                from emp in employees
                                select new XElement( "Employee",
                                   new XAttribute( "ID", emp.EmployeeID ),
                                   new XElement( "Name", emp.EmployeeName ),
                                   new XElement( "Age", emp.Age ) ) );

            root2.Save( @"c:\temp\linq2employees2.xml" );

            Console.WriteLine( root2.ToString() );
            Console.WriteLine();
        }
    }
}
