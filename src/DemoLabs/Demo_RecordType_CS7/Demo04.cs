/***********************************

Compiler generates automatically:
     Properties
     Constructor
     Equals()
     GetHashCode()
     ToString()
      With Cloning Support
      Deconstruct() method
      Copy constructor

    // Example Deconstruct() for Person object
    public void Deconstruct(out string FirstName, out string LastName)
    {
        FirstName = this.FirstName;
        LastName  = this.LastName;
    }

 ******/

namespace Demo_RecordType_C7.Demo04;


internal record Person(string FirstName, string LastName);


internal static class RunThis
{
    internal static void Run()
    {
        var p1 = new Person(FirstName: "Manoj", LastName: "Sharma");
        Console.WriteLine(p1);
        Console.WriteLine();

        // easily cloned with Mutation!;
        var p2 = p1 with { LastName = "Verma" };
        Console.WriteLine(p2);
        Console.WriteLine();

        // example of Deconstruction!
        //  The compiler compiles this as Person.Deconstruct(out FirstName, out LastName) 
        //    and generates two local variables - FirstName and LastName
        var (firstName, lastName) = p2;
        Console.WriteLine( $"{firstName} {lastName}" );
        Console.WriteLine();


        // Another example of Deconstruction
        var people = new List<Person>
        {
            new("Manoj", "Sharma"),
            new("Amit", "Verma")
        };
        // foreach(Person person in people) { ... }
        foreach (var (first, last) in people)           // using Deconstruction
        {
            Console.WriteLine($"{first} {last}");
        }
        Console.WriteLine();


        // NOTE: Tuple is differnt
        var t = ("Manoj", "Sharma");
        Console.WriteLine(t);
        Console.WriteLine(t.GetType());

    }

}