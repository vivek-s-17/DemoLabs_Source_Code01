namespace Demo_NullableReferenceTypes;


class Demo02
{

    public static void Run()
    {
        string? message = null;     //  convert to Nullable Reference Type
        string? message2 = default;  

        Console.WriteLine($"Message : {message}");
        Console.WriteLine($"Message2 : {message2}");
        Console.WriteLine();

        // Raises a WARNING: "Dereference of a possible null reference" because it may be null
        // Console.WriteLine($"Message : {message.ToUpper()}");

        // SOLUTION #1: C# 5.0 approach - Use "Null-Coalescing Operator" ( ?? ) 
        //              To provide a fallback default value for a possible NULL
        Console.WriteLine($"Message : {(message is not null ? message : "")}");
        Console.WriteLine($"Message : {(message ?? "").ToUpper()}");
        Console.WriteLine($"Message : {(message ?? string.Empty).ToUpper()}");          // OR
        Console.WriteLine($"Message : {message}");                               // implicitly calls .ToString() for String.Empty
        Console.WriteLine();

        // SOLUTION #2: Use the "Postfix Unary Null-Forgiving Operator" ( ! )
        //              Use only if you are sure that the value will not be null at Runtime.
        // NOTE: If null, at runtime it will still throw NullReference Exception
        Console.WriteLine($"Length = {message!.ToUpper()}");            // compare with LINE #22
        Console.WriteLine();

        // SOLUTION #3: Use "Null-Coalescing Assignment Operator" ( ??= )
        //              To initialize if null
        //    message = message is null ? string.Empty : message;
        Console.WriteLine($"Message : {(message ??= "").ToUpper()}");
        Console.WriteLine($"Message : {(message ??= string.Empty).ToUpper()}");
        Console.WriteLine($"Message : {message}");
        Console.WriteLine();


#pragma warning disable CS0219          // Variable is assigned but its value is never used
#pragma warning disable CS8600          // Converting null literal or possible null value to non-nullable type.

        // Another set of examples of "Postfix Unary Null-Forgiving Operator" ( ! )
        string x = null;        // This gives a warning
        string y = null!;       // This is okay!
        string z = default!;    // This is also okay!

#pragma warning restore CS0219          // Variable is assigned but its value is never used
#pragma warning restore CS8600          // Converting null literal or possible null value to non-nullable type.

    }
}
