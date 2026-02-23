/// <summary>
///     Issue #3: Copy constructor required to copy/clone
/// </summary>


namespace Demo_RecordType_C7.Demo03;


class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;


    // Default constructor
    public Person() { }


    // Copy constructor
    // Decide to whether or not offer SHALLOW COPY and DEEP COPY
    public Person(Person other)
    {
        if (other is null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        FirstName = other.FirstName;
        LastName = other.LastName;
    }

}


public record PersonRecord(string FirstName, string LastName);


internal static class RunThis
{
    internal static void Run()
    {
        // Working with regular objects!
        var p1 = new Person() { FirstName = "Manoj", LastName = "Sharma" };
        var p2 = new Person(p1);                    // copy
        p2.LastName = "Verma";                      // to mutate the copied object, do it manually!

        Console.WriteLine("p1 => ID: {0}, Name: {1}", p1.FirstName, p1.LastName);
        Console.WriteLine("p2 => ID: {0}, Name: {1}", p2.FirstName, p2.LastName);
        Console.WriteLine("{0} : p1 == p2", p1 == p2);
        Console.WriteLine("{0} : p1.Equals(p2)", p1.Equals(p2));
        Console.WriteLine();


        var p1000 = new PersonRecord(FirstName: "Manoj", LastName: "Sharma");
        var p2000 = p1000;
        //  p2000.FirstName = p2000.FirstName.ToUpper();        // COMPILER ERROR: cannot mutate the copy!
        var p3000 = p1000 with { LastName = "Verma" };          // Copied with Mutation!

        Console.WriteLine($"p1000 => {p1000}");
        Console.WriteLine($"p2000 => {p2000}");
        Console.WriteLine($"p3000 => {p3000}");
        Console.WriteLine("{0} : p1000 == p2000", p1000 == p2000);
        Console.WriteLine("{0} : p1000.Equals(p2000)", p1000.Equals(p2000));
        Console.WriteLine("{0} : p1000 == p3000", p1000 == p3000);
        Console.WriteLine("{0} : p1000.Equals(p3000)", p1000.Equals(p3000));
        Console.WriteLine();



        /****************
         * 
        **************/

    }

}