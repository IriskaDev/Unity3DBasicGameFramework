using System;

public interface IConf<K>
{
    K GetKey();
}

public interface IConf<K1, K2>
{
    K1 GetFirstKey();
    K2 GetSecondKey();
}

public interface IConf<K1, K2, K3>
{
    K1 GetFirstKey();
    K2 GetSecondKey();
    K3 GetThirdKey();
}
