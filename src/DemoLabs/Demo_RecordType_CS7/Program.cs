// Why do we need a new type - RECORD

// Demo_RecordType_C7.Demo01.RunThis.Run();

// Demo_RecordType_C7.Demo02.RunThis.Run();

// Demo_RecordType_C7.Demo03.RunThis.Run();


/********
    NEED FOR a new Type: Record

    In an application, we often create types that are just data carriers.  For example:
    - DTOs
    - ViewModels
    - API request/response models
    - Domain value objects

    Using a normal class to address this, required implementation of too much Boilerplate code!
    - Constructor
    - Properties
    - Equality overrides
    - ToString
    - Copy logic

    RECORDs on the other hand, offer:
    - Value-based equality
    - Immutability (by default)
    - Less boilerplate code
    - functional programming influenced C# language evolution
        - immutability
        - pattern matching
        - expressions
        - with expressions
*/

// Demo_RecordType_C7.Demo04.RunThis.Run();

// Demo_RecordType_C7.Demo05.RunThis.Run();

Demo_RecordType_C7.Demo06.RunThis.Run();
