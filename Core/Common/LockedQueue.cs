/*
 * @author shenjianping
 */


using System;
using System.Collections.Generic;

namespace Common
{
    public class LockedQueue<T> where T : class
    {
        private Queue<T> m_Queue;
        private object m_SyncObj = new object();

        public LockedQueue(int capcity = 32)
        {
            m_Queue = new Queue<T>(capcity);
        }

        public int Count
        {
            get
            {
                lock (m_SyncObj)
                {
                    return m_Queue.Count;
                }
            }
        }

        public T Dequeue()
        {
            lock (m_SyncObj)
            {
                if (m_Queue.Count <= 0)
                    return null;
                return m_Queue.Dequeue();
            }
        }


        public void Enqueue(T t)
        {
            lock (m_SyncObj)
            {
                m_Queue.Enqueue(t);
            }
        }
    }
}
