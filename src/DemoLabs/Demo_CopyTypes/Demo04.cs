
// Demo of ICloneable (shallow copy example)
namespace Demo_CopyTypes.Demo04
{
    public class Car
        : System.ICloneable
    {
        public string RegNo { get; set;  }

        public Engine ObjEngine { get; set; }


        #region System.ICloneable members
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }

    public class Engine
    {
        public int BHP { get; set; }
    }

    public static class RunThis
    {
        public static void Run()
        {
            Car objCar = new Car();
            objCar.RegNo = "KA 01 Car 01";
            objCar.ObjEngine = new Engine();
            objCar.ObjEngine.BHP = 1000;


            // IClonable interface example
            Car objCopy = (Car)objCar.Clone();

            Console.WriteLine("-- Before changing the data");
            Console.WriteLine("Car: {0} {1}", objCar.RegNo, objCar.ObjEngine.BHP);
            Console.WriteLine("CarCopy: {0} {1}", objCopy.RegNo, objCopy.ObjEngine.BHP);
            Console.WriteLine();

            objCopy.RegNo = "TN 01 Car 02";
            objCopy.ObjEngine.BHP = 5000;

            Console.WriteLine("-- After changing the data");
            Console.WriteLine("Car: {0} {1}", objCar.RegNo, objCar.ObjEngine.BHP);
            Console.WriteLine("CarCopy: {0} {1}", objCopy.RegNo, objCopy.ObjEngine.BHP);
            Console.WriteLine();
        }

    }
}
