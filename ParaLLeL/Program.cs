using System;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;

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

        public static Task task = null;

        public static int value;

        public Multidata()
        {
            
        }

        public Multidata(int _value)
        {
            value = _value * _value;
        }

        

        public void StartTasks(object task_object)
        {
            for (int i = 0; i < value; ++i)
            {
                var task = new Task(TaskProc, i);
                task.Start();
                task.Wait();
                ShowNumber(task_object);
            }
            Console.WriteLine($"\nTOTAL_TASKS[{value}]");

        }



        public static void TaskProc(object par)
        {
            Console.WriteLine("TaskProc ID: " + par);
        }

        public static void ShowNumber(object par)
        {
            int copyId;

            lock (numLocker)
            {
                copyId = ++id;
            }
            Console.WriteLine($"Number: {copyId}");
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
            var multidata = new Multidata(3);
            multidata.StartTasks(multidata);
  
        }


       // #region ThreadPool
       /* static void Main1(string[] args)
        {
            var multidata = new Multidata();
            Task task = new Task(Multidata.TaskProc2, multidata.cts);
            
            multidata.Tasks(multidata);
            

            Task.Run(Multidata.TaskProc1).Wait();
           
            Multidata.task = Task.Run(() => Multidata.TaskProc2("Hello"));
            task.Wait();

        }
        #endregion

        #region ThreadPool
        static void Main0(string[] args)
        {
            
            Multidata.id = 0;
            var multidata = new Multidata();
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(multidata.ThreadPoolCallback, i);
            }
            Thread.Sleep(3000);
        }
        #endregion */

      



    }


}
