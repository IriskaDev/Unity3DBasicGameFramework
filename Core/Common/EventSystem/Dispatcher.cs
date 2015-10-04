using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


static public class Dispatcher {
    public delegate void CALLBACK_FUNC();
    public delegate void CALLBACK_FUNC_1<T>(T param);
    public delegate void CALLBACK_FUNC_2<T1, T2>(T1 pram1, T2 param2);
    public delegate void CALLBACK_FUNC_3<T1, T2, T3>(T1 param1, T2 param2, T3 param3);

    static private Dictionary<string, HashSet<Delegate>> callbackFuncs = new Dictionary<string, HashSet<Delegate>>();

    static private void TryAddDelegate(string evt, Delegate func)
    {
        HashSet<Delegate> hashSet;
        if (callbackFuncs.TryGetValue(evt, out hashSet))
        {
            if (hashSet.Contains(func))
            {
                return;
            }
            hashSet.Add(func);
            return;
        }
        hashSet = new HashSet<Delegate>();
        hashSet.Add(func);
        callbackFuncs.Add(evt, hashSet);
    }

    static public void AddListener(string evt, CALLBACK_FUNC func)
    {
        TryAddDelegate(evt, func);
    }

    static public void AddListener(string evt, CALLBACK_FUNC_1<object> func)
    {
        TryAddDelegate(evt, func);
    }

    static public void AddListener<T>(string evt, CALLBACK_FUNC_1<T> func)
    {
        TryAddDelegate(evt, func);
    }

    static public void AddListener<T1, T2>(string evt, CALLBACK_FUNC_2<T1, T2> func)
    {
        TryAddDelegate(evt, func);
    }

    static public void AddListener<T1, T2, T3>(string evt, CALLBACK_FUNC_3<T1, T2, T3> func)
    {
        TryAddDelegate(evt, func);
    }


    static private void TryRemoveDelegate(string evt, Delegate func)
    {
        HashSet<Delegate> hashSet;
        if (callbackFuncs.TryGetValue(evt, out hashSet))
        {
            if (hashSet.Contains(func))
            {
                hashSet.Remove(func);
            }
        }
    }

    static public void RemoveListener(string evt, CALLBACK_FUNC func)
    {
        TryRemoveDelegate(evt, func);
    }

    static public void RemoveListener(string evt, CALLBACK_FUNC_1<object> func)
    {
        TryRemoveDelegate(evt, func);
    }

    static public void RemoveListener<T>(string evt, CALLBACK_FUNC_1<T> func)
    {
        TryRemoveDelegate(evt, func);
    }

    static public void RemoveListener<T1, T2>(string evt, CALLBACK_FUNC_2<T1, T2> func)
    {
        TryRemoveDelegate(evt, func);
    }

    static public void RemoveListener<T1, T2, T3>(string evt, CALLBACK_FUNC_3<T1, T2, T3> func)
    {
        TryRemoveDelegate(evt, func);
    }

    static public void Dispatch(string evt)
    {
        HashSet<Delegate> hashSet;
        if (!callbackFuncs.TryGetValue(evt, out hashSet))
        {
            return;
        }
        HashSet<Delegate>.Enumerator em = hashSet.GetEnumerator();
        do
        {
            CALLBACK_FUNC handler = (em.Current as CALLBACK_FUNC);
            if (handler == null)
                continue;
            handler.Invoke();
        } while (em.MoveNext());
    }

    static public void Dispatch(string evt, object param)
    {
        Dispatch<object>(evt, param);
    }

    static public void Dispatch<T>(string evt, T param)
    {
        HashSet<Delegate> hashSet;
        if (!callbackFuncs.TryGetValue(evt, out hashSet))
        {
            return;
        }
        HashSet<Delegate>.Enumerator em = hashSet.GetEnumerator();
        do
        {
            CALLBACK_FUNC_1<T> handler = (em.Current as CALLBACK_FUNC_1<T>);
            if (handler == null)
                continue;
            handler.Invoke(param);
        } while (em.MoveNext());
    }

    static public void Dispatch<T1, T2>(string evt, T1 param1, T2 param2)
    {
        HashSet<Delegate> hashSet;
        if (!callbackFuncs.TryGetValue(evt, out hashSet))
        {
            return;
        }
        HashSet<Delegate>.Enumerator em = hashSet.GetEnumerator();
        do
        {
            CALLBACK_FUNC_2<T1, T2> handler = (em.Current as CALLBACK_FUNC_2<T1, T2>);
            if (handler == null)
                continue;
            handler.Invoke(param1, param2);
        } while (em.MoveNext());
    }

    static public void Dispatch<T1, T2, T3>(string evt, T1 param1, T2 param2, T3 param3)
    {
        HashSet<Delegate> hashSet;
        if (!callbackFuncs.TryGetValue(evt, out hashSet))
        {
            return;
        }
        HashSet<Delegate>.Enumerator em = hashSet.GetEnumerator();
        do
        {
            CALLBACK_FUNC_3<T1, T2, T3> handler = (em.Current as CALLBACK_FUNC_3<T1, T2, T3>);
            if (handler == null)
                continue;
            handler.Invoke(param1, param2, param3);
        } while (em.MoveNext());
    }
}
