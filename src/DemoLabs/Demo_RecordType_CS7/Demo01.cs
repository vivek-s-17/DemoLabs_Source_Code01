/// <summary>
///     Issue #1: Equality was reference based.
/// </summary>


namespace Demo_RecordType_C7.Demo01;


class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}



class PersonSolved 
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;



    #region To enable Value-Based Comparison override the following members in the Type


    //  GetHashCode() is mandatory when Equals is overriden - to provide support to work with collections.
    //  So, as a rule, Equals() and GetHashCode() should return the same hashcode.
    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName);
    }


    public override bool Equals(object? obj)
    {
        /**********
        
        if( obj is null )
        {
            return false;
        }

        var other = obj as PersonSolved;
        if( other is null )
        {
            return false;
        }

        return this.FirstName == other.FirstName && this.LastName == other.LastName;

        ********/

        if( obj is not PersonSolved other)
        {
            return false; 
        }

        return FirstName == other.FirstName && LastName == other.LastName;
    }


    public static bool operator ==(PersonSolved left, PersonSolved right)
    {
        // Check if both objects point to the same
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }


    public static bool operator !=(PersonSolved left, PersonSolved right)
    {
        return !(left == right); 
    }


    // Optionally override ToString() to address Serialization needs
    public override string ToString()
    {
        return $"{{ firstName: {FirstName}, lastName: {LastName} }}";
    }

    #endregion

}



class PersonResolved : IEquatable<PersonResolved>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public bool Equals(PersonResolved? other)
    {
        if (other is null)
        {
            return false;
        }

        return FirstName == other.FirstName && LastName == other.LastName;
    }

    public override bool Equals(object? obj)
        => Equals(obj as PersonSolved);

    public override int GetHashCode()
        => HashCode.Combine(FirstName, LastName);

    public static bool operator ==(PersonResolved left, PersonResolved right)
        => EqualityComparer<PersonResolved>.Default.Equals(left, right);

    public static bool operator !=(PersonResolved left, PersonResolved right)
        => !(left == right);
}


public record PersonRecord(string FirstName, string LastName);


internal static class RunThis
{
    internal static void Run()
    {
        // Two objects with same data are not equal.
        // REASON: class equality compares memory reference, not data!
        var p1 = new Person() { FirstName = "Manoj", LastName = "Sharma" };
        var p2 = new Person() { FirstName = "Manoj", LastName = "Sharma" };
        var p3 = p1;

        Console.WriteLine( "--- Two objects with same data are NOT EQUAL." );
        Console.WriteLine( "{0} : p1 == p2", p1 == p2 );                // false
        Console.WriteLine( "{0} : p1.Equals(p2)", p1.Equals(p2) );      // false
        Console.WriteLine( "{0} : p3 = p1", p3 == p1 );                 // true: references the same object!
        Console.WriteLine();


        // Two objects with same data are not equal.
        // REASON: class equality compares memory reference, not data!
        var p10 = new PersonSolved() { FirstName = "Manoj", LastName = "Sharma" };
        var p20 = new PersonSolved() { FirstName = "Manoj", LastName = "Sharma" };
        var p30 = p10;

        Console.WriteLine("--- Two objects with same data are NOW EQUAL.");
        Console.WriteLine("{0} : p10 == p20", p10 == p20);
        Console.WriteLine("{0} : p10.Equals(p20)", p10.Equals(p20));
        Console.WriteLine("{0} : p30 = p10", p30 == p10);
        Console.WriteLine();



        var p100 = new PersonResolved() { FirstName = "Manoj", LastName = "Sharma" };
        var p200 = new PersonResolved() { FirstName = "Manoj", LastName = "Sharma" };
        var p300 = p100;

        Console.WriteLine("--- Two objects with same data are NOW EQUAL.");
        Console.WriteLine("{0} : p100 == p200", p100 == p200);
        Console.WriteLine("{0} : p100.Equals(p200)", p100.Equals(p200));
        Console.WriteLine("{0} : p300 = p100", p300 == p100);
        Console.WriteLine();

        Console.WriteLine("----------------------------------\n");

        var p1000 = new PersonRecord(FirstName: "Manoj", LastName: "Sharma");
        var p2000 = new PersonRecord(FirstName: "Manoj", LastName: "Sharma");
        var p3000 = p1000;

        Console.WriteLine("--- Two RECORDs with same data are NOW EQUAL.");
        Console.WriteLine("{0} : p1000 == p2000", p1000 == p2000);
        Console.WriteLine("{0} : p1000.Equals(p2000)", p1000.Equals(p2000));
        Console.WriteLine("{0} : p3000 = p1000", p3000 == p1000);
    }

}
