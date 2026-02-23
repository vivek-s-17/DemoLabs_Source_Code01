namespace Demo_RecordType_C7.Demo05;

abstract class MyBaseClass
{
    public string firstName;
    public MyBaseClass(string firstname)
    {
        this.firstName = firstname; 
    }
}

class MyDerivedClass : MyBaseClass
{
    public string lastName;

    public MyDerivedClass(string firstname, string lastName) : base(firstname)
    {
        this.lastName = lastName;
    }
}



internal abstract record Person( string FirstName, string LastName );

internal record Employee(
    int Id,
    string FirstName,
    string Surname,
    string? Designation = null)
: Person( FirstName, Surname)            // SAME AS: Person( FirstName, LastName: Surname )
{
    public bool IsEnabled { get; set; } = true;

    public void Works()
    {
        Console.WriteLine("a method in the Record!");
    }
}



internal static class RunThis
{
    internal static void Run()
    {
        MyBaseClass obj = new MyDerivedClass("Manoj", "Sharma");
        Console.WriteLine(obj.firstName);
        // Console.WriteLine(obj.lastName);        // COMPILER ERROR: lastName is property of DerivedClass
        Console.WriteLine();


        Console.WriteLine("--- Employee p1 = new Employee(....)");
        Employee p1
            = new Employee(Id: 10, FirstName: "Manoj", Surname: "Sharma", Designation: "CEO");
        Console.WriteLine(p1);
        Console.WriteLine(p1.GetType());
        p1.Works();
        Console.WriteLine();

        // The object is mutated into the Derived Record Type!
        Console.WriteLine("--- Person p2 = new Employee(....)");
        Person p2
            = new Employee(Id: 10, FirstName: "Manoj", Surname: "Sharma", Designation: "CEO") { IsEnabled = false };
        Console.WriteLine(p2);
        Console.WriteLine(p2.GetType());
        Console.WriteLine();
    }
}
