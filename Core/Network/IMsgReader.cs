/*
 * @author shenjianping
 */

using System;

namespace Network
{
    public interface IMsgReader
    {
        void Read(ByteArray bytes);
        short GetProtoID();
    }
}
