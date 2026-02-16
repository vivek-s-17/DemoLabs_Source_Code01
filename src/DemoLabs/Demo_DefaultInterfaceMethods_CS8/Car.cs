namespace Demo_DefaultInterfaceMethods;

class Car : IVehicle
{
    public Car(string regNo)
    {
        this.RegNo = regNo;
    }

    #region IVehicle Members

    public string RegNo { get; private set; }


    public void ShowInfo()
    {
        // (this as IVehicle).DisplayInfo();

        Console.WriteLine($"ShowInfo() of Car called.");
    }

    #endregion
}
