using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Singleton <T> where T: class, new()
    {
        private static T m_Instance = null;
        private static bool m_callingStaticMethod = false;

        public Singleton()
        {
            if (!m_callingStaticMethod)
            {
                throw new Exception("This is a Singleton!");
            }
        }

        public static T GetInstance()
        {
            if (m_Instance == null)
            {
                m_callingStaticMethod = true;
                m_Instance = new T();
                m_callingStaticMethod = false;
            }

            return m_Instance;
        }

        public static T Instance
        {
            get
            {
                return GetInstance();
            }
        }

    }
}
