namespace Demo_PrimaryConstructor;

internal class Demo01
{
    private int id;
    private string name { get; set; }

    internal Demo01( int id, string name )
    {
        this.id = id;
        this.name = name;
    }

    public override string ToString()
    {
        return $"ID: {id}, Name = {name}";
    }
}


// Primary Constructor!
// NOTE: all constructor parameters are private to the object!
internal class Demo02(int id, string name)
{
    public override string ToString()
    {
        name = name.ToUpper();
        return $"ID: {id}, Name = {name}";
    }
}


