namespace Demo_ReadonlyMembers;

class MyClass
{
    public string? Name { get; set; }

    // Initialize in Constructor or during Declaration
    public readonly string ReadOnlyDataField;                   
    public readonly string ReadOnlyDataField2 = "something!";

    // Can be changed by any member of the Class, but is readonly at the object level!
    public string ReadOnlyProperty { get; private set; }

    public MyClass ()
    {
        this.Name = null;
        ReadOnlyDataField = "something";            // initialize the readonly data-field
        ReadOnlyProperty = "different";
    }


    // NOTE: "readonly" is not VALID for a Class Member
    public  void Display()
    {
        Console.WriteLine($"Name: {this.Name}");
        // this.Name = this.Name?.ToUpper();
        ReadOnlyProperty = ReadOnlyProperty.ToUpper();
        // ReadOnlyDataField = ReadOnlyDataField.ToUpper();
    }

}
