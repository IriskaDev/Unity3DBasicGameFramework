/*
 * @author shenjianping
 */


using System;
using System.Collections.Generic;

namespace Common
{
    public class ObjPool<T> where T : new()
    {
        private LinkedList<T> m_LinkArray;
        private int m_MaxSize;
        private object m_SyncObj = new object();

        public ObjPool(int maxSize = 64)
        {
            if (maxSize < 8)
                maxSize = 8;
            m_LinkArray = new LinkedList<T>();
            m_MaxSize = maxSize;
        }

        public T Pop()
        {
            if (m_LinkArray.Count <= 0)
            {
                return new T();
            }

            lock (m_SyncObj)
            {
                if (m_LinkArray.Count > 0)
                {
                    T t = m_LinkArray.First.Value;
                    m_LinkArray.RemoveFirst();
                    return t;
                }
                else
                {
                    return new T();
                }
            }
        }

        public void Push(T t)
        {
            if (m_LinkArray.Count >= m_MaxSize)
                return;

            lock (m_SyncObj)
            {
                m_LinkArray.AddLast(t);
            }
        }
    }
}
