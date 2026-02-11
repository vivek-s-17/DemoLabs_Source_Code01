using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_Destructor_DotNet10
{
    internal class DisposableEmployee
        : System.IDisposable
    {
        public DisposableEmployee()
        {
            isDisposed = false;
            Console.WriteLine("Constructor got called!");
        }

        ~DisposableEmployee()
        {
            Console.WriteLine("Destructor got called!");
        }


        #region System.IDisposable members

        bool isDisposed;

        void CheckIfDisposed()
        {
            if (isDisposed)
                throw new ObjectDisposedException(this.GetType().FullName);
        }

        public void Dispose()
        {
            CheckIfDisposed();

            Console.WriteLine("Dispose got called!");
            isDisposed = true;
        }

        #endregion
    }
}
