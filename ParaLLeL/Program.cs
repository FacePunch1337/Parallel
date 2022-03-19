using System;
using System.Threading;

namespace ParaLLeL
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Thread t;
            CancellationTokenSource cts;


            cts = new CancellationTokenSource();

            for (int i = 0; i < 10; i++)
                {
                    t = new Thread(ThreadProc);
                    t.Start(new Multidata { time = DateTime.Now, ms = DateTime.Now.Millisecond, token = cts.Token });
                    t.Join();
                    Thread.Sleep(1000);

                    if (Multidata.id == 3)
                    {
                         cts.Cancel();
                    }

                }

        }

        


        private static void ThreadProc(object par)
        {
            if (par == null) return;
            Multidata token = (Multidata)par;
            Console.WriteLine("ThreadProc" + par);
            for (int i = 0; i < 100; i++)
            {
                
                if (token.token.IsCancellationRequested)
                {
                    Console.WriteLine("ThreadProc Cancelled");
                    return;
                }
                

                if (!token.token.IsCancellationRequested)
                {
                    Console.WriteLine("ThreadProc Stop" + par);
                    return;
                }

                

            }
            
        }
    }

    class Multidata
    {
        public static String name { get; set ; }

        public static int id { get; set; }
        public DateTime time { get; set; }
        public int ms { get; set; }

        
        public CancellationToken token { get; set; }

        public Multidata()
        {
            name = "Thread";
            id += Thread.CurrentThread.ManagedThreadId;
        }
        public override string ToString()
        {
            return $"\tName: {name} | ID: {id} | DateTime: {time} : {ms} |'\t' {token}";
        }
    }
}
