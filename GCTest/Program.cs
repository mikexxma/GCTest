using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace GCTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestClass t = new TestClass();
            //t.Dispose();
            //Console.ReadLine();

        }
    }

    //gc 无法释放的资源 如文件流，数据库连接叫做非托管资源可以实现IDisposable 接口手动释放
    class TestClass : IDisposable
    {

        public TestClass()
        {
            Console.WriteLine("Test Object is created");
        }
        ~TestClass()
        {
            Console.WriteLine("Test Object has been released");
        }


        public void Dispose()
        {
            Console.WriteLine("Test Object Dispose called");
            //throw new NotImplementedException();
        }
    }
    class TestDispose : IDisposable
    {

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TestDispose() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

    // 当这个对象被gc回收的时候 调用析构函数 Dispose(false) 清理掉非托管资源因为托管资源已经被清理了
    // 我们手动调用Dispose() 方法直接清理掉这个对象的 托管和非托管资源
    class BaseResource : IDisposable
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool CloseHandle(IntPtr handle);

        //非托管资源
        private IntPtr handle;
        //托管资源 
        private Component components;

        private bool disposed = false;
        public BaseResource() { }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                //释放托管资源
                components.Dispose();
            }
            CloseHandle(handle);
            handle = IntPtr.Zero;
            disposed = true;
        }
        ~BaseResource()
        {
            Dispose(false);
        }

        public void DoSomething()
        {
            if (this.disposed)
            {
              
            }
        }

    }

   

}
