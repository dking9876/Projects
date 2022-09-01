using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    class Test
    { 
        public static void TestEx1()
        {
            Musician m1 = new Musician(1, 5, new string[] { "gitar", "psanter", "hachsra" });
            Musician m2 = new Musician(2, 7, new string[] { "gitar", "sacsopon", "hachsra" });
            Musician m3 = new Musician(3, 4, new string[] { "halil", "psanter", "hachsra" });
            Musician m4 = new Musician(3, 6, new string[] { "halil", "psanter", "hachsra" });
            Orchestra o1 = new Orchestra("one");
            Orchestra o2 = new Orchestra("two");
            o1.AddMusician(m1);
            o1.AddMusician(m2);
            o1.AddMusician(m3);
            o2.AddMusician(m4);
            Orchestra[] arrOrchestras = new Orchestra[] { o1, o2 };
            Console.WriteLine(Program.OrchestraWithMostYears(3, arrOrchestras));
        }
        public static void TestEx2()
        {
            Musician m1 = new Musician(1, 5, new string[] { "gitar", "psanter", "hachsra" });
            Musician m2 = new Musician(2, 7, new string[] { "gitar", "sacsopon", "hachsra" });
            Musician m3 = new Musician(3, 4, new string[] { "halil", "psanter", "hachsra" });
            Musician m4 = new Musician(7, 6, new string[] { "sacsopon", "psanter", "gitar" });
            Orchestra o1 = new Orchestra("one");
            Orchestra o2 = new Orchestra("two");
            o1.AddMusician(m1);
            o1.AddMusician(m2);
            o1.AddMusician(m3);
            o2.AddMusician(m4);
            Orchestra[] arrOrchestras = new Orchestra[] { o1, o2 };

            int[] arr = Program.AllMusiansPlayInstrument(arrOrchestras, "gitar", new int[10]);

            for (int i = 0; i < 10; i++)
            {
                if (arr[i] != 0)
                {
                    Console.WriteLine(arr[i]);
                }

            }
        }
        public static void TestEx3()
        {
            Musician m1 = new Musician(1, 5, new string[] { "halil", "psanter", "hachsra" });
            Musician m2 = new Musician(2, 7, new string[] { "gitar", "sacsopon", "hachsra" });
            Musician m3 = new Musician(3, 4, new string[] { "halil", "psanter", "hachsra" });
            Musician m4 = new Musician(7, 6, new string[] { "sacsopon", "psanter", "gitar" });
            Orchestra o1 = new Orchestra("one");
            Orchestra o2 = new Orchestra("two");
            o1.AddMusician(m1);
            o1.AddMusician(m2);
            o1.AddMusician(m3);
            o2.AddMusician(m4);
            Orchestra[] arrOrchestras = new Orchestra[] { o1, o2 };

           int NumberOfMusicians =  Program.NumberOfMusiciansPlayInstruments(arrOrchestras, "one", new string[] { "halil", "psanter", "hachsra" });
            Console.WriteLine(NumberOfMusicians);
        }
    }
}
