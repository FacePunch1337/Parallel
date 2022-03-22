using System;
using System.Threading;

namespace ParaLLeL
{
    class Multidata
    {
        private static readonly object numLocker = new object();
        public static String name { get; set; }
        public static int id { get; set; }
        public DateTime time { get; set; }
        public int ms { get; set; }

        public CancellationTokenSource cts = new CancellationTokenSource();
        public CancellationToken token { get; set; }
        

        public Multidata()
        {
            name = "Thread";
        }

        public void ThreadPoolCallback(object threadContext)
        {
            int copyId;

            lock (numLocker)
            {
                copyId = Multidata.id++;
            }

            Console.WriteLine($"ID: {copyId} - Start | {ToString()} ");
            Thread.Sleep(1000);
            Console.WriteLine($"ID: {copyId} - Finish | {ToString()} ");
        }
        public override string ToString()
        {
            return $"\tName: {name} | DateTime: {time = DateTime.Now} : {ms = DateTime.Now.Millisecond} |'\t' {cts.Token}";
        }
    }

    internal class Program
    {

     
        static void Main(string[] args)
        {
            
            Multidata.id = 0;
            var multidata = new Multidata();
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(multidata.ThreadPoolCallback, i);
            }
            Thread.Sleep(3000);
        }

       
    }

   
}
