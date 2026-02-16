namespace Demo_DefaultInterfaceMethods;

class Scooter : IVehicle
{
    public Scooter(string regNo)
    {
        this.RegNo = regNo;
    }


    #region IVehicle Members

    public string RegNo { get; private set; }


    public void ShowInfo()
    {
        Console.WriteLine($"ShowInfo() of Scooter called.");
    }


    /// <remarks>
    ///     NOTE: 
    ///         Scooter is defining its own implementation of the DisplayInfo() method
    ///         which has the same signature as that defined in the IVehicle Interface
    /// </remarks>
    public void DisplayInfo()
    {
        Console.WriteLine(
            $"DisplayInfo() of Scooter called with {this.GetType().Name.ToUpper()} object");
    }

    #endregion

}
