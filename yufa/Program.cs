using System;
using System.Threading;

namespace yufa
{
    class Program
    {
        static  int numIterations = 100;
        static AutoResetEvent myResetEvent = new AutoResetEvent(true);
        static int number;
        static void Main(string[] args)
        {
            //Create and start the reader thread.
            Thread myReaderThread = new Thread(new ThreadStart(MyReadThreadProc));
            myReaderThread.Name = "ReaderThread";
            myReaderThread.Start();

            for (int i = 1; i <= numIterations; i++)
            {
                Console.WriteLine("Writer thread writing value: {0}", i);
                number = i;

                //Signal that a value has been written.
                //myResetEvent.Set();

                //Give the Reader thread an opportunity to act.
                Thread.Sleep(1);
            }

            //Terminate the reader thread.
            myReaderThread.Abort();
            Console.ReadLine();


        }



        static void MyReadThreadProc()
        {
            while (true)
            {
                myResetEvent.WaitOne();
                Console.WriteLine("{0} reading value: {1}", Thread.CurrentThread.Name, number);
            }
        }
    }
}
