using System;

namespace Company
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker w1 = new Worker("a", "a", "a", "a", "a", "a", 30, 170);
            Worker w2 = new Worker("b", "b", "b", "b", "b", "b", 30, 170);
            salesman s1 = new salesman("a", "a", "a", "a", "a", "a", 100000);
            salesman s2 = new salesman("b", "b", "b", "b", "b", "b", 120000);
            Boss b2 = new Boss("b", "b", "b", "b", "b", "b", 30000);
            double allsalary = 0;
            allsalary = allsalary + w1.CalcSalary() + w2.CalcSalary() + 
                s1.CalcSalary() + s2.CalcSalary() + b2.CalcSalary();
            BigBoss bb1 = new BigBoss("b", "b", "b", "b", "b", "b", 40000, (allsalary * 0.02));
            Console.WriteLine(w1.CalcSalary());
            Console.WriteLine(w2.CalcSalary());
            Console.WriteLine(bb1.CalcSalary());
            Console.WriteLine(b2.adress());
            human h1 = (human)bb1;
            Console.WriteLine(h1.CalcSalary());
            Boss h2 = (Boss)h1;
            Console.WriteLine(h2.CalcSalary());
            human[] humans = new human[3] ;
            humans[0] = (human)w1; 
        }
    }
}
