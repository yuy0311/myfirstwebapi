using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MyFirstWebAPI.Utility;
using MyFirstWebAPI.Models.Utility;

namespace MyFirstWebAPI
{
    public class ConsoleProgram
    {
        static void Main(string[] args)
        {
            MyClass c = new MyClass();
            Console.WriteLine(c.ReadValue().getMYName());
            Console.WriteLine("Ni HAO SHANGHAI");
         
          //  AppConfigXMLParser.getXMLValue("Timetable", "server");
          //  AppConfigXMLParser.getAttributeValue("Timetable", "name");
        }
    }

    public class MyClass
    {
        public Myobject ReadValue()
        {
            return HeavyMethodAsync("Hello World").Result;
        }
        public async Task<Myobject> HeavyMethodAsync(string input)
        {
            return await Task.Run(() => getValue(input));
        }

        private Myobject getValue(string input)
        {
            Thread.Sleep(5000);
            Console.WriteLine("xxx-xxxx");
            return new Myobject(input);
        }
    }

    public class Myobject
    {
        private string name;

        public Myobject(string input)
        {
            this.name = input;
        }

        public string getMYName()
        {
            return this.name;
        }
    }
}