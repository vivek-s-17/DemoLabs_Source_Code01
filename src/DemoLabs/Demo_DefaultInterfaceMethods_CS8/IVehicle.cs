namespace Demo_DefaultInterfaceMethods;


interface IVehicle
{

    string RegNo { get; }


    void ShowInfo();


    // Example: Interface Method
    void DisplayInfo()
    {
        Console.WriteLine("DisplayInfo() from IVehicle of Type: {0}", this.GetType() );
    }


    // NOT ALLOWED: Interfaces cannot contain Instance Fields
    // private readonly int id = 10;


    // But, Interfaces can contain Static Fields
    static readonly string Author = "Manoj Kumar Sharma";
    static private List<IVehicle>? Vehicles = null;      // NOTICE, this is a PRIVATE member!


    // And Interfaces can now have Static Methods also
    static void AddVehicle(IVehicle objVehicle)
    {
        //--------- pre- C# 6.0
        //if(IVehicle.Vehicles == null)
        //{
        //    IVehicle.Vehicles = new List<IVehicle>();
        //}
        // IVehicle.Vehicles.Add(objVehicle);


        // C# 6.0 - null coalescing operator ??
        //          used to implement Late Instantiation Design Pattern.
        (IVehicle.Vehicles ??= new List<IVehicle>()).Add(objVehicle);
    }


    // Static Property with { get; } 
    static int NumberOfVehicles
        => (IVehicle.Vehicles??= new List<IVehicle>()).Count;

}
