namespace Demo_IComparable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Sorting an Array of Rank 1
            // Demo01();

            // Sorting an Array of Rank 2
            // Demo02();

            Employee[] arrEmployees = new Employee[3];
            arrEmployees[0] = new Employee() { Id = 30, Name = "First Employee", Salary = 300M };
            arrEmployees[1] = new Employee() { Id = 10, Name = "Second Employee", Salary = 100M };
            arrEmployees[2] = new Employee() { Id = 20, Name = "Third Employee", Salary = 200M };

            Console.WriteLine("-- array of employees BEFORE sorting");
            foreach (Employee emp in arrEmployees)
            {
                Console.WriteLine("{0} {1, -20} {2,15:C}", emp.Id, emp.Name, emp.Salary);
            }

            arrEmployees.Sort();        // internally calls .CompareTo() for every object in the array

            Console.WriteLine("-- array of employees AFTER sorting");
            foreach (Employee emp in arrEmployees)
            {
                Console.WriteLine("{0} {1, -20} {2,15:C}", emp.Id, emp.Name, emp.Salary);
            }

            Console.WriteLine("-- array of employees AFTER sorting on NAME");
            Employee.SortOn = Employee.SortOnFields.Name;
            arrEmployees.Sort();
            foreach (Employee emp in arrEmployees)
            {
                Console.WriteLine("{0} {1, -20} {2,15:C}", emp.Id, emp.Name, emp.Salary);
            }

        }

        private static void Demo02()
        {
            int[,] arr =
            {
                { 2, 3, 5 },
                { 7, 1, 4 }
            };

            // arr.Sort();          // Does not know to sort Horizontally, or Vertically!
        }

        private static void Demo01()
        {
            int[] arr = { 99, 45, 55, 50, 9, 70, 21, 30, 13 };

            Console.WriteLine("-- using FOR LOOP");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write("{0} ", arr[i]);
            }
            Console.WriteLine();

            Console.WriteLine("-- using FOREACH LOOP");
            foreach (int i in arr)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            Console.WriteLine();

            arr.Sort();
            Console.WriteLine("-- after SORTING");
            foreach (int i in arr)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
        }
    }
}
