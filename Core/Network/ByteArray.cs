/*
 * @author shenjianping
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Network
{
    public class ByteArray
    {
        public const int MSG_MAX_LEN = 65535;
        public const int MSG_HEAD_LEN = 8;

        private byte[] m_byteArray;
        private int m_Length;
        private int m_WritePos = 0;
        private int m_ReadPos = 0;
        private short m_PacketId;

        public ByteArray()
        {
            m_byteArray = new byte[256];
            m_Length = 256;
            Reset();
        }

        public ByteArray(int size)
        {
            if (size < 32)      // 最小长度
                size = 32;
            m_byteArray = new byte[size];
            m_Length = size;
            Reset();
        }

        public void Reset()
        {
            m_WritePos = MSG_HEAD_LEN;
            m_ReadPos = 0;
            m_PacketId = 0;
        }

        public short PacketId
        {
            get
            {
                return m_PacketId;
            }
        }

        public int BufferLength
        {
            get
            {
                return m_WritePos;
            }
        }

        public byte[] BufferArray
        {
            get
            {
                return m_byteArray;
            }
        }

        public void CopyFromNet(byte[] source, int srcIndex, int length)
        {
            if (!CheckWrite(length))
                throw new ByteArrayOutofRangeException();
            Array.Copy(source, srcIndex, m_byteArray, 0, length);
        }

        public void ReadHead()
        {
            m_ReadPos = 2;
            m_PacketId = ReadShort();
            m_ReadPos += 4;             // flag不理会
        }

        public void WriteHead(short packetId, int flag = 0)
        {
            byte[] b = BitConverter.GetBytes((short)m_WritePos);
            Array.Copy(b, 0, m_byteArray, 0, 2);
            m_PacketId = packetId;
            b = BitConverter.GetBytes(packetId);
            Array.Copy(b, 0, m_byteArray, 2, 2);
        }

        public void WriteByte(byte val)
        {
            if (!CheckWrite(1))
                throw new ByteArrayOutofRangeException();

            m_byteArray[m_WritePos] = val;
            m_WritePos += 1;
        }

        public void WriteBool(bool val)
        {
            WriteByte((byte)(val ? 1 : 0));
        }

        public void WriteShort(short val)
        {
            if (!CheckWrite(2))
                throw new ByteArrayOutofRangeException();

            byte[] b = BitConverter.GetBytes(val);

            Array.Copy(b, 0, m_byteArray, m_WritePos, 2);
            m_WritePos += 2;
        }

        public void WriteInt(int val)
        {
            if (!CheckWrite(4))
                throw new ByteArrayOutofRangeException();

            byte[] b = BitConverter.GetBytes(val);

            Array.Copy(b, 0, m_byteArray, m_WritePos, 4);
            m_WritePos += 4;
        }

        public void WriteInt64(Int64 val)
        {
            if (!CheckWrite(8))
                throw new ByteArrayOutofRangeException();

            byte[] b = BitConverter.GetBytes(val);

            Array.Copy(b, 0, m_byteArray, m_WritePos, 8);
            m_WritePos += 8;
        }

        public void WriteFloat(float val)
        {
            if (!CheckWrite(4))
                throw new ByteArrayOutofRangeException();

            byte[] b = BitConverter.GetBytes(val);

            Array.Copy(b, 0, m_byteArray, m_WritePos, 4);
            m_WritePos += 4;
        }

        public void WriteString(string val)
        {
            byte[] b = Encoding.UTF8.GetBytes(val);
            if (!CheckWrite(2 + b.Length))
                throw new ByteArrayOutofRangeException();

            byte[] bl = BitConverter.GetBytes((short)b.Length);

            Array.Copy(bl, 0, m_byteArray, m_WritePos, 2);
            m_WritePos += 2;

            Array.Copy(b, 0, m_byteArray, m_WritePos, b.Length);
            m_WritePos += b.Length;
        }

        public byte ReadByte()
        {
            if (!CheckRead(1))
                throw new ByteArrayOutofRangeException();

            byte b = m_byteArray[m_ReadPos];
            m_ReadPos += 1;
            return b;
        }

        public bool ReadBool()
        {
            return ReadByte() > 0;
        }

        public short ReadShort()
        {
            if (!CheckRead(2))
                throw new ByteArrayOutofRangeException();

            short val = BitConverter.ToInt16(m_byteArray, m_ReadPos);
            m_ReadPos += 2;
            return val;
        }

        public int ReadInt()
        {
            if (!CheckRead(4))
                throw new ByteArrayOutofRangeException();

            int val = BitConverter.ToInt32(m_byteArray, m_ReadPos);
            m_ReadPos += 4;
            return val;
        }

        public Int64 ReadInt64()
        {
            if (!CheckRead(8))
                throw new ByteArrayOutofRangeException();

            Int64 val = BitConverter.ToInt64(m_byteArray, m_ReadPos);
            m_ReadPos += 8;
            return val;
        }

        public float ReadFloat()
        {
            if (!CheckRead(4))
                throw new ByteArrayOutofRangeException();

            float val = BitConverter.ToSingle(m_byteArray, m_ReadPos);
            m_ReadPos += 4;
            return val;
        }

        public string ReadString()
        {
            short strLength = ReadShort();
            if (!CheckRead(strLength))
                throw new ByteArrayOutofRangeException();

            string val = Encoding.UTF8.GetString(m_byteArray, m_ReadPos, strLength);
            m_ReadPos += strLength;
            return val;
        }

        private bool CheckWrite(int length)
        {
            int newLength = m_WritePos + length;
            if (newLength <= MSG_MAX_LEN)
            {
                if (newLength <= m_Length)
                    return true;

                byte[] oldBytes = m_byteArray;
                int oldLength = m_Length;
                m_Length = m_Length + m_Length / 2;
                if (m_Length < newLength)
                    m_Length = newLength + 1;
                if (m_Length > MSG_MAX_LEN)
                    m_Length = MSG_MAX_LEN;

                m_byteArray = new byte[m_Length];
                Array.Copy(oldBytes, m_byteArray, oldLength);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckRead(int length)
        {
            return (m_ReadPos + length) <= MSG_MAX_LEN;
        }
    }
}
