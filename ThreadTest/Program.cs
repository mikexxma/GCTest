using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ThreadTest
{
    class Program
    {

        public bool done ;// Static fields are shared between all threads
        static object locker = new object();
        static EventWaitHandle are = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            ////Hello world thread
            //Thread t = new Thread(print);
            //t.Start();
            //for (int i = 0; i < 50; i++)
            //{
            //    Console.WriteLine("I am from Main thread");
            //}

            ////unsafe thread
            //Program p = new Program();
            //p.done = true;
            //new Thread(p.print1).Start();
            //p.print1();


            //实现线程安全
            //Program p = new Program();
            //p.done = true;
            //new Thread(p.print1).Start();
            //p.print1();

            

            ////线程调用带参数的方法
            //Thread t1 = new Thread(new ParameterizedThreadStart(print));
            //t1.Name = "one_param_thread";
            //t1.Start("hello");
            ////线程带多参的方法
            //String s1 = "hello";
            //String s2 = "Mike";
            //Thread t2 = new Thread(delegate() { print(s1, s2); });
            //t2.Name = "two_param_thread";
            //t2.Start();
           

            ////阻止线程 interupt abort的区别在于 interupt 会释放组织线程的状态然后继续执行到线程结束，abort直接在调用的时候阻止
            //Thread t3 = new Thread(delegate () { try { Thread.Sleep(100000); } catch (Exception e) { Console.WriteLine("Thread 3 has been interupted"); } Console.WriteLine("I am thread 3"); });
            //t3.Start();
            ////t3.Interrupt();
            //t3.Abort();


            //线程同步方法 AutoResetEvent
           
            Thread t4 = new Thread(t4fun);
            t4.Name = "t4";
            Thread t5 = new Thread(t5fun);
            t5.Name = "t5";
            Thread.Sleep(2000);
            t4.Start();
            t5.Start();
            //主线程完事通知下个线程进来 t4 和 t5去抢
            are.Set();
            Console.ReadLine();
        }
        static void print()
        {
            for (int i=0; i <= 40; i++)
            {
                Console.WriteLine("hello world other thread");
            }
        }

        void print1()
        {
            lock (locker)
            {
                if (done)
                {
                    //加入locker 使其他线程等待
                    Console.WriteLine("hello not safe thread");
                    done = false;
                }
            }
            //if (done)
            //{
            //    //第一个线程运行没来得及更改了 done的值  第二个线程就已经进入if语句了
            //    Console.WriteLine("hello not safe thread");
            //    done = false;
            //}
        }

        static void print(Object s1)
        {
            Console.WriteLine(s1+" "+Thread.CurrentThread.Name);
        }
        static void print(string s1, string s2)
        {
            Console.WriteLine(s1 + " " + s2 + " " + Thread.CurrentThread.Name);
        }

        static void t4fun()
        {
            //在AutoResetEvent前面排队
            are.WaitOne();
            Console.WriteLine("Hello delay print "+Thread.CurrentThread.Name);
            //进去之后通知下一个人来
            are.Set();
        }

        static void t5fun()
        {
            //在AutoResetEvent前面排队
            are.WaitOne();
            Console.WriteLine("Hello delay print " + Thread.CurrentThread.Name);
        }

        
    }
}
