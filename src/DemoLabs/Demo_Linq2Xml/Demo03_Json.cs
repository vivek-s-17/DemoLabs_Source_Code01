namespace Demo_Linq2Xml
{
    internal class Demo03_Json
    {
        internal static void RunThis()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee { EmployeeID=10, EmployeeName="First Employee", Age=45 },
                new Employee { EmployeeID=20, EmployeeName="Second Employee", Age=18 },
                new Employee { EmployeeID=30, EmployeeName="Third Employee", Age=24 }
            };

            //      \  /
            string jsonString = System.Text.Json.JsonSerializer.Serialize(employees);
            Console.WriteLine(jsonString);

            var employees2 = System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(jsonString);
            if (employees2 is not null)
            {
                foreach (Employee employee in employees2)
                {
                    Console.WriteLine(employee.EmployeeName);
                }
            }

        }

    }
}
