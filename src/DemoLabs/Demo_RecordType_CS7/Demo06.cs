namespace Demo_RecordType_C7.Demo06;

internal record struct PersonRecordStruct1(string Name, int Age);

internal readonly record struct PersonRecordReadonlyStruct(string Name, int Age);


internal record class PersonRecordClass1(string Name, int Age);

internal record PersonRecordClass2(string Name, int Age);         // is CLASS by default.


internal record MyDemo<T>
{
}


class A
{
    public string Name { get; private set; }
    public int Age { get; init; }

    public A(string name, int age)
    {
        Name = name;
        Age = age;
    }

    void m()
    {
        Name = Name.ToUpper();
        // Age++;           // COMPILER ERROR: can only be SET in the Constructor (ONLY initialized)

        A obj = new A();
        obj.Name = Name.ToLower();
        // obj.Age--;          // COMPILER ERROR: can only be SET in the Constructor (ONLY initialized)
    }
}



internal static class RunThis
{
    internal static void Run()
    {
        PersonRecordStruct1 p1 = new PersonRecordStruct1(Name: "manoj", Age: 30);
        p1.Name = p1.Name.ToUpper();


        PersonRecordClass2 p2 = new PersonRecordClass2(Name: "manoj", Age: 30);
        // p2.Name = p2.Name.ToUpper();                        // COMPILER ERROR


        PersonRecordReadonlyStruct p10 = new PersonRecordReadonlyStruct(Name: "manoj", Age: 30);
        // p10.Name = p10.Name.ToUpper();                          // COMPILER ERROR

    }
}