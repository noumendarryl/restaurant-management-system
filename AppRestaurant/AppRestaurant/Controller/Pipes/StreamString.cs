using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Controller.Pipes
{
    public abstract class StreamString
    {
        private static UnicodeEncoding streamEncoding = new UnicodeEncoding();

        protected static void writeString(Stream pipeWrite, string outString)
        {
            byte[] outBuffer = streamEncoding.GetBytes(outString);

            int len = outBuffer.Length;
            if (len > ushort.MaxValue)
            {
                len = ushort.MaxValue;
            }

            pipeWrite.WriteByte((byte)(len / 256));
            pipeWrite.WriteByte((byte)(len & 255));
            pipeWrite.Write(outBuffer, 0, len);
            pipeWrite.Flush();
        }

        protected static List<string> readString(Stream pipeRead)
        {
            int len;
            var values = new List<string>();
            len = pipeRead.ReadByte() * 256;
            len += pipeRead.ReadByte();
            var inBuffer = new byte[len];
            pipeRead.Read(inBuffer, 0, len);
            values.Add(streamEncoding.GetString(inBuffer));
            return values;
        }
    }
}
