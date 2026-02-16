#nullable disable       // default as CSPROJ is configured as "Allowed"
// #nullable enable

namespace Demo_NullableReferenceTypes;

class Demo01
{
    public static void Run()
    {
        string message = null;

        Console.WriteLine($"Message : {message}");

        // -- Will raise a Null Reference Exception
        Console.WriteLine($"Message : {message.ToUpper()}");
    }
}
