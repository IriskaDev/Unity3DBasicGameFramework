/*
 * @author shenjianping
 */

using System;
using System.Text;

namespace Network
{
    public class ByteArrayOutofRangeException : Exception
    {
        public ByteArrayOutofRangeException() : base("The ByteArray is out of range!") { }
        public ByteArrayOutofRangeException(short packetId) : base(string.Format("The ByteArray is out of range in packetId {0}!", packetId)) { }
    }

    public class MessageDuplicatedListenException : Exception
    {
        public MessageDuplicatedListenException() : base("Packet listen duplicated!") { }
        public MessageDuplicatedListenException(short packetId) : base(string.Format("Packet {0} listen duplicated!", packetId)) { }
    }

    public class ErrorMessageHandleException : Exception
    {
        public ErrorMessageHandleException() : base("Packet handle error!") { }
        public ErrorMessageHandleException(short packetIdTrue, short packetIdFalse) :
            base(string.Format("Packet {0} handle error, but you use {1}!", packetIdTrue, packetIdFalse)) { }
    }
}
