using System.Collections;

namespace Demo_IEnumerable
{
    public class Company
        : System.Collections.IEnumerable
    {
        public string CompanyName { get; private set; }

        private System.Collections.ArrayList theEmployees { get; set; }


        public Company(string name)
        {
            CompanyName = name; 
            theEmployees = new System.Collections.ArrayList();
        }


        public void AddEmployee(Employee newEmployee)
        {
            theEmployees.Add(newEmployee);
        }


        public void DisplayInfo()
        {
            Console.WriteLine("Employees of {0}:", this.CompanyName.ToUpper());

            for(int i = 0; i < theEmployees.Count; i++)
            {
                Employee? emp = theEmployees[i] as Employee;
                if( emp is not null)
                {
                    Console.WriteLine("{0} {1} {2}", emp.Id, emp.Name, emp.Salary);
                }
            }
        }

        #region System.Collections.IEnumerable members

        public IEnumerator GetEnumerator()
        {
            for(int i = 0; i < theEmployees.Count; i++)
            {
                yield return theEmployees[i];
            }
            //foreach(Employee emp in theEmployees)
            //{
            //    yield return emp;
            //}
        }

        #endregion
    }
}
