
// Demo of ICloneable (deep copy example)
// NOTE: implement ICloneable to all aggregated objects!
//       don't implement in aggregated objects results in SHALLOW COPY (Demo04 example)
namespace Demo_CopyTypes.Demo05
{
    public class Car
        : System.ICloneable
    {
        public string RegNo { get; set;  }

        public Engine ObjEngine { get; set; }


        #region System.ICloneable members
        
        public object Clone()
        {
            Car objClone = (Car)this.MemberwiseClone();
            objClone.ObjEngine = (Engine)this.ObjEngine.Clone();
            return objClone;
        }

        #endregion
    }

    public class Engine
        : System.ICloneable
    {
        public int BHP { get; set; }

        #region System.ICloneable members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
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
