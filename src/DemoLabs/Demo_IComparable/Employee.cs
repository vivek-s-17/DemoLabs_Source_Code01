using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_IComparable
{
    public class Employee
        : System.IComparable
    {
        
        public enum SortOnFields
        {
            Name,
            Id
        };



        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Salary { get; set; }

        public static SortOnFields SortOn { get; set; } = SortOnFields.Id;

        #region System.IComparable members

        public int CompareTo(object? obj)
        {
            Employee? objOther = obj as Employee;
            if (objOther is not null)
            {
                switch(Employee.SortOn)
                {
                    default:
                    case SortOnFields.Id:
                        // return objOther.Id.CompareTo(this.Id);          // descending order on ID
                        return this.Id.CompareTo(objOther.Id);       // ascending order on ID
                    case SortOnFields.Name:
                        return this.Name.CompareTo(objOther.Name);
                }
            }

            return 0;
        }

        #endregion
    }
}
