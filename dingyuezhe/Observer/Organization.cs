using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingyuezhe.Observer
{
    public class GuaiWuObj
    {
        public string name { get; set; }

        public int hp { get; set; }
    }

    public interface IObserver
    {
        void Receive(IObserver name);
    }

    public class Observer : IObserver
    {
        /// <summary>
        /// 订阅者名字
        /// </summary>
        private string m_Name;
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        /// <summary>
        /// 订阅者构造函数
        /// </summary>
        /// <param name="name">订阅者名字</param>
        public Observer(string name)
        {
            this.m_Name = name;
        }

        public void Receive(IObserver name)
        {
            Console.WriteLine("12321");
        }
    }
    public class IcxlOrganization : Organization
    {
        public IcxlOrganization(string name) : base(name) { }
    }


    public abstract class Organization
    {
        /// <summary>
        /// 保存订阅者列表
        /// </summary>
        private List<IObserver> observers = new List<IObserver>();

        /// <summary>
        /// 组织名
        /// </summary>
        public string OrganizationName { get; set; }


        /// <summary>
        /// 博客构造函数
        /// </summary>
        /// <param name="blogTitle">博客标题</param>
        /// <param name="blogInfo">博客信息</param>
        public Organization(string OrganizationName)
        {
            this.OrganizationName = OrganizationName;
        }

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

        /// <summary>
        /// 发布博客通知
        /// </summary>
        public void DaGuai(IObserver o)
        {
            //遍历通知每一个订阅者
            foreach (IObserver ob in observers)
            {
                if (ob != null)
                {
                    ob.Receive(o);
                }
            }
        }


    }
}
