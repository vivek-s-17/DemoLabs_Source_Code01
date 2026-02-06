namespace Demo_CopyTypes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------- reference copy version");
            Demo01.RunThis.Run();

            Console.WriteLine("------------------------- shallow copy version");
            Demo02.RunThis.Run();

            Console.WriteLine("------------------------- deep copy version");
            Demo03.RunThis.Run();

            Console.WriteLine("------------------------- ICloneable.Car version");
            Demo04.RunThis.Run();

            Console.WriteLine("------------------------- ICloneable.Car +  ICloneable.Engine version");
            Demo05.RunThis.Run();
        }

    }
}
