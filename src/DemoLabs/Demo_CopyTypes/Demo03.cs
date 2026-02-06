
// Demo of DEEP COPY
namespace Demo_CopyTypes.Demo03
{
    public class Car
    {
        public string RegNo { get; set;  }

        public Engine ObjEngine { get; set; }
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

            // DEEP Copy
            Car objCopy = new Car();
            objCopy.RegNo = objCar.RegNo;                   // value type assignment
            objCopy.ObjEngine = new Engine();               // new nested object
            objCopy.ObjEngine.BHP = objCar.ObjEngine.BHP;   // value type assignment of the nested object

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
