using System;
using System.Collections.Generic;

namespace observerTest
{
    #region test
    public class Heater
    {
        private int temerature;//水温
        public delegate void heatEventHandler(int para);
        public event heatEventHandler heat;
        public void boilWater()
        {
            for (int i = 0; i < 100; i++)
            {
                temerature = i;
                if (temerature >= 98)
                {
                    if (heat != null)
                    {
                        heat(temerature);
                    }
                }
            }
        }
    }
    public class Alarm
    {
        public void boilVoice(int para)
        {
            Console.WriteLine("嘀嘀嘀");
        }
    }
    public class Show
    {
        public void ShowTemperature(int para)
        {
            Console.WriteLine("水温已经{0}度了", para);
        }
    }
    #endregion


    #region TEST2

    public abstract class ObserverContainer
    {
        /// <summary>
        /// 保存订阅者列表
        /// </summary>
        protected List<IObserver> observers = new List<IObserver>();


        /// <summary>
        /// 添加一个订阅者
        /// </summary>
        /// <param name="observer">具体的订阅者对象</param>
        public void AddObserver(IObserver observer)
        {
            if (observers.Contains(observer))
            {
                return;
            }
            observers.Add(observer);
        }

        /// <summary>
        /// 删除一个订阅者
        /// </summary>
        /// <param name="observer">具体的订阅者对象</param>
        public void RemoveObserver(IObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

    }


    public class HeaterContainer : ObserverContainer
    {
        /// <summary>
        /// 水温
        /// </summary>
        public int temerature { get; set; }

        /// <summary>
        /// 发布博客通知
        /// </summary>
        public void boilWater()
        {
            for (int i = 0; i < 100; i++)
            {
                temerature = i;
                if (temerature >= 98)
                {
                    //遍历通知每一个订阅者
                    foreach (IObserver ob in observers)
                    {

                        ob.boilWaterReceive(temerature);
                    }
                }
            }
        }

    }

    public interface IObserver
    {
        void boilWaterReceive(int number);
    }

    public class Alarm2 : IObserver
    {
        public void boilWaterReceive(int number)
        {
            Console.WriteLine("警告水温：" + number);
        }
    }

    public class Show2 : IObserver
    {
        public void boilWaterReceive(int number)
        {
            Console.WriteLine("显示屏：" + number);
        }
    }
    #endregion


    class Program
    {
        public delegate void NotifyEventHandler(object sender);

        public static NotifyEventHandler NotifyEvent;
        public static event NotifyEventHandler OnNewValueChanged;


        public static void Receive(object obj)
        {
            Console.WriteLine(obj);
            Console.WriteLine("Receive");
        }
        public static void Receive1(object obj)
        {
            Console.WriteLine(obj);
            Console.WriteLine("Receive1");
        }
        public static void Receive2(object obj)
        {
            Console.WriteLine(obj);
            Console.WriteLine("Receive2");
        }
        static void Main(string[] args)
        {
            //NotifyEvent += new NotifyEventHandler(Receive1);
            //NotifyEvent += new NotifyEventHandler(Receive1);
            //NotifyEvent += new NotifyEventHandler(Receive1);
            //NotifyEvent("123");
            //OnNewValueChanged += new NotifyEventHandler(Receive1);
            //OnNewValueChanged += new NotifyEventHandler(Receive1);
            //OnNewValueChanged += new NotifyEventHandler(Receive1);
            //OnNewValueChanged("123");
            //Heater heater = new Heater();
            //Alarm alarm = new Alarm();
            //Show show = new Show();
            //heater.heat += alarm.boilVoice;
            //heater.heat += show.ShowTemperature;
            //heater.boilWater();
            //Console.Read();

            HeaterContainer container = new HeaterContainer();
            Alarm2 r = new Alarm2();
            Show2 b = new Show2();
            container.AddObserver(r);
            container.AddObserver(b);
            container.boilWater();


            Console.ReadLine();


        }
    }
}
