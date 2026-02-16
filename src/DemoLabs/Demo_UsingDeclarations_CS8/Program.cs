using System;

namespace cs8_con_UsingDeclarations
{
    class Program
    {
        static void Main(string[] args)
        {

            // Traditional approach
            using (Organization org1 = new Organization())
            {
               // use org1 object
            }                                   // implicitly calls org1.Dispose() 


            // C# 8.0 approach
            using Organization org2 = new Organization();
            // use org2 object

        }                                       // implicitly calls org2.Dispose() on EXIT OF METHOD
    }
}
