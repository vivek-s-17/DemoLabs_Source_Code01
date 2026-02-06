using System.Collections;

namespace Demo_IEnumerable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Company objCompany = new Company("Microsoft");

            Employee empFirst = new Employee()
                { Id = 101, Name = "First Employee", Salary = 1000M };
            Employee empSecond = new Employee()
                { Id = 102, Name = "Second Employee", Salary = 170.85M };
            Employee empThird = new Employee()
                { Id = 103, Name = "Third Employee", Salary = 2050.50M };

            objCompany.AddEmployee(empFirst);
            objCompany.AddEmployee(empSecond);
            objCompany.AddEmployee(empThird);

            objCompany.DisplayInfo();
            Console.WriteLine();

            foreach(Employee emp in objCompany)     // implicitly calls the GetEnumerator() method
            {
                // emp = new Employee();            // READ-ONLY FORWARD-ONLY

                Console.WriteLine("{0,2} {1,-30} {2,15:C}", emp.Id, emp.Name, emp.Salary);
            }

            Console.WriteLine();

            // ONLY FOR REFERENCE
            // IEnumerator obj = objCompany.GetEnumerator();
            // obj.Reset();
            // while(obj.MoveNext())
            // {
            //    Employee? emp = obj.Current as Employee;
            //    if( emp is not null)
            //    {
            //        Console.WriteLine("{0,2} {1,-30} {2,15:C}", emp.Id, emp.Name, emp.Salary);
            //    }
            // }

        }
    }
}
