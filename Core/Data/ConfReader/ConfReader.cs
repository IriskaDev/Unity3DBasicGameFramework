using System;
using System.Collections.Generic;
using System.Reflection;


public static class ConfReader
{
    public static Dictionary<K, C> ReadConf<K, C>(object data) where C: IConf<K>, new()
    {
        Dictionary<K, C> dict = new Dictionary<K, C>();
        return dict;
    }

    public static Dictionary<K1, Dictionary<K2, C>> ReadConf<K1, K2, C>(object data) where C: IConf<K1, K2>, new()
    {
        Dictionary<K1, Dictionary<K2, C>> dict = new Dictionary<K1, Dictionary<K2, C>>();
        //about how to set a property's value by it's name
        //C conf = new C();
        //Type cType = typeof(C);
        //PropertyInfo pInfo = cType.GetProperty("propertyName");
        //pInfo.SetValue(conf, value, null);
        return dict;
    }

    public static Dictionary<K1, Dictionary<K2, Dictionary<K3, C>>> ReadConf<K1, K2, K3, C>(object data) where C: IConf<K1, K2, K3>, new()
    {
        Dictionary<K1, Dictionary<K2, Dictionary<K3, C>>> dict = new Dictionary<K1, Dictionary<K2, Dictionary<K3, C>>>();
        return dict;
    }
}
