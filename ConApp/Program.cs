﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Threading;

namespace ConApp
{

    public class Person {
        public int? Id { get; set; } = null;
        public string Name { get; } = "yang";
        public string Career { get; } = "programer";


    }

    class Program
    {
        private static int? age  = null;
        private static string name = "";
        private static int height = 123_232;

        static void Main(string[] args)
        {
            Test();
            Console.WriteLine("Test End!");
            Console.ReadLine();
        }

        static async void Test()
        {
            await Test1();
            Console.WriteLine("Test1 End!");
        }


        static Task Test1()
        {
            Thread.Sleep(1000);
            Console.WriteLine("create task in test1");

            return Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("Test1");
            });
        }

    }
}
