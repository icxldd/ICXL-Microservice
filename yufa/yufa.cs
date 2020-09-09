using System;
using System.Collections.Generic;
using System.Text;

namespace yufa
{
    public class Yufa : IDisposable
    {
        public string a = "";
        public string id { get; set; }
        public static bool operator ==(Yufa t1, Yufa t2)
        {
            if (t1.id == t2.id)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
       
        public static bool operator !=(Yufa t1, Yufa t2)
        {

            Console.WriteLine("！=");
            return true;
        }

        public static bool operator >(Yufa t1, Yufa t2)
        {

            Console.WriteLine(">");
            return true;
        }

        public static bool operator <(Yufa t1, Yufa t2)
        {
            return true;
        }


        public static bool operator >=(Yufa t1, Yufa t2)
        {
            return true;
        }

        public static bool operator <=(Yufa t1, Yufa t2)
        {
            return true;
        }


        public static bool operator +(Yufa t1, Yufa t2)
        {
            return true;
        }

        public static bool operator -(Yufa t1, Yufa t2)
        {
            return true;
        }

        public static bool operator %(Yufa t1, Yufa t2)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public void Dispose()
        {
            this.a = null;
            Console.WriteLine(nameof(Yufa)+ "Dispose");

        }

        public string this[int x]
        {
            get { return x.ToString(); }
            set { }
        }
    }
}
