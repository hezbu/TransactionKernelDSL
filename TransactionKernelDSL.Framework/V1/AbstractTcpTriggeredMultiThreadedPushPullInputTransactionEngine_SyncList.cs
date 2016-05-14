using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TransactionKernelDSL.Framework.V1
{
    public partial class AbstractTcpTriggeredMultiThreadedPushPullInputTransactionEngine : AbstractThreadedInputTransactionEngine
    {
        public class SyncCache<T> where T : class, new()
        {
            private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
            private Dictionary<string, T> innerCache = new Dictionary<string, T>();

            public int Count
            { get { return innerCache.Count; } }

            public T Read(string key)
            {
                cacheLock.EnterReadLock();
                try
                {
                    if (innerCache.ContainsKey(key))
                    {
                        return innerCache[key];
                    }
                    else return null;
                }
                finally
                {
                    cacheLock.ExitReadLock();
                }
            }

            public void Add(string key, T value)
            {
                cacheLock.EnterWriteLock();
                try
                {
                    innerCache.Add(key, value);
                }
                finally
                {
                    cacheLock.ExitWriteLock();
                }
            }

            public bool AddWithTimeout(string key, T value, int timeout)
            {
                if (cacheLock.TryEnterWriteLock(timeout))
                {
                    try
                    {
                        innerCache.Add(key, value);
                    }
                    finally
                    {
                        cacheLock.ExitWriteLock();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }


            public void Delete(string key)
            {
                cacheLock.EnterWriteLock();
                try
                {
                    innerCache.Remove(key);
                }
                finally
                {
                    cacheLock.ExitWriteLock();
                }
            }

            public void DeleteAll()
            {
                cacheLock.EnterWriteLock();
                try
                {
                    innerCache.Clear();
                }
                finally
                {
                    cacheLock.ExitWriteLock();
                }
            }


            ~SyncCache()
            {
                if (cacheLock != null) cacheLock.Dispose();
            }
        }
    }
}
