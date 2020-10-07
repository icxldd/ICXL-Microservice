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
    public abstract class Fruit
    {
        public string vendor { get; set; } //默认为private

        public abstract float Price { get; } //抽象属性必须是公有的

        public abstract void GrowInArea();
       

    }



    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
