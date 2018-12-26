using System;
using System.Threading;

namespace AutoUpdate.Executor
{
    class Program
    {
        static void Main(string[] args)
        {
            var i = 15;

            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Hello World! " + i);

                if (--i < 0)
                {
                    return;
                }
            }
        }
    }
}
