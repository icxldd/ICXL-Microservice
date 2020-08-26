using System;

namespace FactoryTest
{
    public class IronManComponentFactory
    {
        public static object CreateComponent(string comname)
        {
            switch (comname)
            {
                case "String":
                    return new String("");
                case "double":
                    return new double();
            }
            return null;
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
