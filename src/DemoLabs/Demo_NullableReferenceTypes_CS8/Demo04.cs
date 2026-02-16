namespace Demo_NullableReferenceTypes;


class Demo04
{
    class Employee
    {
        public string Name { get; private set; }

        public string? Designation { get; set; }

        public Employee(string name)
        {
            this.Name = name;
        }
    }


    public static void Run()
    {
        Employee emp = new Employee("First Employee");
        
        // Console.WriteLine($"{emp.Name} [ {emp.Designation!.ToUpper()} ]");      // THROWS NullReferenceException

        Demo(emp.Designation);

        Console.WriteLine($"{emp.Name} [ {emp.Designation?.ToUpper()} ]");     // Use Null-Conditional operator to ignore if null  
    }


    // Example of how to Indicate that parameter is nullable.
    private static void Demo(string? designation)
    {
        if ( designation is not null )
        {
            Console.WriteLine( designation.ToUpper() );
        }
    }

}
