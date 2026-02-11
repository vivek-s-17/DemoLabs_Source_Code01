namespace Demo_AnonymousType
{
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Demo01();

            // ------------------------------------------------

            // ANONYMOUS TYPES are inferred on Assignment
            // - to provide immutable readonly object
            // NOTE:
            //  (a) new anonymous type is created if order changes
            //  (b) new anonymous type is created if property name changes (including when case-changes)
            //  (c) cannot inherit
            //  (d) cannot apply interface(s)
            //  (e) cannot have any methods (including constructor)
            //  (f) all properties are by default always "public" and instance properties
            //  (g) cannot define datatype for the property - it is always inferred on assignment
            var v1 = new { Id = 1, Name = "something" };
            var v2 = new { Id = 2, Name = "another thing" };
            var v3 = new { Name = "different thing", Id = 3 };
            var v4 = new { name = "different thing", Id = 3 };          // changed the "Name" to "name"
            Console.WriteLine("{0}: ID={1} NAME={2}", v1.GetType(), v1.Id, v1.Name);
            Console.WriteLine("{0}: ID={1} NAME={2}", v2.GetType(), v2.Id, v2.Name);
            Console.WriteLine("{0}: ID={1} NAME={2}", v3.GetType(), v3.Id, v3.Name);
            Console.WriteLine("{0}: ID={1} NAME={2}", v4.GetType(), v4.Id, v4.name);
            Console.WriteLine();

            var e1 = new Employee { Id = 1, Name = "something" };
            var e2 = new Employee { Id = 2, Name = "another thing" };
            var e3 = new Employee { Name = "different thing", Id = 3 };
            Console.WriteLine("{0}: ID={1} NAME={2}", e1.GetType(), e1.Id, e1.Name);
            Console.WriteLine("{0}: ID={1} NAME={2}", e2.GetType(), e2.Id, e2.Name);
            Console.WriteLine("{0}: ID={1} NAME={2}", e3.GetType(), e3.Id, e3.Name);
            Console.WriteLine();
        }

        static void Demo01()
        {
            // emp1 is an object of type "Demo_AnonymousType.Employee"
            Employee emp1 = new Employee() { Id = 1, Name = "First Employee" };
            Console.WriteLine("{0}: ID={1} NAME={2}",
                emp1.GetType(), emp1.Id, emp1.Name);

            // emp2 is an object of type "Demo_AnonymousType.Employee"
            var emp2 = new Employee() { Id = 2, Name = "Second Employee" };
            Console.WriteLine("{0}: ID={1} NAME={2}",
                emp2.GetType(), emp2.Id, emp2.Name);
            emp2.Name = emp2.Name.ToUpper();
            Console.WriteLine("{0}: ID={1} NAME={2}",
                emp2.GetType(), emp2.Id, emp2.Name);


            var empX = new Employee();
            empX.Id = 10;
            empX.Name = "something";

            Console.WriteLine();

            // ANONYMOUS TYPES are inferred on Assignment
            // - to provide immutable readonly object

            // emp3 is an object of AnonymousType[Int32,String] 
            var emp3 = new { Id = 100, Name = "Employee 100" };
            Console.WriteLine("{0}: ID={1} NAME={2}",
                emp3.GetType(), emp3.Id, emp3.Name);
            // emp3.Name = emp3.Name.ToUpper();       => all properties of ANONYMOUS TYPE are READONLY

            var empY = new { };
            // var empZ = null;
            // var empZ = typeof(emp3);
            Type empZ = emp3.GetType();
            var empZb = emp3.GetType();
        }
    }
}
