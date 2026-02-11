using System.Linq;

namespace Demo_Linq.Demo01
{
    internal static class Demo
    {
        internal static void RunThis()
        {
            // C#12 collection initialization
            int[] arr = [25, 67, 17, 88, 20, 92, 62, 97, 35, 48, 77, 21, 16, 52];

            Console.WriteLine("-- all the numbers");
            foreach (int i in arr)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();


            Console.WriteLine("-- between 30 and 70");
            foreach (int i in arr)
            {
                if(i >= 30 && i <= 70)
                {
                    Console.Write("{0} ", i);
                }
            }
            Console.WriteLine();


            Console.WriteLine("-- between 30 and 70 - after CLONING & SORTING");
            int[]? arrClone = arr.Clone() as int[];
            if (arrClone is not null)
            {
                arrClone.Sort();

                foreach (int i in arrClone)
                {
                    if (i >= 30 && i <= 70)
                    {
                        Console.Write("{0} ", i);
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("-- between 30 and 70 - after CLONING, SORTING & REVERSING");
            int[]? arrClone2 = arr.Clone() as int[];
            if(arrClone2 is not null)
            {
                arrClone2.Sort();
                Array.Reverse(arrClone2);

                foreach (int i in arrClone2)
                {
                    if (i >= 30 && i <= 70)
                    {
                        Console.Write("{0} ", i);
                    }
                }
                Console.WriteLine();
            }


            //Console.WriteLine("-- between 30 and 70 - after CLONING, SORTING & REVERSING - partially optimized");
            //int[]? arrClone3 = arr.Clone() as int[];
            //if (arrClone3 is not null)
            //{
            //    arrClone3.Sort();
            //    for(int i = arrClone3.Length - 1; 
            //        i > -1 && (arrClone3[i] >= 30 && arrClone3[i] <= 70); 
            //        i--)
            //    {
            //        Console.Write("{0} ", arrClone3[i]);
            //    }
            //    Console.WriteLine();
            //}


            /****************
                Console.WriteLine("-- between 30 and 70");
                foreach (int i in arr)
                {
                    if(i >= 30 && i <= 70)
                    {
                        Console.Write("{0} ", i);
                    }
                }
                Console.WriteLine();
            **************/

            Console.WriteLine("--- using LINQ");
            var query = from i in arr
                        orderby i descending
                        where i >= 30 && i <= 70
                        select i;
            foreach (var i in query)            // executed on GetEnumerator() on entry in FOREACH loop
            {
                Console.Write("{0} ", i);
            }

            // LAMBDA VERSION of the LINQ Query shown above
            List<int>? query2 = arr.Where( i => i >= 30 && i <= 70)
                                   .OrderByDescending(i => i)
                                   .ToList();
            if(query2 is not null)
            {
                foreach (var i in query2)            // executed on GetEnumerator() on entry in FOREACH loop
                {
                    Console.Write("{0} ", i);
                }
            }

            int[]? query3 = arr.Where(i => i >= 30 && i <= 70)
                               .OrderByDescending(i => i)
                               .ToArray();
            if (query3 is not null)
            {
                foreach (var i in query3)            // executed on GetEnumerator() on entry in FOREACH loop
                {
                    Console.Write("{0} ", i);
                }
            }
        }

    }
}
