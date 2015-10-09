/*
 * @author shenjianping
 */

using System;

namespace Network
{
    public interface IMsgWriter
    {
        void Write(ByteArray bytes);
        short GetProtoID();
    }
}
