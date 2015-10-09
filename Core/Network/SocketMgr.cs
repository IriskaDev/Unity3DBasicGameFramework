/*
 * @author shenjianping
 */

using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using Common;

namespace Network
{
    public delegate void MessageEventHandler(ByteArray bytes);

    public class SocketMgr
    {
        #region Singleton
        private static SocketMgr m_Instance = null;
        private static readonly object s_SynObject = new object();

        private SocketMgr()
        {
            m_Handlers = new Dictionary<short, MessageEventHandler>(512);
            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_RecvBuffer = new byte[ByteArray.MSG_MAX_LEN];
            m_RecvLength = 0;
            m_BytesPool = new ObjPool<ByteArray>(32);
            m_MessageQueue = new LockedQueue<ByteArray>(32);
            m_SendBuffer = new ByteArray(128);
        }

        public static SocketMgr Instance
        {
            get
            {
                if (null == m_Instance)
                {
                    lock (s_SynObject)
                    {
                        if (null == m_Instance)
                        {
                            m_Instance = new SocketMgr();
                        }
                    }
                }
                return m_Instance;
            }
        }
        #endregion

        private Socket m_Socket;
        private bool m_IsConnnected;
        private string m_ServerIP;
        private int m_Port;
        private Thread m_RecvThread;                            // 接收线程
        private byte[] m_RecvBuffer;                            // 接收缓冲
        private int m_RecvLength;                               // 缓冲当前接受的字节数
        private ObjPool<ByteArray> m_BytesPool;                 // 消息池
        private LockedQueue<ByteArray> m_MessageQueue;          // 主线程处理的消息队列
        private const int MAX_HANDLE_MSG_COUNT = 1024;          // 主线程一帧最大处理消息数
        private ByteArray m_SendBuffer;

        private Dictionary<short, MessageEventHandler> m_Handlers;

        public bool Connect(string ipAddr, int port)
        {
            m_ServerIP = ipAddr;
            m_Port = port;
            IPAddress ip = IPAddress.Parse(ipAddr);
            try
            {
                m_Socket.Connect(ip, port);
                m_RecvThread = new Thread(new ThreadStart(SocketReceive));
                m_IsConnnected = true;
                m_RecvThread.Start();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                m_IsConnnected = false;
                return false;
            }
        }

        public void Close()
        {
            m_Socket.Close();
            m_IsConnnected = false;
            m_RecvThread.Abort();
        }

        public void HandleMessage()
        {
            int receiveCount = 1;
            ByteArray bytes = m_MessageQueue.Dequeue();
            MessageEventHandler handler;
            while (bytes != null)
            {
                if (m_Handlers.TryGetValue(bytes.PacketId, out handler))
                {
                    handler(bytes);
                }
                else
                {
                    Console.WriteLine("The message of packetId {0} has no handler!", bytes.PacketId);
                }
                m_BytesPool.Push(bytes);

                receiveCount += 1;
                if (receiveCount > MAX_HANDLE_MSG_COUNT)
                    break;
                bytes = m_MessageQueue.Dequeue();
            }
        }

        // 接下去要写发送消息接口, 完了之后写测试代码
        public void SendMsg<T>(T t) where T : IMsgWriter
        {
            m_SendBuffer.Reset();
            t.Write(m_SendBuffer);
            m_SendBuffer.WriteHead(t.GetProtoID());
            m_Socket.Send(m_SendBuffer.BufferArray, m_SendBuffer.BufferLength, SocketFlags.None);
        }

        public T RecvMsg<T>(ByteArray bytes) where T : IMsgReader, new()
        {
            // Bytes再Receive的时候已经ReadHead
            // TODO: 这里可以根据PacketID的范围来确定是否使用内存池
            T t = new T();
            if (bytes.PacketId != t.GetProtoID())
                throw new ErrorMessageHandleException(bytes.PacketId, t.GetProtoID());
            t.Read(bytes);
            return t;
        }

        private void SocketReceive()
        {
            // try
            {
                m_RecvLength = 0;
                int readPos = 0;
                while (m_IsConnnected)
                {
                    int readLength = m_Socket.Receive(m_RecvBuffer, m_RecvLength, ByteArray.MSG_MAX_LEN - m_RecvLength, SocketFlags.None);
                    m_RecvLength += readLength;
                    while ((m_RecvLength - readPos) >= ByteArray.MSG_HEAD_LEN)
                    {
                        short val = BitConverter.ToInt16(m_RecvBuffer, readPos);
                        // 字节序转换?
                        if (val > (m_RecvLength - readPos))
                            break;
                        ByteArray bytes = m_BytesPool.Pop();
                        bytes.Reset();
                        bytes.CopyFromNet(m_RecvBuffer, readPos, (int)val);
                        bytes.ReadHead();
                        readPos += val;

                        m_MessageQueue.Enqueue(bytes);
                    }

                    // 将后面未接满的数据
                    if (readPos > ByteArray.MSG_MAX_LEN / 2 || m_RecvLength >= ByteArray.MSG_MAX_LEN)
                    {
                        Array.Copy(m_RecvBuffer, readPos, m_RecvBuffer, 0, m_RecvLength - readPos);
                        m_RecvLength -= readPos;
                        readPos = 0;
                    }
                }
            }
            //catch(Exception e)
            //{
            // Console.WriteLine("Reveive thread abort!" + e.Message);
            // }
        }

        public void AddMessageListener(short packetId, MessageEventHandler handler)
        {
            if (m_Handlers.ContainsKey(packetId))
            {
                // TODO: 客户端是否会存在两个地方都需要监听同一个消息的地方
                // TODO: 可以通过Delete.MethodInfo来判定是否是重复监听
                // m_Handlers[packetId] += handler;
                throw new MessageDuplicatedListenException(packetId);
            }
            else
            {
                m_Handlers[packetId] = handler;
            }
        }

        public void RemoveMessageListener(short packetId, MessageEventHandler handler)
        {
            // TODO: 如果允许重复监听, 这里需做相应修改
            if (m_Handlers.ContainsKey(packetId))
            {
                m_Handlers.Remove(packetId);
            }
        }
    }
}
