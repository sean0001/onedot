using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using cmd.libs;


namespace cmd
{

    public abstract class Animal
    {
        public abstract void Eat();
    }

    public class Cat : Animal
    {
        public override void Eat()
        {
            Console.WriteLine("Cat eat");
        }
    }

    public class Dog : Animal
    {
        public override void Eat()
        {
            Console.WriteLine("Dog eat");
        }
    }

    public class WolfDog : Dog
    {
        public override void Eat()
        {
            Console.WriteLine("Wolfdog eat");
        }
    }






    public class Calculate
    {
        public int a { get; set; } = 2;
        private int b { get; set; } = 4;

        public int? year { get; set; } = null;

        public int add() => a + b;

    }






    public class work<T>
    {

        //public void DoWork<T>()
        //{
        //    Type t = typeof(T);
        //    Console.WriteLine("Get Class: {0}", t.Name);
        //    PropertyInfo[] properties = t.GetProperties();
        //    foreach (PropertyInfo property in properties)
        //    {
        //        Console.WriteLine("\tproperty.Name: " + property.Name + "\tproperty.MemberType: " + property.PropertyType);
        //    }
        //}




        public void DoWork()
        {
            Type t = typeof(T);
            Console.WriteLine("Get Class: { 0 }", t.Name);
            PropertyInfo[] properties = t.GetProperties();
            MethodInfo[] methods = t.GetMethods();

            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine("\tproperty.Name: " + property.Name + "\tproperty.MemberType: " + property.PropertyType);
            }

            foreach (MethodInfo method in methods)
            {
                Console.WriteLine("\tmethod.Name: " + method.Name + "\tmethod.MemberType: " + method.MemberType);
            }
        }



    }





    public class aaa
    {
        public aaa a { get; set; }
    }







    public class test<T>
    {


        public test()
        {


        }




        private T ob;

        public T getOb()
        {

            return ob;
        }



        public void setOb(T ob)
        {

            this.ob = ob;

        }



        public void showType()
        {

            Console.WriteLine("T的实际类型是: " + ob.GetType().ToString());
        }


    }




    class Program
    {
        static void Main(string[] args)
        {

            var mm = encryptDes.DESEncrypt("1");
            Console.WriteLine(mm);



            var m1 = encryptDes.DESDecrypt("6nMPej9TFf8QxRVa+rD8bg==");
            Console.WriteLine(m1);
            Console.ReadLine();


        }

    }





    /*

        class Program
    {

        public static int? age { get; set; } = null;
        public static string name { get; set; } = "李明";



        public static void calculate(out int x, out int y)
        {
            x = 10;
            y = 10;
        }


      

        static void Main(string[] args)
        {

            void testfunc()
            {
                Console.WriteLine("a:{0}", "ssssssssssssssssss");
            }



            Stopwatch sw = new Stopwatch();
            sw.Start();


            void testAnimal(Animal aa) {

                aa.Eat();

            }





            testAnimal(new Cat());





            Calculate cc1=  new Calculate();
            cc1= null;
            Console.WriteLine($"\tyear:{cc1?.year}");
            Console.WriteLine($"\tnameof:{nameof(Calculate)}");
            Console.ReadLine();

            
            

            var w = new work<Calculate>();
            w.DoWork();


            var m = new test<char>();
            m.showType();

            int a1 = 22;
            int a2 = 44;

            calculate(out a1, out a2);
            Console.WriteLine($"a1:{a1}, a2:{a2}");
            Console.ReadLine();

            //var mmm  =  age ?? throw new Exception("");
            //dynamic a = new object();
            //a.ss = "1111111111";
            //a.aa = 123;
            //a.bb = new int[] { 1, 2, 3 };
            //Console.WriteLine("{0}", a.ss);

            int a = 12;

            ref int x = ref a;

            int? bb = null;

            var mms = age?.ToString();

            x = x - 8;
            testfunc();
            int cc = 123_66;

            Console.WriteLine("a:{0}", a);
            Console.WriteLine("x:{0}", cc);

            Console.WriteLine($"ssss:{a + bb}");

            Console.Read();

        }
    }

    */

}
