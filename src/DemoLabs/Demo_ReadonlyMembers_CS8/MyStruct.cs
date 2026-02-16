namespace Demo_ReadonlyMembers;


struct MyStruct
{
    public string Name { get; set; }


    /// <remarks>
    ///     Marking the method as "READONLY", the Compiler is informed the intent of the method.
    ///     Thus, no member will be allowed to be changed.
    /// </remarks>
    public readonly void Display(string message)
    {
        message = message.ToUpper();
        // this.Name = this.Name.ToUpper();       // Compiler Error in READONLY method

        Console.WriteLine($"Message: {message}");
        Console.WriteLine($"Name: {this.Name}");

        int x = 5;
        x = HelperMethod(x);                // NOTE: The Instance method also needs to be marked as "readonly"
        Console.WriteLine($"i = {x}");

        AnotherHelperMethod( x );
    }


    /// <remarks>
    ///     Marked as "readonly" to suppress the WARNING:
    ///     Call to non-readonly member from a 'readonly' member results in
    ///     an implicit copy of 'this' (which is called the "defensive copy").
    /// </remarks>
    private readonly int HelperMethod(int i)
    {
        i++;

        // NOTE: You cannot alter STATE as method is marked READONLY.
        // this.Name = this.Name.ToUpper();               // Compiler Error in READONLY method

        return i;
    }


    private static int AnotherHelperMethod(int i )
    {
        i++;
        return i;
    }
}
