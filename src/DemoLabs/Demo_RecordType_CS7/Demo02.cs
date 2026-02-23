/// <summary>
///     Issue #2: Hard to enforce Immutability in a clean manner
/// </summary>


namespace Demo_RecordType_C7.Demo02;


class Person
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;

    public Person (string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}


/************
class PersonWithPrimaryConstructor(string? firstName, string? lastName)
{
    public override string ToString()
    {
        return $"{firstName} {lastName}";
    }
}
***/

public record PersonRecord(string FirstName, string LastName);


internal static class RunThis
{
    internal static void Run()
    {
        var p1 = new Person( firstName: "Manoj", lastName: "Sharma" );
        // p1.FirstName = "cannot be changed";                      // COMPILER ERROR

        var p1000 = new PersonRecord(FirstName: "Manoj", LastName: "Sharma");
        // p1000.FirstName = "cannot be changed";                   // COMPILER ERROR
    }

}